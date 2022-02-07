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
    class AddRessource : BaseAdapter<Ressourcen>
    {

        List<Ressourcen> Items;
        Activity Context;

        public AddRessource(Activity context, List<Ressourcen> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Ressourcen this[int position]
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
                view = Context.LayoutInflater.Inflate(Resource.Layout.planet_ressource, null);

            if(item.Art.ToString() != ResArt.Universale.ToString())
            {
                view.FindViewById<TextView>(Resource.Id.RTyp).Text = item.Art.ToString();
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.RTyp).Text = "";
            }
                
            view.FindViewById<TextView>(Resource.Id.Ressource).Text = item.Ress.Name;
            view.FindViewById<TextView>(Resource.Id.Haeufigkeit).Text = item.Haeufigkeit;

            view.FindViewById<ImageView>(Resource.Id.RImage).SetImageResource(item.Ress.Image);

            return view;
        }
    }
}