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
            
            var measuredList = new List<SizeRequest>();
            foreach (var item in this.Children)
            {
                measuredList.Add(  item.Measure(ViewPanel.MeasureWidth, double.PositiveInfinity));
            }
            if (Children == null || Children.Count <= 0)
            {
                return new SizeRequest(new Size(ViewPanel.MeasureWidth, 0));
            }
            Size size = new Size(ViewPanel.Panel.Width * Children.Count(), measuredList.Select(m => m.Request.Height).OrderByDescending(m => m).First());
            return new SizeRequest(size, size);
        }
        
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double posX = 0;
            foreach (var item in this.Children)
            {
                item.Layout(new Rectangle(posX, y, ViewPanel.MeasureWidth, height));
                posX += ViewPanel.MeasureWidth;
            }
        }
    }
}
