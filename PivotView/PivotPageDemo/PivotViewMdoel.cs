using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotPageDemo
{
    public class PivotItemModel
    {
        public string Title { get; set; }
        public Lazy<View> View { get; set; }
    }
    public class PivotViewMdoel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ObservableCollection<PivotItemModel> _pivotItems;
        public ObservableCollection<PivotItemModel> PivotItems
        {
            get { return _pivotItems; }
            set { _pivotItems = value;OnPropertyChanged(); }
        }

        private ObservableCollection<PivotItemModel> _headers;
        public ObservableCollection<PivotItemModel> Headers
        {
            get { return _headers; }
            set { _headers = value; OnPropertyChanged(); }
        }
        private ObservableCollection<View> _views;
        public ObservableCollection<View> Views
        {
            get { return _views; }
            set { _views = value; OnPropertyChanged(); }
        }

        public PivotViewMdoel()
        {
            Views = new ObservableCollection<View>();
            Headers = new ObservableCollection<PivotItemModel>();

            LoadData();
        }
        public async void LoadData()
        {
            Headers.Add(new PivotItemModel { Title = "Mokey" });
            Views.Add(new MokeyView());


            Headers.Add(new PivotItemModel { Title = "Test" });
            Views.Add(new TestView());

            Headers.Add(new PivotItemModel { Title = "Test2" });
            Views.Add(new TestView2());

            Headers.Add(new PivotItemModel { Title = "Test3" });
            Views.Add(new TestVIew3());

            Headers.Add(new PivotItemModel { Title = "Test" });
            Views.Add(new TestView());

            Headers.Add(new PivotItemModel { Title = "Test2" });
            Views.Add(new TestView2());

            Headers.Add(new PivotItemModel { Title = "Test3" });
            Views.Add(new TestVIew3());
            Headers.Add(new PivotItemModel { Title = "Test" });
            Views.Add(new TestView());

            Headers.Add(new PivotItemModel { Title = "Test2" });
            Views.Add(new TestView2());

            Headers.Add(new PivotItemModel { Title = "Test3" });
            Views.Add(new TestVIew3());

            //Headers.Add(new PivotItemModel { Title = "MokeyFLow" });
            //Views.Add(new FlowLayoutPage());
        }
    }
}
