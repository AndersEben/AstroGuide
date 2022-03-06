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
    class AddRessourcen : BaseAdapter<Ressource>
    {
        List<Ressource> Items;
        Activity Context;

        public AddRessourcen(Activity context, List<Ressource> items)
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
                view = Context.LayoutInflater.Inflate(Resource.Layout.ressourcen_ressource, null);

            var RL = view.FindViewById<RelativeLayout>(Resource.Id.RResRL);
            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, Einstellungen.BigAdapterSize);
            RL.LayoutParameters = param;

            var LL = view.FindViewById<LinearLayout>(Resource.Id.addText);
            RelativeLayout.LayoutParams LLparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            LL.LayoutParameters = LLparam;

            var image = view.FindViewById<ImageView>(Resource.Id.ResListBild);
            var imgparam = new LinearLayout.LayoutParams(Einstellungen.BigAdapterImgSize, LinearLayout.LayoutParams.MatchParent);
            imgparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.BigListMarginLeft, 0, 0, 0);
            image.LayoutParameters = imgparam;
            image.SetImageResource(item.Image);

            var text = view.FindViewById<TextView>(Resource.Id.ResListName);
            RelativeLayout.LayoutParams txtparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, Einstellungen.BigAdapterSize);
            txtparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.BigListMarginLeftText, 0, 0, 0);
            txtparam.AddRule(LayoutRules.CenterVertical);
            text.LayoutParameters = txtparam;

            text.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            text.Text = item.Name;

            return view;
        }
    }
}