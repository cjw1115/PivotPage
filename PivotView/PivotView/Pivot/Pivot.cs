using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class Pivot : ContentPage
    {
        private ItemsControl _headers;
        private CarouselView _content;

        public Pivot()
        {
            _headers = new ItemsControl();
            _content = new CarouselView();

            
        }
    }
}
