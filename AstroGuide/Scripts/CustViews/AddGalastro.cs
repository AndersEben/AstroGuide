﻿using Android.App;
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
    class AddGalastro : BaseAdapter<Galastropode>
    {
        List<Galastropode> Items;
        Activity Context;

        public AddGalastro(Activity context, List<Galastropode> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override Galastropode this[int position]
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
            text.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            text.Text = item.Name;
            view.FindViewById<ImageView>(Resource.Id.ResListBild).SetImageResource(item.Icon);

            return view;
        }
    }
}