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
            if (carouselView.Position == itemsControl.SelectedIndex)
            {
                return;
            }
            else
            {
                carouselView.Position = itemsControl.SelectedIndex;
            }
            System.Diagnostics.Debug.WriteLine($"Selected:{e.SelectedItem.ToString()}");
        }

        private void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            if(itemsControl.SelectedIndex == (int)e.SelectedPosition)
            {
                return;
            }
            else
            {
                itemsControl.SelectedIndex = (int)e.SelectedPosition;
            }
        }
        

    }
}
