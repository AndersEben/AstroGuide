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
    class AddRezept : BaseAdapter<Ressource>
    {
        List<Ressource> Items;
        Activity Context;

        public AddRezept(Activity context, List<Ressource> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Ressource this[int position]
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

            view.FindViewById<ImageView>(Resource.Id.RRIcon).SetImageResource(item.Image);
            view.FindViewById<TextView>(Resource.Id.RRezept).Text = item.Name;

            return view;
        }
    }
}