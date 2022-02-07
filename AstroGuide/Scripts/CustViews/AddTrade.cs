using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AstroGuide.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts.CustViews
{
    class AddTrade : BaseAdapter<Trade>
    {
        List<Trade> Items;
        Activity Context;

        public AddTrade(Activity context, List<Trade> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Trade this[int position]
        {
            get { return Items[position]; }
        }

        public override int Count
        {
            get { return Items.Count(); }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            View view = convertView;
            if (view == null)
                view = Context.LayoutInflater.Inflate(Resource.Layout.ressourece_ressource, null);

            var text = view.FindViewById<TextView>(Resource.Id.RRezept);
            text.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 35);
            text.Text = item.Ratio + " " + item.Obj.ToString() + " / " + item.Item;
            view.FindViewById<ImageView>(Resource.Id.RRIcon).SetImageResource(item.Image);

            return view;
        }
    }
}