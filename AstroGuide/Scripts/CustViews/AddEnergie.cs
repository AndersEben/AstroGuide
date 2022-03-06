using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AstroGuide.Scripts.Planeten;
using AstroGuide.Scripts.Settings;
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


            var RL = view.FindViewById<RelativeLayout>(Resource.Id.PERL);
            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, Einstellungen.AdapterSize);
            RL.LayoutParameters = param;

            var LL = view.FindViewById<LinearLayout>(Resource.Id.PEnergie);
            RelativeLayout.LayoutParams LLparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.MatchParent);
            LL.LayoutParameters = LLparam;
            LL.Orientation = Orientation.Vertical;

            var text = view.FindViewById<TextView>(Resource.Id.EForce);
            RelativeLayout.LayoutParams txtparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            txtparam.AddRule(LayoutRules.AlignParentRight);
            txtparam.AddRule(LayoutRules.CenterInParent);
            text.LayoutParameters = txtparam;
            text.Text = item.Staerke.ToString();
            //text.Gravity = GravityFlags.Center;

            var textST = view.FindViewById<TextView>(Resource.Id.EQuelle);
            var txtSTparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtSTparam.Gravity = GravityFlags.Center;
            txtSTparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.ListMarginLeft, 0, 0, 0);
            textST.LayoutParameters = txtSTparam;
            textST.Text =  item.Name;
            //textST.Gravity = GravityFlags.Center;

            return view;
        }
    }
}