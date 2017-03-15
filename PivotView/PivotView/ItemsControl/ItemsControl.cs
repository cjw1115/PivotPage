using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using System.Linq;

namespace PivotView
{
    public class ItemsControl : ContentView
    {
        private ScrollViewExpand _scrollView;
        public event EventHandler<ScrolledEventArgs> PannelScrolled;
        public event EventHandler PannelScrollStarted;
        public event EventHandler PannelScrollStopped;
        public ItemsControl()
        {
            this._scrollView = new ScrollViewExpand();
            this._scrollView.Orientation = Orientation;

            this.itemsPanel = new StackLayout() { Orientation = StackOrientation.Horizontal };

            this.Content = this._scrollView;
            this._scrollView.Content = this.itemsPanel;
            this._scrollView.BeginScroll += _scrollView_BeginScroll; ;
            this._scrollView.EndScroll += _scrollView_EndScroll;
            this._scrollView.Scrolled += _scrollView_Scrolled;
            _tapGestureRecognizer = new TapGestureRecognizer();
            _tapGestureRecognizer.Tapped += OnTapped;
        }

        private void _scrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            PannelScrolled?.Invoke(this, e);
        }

        private void _scrollView_EndScroll(object sender, EventArgs e)
        {
            PannelScrollStopped?.Invoke(this, null);
        }

        private void _scrollView_BeginScroll(object sender, EventArgs e)
        {
            PannelScrollStarted?.Invoke(this,null);
        }

        private StackLayout itemsPanel = null;
        public StackLayout ItemsPanel
        {
            get { return this.itemsPanel; }
            set { this.itemsPanel = value; }
        }

        private ScrollOrientation _orientation = ScrollOrientation.Horizontal;
        public ScrollOrientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                if(value== ScrollOrientation.Horizontal)
                {
                    ((StackLayout)ItemsPanel).Orientation = StackOrientation.Horizontal;
                }
                else if(value== ScrollOrientation.Vertical)
                {
                    ((StackLayout)ItemsPanel).Orientation = StackOrientation.Vertical;
                }
                this.InvalidateLayout();
            }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(ItemsControl), defaultBindingMode: BindingMode.OneWay, defaultValue: null, propertyChanged: OnItemsSourceChanged);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ItemsControl;
            if (control == null)
            {
                return;
            }

            var oldCollection = oldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= control.OnCollectionChanged;
            }

            if (newValue == null)
            {
                return;
            }

            control.ItemsPanel.Children.Clear();

            foreach (var item in (IEnumerable)newValue)
            {
                object content;
                content = control.ItemTemplate.CreateContent();
                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }

                view.GestureRecognizers.Add(control._tapGestureRecognizer);
                view.BindingContext = item;
                control.ItemsPanel.Children.Add(view);
            }

            

            var newCollection = newValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                newCollection.CollectionChanged += control.OnCollectionChanged;
            }
            control.SelectedItem = control.ItemsPanel.Children[control.SelectedIndex].BindingContext;
            control.UpdateChildrenLayout();
            control.InvalidateLayout();
            
        }
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                this.ItemsPanel.Children.RemoveAt(e.OldStartingIndex);
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }


            if (e.NewItems == null)
            {
                return;
            }
            foreach (var item in e.NewItems)
            {
                var content = this.ItemTemplate.CreateContent();

                View view;
                var cell = content as ViewCell;
                if (cell != null)
                {
                    view = cell.View;
                }
                else
                {
                    view = (View)content;
                }
                if (!view.GestureRecognizers.Contains(this._tapGestureRecognizer))
                {
                    view.GestureRecognizers.Add(this._tapGestureRecognizer);
                }
                view.BindingContext = item;
                this.ItemsPanel.Children.Insert(e.NewItems.IndexOf(item), view);
            }
            

            this.UpdateChildrenLayout();
            this.InvalidateLayout();
            
        }
        
        

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(ItemsControl), defaultValue: default(DataTemplate));
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }
        public static readonly BindableProperty SelectedItemTemplateProperty = BindableProperty.Create("SelectedItemTemplate", typeof(DataTemplate), typeof(ItemsControl), defaultValue: default(DataTemplate));
        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)this.GetValue(SelectedItemTemplateProperty); }
            set { this.SetValue(SelectedItemTemplateProperty, value); }
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(ItemsControl), default(object), propertyChanged: OnSelectedItemChanged);
        public object SelectedItem
        {
            get { return (object)this.GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int), typeof(ItemsControl), 0, propertyChanged: OnSelectedIndexChanged);
        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var list = (ItemsControl)bindable;
            var newIndex = (int)newValue;
            var oldIndex = (int)oldValue;
            if (newIndex == oldIndex)
            {
                return;
            }
            if (((list.SelectedItem)) == list.itemsPanel.Children[newIndex].BindingContext)
            {
                return;
            }
            else
            {
                list.SelectedItem = list.itemsPanel.Children[newIndex].BindingContext;
            }
        }
        private void OnTapped(object sender, EventArgs e)
        {
            var view = (BindableObject)sender;
            this.SelectedItem = view.BindingContext;

        }

        private TapGestureRecognizer _tapGestureRecognizer;
        private View _lastSelectedView;
        static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var list = (ItemsControl)bindable;

            if (list._lastSelectedView != null)
            {
                for (int i = 0; i < list.ItemsPanel.Children.Count; i++)
                {
                    if (list.ItemsPanel.Children[i].BindingContext == list._lastSelectedView.BindingContext)
                    {
                        list.ItemsPanel.Children[i] = list._lastSelectedView;
                        break;
                    }
                }
            }

            View view;
            var content = list.SelectedItemTemplate.CreateContent();
            var cell = content as ViewCell;
            if (cell != null)
            {
                view = cell.View;
            }
            else
            {
                view = (View)content;
            }
            view.GestureRecognizers.Add(list._tapGestureRecognizer);
            view.BindingContext = newValue;
            for (int i=0;i<list.ItemsPanel.Children.Count;i++)
            {
                if (list.ItemsPanel.Children[i].BindingContext == newValue)
                {
                    list._lastSelectedView = list.ItemsPanel.Children[i];
                    list.ItemsPanel.Children[i] = view;
                    list.SelectedIndex = i;
                    break;
                }
            }

            
            if (list.ItemSelected != null)
            {
                list.ItemSelected(list, new SelectedItemChangedEventArgs(newValue));
            }

        }
        public int Count
        {
            get { return this.ItemsPanel.Children.Count; }
        }

        
        public double RealWidth
        {
            get { return this.itemsPanel.Children.Sum(m => m.Width) + (this.itemsPanel.Children.Count - 1) * this.itemsPanel.Spacing;  }
        }
        public double ScrollX
        {
            get { return _scrollView.ScrollX; }
        }
        public double ScrollY
        {
            get { return _scrollView.ScrollY; }
        }
    }
}