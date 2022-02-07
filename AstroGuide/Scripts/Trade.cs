using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts
{
    public class Trade
    {
        public string Item { get; set; }
        public string Ratio { get; set; }

        public VerwendungsTyp Typ { get; set; }
        public TauschObjekt Obj { get; set; }
        public int Image { get; set; }

        public Trade(string item, string ratio, VerwendungsTyp typ)
        {
            this.Item = item;
            this.Ratio = ratio;
            this.Typ = typ;
        }

        public Trade(string item, int image, string ratio, TauschObjekt obj)
        {
            this.Item = item;
            this.Image = image;
            this.Ratio = ratio;
            this.Obj = obj;
        }

        public void SetTradeObjekt(TauschObjekt obj)
        {
            this.Obj = obj;
        }

        public void SetImage(int image)
        {
            this.Image = image;
        }
    }
}