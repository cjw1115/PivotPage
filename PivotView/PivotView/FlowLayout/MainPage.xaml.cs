using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PivotView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlowLayoutPage : ContentView
    {
        MainPageViewModel VM = new MainPageViewModel();
        public FlowLayoutPage()
        {
            InitializeComponent();
            this.BindingContext = VM;
            VM.LoadData();
        }


        Random rand = new Random();
        private void Add_Clicked(object sender, EventArgs e)
        {
            var index = rand.Next(1, 6);
            ItemModel item = new ItemModel()
            {
                ImageUri = $"pic{index}.png"
            };
            VM.Items.Add(item);
        }
        private void Del_Clicked(object sender, EventArgs e)
        {
            var index = rand.Next(0, VM.Items.Count-1);

            VM.Items.RemoveAt(index);
            
        }
    }
    public class ItemModel
    {
        public string ImageUri { get; set; }
        public string Title { get; set; }
    }
    public class MainPageViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
        

        public ObservableCollection<ItemModel> _items;
        public ObservableCollection<ItemModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            
        }
        public void LoadData()
        {
            Items = new ObservableCollection<ItemModel>
                {
                    new ItemModel{ ImageUri="pic1.png" ,Title="猴子1"},
                    new ItemModel{ ImageUri="pic2.png" ,Title="猴子2"},

                    new ItemModel{ ImageUri="pic3.png" ,Title="猴子3"}
                };
        }
    }
}
