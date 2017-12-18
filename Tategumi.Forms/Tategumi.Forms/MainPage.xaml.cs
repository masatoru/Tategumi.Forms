using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tategumi.Core;
using Xamarin.Forms;

namespace Tategumi.Forms
{
    public partial class MainPage : ContentPage
    {
        protected SKMatrix Matrix = SKMatrix.MakeIdentity();
        public bool IsInitialized { get; private set; } = false;

        public MainPage()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            // reset the matrix for the new sample
            Matrix = SKMatrix.MakeIdentity();
            IsInitialized = true;

//            if (!IsInitialized)
//			{
//				await OnInit();
//
//				IsInitialized = true;
//
//				Refresh();
//			}
        }



        private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
        {
            TextSample.DrawSample(e.Surface.Canvas, e.Info.Width, e.Info.Height);

            //lastImage?.Dispose();
            //lastImage = e.Surface.Snapshot();

//			var view = sender as SKCanvasView;
//			DrawScaling(view, e.Surface.Canvas, view.CanvasSize);
        }
	    private void OnTapSample(object sender, EventArgs e)
	    {
	    }

	    private void OnPanSample(object sender, PanUpdatedEventArgs e)
	    {
	    }

	    private void OnPinchSample(object sender, PinchGestureUpdatedEventArgs e)
	    {
	    }
    }
}