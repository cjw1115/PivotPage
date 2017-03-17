using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class ViewPanel : View
    {
        /// <summary>
        /// 支持数据绑定的Child View集合
        /// </summary>
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create("Children", typeof(IList), typeof(ViewPanel));
        public IList Children
        {
            get { return (IList)this.GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        /// <summary>
        /// 为每一个Child View设置布局大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            foreach (View item in Children)
            {
                item.Layout(new Rectangle(0, 0, width, height));
            }
        }

        public event EventHandler<SelectedPositionChangedEventArgs> SelectChanged;
        public static readonly BindableProperty CurrentIndexProperty = BindableProperty.Create("CurrentIndex", typeof(int), typeof(ViewPanel), 0);
        public int CurrentIndex
        {
            get { return (int)this.GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        public void OnSelectChanged()
        {
            SelectChanged?.Invoke(this, new SelectedPositionChangedEventArgs(CurrentIndex));
        }

        public delegate void SelectDelegate(int index);
        public SelectDelegate Select{get;set;}
    }
}
