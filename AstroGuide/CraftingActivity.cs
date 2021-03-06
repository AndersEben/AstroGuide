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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.CustTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class CraftingActivity : AppCompatActivity
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
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
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

        private LinearLayout SetLinearLayout(string text, List<Craft> test)
        {
            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            LL.Visibility = ViewStates.Gone;
            LL.SetPadding(Einstellungen.LL_AddE_padding_left, Einstellungen.LL_AddE_padding_top, Einstellungen.LL_AddE_padding_right, Einstellungen.LL_AddE_padding_bottem);

            TextView txtv = new TextView(this);
            txtv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                ViewGroup.LayoutParams.WrapContent);
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementL);

            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);
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

            lv.LayoutParameters.Height = test.Count * Einstellungen.BigAdapterSpaceCalc;
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

            //SetContentView(Resource.Layout.crafting);
            //FindViewById<TextView>(Resource.Id.TitleCrafting).SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / 10);

            LinearLayout TBLL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            TBLL.LayoutParameters = Lparam;
            TBLL.Orientation = Orientation.Vertical;

            Toolbar ToolBar = new Toolbar(this);
            var toolbarparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ToolBar.Background = Resources.GetDrawable(Resource.Color.colorPrimary);
            ToolBar.LayoutParameters = toolbarparam;
            ToolBar.SetContentInsetsAbsolute(10, 10);
            ToolBar.ContentInsetStartWithNavigation = 0;

            TBLL.AddView(ToolBar);

            FrameLayout TBFrameLayout = new FrameLayout(this);
            var FrameLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            TBFrameLayout.LayoutParameters = FrameLparam;
            ToolBar.AddView(TBFrameLayout);



            FrameLayout.LayoutParams ImageparamLeft = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageView TBImageLeft = new ImageView(this);
            ImageparamLeft.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
            TBImageLeft.LayoutParameters = ImageparamLeft;
            TBImageLeft.SetPadding(0, 0, 25, 0);
            TBImageLeft.SetImageResource(Resource.Drawable.backbutton_white);
            TBFrameLayout.AddView(TBImageLeft);
            TBImageLeft.Click += (o, e) =>
            {
                base.OnBackPressed();
            };

            FrameLayout.LayoutParams Textparam = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WrapContent, FrameLayout.LayoutParams.MatchParent);
            TextView TBText = new TextView(this);
            Textparam.Gravity = GravityFlags.Center;
            TBText.LayoutParameters = Textparam;
            TBText.SetTextColor(Android.Graphics.Color.White);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBFrameLayout.AddView(TBText);

            FrameLayout.LayoutParams ImageparamRight = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageView TBImageRight = new ImageView(this);
            ImageparamRight.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            TBImageRight.LayoutParameters = ImageparamRight;
            TBImageRight.SetPadding(0, 0, 25, 0);
            TBImageRight.SetImageResource(Resource.Drawable.homebutton_white);
            TBFrameLayout.AddView(TBImageRight);
            TBImageRight.Click += (o, e) =>
            {
                FinishAffinity();
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            };


            LinearLayout LL = new LinearLayout(this);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.MatchParent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);

            LL.LayoutParameters = para;
            LL.Orientation = Orientation.Vertical;
            TBLL.AddView(LL);

            TextView txtv = new TextView(this);
            var param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            param.SetMargins(Einstellungen.LL_E1_margin_left, Einstellungen.LL_E1_margin_top, Einstellungen.LL_E1_margin_right, Einstellungen.LL_E1_margin_bottem);
            txtv.LayoutParameters = param;
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            txtv.Visibility = ViewStates.Gone;
            txtv.Text = "Crafting";
            TBText.Text = "Crafting";

            txtv.SetPadding(Einstellungen.LL_AddE_padding_left, Einstellungen.LL_AddE_padding_top, Einstellungen.LL_AddE_padding_right, Einstellungen.LL_AddE_padding_bottem);
            txtv.Gravity = GravityFlags.Center;

            LL.AddView(txtv);

            AndroidX.Core.Widget.NestedScrollView SV = new AndroidX.Core.Widget.NestedScrollView(this);
            LinearLayout.LayoutParams SVparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SV.LayoutParameters = SVparam;

            LL.AddView(SV);

            LinearLayout CraftHolder = new LinearLayout(this);
            LinearLayout.LayoutParams SVLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SVLparam.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            CraftHolder.LayoutParameters = SVLparam;
            CraftHolder.Orientation = Orientation.Vertical;

            SV.AddView(CraftHolder);

            //var CraftHolder = FindViewById<LinearLayout>(Resource.Id.CraftHolder);
            SetContentView(TBLL);



            ListView searchlv = new ListView(this);
            searchlv.Adapter = new AddCraft(this, new List<Craft>());

            searchlv.NestedScrollingEnabled = true;
            searchlv.LayoutParameters = new LinearLayout.LayoutParams(  LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);

            searchlv.ItemClick += (o, e) =>
            {
                var item = searchlv.Adapter as AddCraft;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CraftActivity));
                intent.PutExtra("Craft", ress.Name);
                this.StartActivity(intent);
            };

            SearchView searchView = new SearchView(this);
            var LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);
            LayoutParameters.SetMargins(0, 0, 0, 45);
            searchView.LayoutParameters = LayoutParameters;
            searchView.SetIconifiedByDefault(false);


            CraftHolder.AddView(searchView);
            CraftHolder.AddView(searchlv);
            searchlv.LayoutParameters.Height = (0 * Einstellungen.ListPlanetHeight);


            #region Crafter
            var RLcrafter = SetRelativeLayout("Crafter");
            CraftHolder.AddView(RLcrafter);

            LinearLayout LLCrafter = new LinearLayout(this);
            LinearLayout.LayoutParams LCrafterparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LLCrafter.LayoutParameters = Lparam;
            LLCrafter.Orientation = Orientation.Vertical;
            LLCrafter.Visibility = ViewStates.Gone;
            LLCrafter.SetPadding(0, 0, 0, 45);

            ListView lv = new ListView(this);
            lv.Adapter = new AddCrafter(this, CraftingTest.Alle_crafter);

            lv.NestedScrollingEnabled = true;
            lv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                LinearLayout.LayoutParams.WrapContent);

            LLCrafter.AddView(lv);
            lv.ItemClick += (o, e) =>
            {
                var item = lv.Adapter as AddCrafter;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CrafterActivity));
                intent.PutExtra("Crafter", ress.Name);
                this.StartActivity(intent);
            };

            lv.LayoutParameters.Height = CraftingTest.Alle_crafter.Count * Einstellungen.BigAdapterSpaceCalc;

            CraftHolder.AddView(LLCrafter);

            RLcrafter.Click += (o, e) =>
            {
                for (int i = 0; i < RLcrafter.ChildCount; i++)
                {
                    if (RLcrafter.GetChildAt(i).GetType() == typeof(ImageView))
                    {
                        var image = (ImageView)RLcrafter.GetChildAt(i);

                        if (LLCrafter.Visibility == ViewStates.Gone)
                        {
                            image.SetImageResource(Resource.Drawable.minus);
                            LLCrafter.Visibility = ViewStates.Visible;
                        }
                        else
                        {
                            image.SetImageResource(Resource.Drawable.plus);
                            LLCrafter.Visibility = ViewStates.Gone;
                        }

                        break;
                    }
                }
            };

            #endregion


            #region Craft
            var RLcraft = SetRelativeLayout("Craft");
            CraftHolder.AddView(RLcraft);

            List<LinearLayout> Layouts = new List<LinearLayout>();
            foreach (string item in Enum.GetNames(typeof(CraftType)))
            {
                var selCrafts = CraftingTest.Alle_craft.FindAll(x => x.Typ.ToString() == item);
                var layout = SetLinearLayout(Funktionen.ShowEnumLabel((CraftType)System.Enum.Parse(typeof(CraftType),item)),selCrafts);

                CraftHolder.AddView(layout);
                Layouts.Add(layout);
            }

            RLcraft.Click += (o, e) =>
            {
                for (int i = 0; i < RLcraft.ChildCount; i++)
                {
                    if (RLcraft.GetChildAt(i).GetType() == typeof(ImageView))
                    {
                        var image = (ImageView)RLcraft.GetChildAt(i);

                        if(Layouts[0].Visibility == ViewStates.Gone)
                        {
                            image.SetImageResource(Resource.Drawable.minus);
                        }
                        else
                        {
                            image.SetImageResource(Resource.Drawable.plus);
                        }

                        break;
                    }

                }

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

            #endregion

            searchView.QueryTextChange += (o, e) =>
            {

                List<Craft> gefunden = new List<Craft>();

                if (e.NewText == "" || e.NewText == null)
                {
                    searchlv.Adapter = new AddCraft(this, gefunden);
                    searchlv.LayoutParameters.Height = gefunden.Count * Einstellungen.BigAdapterSpaceCalc;
                    RLcraft.Visibility = ViewStates.Visible;
                    RLcrafter.Visibility = ViewStates.Visible;
                }
                else
                {
                    List<Verwendung> treffer = new List<Verwendung>();

                    gefunden = CraftingTest.AllCraft().FindAll(x => x.Name.ToLower().Contains(e.NewText.ToLower()));

                    foreach (var item in gefunden)
                    {
                        treffer.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Craft));
                    }

                    searchlv.Adapter = new AddCraft(this, gefunden);
                    searchlv.LayoutParameters.Height = gefunden.Count * Einstellungen.BigAdapterSpaceCalc;

                    RLcraft.Visibility = ViewStates.Gone;
                    RLcrafter.Visibility = ViewStates.Gone;
                    LLCrafter.Visibility = ViewStates.Gone;

                    foreach (var item in Layouts)
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