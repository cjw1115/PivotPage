# PivotPage ———— 多页面切换控件
PivotPage是一个多页面切换控件，类似安卓中的ViewPager和UWP中的Pivot枢轴控件。

起初打算直接通过ScrollView+StackLayout直接实现该控件，但是在具体实现的时候，发现iOS中，利用UIScrollView的PagingEnabled 属性，可以很方便的实现分页效果。但是，在安卓平台中，原生的ScrollView只提供了很有限的事件方法，需要比较绕的操作才能实现诸如滚动开始，滚动停止等事件的监听，极其不便，所以直接利用ViewPager实现多页面切换的效果。

又考虑到在Xamarin.Forms层面，应该有同意的方法实现页面切换等操作，和具体平台相互隔离
所以使用了一个自定义控件ViewPanel,该控件继承自ScrollView

#### 为什么ViewPanel不直接继承自View呢？

1. 在安卓平台中，无论是View还是ScrollView，最后都要通过ViewRenderer用ViewPager来实现，没有区别。

2. 在iOS中，情况有些不同。应为iOS中该控件基本都是用FORMS直接实现的，唯一用到的Renderer是ScrollViewRenderer，用来设置ScrollView对应的UIScrollView的PagingEnabled属性，让滚动条按页滑动，所以让ViewPanel直接继承自ScrollView，省去一些不必要的代码

#### 如何实现在ViewPanel中显示自定义的控件？

在iOS中，ViewPanel是由ScrollView+StackLayout直接实现的，所以把我们自定义的View直接添加到StackLayout中就可以了，代码实现类似于：

        ···
        _stackLaout.Children.Add(view);
        ···

在Android中，ViewPanel由一个ViewPager实现，ViewPanel为ViewPager提供所有的子元素，利用如下方法添加到ViewPager中：
（来资源PageAdapter）
        
        public override Java.Lang.Object InstantiateItem(Android.Views.View container, int position)
        {

            var viewPager = container.JavaCast<ViewPager>();

            var view = _views[position] as Xamarin.Forms.View;

            view.Parent = _customViewPage;

            if (Platform.GetRenderer(view) == null)

                Platform.SetRenderer(view, Platform.CreateRenderer(view));

            var renderer = Platform.GetRenderer(view);

            var viewGroup = renderer.ViewGroup;

            viewPager.AddView(viewGroup);

            return viewGroup;

        }

使用ViewPager的时候，数据由PageAdapter提供。根据我们提供的View,创建相应的NativeView添加到ViewPager中。

***

控件由两部分组成：
1. Header:放置各个页面标题，LOGO等，数据模板自定义，支持数据绑定
2. Views:自定义控件ViewPanel，继承自ScrollView（主要方便iOS），安卓中不使用ScrollView的任何特性，只当作简单的View

注意：使用的时候需要自定义两个集合，一个存放Views，一个存放Header，二者中元素一一对应，由使用者维护

(在MVVM中使用举例)

        Headers.Add(new PivotItemModel { Title = "Mokey" });
        Views.Add(new MokeyView());
        Headers.Add(new PivotItemModel { Title = "Test" });
        Views.Add(new TestView());

###
目前实现的版本
* iOS:利用Xamarin.Fomrs中SrollView+StackLayout实现ViewPanel
* Android:直接使用ViewPager实现ViewPanel

![Demo](https://github.com/cjw1115/PivotPage/blob/master/PivotView/PivotPageDemo/DemoAssets/demo.png)
### GitHub地址：[PivotPage](https://github.com/cjw1115/PivotPage)
