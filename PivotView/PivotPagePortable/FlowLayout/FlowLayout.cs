using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotPagePortable
{
    public class FlowLayout : Layout<View>
    {
        
        public static readonly BindableProperty ColumnProperty = BindableProperty.Create("Column", typeof(int), typeof(FlowLayout),1);
        public int Column
        {
            get { return (int)this.GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }
        private double columnWidth = 0;

        public static readonly BindableProperty RowSpacingProerpty = BindableProperty.Create("RowSpacing", typeof(double), typeof(FlowLayout),0.0);
        public double RowSpacing
        {
            get { return (double)this.GetValue(RowSpacingProerpty); }
            set { SetValue(RowSpacingProerpty, value); }
        }
        public static readonly BindableProperty ColumnSpacingProerpty = BindableProperty.Create("ColumnSpacing", typeof(double), typeof(FlowLayout), 0.0);
        public double ColumnSpacing
        {
            get { return (double)this.GetValue(ColumnSpacingProerpty); }
            set { SetValue(ColumnSpacingProerpty, value); }
        }
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            
            double[] colHeights = new double[Column];
            double allColumnSpacing = ColumnSpacing * (Column - 1);
            columnWidth = (width- allColumnSpacing )/ Column;
            foreach (var item in this.Children)
            {
                var measuredSize=item.Measure(columnWidth, height, MeasureFlags.IncludeMargins);
                int col = 0;
                for (int i = 1; i < Column; i++)
                {
                    if (colHeights[i] < colHeights[col])
                    {
                        col = i;
                    }
                }
                item.Layout(new Rectangle(col * (columnWidth + ColumnSpacing), colHeights[col], columnWidth, measuredSize.Request.Height));


                colHeights[col] += measuredSize.Request.Height+RowSpacing;
            }
        }
        private double _maxHeight;
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {   
            double[] colHeights = new double[Column];
            double allColumnSpacing = ColumnSpacing * (Column - 1);
            columnWidth = (widthConstraint - allColumnSpacing) / Column;
            foreach (var item in this.Children)
            {
                var measuredSize = item.Measure(columnWidth, heightConstraint, MeasureFlags.IncludeMargins);
                int col = 0;
                for (int i = 1; i < Column; i++)
                {
                    if (colHeights[i] < colHeights[col])
                    {
                        col = i;
                    }
                }
                colHeights[col] += measuredSize.Request.Height + RowSpacing;
            }
            _maxHeight = colHeights.OrderByDescending(m => m).First();
            return new SizeRequest(new Size(widthConstraint, _maxHeight));
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(FlowLayout), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(FlowLayout), null,propertyChanged: ItemsSource_PropertyChanged);
        public IList ItemsSource
        {
            get { return (IList)this.GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        private static void ItemsSource_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flowLayout = (FlowLayout)bindable;
            var newItems = newValue as IList;
            var oldItems = oldValue as IList;
            var oldCollection = oldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= flowLayout.OnCollectionChanged;
            }

            if (newValue == null)
            {
                return;
            }

            if (newItems == null)
                return;
            if(oldItems == null||newItems.Count!= oldItems.Count)
            {
                flowLayout.Children.Clear();
                for (int i = 0; i < newItems.Count; i++)
                {
                    var child = flowLayout.ItemTemplate.CreateContent();
                    ((BindableObject)child).BindingContext = newItems[i];
                    flowLayout.Children.Add((View)child);
                }
                
            }

            var newCollection = newValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                newCollection.CollectionChanged += flowLayout.OnCollectionChanged;
            }

            flowLayout.UpdateChildrenLayout();
            flowLayout.InvalidateLayout();
        }
      

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                this.Children.RemoveAt(e.OldStartingIndex);
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }

            if (e.NewItems == null)
            {
                return;
            }
            for (int i = 0; i < e.NewItems.Count; i++)
            {
                var child = this.ItemTemplate.CreateContent();
                ((BindableObject)child).BindingContext = e.NewItems[i];
                this.Children.Add((View)child);
            }

            this.UpdateChildrenLayout();
            this.InvalidateLayout();
        }
    }
}
