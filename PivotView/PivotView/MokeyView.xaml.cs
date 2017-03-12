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
    public partial class MokeyView: ContentView
    {
        MokeyViewModel VM = new MokeyViewModel();
        public MokeyView()
        {
            InitializeComponent();
            this.BindingContext = this.VM;
            this.VM.LoadData();
        }
    }
    public class MokeyModel
    {
        public string ImageUri { get; set; }
        public string Name { get; set; }
    }
    public class MokeyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        public async void LoadData()
        {
            IsLoading = true;
            await Task.Delay(3000);
            Items = new ObservableCollection<ItemModel>
                {
                    new ItemModel{ ImageUri="pic1.png" ,Title="猴子1"},
                    new ItemModel{ ImageUri="pic2.png" ,Title="猴子2"},

                    new ItemModel{ ImageUri="pic3.png" ,Title="猴子3"}
                };
            IsLoading = false;
        }
        private bool _isLoading=false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value;OnPropertyChanged(); }
        }

    }
}
