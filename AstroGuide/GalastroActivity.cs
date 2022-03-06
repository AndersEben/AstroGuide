using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
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
    class GalastroActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.galastropod);

            var galast = GalastropodenTest.FindGalast(Intent.GetStringExtra("Galastropode"));

            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBText.Text = galast.Name;

            var TBImageRigth = FindViewById<ImageView>(Resource.Id.TBImageRight);
            FrameLayout.LayoutParams ImageparamRight = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageparamRight.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            TBImageRigth.Click += (o, e) =>
            {
                FinishAffinity();
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            };

            var TBImageLeft = FindViewById<ImageView>(Resource.Id.TBImageLeft);
            FrameLayout.LayoutParams ImageparamLeft = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageparamLeft.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
            TBImageLeft.LayoutParameters = ImageparamLeft;
            TBImageLeft.Click += (o, e) =>
            {
                base.OnBackPressed();
            };

            var gName = FindViewById<TextView>(Resource.Id.GalastName);
            gName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            gName.Text = galast.Name;
            

            FindViewById<ImageView>(Resource.Id.GalastroBild).SetImageResource(galast.Image);

            var verwendung = new List<Verwendung>();
            verwendung.Add(new Verwendung(galast.Terrarium.Element1.Name, galast.Terrarium.Element1.Image, VerwendungsTyp.Ressource));
            verwendung.Add(new Verwendung(galast.Terrarium.Element2.Name, galast.Terrarium.Element2.Image, VerwendungsTyp.Ressource));
            verwendung.Add(new Verwendung(galast.Terrarium.Samen.Name, galast.Terrarium.Samen.Image, VerwendungsTyp.Pflanze));


            var LV = FindViewById<ListView>(Resource.Id.GalastTerrarium);
            LV.Adapter = new AddVerwendung(this, verwendung);
            LV.LayoutParameters.Height = (verwendung.Count * Einstellungen.AdapterSpaceCalc);
            LV.ItemClick += (o, e) =>
                {
                    var verw = LV.Adapter as AddVerwendung;
                    var type = verw.GetVerwendung(e.Position);

                    switch (type.Typ)
                    {
                        case VerwendungsTyp.Ressource:
                            Intent intent = new Intent(this, typeof(ResActivity));
                            intent.PutExtra("Ressource", type.Name);
                            this.StartActivity(intent);
                            break;
                        case VerwendungsTyp.Crafter:
                            Intent intent2 = new Intent(this, typeof(CrafterActivity));
                            intent2.PutExtra("Crafter", type.Name);
                            this.StartActivity(intent2);
                            break;
                        case VerwendungsTyp.Craft:
                            Intent intent3 = new Intent(this, typeof(CraftActivity));
                            intent3.PutExtra("Craft", type.Name);
                            this.StartActivity(intent3);
                            break;
                        case VerwendungsTyp.Galastropode:
                            Intent intent4 = new Intent(this, typeof(GalastroActivity));
                            intent4.PutExtra("Galastropode", type.Name);
                            this.StartActivity(intent4);
                            break;
                        case VerwendungsTyp.Pflanze:
                            Intent intent5 = new Intent(this, typeof(PflanzeActivity));
                            intent5.PutExtra("Pflanze", type.Name);
                            this.StartActivity(intent5);
                            break;
                    }
                };

            FindViewById<TextView>(Resource.Id.GalastFood).Text = galast.Food.Name + " Samen";
            FindViewById<TextView>(Resource.Id.GalastBeschreibung).Text = galast.Beschreibung;
            FindViewById<TextView>(Resource.Id.GalastBuff).Text = galast.Buff;

            if (galast.Images != null)
            {
                var imageholder = FindViewById<RecyclerView>(Resource.Id.GalasImageViewer);

                RecyclerView.LayoutManager mLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                imageholder.SetLayoutManager(mLayoutManager2);

                SnapHelper snapHelper = new PagerSnapHelper();
                snapHelper.AttachToRecyclerView(imageholder);

                AddRessourceImage mAdapter2 = new AddRessourceImage(this, galast.Images);
                mAdapter2.onItemClick += (o, e) =>
                {
                    var ad = new AndroidX.AppCompat.App.AlertDialog.Builder(this).Create();
                    ad.SetView(LayoutInflater.Inflate(Resource.Layout.imagePopup, null, false));
                    
                    ad.Show();
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }

            var GalastLL = FindViewById<LinearLayout>(Resource.Id.GalastroContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            //para.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            GalastLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}