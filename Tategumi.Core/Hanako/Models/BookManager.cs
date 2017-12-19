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
        IList<IHKWaxPage> _pglst;     //組版されたページ一覧
        List<HKWaxLine> _lnlst;
        public BookManager()
        {
            _paralst = new List<HKPara>();
            _lnlst = new List<HKWaxLine>();
            _pglst = new List<IHKWaxPage>();
            CurrentArticleId = -1;
        }
        public int PageNum { get; set; }
        public int PageIndex { get; set; }
        public int TateviewWidth { get; set; }
        public int TateviewHeight { get; set; }
        public string Title { get; set; }
        public bool IsFontSizeLarge { get; set; }
        public bool IsVisibleHinshi { get; set; }

        /// <summary>
        /// 本文をURLから読み込み(未使用)
        /// </summary>
        /// <returns>The honbun html from URL.</returns>
        /// <param name="path"></param>
        public void ReadFromPath(string path)
        {
            //var ser = new BookService2(new BookRepository());
            //string hbn = await ser.GetBookFromUrl(new System.Uri(url));
            //_paralst?.Clear();
            //var parser = new HKSimpleParser();
            //parser.ParseFromText(hbn);
            //_paralst = parser.ResultParaList;
        }

        /// <summary>
        /// ページを組版
        /// </summary>
        public void Compose()
        {
            //if (_paralst?.Count == 0 ) return;
            if (isValid() != true) return;

            int devw = TateviewWidth;
            int devh = TateviewHeight;
            //1.Compose
            float fntSz = 24;
            if (IsFontSizeLarge == true)
                fntSz += 24;
            //float gyokanSz = fntSz * 0.5f;
            float gyokanSz = fntSz * 1.0f;
            if (IsVisibleHinshi == true)
                gyokanSz += fntSz * 1.0f;
            float mg = 10;    //デバイスに対しての余白
            HKComposer comp = new HKComposer();
            comp.FontSize = fntSz;
            comp.GyokanSize = gyokanSz;
            comp.Init((float)devw - mg * 2, (float)devh - mg * 2);
            _lnlst.Clear();
            comp.Compose(_paralst, ref _lnlst);

            //2.Page一覧
            _pglst.Clear();
            HKPageCreate.CreatePageList((float)devw - mg * 2, fntSz, _lnlst, ref _pglst);

            //3.Deviceの値に変換
            HKDevice dev = new HKDevice();
            dev.setup(fntSz, (float)devw, (float)devh, mg, mg, mg, mg);
            dev.calcToDevice(ref _pglst);

            PageNum = _pglst.Count;
        }

        bool isValid()
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
