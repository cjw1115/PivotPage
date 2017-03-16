//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using PivotView;
//using PivotView.Droid;
//using static Android.Views.View;

//[assembly: ExportRenderer(typeof(ScrollViewExpand), typeof(ScrollView_Android2))]
//namespace PivotView.Droid
//{
//    public class ScrollView_Android2 : ViewRenderer<ScrollViewExpand, Android.Widget.HorizontalScrollView>//, IOnTouchListener
//    {
//        public static int TouchEventId => -9983761;
//        private Handler handler;
//        protected override void OnElementChanged(ElementChangedEventArgs<ScrollViewExpand> e)
//        {
//            base.OnElementChanged(e);

//            var scrollView = (ScrollViewExpand)this.Element;
//            handler = new MyHandler(TouchEventId, scrollView);

//            if (this.Control == null)
//            {
//                var nativeScrollView = new Android.Widget.HorizontalScrollView(this.Context);
//                nativeScrollView.hei
//                //nativeScrollView.SetOnTouchListener(this);

//                SetNativeControl(nativeScrollView);
//            }
//        }
     
//        //public bool OnTouch(Android.Views.View v, MotionEvent e)
//        //{
//        //    if (e.Action == MotionEventActions.Up)
//        //    {
//        //        handler.SendMessageDelayed(handler.ObtainMessage(ScrollView_Android2.TouchEventId, v), 5);
//        //    }
//        //    return true;
//        //}

//    }

//    public class MyHandler : Handler
//    {
//        private int touchEventId = -9983761;

//        private double lastX;
//        private ScrollViewExpand scrollView;

//        public MyHandler(int _touchEventID, ScrollViewExpand _scrollView)
//        {
//            touchEventId = _touchEventID;
//            scrollView = _scrollView;
//        }
//        public override void HandleMessage(Message msg)
//        {
//            base.HandleMessage(msg);
//            var scroller = (Android.Views.View)msg.Obj;

//            if (msg.What == touchEventId)
//            {
//                if (lastX == scroller.ScrollX)
//                {
//                    scrollView.OnEndScroll();
//                }
//                else
//                {
//                    this.SendMessageDelayed(this.ObtainMessage(touchEventId, scroller), 1);
//                    lastX = scroller.ScrollX;
//                }
//            }
//        }
//    }
//}