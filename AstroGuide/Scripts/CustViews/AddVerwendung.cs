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

namespace AstroGuide.Scripts.CustViews
{
    class AddVerwendung : BaseAdapter<Verwendung>
    {
        List<Verwendung> Items;
        Activity Context;

        public AddVerwendung(Activity context, List<Verwendung> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Verwendung this[int position]
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

        public Verwendung GetVerwendung(int position)
        {
            return Items[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            View view = convertView;
            if (view == null)
                view = Context.LayoutInflater.Inflate(Resource.Layout.ressource_vorkommen, null);

            view.FindViewById<ImageView>(Resource.Id.RVIcon).SetImageResource(item.Image);
            view.FindViewById<TextView>(Resource.Id.VorkommenName).Text = item.Name;

            return view;
        }
    }
}