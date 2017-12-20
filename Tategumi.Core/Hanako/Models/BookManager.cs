using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Hanako.Models;

namespace Tategumi.Models
{
    public class BookManager
    {
        private int CurrentArticleId { get; set; }
        List<HKPara> _paralst;  //段落
        public IList<IHKWaxPage> PageList { get; set; }     //組版されたページ一覧
        List<HKWaxLine> _lnlst;
        public BookManager()
        {
            _paralst = new List<HKPara>();
            _lnlst = new List<HKWaxLine>();
            PageList = new List<IHKWaxPage>();
            CurrentArticleId = -1;
        }
        public int PageNum { get; set; }
        public int PageIndex { get; set; }
        public double TateviewWidth { get; set; }
        public double TateviewHeight { get; set; }
        public string Title { get; set; }
        public bool IsFontSizeLarge { get; set; }
        public float FontSise { get; set; } = 36;

        /// <summary>
        /// 本文をURLから読み込み(未使用)
        /// </summary>
        /// <returns>The honbun html from URL.</returns>
        /// <param name="path"></param>
        public void ReadFromText(string hbn)
        {
            //var ser = new BookService2(new BookRepository());
            //string hbn = await ser.GetBookFromUrl(new System.Uri(url));
            _paralst?.Clear();
            var parser = new HKSimpleParser();
            parser.ParseFromText(hbn);
            _paralst = parser.ResultParaList;
        }

        /// <summary>
        /// ページを組版
        /// </summary>
        public void Compose(bool isForce=false)
        {
            if ( isForce!=true && IsValid() != true) return;

            PageIndex = 0;
            double devw = TateviewWidth;
            double devh = TateviewHeight;
            //1.Compose
            float fntSz = FontSise;
            if (IsFontSizeLarge == true)
                fntSz += 24;
            //float gyokanSz = fntSz * 0.5f;
            float gyokanSz = fntSz * 1.0f;
            float mg = 10;    //デバイスに対しての余白
            HKComposer comp = new HKComposer();
            comp.FontSize = fntSz;
            comp.GyokanSize = gyokanSz;
            comp.Init((float)devw - mg * 2, (float)devh - mg * 2);
            _lnlst.Clear();
            comp.Compose(_paralst, ref _lnlst);

            //2.Page一覧
            PageList.Clear();
            var pglst = PageList;
            HKPageCreate.CreatePageList((float)devw - mg * 2, fntSz, _lnlst, ref pglst);

            //3.Deviceの値に変換
            HKDevice dev = new HKDevice();
            dev.setup(fntSz, (float)devw, (float)devh, mg, mg, mg, mg);
            dev.calcToDevice(ref pglst);

            PageNum = PageList.Count;
        }

        public bool IsValid()
        {
            if (_paralst?.Count == 0) return false;
            if (TateviewWidth <= 0 || TateviewHeight <= 0) return false;
            return true;
        }
        public void GoToNextPage()
        {
            PageIndex++;
        }
        public void GoToPrevPage()
        {
            PageIndex--;
        }
    }
}
