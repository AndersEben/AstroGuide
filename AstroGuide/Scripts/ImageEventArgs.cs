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
    public class ImageEventArgs : EventArgs
    {
        public ImageEventArgs(View view, int position, bool longclick,int pic)
        {
            this.ItemView = view;
            this.Position = position;
            this.LongClick = longclick;
            this.Picture = pic;
        }

        public View ItemView { get; set; }
        public int Position { get; set; }
        public bool LongClick { get; set; }
        public int Picture { get; set; }

    }
}