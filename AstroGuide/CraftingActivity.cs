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
    class CraftingActivity : AppCompatActivity
    {


        private RelativeLayout SetRelativeLayout(string name)
        {

            int pixel = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

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
            txtv.SetPadding(pixel, pixel, pixel, pixel);

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

        private LinearLayout SetLinearLayout(string text, List<Craft> test)
        {
            int pixel = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            LL.Visibility = ViewStates.Gone;

            TextView txtv = new TextView(this);
            txtv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                ViewGroup.LayoutParams.WrapContent);
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 16);

            txtv.SetPadding(pixel, pixel, pixel, pixel);
            txtv.Text = text;

            LL.AddView(txtv);

            ListView lv = new ListView(this);
            lv.Adapter = new AddCraft(this, test);

            lv.NestedScrollingEnabled = true;
            lv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                LinearLayout.LayoutParams.WrapContent);

            LL.AddView(lv);
            lv.ItemClick += (o, e) =>
            {
                var item = lv.Adapter as AddCraft;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CraftActivity));
                intent.PutExtra("Craft", ress.Name);
                this.StartActivity(intent);
            };

            lv.LayoutParameters.Height = (test.Count * Einstellungen.ListPlanetHeight);
            lv.Visibility = ViewStates.Gone;

            txtv.Click += (o, e) =>
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

            SetContentView(Resource.Layout.crafting);
            FindViewById<TextView>(Resource.Id.TitleCrafting).SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 10);



            var ResHolder = FindViewById<LinearLayout>(Resource.Id.CraftHolder);

            int pixel = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            var RLcrafter = SetRelativeLayout("Crafter");

            ResHolder.AddView(RLcrafter);

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            LL.Visibility = ViewStates.Gone;
            LL.SetPadding(0, 0, 0, 45);

            ListView lv = new ListView(this);
            lv.Adapter = new AddCrafter(this, CraftingTest.Alle_crafter);

            lv.NestedScrollingEnabled = true;
            lv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                LinearLayout.LayoutParams.WrapContent);

            LL.AddView(lv);
            lv.ItemClick += (o, e) =>
            {
                var item = lv.Adapter as AddCrafter;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CrafterActivity));
                intent.PutExtra("Crafter", ress.Name);
                this.StartActivity(intent);
            };

            lv.LayoutParameters.Height = (CraftingTest.Alle_crafter.Count * Einstellungen.ListPlanetHeight);
            //lv.Visibility = ViewStates.Gone;

            ResHolder.AddView(LL);

            RLcrafter.Click += (o, e) =>
            {

                if (LL.Visibility == ViewStates.Gone)
                {
                    LL.Visibility = ViewStates.Visible;
                }
                else
                {
                    LL.Visibility = ViewStates.Gone;
                }
            };


            var RLcraft = SetRelativeLayout("Craft");

            ResHolder.AddView(RLcraft);

            List<LinearLayout> Layouts = new List<LinearLayout>();
            foreach (string item in Enum.GetNames(typeof(CraftType)))
            {
                var selCrafts = CraftingTest.Alle_craft.FindAll(x => x.Typ.ToString() == item);
                var layout = SetLinearLayout(Funktionen.ShowEnumLabel((CraftType)System.Enum.Parse(typeof(CraftType),item)),selCrafts);

                ResHolder.AddView(layout);
                Layouts.Add(layout);
            }

            RLcraft.Click += (o, e) =>
            {
                foreach (var item in Layouts)
                {
                    if (item.Visibility == ViewStates.Gone)
                    {
                        item.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        item.Visibility = ViewStates.Gone;
                    }
                }
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}