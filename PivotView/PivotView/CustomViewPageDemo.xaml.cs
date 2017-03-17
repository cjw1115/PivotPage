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
    public partial class CustomViewPageDemo : ContentPage
    {
        public CustomViewPageDemo()
        {
            InitializeComponent();
            var mokeyView = new MokeyView();
            var testview = new TestView();
            var list = new List<View> { mokeyView , testview };
            this.viewPage.Children = list;
        }
    }
}
