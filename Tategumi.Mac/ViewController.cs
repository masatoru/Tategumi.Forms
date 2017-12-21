using System;
using System.IO;
using AppKit;
using Foundation;
using SkiaSharp;
using SkiaSharp.Views.Mac;
using Tategumi.Core;
using Tategumi.Models;
using Tategumi.TategumiViews;

namespace Tategumi.Mac
{
    public partial class ViewController : NSViewController
    {
        protected SKMatrix Matrix = SKMatrix.MakeIdentity();
        public bool IsInitialized { get; private set; } = false;
        BookManager Manager { get; set; }
        public ViewController(IntPtr handle) : base(handle)
        {
            Manager = new BookManager();
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

        public void Init()
        {
            // reset the matrix for the new sample
            Matrix = SKMatrix.MakeIdentity();

            // リソースから読み込む
            var path = NSBundle.MainBundle.PathForResource("kokoro", "htm");
            var text = System.IO.File.ReadAllText(path);

            Manager.ReadFromText(text);
            Manager.TateviewWidth = canvas.CanvasSize.Width;
            Manager.TateviewHeight = canvas.CanvasSize.Height;
            Manager.FontSize = 24;
            path = NSBundle.MainBundle.PathForResource("ipaexm", "ttf");
            TategumiViewCore.OpenFontStream = () => File.Open(path,FileMode.Open);

            // 組版する
            Manager.Compose();
            IsInitialized = true;
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
            Init();
            if (Manager.IsValid())
                TategumiViewCore.DrawHonbunPage(e.Surface.Canvas, Manager.PageList?[0], false);
            //TextSample.DrawSample(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

    }
}
