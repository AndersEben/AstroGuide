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
    class AddCrafter : BaseAdapter<Crafter>
    {
        List<Crafter> Items;
        Activity Context;

        public AddCrafter(Activity context, List<Crafter> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Crafter this[int position]
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
                view = Context.LayoutInflater.Inflate(Resource.Layout.ressourcen_ressource, null);

            var text = view.FindViewById<TextView>(Resource.Id.ResListName);
            text.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 20);
            text.Text = item.Name;
            view.FindViewById<ImageView>(Resource.Id.ResListBild).SetImageResource(item.Image);

            return view;
        }
    }
}