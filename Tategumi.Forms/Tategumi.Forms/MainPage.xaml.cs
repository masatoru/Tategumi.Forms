using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tategumi.Core;
using Tategumi.Services;
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

        /// <summary>
        /// Read text from path
        /// </summary>
        /// <returns>The book from file name.</returns>
        /// <param name="fileName">File name.</param>
        public string GetBookFromFileName(string fileName)
        {
            return DependencyService.Get<IResourceDirectory>().ReadText(fileName);
        }

        public void Init()
        {
            // reset the matrix for the new sample
            Matrix = SKMatrix.MakeIdentity();
            IsInitialized = true;
        }

        void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
        {
            TextSample.DrawSample(e.Surface.Canvas, e.Info.Width, e.Info.Height);
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