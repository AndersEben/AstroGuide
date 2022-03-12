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
    class PflanzeActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.pflanze);

            var plant = PflanzenTest.FindPLant(Intent.GetStringExtra("Pflanze"));


            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBText.Text = plant.Name;

            var TBImageRigth = FindViewById<ImageView>(Resource.Id.TBImageRight);
            FrameLayout.LayoutParams ImageparamRight = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageparamRight.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            TBImageRigth.LayoutParameters = ImageparamRight; TBImageRigth.Click += (o, e) =>
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




            var RLPfPlaneten = FindViewById<RelativeLayout>(Resource.Id.RLPfPlaneten);
            var TVPfPlaneten = FindViewById<TextView>(Resource.Id.TVPfPlaneten);
            var RLPfBeschreibung = FindViewById<RelativeLayout>(Resource.Id.RLPfBeschreibung);
            var TVPfBeschreibung = FindViewById<TextView>(Resource.Id.TVPfBeschreibung);
            var RLPfTyp = FindViewById<RelativeLayout>(Resource.Id.RLPfTyp);
            var TVPfTyp = FindViewById<TextView>(Resource.Id.TVPfTyp);








            var pName = FindViewById<TextView>(Resource.Id.PflanzeName);
            pName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            pName.Text = plant.Name;

            FindViewById<ImageView>(Resource.Id.PflanzeBild).SetImageResource(plant.Image);
            FindViewById<TextView>(Resource.Id.PflanzeType).Text = plant.Typ.ToString();
            FindViewById<TextView>(Resource.Id.PflanzeBeschreibung).Text = plant.Beschreibung;

            var planeten = PlanetenTest.Alle_Planeten.FindAll(x => x.Pflanzen.FindAll(y => y.Name == plant.Name).Count > 0);

            var pfPlanetenList = FindViewById<ListView>(Resource.Id.PflanzePlaneten);
            pfPlanetenList.Adapter = new AddVorkommen(this, planeten);
            pfPlanetenList.LayoutParameters.Height = planeten.Count * Einstellungen.AdapterSpaceCalc;
            pfPlanetenList.ItemClick += (o, e) =>
            {
                var item = pfPlanetenList.Adapter as AddVorkommen;
                var plan = item[e.Position];

                Intent intent = new Intent(this, typeof(PlanetActivity));
                intent.PutExtra("Planet", plan.Name);
                this.StartActivity(intent);
            };

            if (plant.Images != null)
            {
                var imageholder = FindViewById<RecyclerView>(Resource.Id.PflanzeImageViewer);

                RecyclerView.LayoutManager mLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                imageholder.SetLayoutManager(mLayoutManager2);

                SnapHelper snapHelper = new PagerSnapHelper();
                snapHelper.AttachToRecyclerView(imageholder);

                AddRessourceImage mAdapter2 = new AddRessourceImage(this, plant.Images);
                mAdapter2.onItemClick += (o, e) =>
                {
                    var ad = new AndroidX.AppCompat.App.AlertDialog.Builder(this).Create();
                    ad.SetView(LayoutInflater.Inflate(Resource.Layout.imagePopup, null, false));

                    ad.Show();
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }

            var PflanzeLL = FindViewById<LinearLayout>(Resource.Id.PflanzeContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            //para.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            PflanzeLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}