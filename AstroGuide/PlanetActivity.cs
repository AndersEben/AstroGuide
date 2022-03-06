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
    class PlanetActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.planet);

            var plan = PlanetenTest.FindPlanet(Intent.GetStringExtra("Planet"));

            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBText.Text = plan.Name;

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

            var pName = FindViewById<TextView>(Resource.Id.PlanetName);
            pName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            pName.Text = plan.Name;

            if (plan.Images != null)
            {
                var imageholder = FindViewById<RecyclerView>(Resource.Id.PlanteImageViewer);

                RecyclerView.LayoutManager mLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                imageholder.SetLayoutManager(mLayoutManager2);

                SnapHelper snapHelper = new PagerSnapHelper();
                snapHelper.AttachToRecyclerView(imageholder);

                AddRessourceImage mAdapter2 = new AddRessourceImage(this, plan.Images);
                mAdapter2.onItemClick += (o, e) =>
                {
                    var ad = new AndroidX.AppCompat.App.AlertDialog.Builder(this).Create();
                    ad.SetView(LayoutInflater.Inflate(Resource.Layout.imagePopup, null, false));

                    ad.Show();
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }


            FindViewById<TextView>(Resource.Id.PlanetType).Text = plan.Typ;
            FindViewById<TextView>(Resource.Id.PlanetSize).Text = plan.Groesse.ToString();
            FindViewById<TextView>(Resource.Id.PlanetDiff).Text = plan.Schwierigkeitsgrad.ToString();

            var penergie = FindViewById<ListView>(Resource.Id.PlanetEnergiequellen);
            penergie.Adapter = new AddEnergie(this, plan.Energieqiellen);
            penergie.LayoutParameters.Height = (plan.Energieqiellen.Count * Einstellungen.ListItemHeight + Einstellungen.PlanetOffset);

            var pressource = FindViewById<ListView>(Resource.Id.PlanetRessourcen);
            pressource.Adapter = new AddRessource(this, plan.Ress);
            pressource.LayoutParameters.Height = plan.Ress.Count * Einstellungen.AdapterSpaceCalc;
            pressource.ItemClick += (o,e) =>
            {
                var item = pressource.Adapter as AddRessource;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", ress.Ress.Name);
                this.StartActivity(intent);
            };

            var ppflanze = FindViewById<ListView>(Resource.Id.PlanetPflanzen);
            ppflanze.Adapter = new AddPflanzen(this, plan.Pflanzen);
            ppflanze.LayoutParameters.Height = (plan.Pflanzen.Count * Einstellungen.ListItemHeight + Einstellungen.PlanetOffset);
            ppflanze.ItemClick += (o, e) =>
            {
                var item = ppflanze.Adapter as AddPflanzen;
                var plant = item[e.Position];

                Intent intent = new Intent(this, typeof(PflanzeActivity));
                intent.PutExtra("Pflanze", plant.Name);
                this.StartActivity(intent);
            };

            var portalRess = FindViewById<TextView>(Resource.Id.PlanetPortalRes);
            portalRess.Text = plan.PortalElement.Name;
            portalRess.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };

            var portalRessImage = FindViewById<ImageView>(Resource.Id.PlanetPortalResImage);
            portalRessImage.SetImageResource(plan.PortalElement.Image);
            portalRessImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };


            var Galastro = FindViewById<TextView>(Resource.Id.PlanetGalatrop);
            Galastro.Text = plan.Galastro.Name;
            Galastro.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

            var GalastroImage = FindViewById<ImageView>(Resource.Id.PlanetGalatropImage);
            GalastroImage.SetImageResource(plan.Galastro.Icon);
            GalastroImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

            var PlanetLL = FindViewById<LinearLayout>(Resource.Id.PlanetContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.MatchParent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            PlanetLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}