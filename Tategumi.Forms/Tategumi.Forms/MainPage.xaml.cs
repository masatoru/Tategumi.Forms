using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tategumi.Core;
using Tategumi.Models;
using Tategumi.Services;
using Tategumi.TategumiViews;
using Xamarin.Forms;

namespace Tategumi.Forms
{
    public partial class MainPage : ContentPage
    {
        protected SKMatrix Matrix = SKMatrix.MakeIdentity();
        public bool IsInitialized { get; private set; } = false;
        BookManager Manager { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Manager = new BookManager();
            //Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Init();
        }

        public void Init()
        {
            // reset the matrix for the new sample
            Matrix = SKMatrix.MakeIdentity();

            // リソースから読み込む
            var text = DependencyService.Get<IResourceDirectory>().ReadText("kokoro.htm");
            Manager.ReadFromText(text);
            Manager.TateviewWidth = canvas.CanvasSize.Width;
            Manager.TateviewHeight = canvas.CanvasSize.Height;
            TategumiViewCore.OpenFontStream = 
                () => DependencyService.Get<IResourceDirectory>().OpenFontFile("ipaexm.ttf");

            // 組版する
            Manager.Compose();
            IsInitialized = true;
        }

        void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
        {
            Init();
            if(Manager.IsValid())
                TategumiViewCore.DrawHonbunPage(e.Surface.Canvas,Manager.PageList?[0],false);
        }
        void OnTapSample(object sender, EventArgs e)
        {
        }

        void OnPanSample(object sender, PanUpdatedEventArgs e)
        {
        }

        void OnPinchSample(object sender, PinchGestureUpdatedEventArgs e)
        {
        }
    }
}