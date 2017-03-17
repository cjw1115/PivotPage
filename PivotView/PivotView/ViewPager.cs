using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class CustomViewPager : View
    {
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create("Children", typeof(IList), typeof(CustomViewPager));
        public IList Children
        {
            get { return (IList)this.GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            foreach (View item in Children)
            {
                item.Layout(new Rectangle(0, 0, width, height));
            }
        }
    }
}
