using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class PivotPage:ContentPage
    {
        private Grid _grid;
        private ItemsControl _headerList;
        private ViewPanel _viewPanel;
        void InitLayout()
        {
            _grid = new Grid();
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            _headerList = new ItemsControl();
            _headerList.SetValue(Grid.RowProperty, 0);

            _viewPanel = new ViewPanel() { Orientation= ScrollOrientation.Horizontal};
            _viewPanel.SetValue(Grid.RowProperty, 2);
            
            _grid.Children.Add(_headerList);
            _grid.Children.Add(_viewPanel);

            this.Content = _grid;
        }
        public PivotPage()
        {
            InitLayout();

            _headerList.ItemSelected += headerList_ItemSelected;
            _viewPanel.SelectChanged += _viewPanel_SelectChanged;
        }

        private void _viewPanel_SelectChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            var index = (int)e.SelectedPosition;
            _headerList.SelectedIndex = index;
        }

        private void headerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ScrollTo(_headerList.SelectedIndex, false);
        }
        public void ScrollTo(int index,bool animation)
        {
            _viewPanel.Select?.Invoke(index);
        }

        public DataTemplate SelectedDataTemplate
        {
            get { return _headerList.SelectedItemTemplate; }
            set { _headerList.SelectedItemTemplate = value; }
        }
        public DataTemplate NornamlDataTemplate
        {
            get { return _headerList.ItemTemplate; }
            set { _headerList.ItemTemplate = value; }
        }

        public static readonly BindableProperty HeadersProperty = BindableProperty.Create("Headers", typeof(IEnumerable), typeof(PivotPage), null, propertyChanged: OnHeadersPropertyChnaged);
        public IEnumerable Headers
        {
            get { return (IEnumerable)this.GetValue(HeadersProperty); }
            set { SetValue(HeadersProperty, value); }
        }

        static void OnHeadersPropertyChnaged(BindableObject sender, object oldValue, object newValue)
        {
            var pivot = sender as PivotPage;
            if (pivot.SelectedDataTemplate == null || pivot.NornamlDataTemplate == null)
            {
                return;
            }
            pivot._headerList.ItemsSource = (IEnumerable)newValue;
        }

        public static readonly BindableProperty ViewsProperty = BindableProperty.Create("Views", typeof(IEnumerable), typeof(PivotPage), null, propertyChanged: OnViewsPropertyChnaged);
        public IEnumerable Views
        {
            get { return (IEnumerable)this.GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        static void OnViewsPropertyChnaged(BindableObject sender, object oldValue, object newValue)
        {
            var pivot = sender as PivotPage;
            pivot._viewPanel.Children = (IList)newValue;
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(NornamlDataTemplate))
            {
                this._headerList.ItemsSource = this.Headers;
            }
        }
    }
}
