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
        private BoxView _block;
        private ScrollViewExpand _scrollView;
        //private HorizentalLayout _viewPannel;
        private CustomViewPager _viewPannel;
        void InitLayout()
        {
            _grid = new Grid();
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            _headerList = new ItemsControl();
            _headerList.SetValue(Grid.RowProperty, 0);

            _block = new BoxView();
            _block.HeightRequest = 2;
            _block.WidthRequest = 50;
            _block.HorizontalOptions = LayoutOptions.Start;
            _block.VerticalOptions = LayoutOptions.Start;

            _block.BackgroundColor = Color.Red;
            _block.SetValue(Grid.RowProperty, 1);

            _scrollView = new ScrollViewExpand();
            _scrollView.Orientation = ScrollOrientation.Horizontal;
            _scrollView.SetValue(Grid.RowProperty, 2);

            _viewPannel = new  CustomViewPager();
            _viewPannel.SetValue(Grid.RowProperty, 2);
            //_scrollView.Content = _viewPannel;

            _grid.Children.Add(_headerList);
            _grid.Children.Add(_block);
            //_grid.Children.Add(_scrollView);
            _grid.Children.Add(_viewPannel);

            this.Content = _grid;
        }
        public PivotPage()
        {
            InitLayout();

            _headerList.ItemSelected += headerList_ItemSelected;
            _scrollView.BeginScroll += _scrollView_BeginScroll;
            _scrollView.EndScroll += _scrollView_EndScroll;
            _scrollView.Scrolled += _scrollView_Scrolled;

            //_headerList.PannelScrollStarted += _headerList_PannelScrollStarted;
            //_headerList.PannelScrollStopped += _headerList_PannelScrollStopped ;
            //_headerList.PannelScrolled += _headerList_PannelScrolled;
        }

        //private double oldBlockScrollX;
        //private void _headerList_PannelScrolled(object sender, ScrolledEventArgs e)
        //{
        //    _block.TranslateTo(oldBlockScrollX  - e.ScrollX, _block.TranslationY);
         
        //}

        //private double oldPostion;
        //private void _headerList_PannelScrollStopped(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void _headerList_PannelScrollStarted(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

       

        private void _scrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var x=_scrollView.ScrollX * _headerList.RealWidth / _scrollView.ContentSize.Width;
            _block.TranslateTo(x-_headerList.ScrollX, _block.TranslationY);
            //oldBlockScrollX = _block.TranslationX;
        }

        private void _scrollView_EndScroll(object sender, EventArgs e)
        {
            var index = (int)(_scrollView.ScrollX / _scrollView.Width);
            if(_scrollView.Width/2<(_scrollView.ScrollX % _scrollView.Width))
            {
                index++;
            }
            //ScrollTo(index, true);
            _headerList.SelectedIndex = index;
        }

        private bool isScrolled = false;
        private void _scrollView_BeginScroll(object sender, EventArgs e)
        {
            isScrolled = true;

        }

        private void headerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (isScrolled == false)
            {
                ScrollTo(_headerList.SelectedIndex, false);
            }
            else
            {
                isScrolled = false;
            }
            //var perWidth=scrollView.ContentSize.Width / headerList.Count;
            //scrollView.ScrollToAsync(headerList.SelectedIndex* perWidth, scrollView.ScrollY, true);
            

        }
        public void ScrollTo(int index,bool animation)
        {
            var perWidth = _scrollView.Width;
            _scrollView.ScrollToAsync(index * perWidth, _scrollView.ScrollY, animation);
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
            //var pivot = sender as PivotPage;
            //foreach (var item in (IEnumerable)newValue)
            //{
            //    pivot._viewPannel.Children.Add(item as View);
            //}
            var pivot = sender as PivotPage;
            pivot._viewPannel.Children = (IList)newValue;
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
