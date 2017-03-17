using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class HorizentalLayout:StackLayout
    {
        public HorizentalLayout()
        {
            base.Orientation=StackOrientation.Horizontal;
        }
        public new StackOrientation Orientation
        {
            get { return base.Orientation; }
            private set { }
        }
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double allWidth = 0;
            for (int i = 0; i <Children.Count; i++)
            {
                var measuredSize= Children[i].Measure(width, height);
                var screenWidth=Xamarin.Forms.Application.Current.MainPage.Width;
                Children[i].Layout(new Rectangle(allWidth, y, screenWidth, measuredSize.Request.Height));
                allWidth += screenWidth;
            }
        }
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var screenWidth=Children.Count * Xamarin.Forms.Application.Current.MainPage.Width;
            return new SizeRequest(new Size(screenWidth, heightConstraint));
        }

        //protected override void OnChildAdded(Element child)
        //{
        //    base.OnChildAdded(child);
        //    this.InvalidateLayout();
        //}

    }
}
