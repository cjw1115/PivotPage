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

[assembly:ExportRenderer(typeof(ScrollViewExpand),typeof(ScrollView_Android))]
namespace PivotView.Droid
{
    public class ScrollView_Android:ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var scrollView = (ScrollViewExpand)this.Element;
            this.SetOnTouchListener(new OnTouchListener(scrollView));
        }

        private void ScrollView_EndScroll(object sender, EventArgs e)
        {
            
        }
    }
    public class OnTouchListener : IOnTouchListener
    {
        private static int lastX = 0;
        private static int touchEventId = -9983761;

        private Handler handler = new MyHandler();

        public static ScrollViewExpand _scrollView;

        public IntPtr Handle => throw new NotImplementedException();

        public OnTouchListener(ScrollViewExpand scrollView)
        {
            _scrollView = scrollView;
        }

        public void Dispose()
        {
            
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
           
           if(e.Action== MotionEventActions.Up)
           {
               handler.SendMessageDelayed(handler.ObtainMessage(touchEventId, v), 5);
           }
           return false;
        }

        public class MyHandler:Handler
        {
            public override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                var scroller = (Android.Views.View) msg.Obj;
                if (msg.What == touchEventId)
                {
                    if (lastX == scroller.ScrollX)
                    {
                        //停止了，此处你的操作业务
                        _scrollView.OnEndScroll();
                    } 
                    else
                    {
                        this.SendMessageDelayed(this.ObtainMessage(touchEventId, scroller), 1);
                        lastX = scroller.ScrollX;
                    }
                }
            }
        }
    }
}