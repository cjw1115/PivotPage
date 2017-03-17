using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PivotView;
using PivotView.Droid;
using static Android.Views.View;
using Android.Support.V4.View;
using Java.Lang;
using System.Collections;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

[assembly: ExportRenderer(typeof(CustomViewPager), typeof(ScrollView_Android))]
namespace PivotView.Droid
{
    public class ScrollView_Android:ViewRenderer<CustomViewPager, ViewPager>
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<CustomViewPager> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                //var viewpager = (this.Context as Activity).LayoutInflater.Inflate(Resource.Layout.CustomViewPager, this.Control,true) as ViewPager;
                //var viewpager = new ViewPager(this.Context);
                
                //var root = new Android.Widget.RelativeLayout(this.Context);

                //root.SetBackgroundColor(Color.Green.ToAndroid());

                var viewpager = new ViewPager(this.Context);
                viewpager.Adapter = new CustomPagerAdapter(this.Context, this.Element);

                //root.AddView(viewpager, LayoutParams.MatchParent, LayoutParams.MatchParent);
                


                this.SetNativeControl(viewpager);


            }
        }
    }

    public class CustomPagerAdapter : PagerAdapter
    {
        private CustomViewPager _customViewPage;
        private Context _context;
        private IList _views = new List<Xamarin.Forms.View>();
        public CustomPagerAdapter(Context context, CustomViewPager customViewPage)
        {
            _customViewPage = customViewPage;
            _views = customViewPage.Children ;
            _context = context;
        }
        public override int Count
        {
            get
            {
                return _views.Count;
            }
        }

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

        public override bool IsViewFromObject(Android.Views.View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }
        public override void DestroyItem(Android.Views.View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as Android.Views.View);
        }

        
    }
}