using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotPagePortable
{
    /// <summary>
    /// 多页面切换控件，页面类型为Xamarin.Forms.View
    /// 支持数据绑定,可结合MVVM使用
    /// 安卓:利用ViewPager实现
    /// iOS:利用Forms直接实现，中途用到了iOS中UISCrollView的PagedEnable效果
    /// </summary>
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

        /// <summary>
        /// Header中选中项的DataTemplate
        /// </summary>
        public DataTemplate SelectedDataTemplate
        {
            get { return _headerList.SelectedItemTemplate; }
            set { _headerList.SelectedItemTemplate = value; }
        }
        /// <summary>
        /// Header中未选中项的DataTemplate
        /// </summary>
        public DataTemplate NornamlDataTemplate
        {
            get { return _headerList.ItemTemplate; }
            set { _headerList.ItemTemplate = value; }
        }

        /// <summary>
        /// PivotPage的第一组成部分Header
        /// </summary>
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

        /// <summary>
        /// PivotPage第二组成部分Views
        /// </summary>
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
        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if (propertyName == nameof(NornamlDataTemplate))
        //    {
        //        this._headerList.ItemsSource = this.Headers;
        //    }
        //}
    }
}
