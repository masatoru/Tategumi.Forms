using System;

using AppKit;
using Foundation;
using SkiaSharp.Views.Mac;
using Tategumi.Core;

namespace Tategumi.Mac
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            canvas.PaintSurface += OnPaintCanvas;
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();

            canvas.PaintSurface -= OnPaintCanvas;
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            //OnPaintSurface(e.Surface.Canvas, e.Info.Width, e.Info.Height);
            TextSample.DrawSample(e.Surface.Canvas, e.Info.Width, e.Info.Height);

        }

    }
}
