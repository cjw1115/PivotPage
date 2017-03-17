# PivotPage ———— 多页面切换控件
一个多页面切换控件，类似安卓中的ViewPager和UWP中的Pivot枢轴控件。
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

