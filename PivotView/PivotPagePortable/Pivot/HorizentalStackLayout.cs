using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotPagePortable
{
    public class HorizontalStackLayout : StackLayout
    {

        public HorizontalStackLayout()
        {
            base.Orientation = StackOrientation.Horizontal;
        }

        public new StackOrientation Orientation
        {
            get { return base.Orientation; }
        }
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(ViewPanel.Panel.Width*Children.Count, heightConstraint));
        }
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double posX = 0;
            foreach (var item in this.Children)
            {
                item.Layout(new Rectangle(posX, 0, ViewPanel.Panel.Width, height));
                posX += ViewPanel.Panel.Width;
            }
        }
    }
}
