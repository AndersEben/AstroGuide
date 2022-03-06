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

            var RL = view.FindViewById<RelativeLayout>(Resource.Id.PPRL);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, Einstellungen.AdapterSize);
            RL.LayoutParameters = param;

            var text = view.FindViewById<TextView>(Resource.Id.PflanzenName);
            RelativeLayout.LayoutParams txtparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigthText, 0);
            txtparam.AddRule(LayoutRules.AlignParentRight);
            txtparam.AddRule(LayoutRules.CenterInParent);
            text.LayoutParameters = txtparam;
            text.Text = item.Name;

            var image = view.FindViewById<ImageView>(Resource.Id.PflanzenImage);
            RelativeLayout.LayoutParams imgparam = new RelativeLayout.LayoutParams(Einstellungen.AdapterImgSize, RelativeLayout.LayoutParams.MatchParent);
            imgparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            imgparam.AddRule(LayoutRules.AlignParentRight);
            image.LayoutParameters = imgparam;
            image.SetImageResource(item.Image);

            return view;
        }
    }
}