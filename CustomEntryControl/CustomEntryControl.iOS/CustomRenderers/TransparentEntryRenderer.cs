using CustomEntryControl.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(TransparentEntryRenderer))]
namespace CustomEntryControl.iOS.CustomRenderers
{
    public class TransparentEntryRenderer : EntryRenderer
    {        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.TextColor = UIColor.Black;

            }
        }
    }
}