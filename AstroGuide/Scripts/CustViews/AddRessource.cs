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

            var RL = view.FindViewById<RelativeLayout>(Resource.Id.PRRL);
            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, Einstellungen.AdapterSize);
            RL.LayoutParameters = param;

            var texttyp = view.FindViewById<TextView>(Resource.Id.RTyp);
            var txtTparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtTparam.AddRule(LayoutRules.CenterInParent);
            txtTparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.ListMarginLeftText2, 0, 0, 0);
            texttyp.LayoutParameters = txtTparam;

            if (item.Art.ToString() != ResArt.Universale.ToString())
            {
                texttyp.Text = item.Art.ToString();
            }

            var texthaeufig = view.FindViewById<TextView>(Resource.Id.Haeufigkeit);
            var txtHparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtHparam.AddRule(LayoutRules.CenterInParent);
            txtHparam.AddRule(LayoutRules.AlignParentRight);
            txtHparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginLeft, 0);
            texthaeufig.LayoutParameters = txtHparam;

            if (item.Haeufigkeit != "Oft")
            {
                texthaeufig.Text = item.Haeufigkeit;
            }

            var text = view.FindViewById<TextView>(Resource.Id.Ressource);
            var txtparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, Einstellungen.AdapterSize);
            txtparam.Gravity = GravityFlags.CenterVertical;
            txtparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.ListMarginLeftText,0,0,0);
            text.LayoutParameters = txtparam;
            text.Text = item.Ress.Name;

            var image = view.FindViewById<ImageView>(Resource.Id.RImage);
            var imgparam  = new RelativeLayout.LayoutParams(Einstellungen.AdapterImgSize, RelativeLayout.LayoutParams.MatchParent);
            imgparam.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.ListMarginLeft,0,0,0);
            image.LayoutParameters = imgparam;
            image.SetImageResource(item.Ress.Image);
            
            return view;
        }
    }
}