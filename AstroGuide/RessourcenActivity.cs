using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AstroGuide.Scripts;
using AstroGuide.Scripts.CustViews;
using AstroGuide.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class RessourcenActivity : AppCompatActivity
    {
        private RelativeLayout SetRelativeLayout(string name)
        {

            RelativeLayout RL = new RelativeLayout(this);
            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent,
                                                                                RelativeLayout.LayoutParams.WrapContent);
            RL.LayoutParameters = param;

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Horizontal;

            TextView txtv = new TextView(this);
            txtv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                ViewGroup.LayoutParams.WrapContent);
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 13);
            txtv.Text = name;
            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);

            LL.AddView(txtv);
            RL.AddView(LL);

            ImageView imgv = new ImageView(this);
            RelativeLayout.LayoutParams parm;
            parm = new RelativeLayout.LayoutParams(50, 50);
            parm.AddRule(LayoutRules.AlignParentRight);
            parm.AddRule(LayoutRules.CenterVertical);
            imgv.LayoutParameters = parm;
            imgv.SetImageResource(Resource.Drawable.plus);

            RL.AddView(imgv);

            return RL;
        }

        private LinearLayout SetLinearLayout(RelativeLayout RL, List<Ressource> test)
        {
            int pixel = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            LL.SetPadding(Einstellungen.LL_AddE_padding_left, Einstellungen.LL_AddE_padding_top, Einstellungen.LL_AddE_padding_right, Einstellungen.LL_AddE_padding_bottem);

            ListView lv = new ListView(this);
            lv.Adapter = new AddRessourcen(this, test);

            lv.NestedScrollingEnabled = true;
            lv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                LinearLayout.LayoutParams.WrapContent);

            LL.AddView(lv);
            lv.ItemClick += (o, e) =>
            {
                var item = lv.Adapter as AddRessourcen;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", ress.Name);
                this.StartActivity(intent);
            };

            lv.LayoutParameters.Height = (test.Count * Einstellungen.ListPlanetHeight);
            lv.Visibility = ViewStates.Gone;

            RL.Click += (o, e) =>
            {
                if (lv.Visibility == ViewStates.Gone)
                {
                    lv.Visibility = ViewStates.Visible;
                }
                else
                {
                    lv.Visibility = ViewStates.Gone;
                }
            };

            return LL;
        }



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;

            TextView txtv = new TextView(this);
            var param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            param.SetMargins(Einstellungen.LL_E1_margin_left, Einstellungen.LL_E1_margin_top, Einstellungen.LL_E1_margin_right, Einstellungen.LL_E1_margin_bottem);
            txtv.LayoutParameters = param;
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 10);
            
            txtv.Text = "Ressourcen";
            
            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);
            txtv.Gravity = GravityFlags.Center;

            LL.AddView(txtv);

            ScrollView SV = new ScrollView(this);
            LinearLayout.LayoutParams SVparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SV.LayoutParameters = SVparam;

            LL.AddView(SV);

            LinearLayout SVLL = new LinearLayout(this);
            LinearLayout.LayoutParams SVLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SVLL.LayoutParameters = SVLparam;
            SVLL.Orientation = Orientation.Vertical;

            SV.AddView(SVLL);


            SetContentView(LL);

            List<LinearLayout> Layouts = new List<LinearLayout>();
            foreach (string item in Enum.GetNames(typeof(ResType)))
            {
                var selCrafts = MaterialTest.Alle_Ressourcen.FindAll(x => x.Type.ToString() == item);
                var Rlayout = SetRelativeLayout(Funktionen.ShowEnumLabel((ResType)System.Enum.Parse(typeof(ResType), item)));
                var layout = SetLinearLayout(Rlayout, selCrafts);

                SVLL.AddView(Rlayout);
                SVLL.AddView(layout);
                Layouts.Add(layout);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}