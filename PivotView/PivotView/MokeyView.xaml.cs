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

        
        Random rand = new Random();
        private void Button_Clicked(object sender, EventArgs e)
        {
            var index = rand.Next(1, 6);
            MokeyModel item = new MokeyModel()
            {
                ImageUri = $"pic{index}.png",
                Name=$"猴子{index}"
            };
            VM.Items.Add(item);
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


        public ObservableCollection<MokeyModel> _items;
        public ObservableCollection<MokeyModel> Items
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
            Items = new ObservableCollection<MokeyModel>
                {
                    new MokeyModel{ ImageUri="pic1.png" ,Name="猴子1"},
                    new MokeyModel{ ImageUri="pic2.png" ,Name="猴子2"},

                    new MokeyModel{ ImageUri="pic3.png" ,Name="猴子3"}
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
