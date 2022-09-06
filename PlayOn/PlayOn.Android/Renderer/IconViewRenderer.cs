using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using IconApp;
using Xamarin.Forms;
using IconApp.Droid.Renderers;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content;
using Android.Support.V4.Content;

[assembly: ExportRendererAttribute(typeof(IconView), typeof(IconViewRenderer))]

namespace IconApp.Droid.Renderers
{
    public class IconViewRenderer : ViewRenderer<IconView, ImageView>
    {
        private bool _isDisposed;

        public IconViewRenderer(Context context) : base(context)
        {
            base.AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<IconView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                SetNativeControl(new ImageView(Context));
            }
            UpdateBitmap(e.OldElement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == IconView.SourceProperty.PropertyName)
            {
                UpdateBitmap(null);
            }
            else if (e.PropertyName == IconView.ForegroundProperty.PropertyName)
            {
                UpdateBitmap(null);
            }
        }

        private void UpdateBitmap(IconView previous = null)
        {
            if (!_isDisposed && !string.IsNullOrWhiteSpace(Element.Source))
            {
                var d = Context.GetDrawable(Element.Source).Mutate();
                d.SetTint(Element.Foreground.ToAndroid());
                d.Alpha = Element.Foreground.ToAndroid().A;
                Control.SetImageDrawable(d);
                ((IVisualElementController)Element).NativeSizeChanged();
            }
        }
    }
}

