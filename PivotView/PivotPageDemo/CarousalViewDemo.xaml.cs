using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PivotPageDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarousalViewDemo : ContentPage
    {
        CarousalViewModel VM = new CarousalViewModel();

        public CarousalViewDemo()
        {
            this.BindingContext = this.VM;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            VM.LoadData();
        }
    }
    public class CarousalItem
    {
        public string Img { get; set; }
    }
    public class CarousalViewModel:INotifyPropertyChanged
    {
        private List<CarousalItem> _items;
        public List<CarousalItem> Items
        {
            get { return _items; }
            set { _items = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarousalViewModel()
        {
            Items = new List<CarousalItem>();
        }
        public void LoadData()
        {
            List<CarousalItem> imgs = new List<CarousalItem>()
        {
            new CarousalItem{ Img="banner1.png" },

            new CarousalItem{ Img="banner2.png" },

            new CarousalItem{ Img="banner3.png" },
        };
            Items = imgs;
        }
    }
}
