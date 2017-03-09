using System;
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
    public partial class TextPage : ContentPage
    {
        MainPageViewModel VM;
        public TextPage()
        {
            VM = new MainPageViewModel();
            InitializeComponent();
            this.BindingContext = VM;
            this.VM.LoadData();
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
            var index = rand.Next(0, VM.Items.Count - 1);

            VM.Items.RemoveAt(index);


        }
    }
    public class MyListView : ListView
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}
