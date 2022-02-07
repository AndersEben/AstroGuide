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
    class AddPflanzen : BaseAdapter<Pflanze>
    {
        List<Pflanze> Items;
        Activity Context;

        public AddPflanzen(Activity context, List<Pflanze> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Pflanze this[int position]
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
                view = Context.LayoutInflater.Inflate(Resource.Layout.planet_pflanzen, null);


            view.FindViewById<TextView>(Resource.Id.PflanzenName).Text = item.Name;

            view.FindViewById<ImageView>(Resource.Id.PflanzenImage).SetImageResource(item.Image);

            return view;
        }
    }
}