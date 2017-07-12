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
    public class CarouselItem
    {
        public string Img { get; set; }
    }
    public class CarousalViewModel:INotifyPropertyChanged
    {
        private List<CarouselItem> _items;
        public List<CarouselItem> Items
        {
            get { return _items; }
            set { _items = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarousalViewModel()
        {
            Items = new List<CarouselItem>();
        }
        public void LoadData()
        {
            List<CarouselItem> imgs = new List<CarouselItem>()
        {
            new CarouselItem{ Img="banner1.png" },

            new CarouselItem{ Img="banner2.png" },

            new CarouselItem{ Img="banner3.png" },
        };
            Items = imgs;
        }
    }
}
