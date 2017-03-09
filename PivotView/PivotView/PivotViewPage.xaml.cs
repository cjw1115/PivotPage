using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PivotView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PivotViewPage : ContentPage
    {
        MainPageViewModel VM=new MainPageViewModel();
        public PivotViewPage()
        {
            InitializeComponent();
            this.BindingContext = VM;
            VM.LoadData();
            Label label = new Label();
        }

        Random rand = new Random();
        private void Button_Clicked(object sender, EventArgs e)
        {
            var index = rand.Next(1, 6);
            ItemModel item = new ItemModel()
            {
                Title = $"猴子{index}",
                ImageUri = $"pic{index}.png"
            };
            VM.Items.Add(item);
        }

        private void ItemsControl_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Selected:{e.SelectedItem.ToString()}");
        }
    }
}
