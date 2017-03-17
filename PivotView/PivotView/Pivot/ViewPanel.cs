using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    /// <summary>
    /// 用于显示多个试图的容器
    /// 在iOS中，利用ScrollView+Stacklayout间接实现安卓中ViewPager的效果
    /// </summary>
    public class ViewPanel : ScrollView
    {
        /// <summary>
        /// 只在IOS中有用，充当试图真实的容器
        /// </summary>
        private StackLayout _horizentalLayout;
        public ViewPanel()
        {
            _horizentalLayout = new StackLayout() {  Orientation =  StackOrientation.Horizontal};
            this.Content = _horizentalLayout;
        }
        /// <summary>
        /// 支持数据绑定的Child View集合
        /// </summary>
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create("Children", typeof(IList), typeof(ViewPanel),propertyChanged: OnChildrenChanged);
        public IList Children
        {
            get { return (IList)this.GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }
        /// <summary>
        /// 当视图集合Children发生变化后，如果系统是iOS，则将Children中的试图全部添加到
        /// StackLayout中，在安卓中无效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        static void OnChildrenChanged(BindableObject sender,Object oldValue,Object newValue)
        {
            if(Device.OS == TargetPlatform.iOS)
            {
                var viewPanel = sender as ViewPanel;
                var stackLayout = viewPanel.Content as StackLayout;
                foreach (View item in viewPanel.Children)
                {
                    stackLayout.Children.Add(item);
                }
            }
            
        }

        /// <summary>
        /// 为每一个Child View设置布局大小，此方法只使用于安卓，iOS中无效
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (Children == null)
                return;
            if(Device.OS== TargetPlatform.Android)
            {
                foreach (View item in Children)
                {
                    item.Layout(new Rectangle(0, 0, width, height));
                }
            }
        }

        public event EventHandler<SelectedPositionChangedEventArgs> SelectChanged;
        public static readonly BindableProperty CurrentIndexProperty = BindableProperty.Create("CurrentIndex", typeof(int), typeof(ViewPanel), 0);
        public int CurrentIndex
        {
            get { return (int)this.GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        /// <summary>
        /// 调用事件SelectChanged
        /// </summary>
        public void OnSelectChanged()
        {
            SelectChanged?.Invoke(this, new SelectedPositionChangedEventArgs(CurrentIndex));
        }

        /// <summary>
        /// 更具索引设置ViewPanel显示的View
        /// </summary>
        /// <param name="index">索引，从0开始</param>
        public delegate void SelectDelegate(int index);
        
        public SelectDelegate Select{get;set;}
    }
}
