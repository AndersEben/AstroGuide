using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AstroGuide.Scripts.Planeten;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts.CustViews
{
    class AddEnergie : BaseAdapter<Energie>
    {
        List<Energie> Items;
        Activity Context;

        public AddEnergie(Activity context, List<Energie> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Energie this[int position]
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
                view = Context.LayoutInflater.Inflate(Resource.Layout.planet_energie, null);

            view.FindViewById<TextView>(Resource.Id.EQuelle).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.EForce).Text = item.Staerke.ToString();

            return view;
        }
    }
}