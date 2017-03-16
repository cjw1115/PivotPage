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

[assembly: ExportRenderer(typeof(HorizentalLayout), typeof(ScrollView_Android))]
namespace PivotView.Droid
{
    public class ScrollView_Android:ViewRenderer<HorizentalLayout, ViewPager>
    {
        public static int TouchEventId => -9983761;
        private Handler handler;
        protected override void OnElementChanged(ElementChangedEventArgs<HorizentalLayout> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                var viewpager = (this.Context as Activity).LayoutInflater.Inflate(Resource.Layout.CustomViewPager, null) as ViewPager;

                viewpager.Adapter = new CustomPagerAdapter(this.Context, this.Element.Children);
                SetNativeControl(viewpager);

            }
        }

        private void ScrollView_Android_Touch(object sender, TouchEventArgs e)
        {
            e.Handled = true;

        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {
                handler.SendMessageDelayed(handler.ObtainMessage(ScrollView_Android.TouchEventId, v), 5);
            }
            return false;
        }

    }

    public class CustomPagerAdapter : PagerAdapter
    {
        private Context _context;
        private IList _views;
        public CustomPagerAdapter(Context context,IEnumerable views)
        {
            _views = new List<Xamarin.Forms.View>();
            foreach (var item in views)
            {
                _views.Add(item);
            }
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
            var renderer = Platform.GetRenderer(view);
            var viewGroup = renderer.ViewGroup;
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