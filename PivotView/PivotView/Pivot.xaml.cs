using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PivotView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pivot : ContentPage
    {
        public Pivot()
        {
            InitializeComponent();
        }
        private void headerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var perWidth=scrollView.ContentSize.Width / headerList.Count;
            //scrollView.ScrollToAsync(headerList.SelectedIndex* perWidth, scrollView.ScrollY, true);
            ScrollTo(headerList.SelectedIndex);

        }
        public void ScrollTo(int index)
        {
            var perWidth = scrollView.Width;
            scrollView.ScrollToAsync(index * perWidth,scrollView.ScrollY,false);
        }

        public DataTemplate SelectedDataTemplate
        {
            get { return headerList.SelectedItemTemplate; }
            set { headerList.SelectedItemTemplate = value; }
        }
        public DataTemplate NornamlDataTemplate
        {
            get { return headerList.ItemTemplate; }
            set { headerList.ItemTemplate = value; }
        }

        public static readonly BindableProperty HeadersProperty = BindableProperty.Create("Headers", typeof(IEnumerable), typeof(Pivot), null, propertyChanged: OnHeadersPropertyChnaged);
        public IEnumerable Headers
        {
            get { return (IEnumerable)this.GetValue(HeadersProperty); }
            set { SetValue(HeadersProperty, value); }
        }

        static void OnHeadersPropertyChnaged(BindableObject sender,object oldValue,object newValue)
        {
            Pivot pivot = sender as Pivot;
            if (pivot.SelectedDataTemplate == null || pivot.NornamlDataTemplate == null)
            {
                return;
            }
            pivot.headerList.ItemsSource = (IEnumerable)newValue;
        }

        public static readonly BindableProperty ViewsProperty = BindableProperty.Create("Views", typeof(IEnumerable), typeof(Pivot), null, propertyChanged: OnViewsPropertyChnaged);
        public IEnumerable Views
        {
            get { return (IEnumerable)this.GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        static void OnViewsPropertyChnaged(BindableObject sender, object oldValue, object newValue)
        {
            Pivot pivot = sender as Pivot;
            foreach (var item in (IEnumerable)newValue)
            {
                pivot.viewPannel.Children.Add(item as View);
            }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName== nameof(NornamlDataTemplate))
            {
                this.headerList.ItemsSource = this.Headers;
            }
        }
    }
}
