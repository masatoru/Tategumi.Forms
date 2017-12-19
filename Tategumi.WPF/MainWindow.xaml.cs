using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using Tategumi.Models;
using Tategumi.TategumiViews;

namespace Tategumi.WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        protected SKMatrix Matrix = SKMatrix.MakeIdentity();
        public bool IsInitialized { get; private set; }
        BookManager Manager { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Manager = new BookManager();
        }

        public void Init()
        {
            if (IsInitialized) return;
            // reset the matrix for the new sample
            Matrix = SKMatrix.MakeIdentity();

            // リソースから読み込む
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(@"Tategumi.WPF.kokoro.htm"))
            using (StreamReader reader = new StreamReader(stream))
            {
                var text = reader.ReadToEnd();
                Manager.ReadFromText(text);
            }
            Manager.TateviewWidth = canvas.CanvasSize.Width;
            Manager.TateviewHeight = canvas.CanvasSize.Height;

            //TategumiViewCore.OpenFontStream = () => File.Open(@"ipaexm.ttf", FileMode.Open);
            TategumiViewCore.OpenFontStream = 
                () => assembly.GetManifestResourceStream(@"Tategumi.WPF.ipaexm.ttf");

            // 組版する
            Manager.Compose();
            IsInitialized = true;
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            Init();
            if (Manager.IsValid())
                TategumiViewCore.DrawHonbunPage(e.Surface.Canvas, Manager.PageList?[0], false);
        }
    }
}