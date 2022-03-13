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
            TBImageRigth.LayoutParameters = ImageparamRight; 
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



            var RLPRessourcen = FindViewById<RelativeLayout>(Resource.Id.RLPRessourcen);
            var TVPRessourcen = FindViewById<TextView>(Resource.Id.TVPRessourcen);
            var RLPPflanzen = FindViewById<RelativeLayout>(Resource.Id.RLPPflanzen);
            var TVPPflanzen = FindViewById<TextView>(Resource.Id.TVPPflanzen);
            var RLPGalstropode = FindViewById<RelativeLayout>(Resource.Id.RLPGalstropode);
            var TVPGalastropode = FindViewById<TextView>(Resource.Id.TVPGalastropode);
            var RLPPortalMRessource = FindViewById<RelativeLayout>(Resource.Id.RLPPortalMRessource);
            var TVPPortalMRessource = FindViewById<TextView>(Resource.Id.TVPPortalMRessource);
            var RLPEnergiequellen = FindViewById<RelativeLayout>(Resource.Id.RLPEnergiequellen);
            var TVPEnergiequellen = FindViewById<TextView>(Resource.Id.TVPEnergiequellen);
            var RLPSchwierigkeitsgrad = FindViewById<RelativeLayout>(Resource.Id.RLPSchwierigkeitsgrad);
            var TVPSchwierigkeitsgrad = FindViewById<TextView>(Resource.Id.TVPSchwierigkeitsgrad);
            var RLPGroesse = FindViewById<RelativeLayout>(Resource.Id.RLPGroesse);
            var TVPGroesse = FindViewById<TextView>(Resource.Id.TVPGroesse);
            var RLPTyp = FindViewById<RelativeLayout>(Resource.Id.RLPTyp);
            var TVPTyp = FindViewById<TextView>(Resource.Id.TVPTyp);






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

                    var alertLL = ad.FindViewById<LinearLayout>(Resource.Id.AlertLL);
                    var alertPara = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / 2, Einstellungen.DisplayHeight / 2);
                    alertPara.Gravity = GravityFlags.Center;
                    alertLL.LayoutParameters = alertPara;

                    var alertImage = ad.FindViewById<ImageView>(Resource.Id.PopupImageView);
                    var alertImagePara = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                    alertImagePara.Gravity = GravityFlags.Center;
                    alertImage.LayoutParameters = alertImagePara;
                    alertImage.SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }


            var PlanType = FindViewById<TextView>(Resource.Id.PlanetType);
            var planpara = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            planpara.AddRule(LayoutRules.AlignParentRight);
            planpara.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            PlanType.LayoutParameters = planpara;
            PlanType.Text = plan.Typ;


            var PlanSize = FindViewById<TextView>(Resource.Id.PlanetSize);
            var Grpara = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            Grpara.AddRule(LayoutRules.AlignParentRight);
            Grpara.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            PlanSize.LayoutParameters = Grpara;
            PlanSize.Text = plan.Groesse.ToString();

            var PlanDiff = FindViewById<TextView>(Resource.Id.PlanetDiff);
            var Diffpara = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            Diffpara.AddRule(LayoutRules.AlignParentRight);
            Diffpara.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            PlanDiff.LayoutParameters = Diffpara;
            PlanDiff.Text = plan.Schwierigkeitsgrad.ToString();

            var penergie = FindViewById<ListView>(Resource.Id.PlanetEnergiequellen);
            penergie.Adapter = new AddEnergie(this, plan.Energieqiellen);
            penergie.LayoutParameters.Height = (plan.Energieqiellen.Count * Einstellungen.AdapterSpaceCalc);

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
            


            var PMpara = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            PMpara.SetMargins(0,0,0,50);
            RLPPortalMRessource.LayoutParameters = PMpara;

            var portalRess = FindViewById<TextView>(Resource.Id.PlanetPortalRes);

            RelativeLayout.LayoutParams txtparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            txtparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigthText, 0);
            txtparam.AddRule(LayoutRules.AlignParentRight);
            txtparam.AddRule(LayoutRules.CenterInParent);
            portalRess.LayoutParameters = txtparam;

            portalRess.Text = plan.PortalElement.Name;
            portalRess.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };

            var portalRessImage = FindViewById<ImageView>(Resource.Id.PlanetPortalResImage);

            RelativeLayout.LayoutParams imgparam = new RelativeLayout.LayoutParams(Einstellungen.AdapterImgSize, Einstellungen.AdapterImgSize);
            imgparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigth, 0);
            imgparam.AddRule(LayoutRules.AlignParentRight);
            portalRessImage.LayoutParameters = imgparam;

            portalRessImage.SetImageResource(plan.PortalElement.Image);
            portalRessImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };


            var Galpara = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, Einstellungen.AdapterImgSize);
            Galpara.SetMargins(0, 0, 0, 50);
            RLPGalstropode.LayoutParameters = Galpara;

            var Galastro = FindViewById<TextView>(Resource.Id.PlanetGalatrop);

            RelativeLayout.LayoutParams Gtxtparam = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            Gtxtparam.SetMargins(0, 0, Einstellungen.TextSizeListOffset / Einstellungen.ListMarginRigthText, 0);
            Gtxtparam.AddRule(LayoutRules.AlignParentRight);
            Gtxtparam.AddRule(LayoutRules.CenterInParent);

            Galastro.LayoutParameters = Gtxtparam;
            Galastro.Text = plan.Galastro.Name;
            Galastro.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

            var GalastroImage = FindViewById<ImageView>(Resource.Id.PlanetGalatropImage);
            GalastroImage.LayoutParameters = imgparam;
            GalastroImage.SetImageResource(plan.Galastro.Icon);
            GalastroImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

            var PlanetLL = FindViewById<LinearLayout>(Resource.Id.PlanetContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.MatchParent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, Einstellungen.TextSizeListOffset / Einstellungen.PageMarginTop, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            PlanetLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}