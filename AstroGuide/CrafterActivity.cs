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
    class CrafterActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.craft);

            var craf = CraftingTest.FindCrafter(Intent.GetStringExtra("Crafter"));

            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            if (craf.Name.Length < 16)
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            }
            else if (craf.Name.Length < 25)
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementL);
            }
            else
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            }

            //TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBText.Text = craf.Name;

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
            TBImageLeft.LayoutParameters = ImageparamLeft; TBImageLeft.Click += (o, e) =>
            {
                base.OnBackPressed();
            };




            var RLCVerwendung = FindViewById<RelativeLayout>(Resource.Id.RLCVerwendung);
            var TVCVerwendung = FindViewById<TextView>(Resource.Id.TVCVerwendung);
            var RLCForschungswert = FindViewById<RelativeLayout>(Resource.Id.RLCForschungswert);
            var TVCForschungswert = FindViewById<TextView>(Resource.Id.TVCForschungswert);
            var RLCBeschreibung = FindViewById<RelativeLayout>(Resource.Id.RLCBeschreibung);
            var TVCBeschreibung = FindViewById<TextView>(Resource.Id.TVCBeschreibung);
            var RLCHerstellungsort = FindViewById<RelativeLayout>(Resource.Id.RLCHerstellungsort);
            var TVCHerstellungsort = FindViewById<TextView>(Resource.Id.TVCHerstellungsort);
            var RLCRezept = FindViewById<RelativeLayout>(Resource.Id.RLCRezept);
            var TVCRezept = FindViewById<TextView>(Resource.Id.TVCRezept);
            var RLCTyp = FindViewById<RelativeLayout>(Resource.Id.RLCTyp);
            var TVCTyp = FindViewById<TextView>(Resource.Id.TVCTyp);







            var CType = FindViewById<TextView>(Resource.Id.CraftType).Text = "Crafter";

            if (craf.Rezept.Count > 0)
            {
                var LHer = FindViewById<ListView>(Resource.Id.CLHerstellung);
                LHer.Adapter = new AddRezept(this, craf.Rezept);
                LHer.LayoutParameters.Height = craf.Rezept.Count * Einstellungen.AdapterSpaceCalc;
                LHer.ItemClick += (o, e) =>
                {
                    var item = LHer.Adapter as AddRezept;
                    var ress = item[e.Position];

                    Intent intent = new Intent(this, typeof(ResActivity));
                    intent.PutExtra("Ressource", ress.Name);
                    this.StartActivity(intent);
                };
            }
            else
            {
                FindViewById<RelativeLayout>(Resource.Id.RLCRezept).Visibility = ViewStates.Gone;
            }
            


            if(craf.Hersteller.Name != craf.Name)
            {
                var txtHersteller = FindViewById<TextView>(Resource.Id.CraftHerstellung);
                txtHersteller.Text = craf.Hersteller.Name;
                txtHersteller.Click += (o, e) =>
                {
                    Intent intent = new Intent(this, typeof(CrafterActivity));
                    intent.PutExtra("Crafter", craf.Hersteller.Name);
                    this.StartActivity(intent);
                };

                var imgHersteller = FindViewById<ImageView>(Resource.Id.CraftHerstellungImage);
                imgHersteller.SetImageResource(craf.Hersteller.Image);
                imgHersteller.Click += (o, e) =>
                {
                    Intent intent = new Intent(this, typeof(CrafterActivity));
                    intent.PutExtra("Crafter", craf.Hersteller.Name);
                    this.StartActivity(intent);
                };
            }
            else
            {
                FindViewById<RelativeLayout>(Resource.Id.RLCHerstellungsort).Visibility = ViewStates.Gone;
            }
            

            var cName = FindViewById<TextView>(Resource.Id.CraftName);
            cName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            cName.Text = craf.Name;

            //FindViewById<ImageView>(Resource.Id.CraftBild).SetImageResource(craf.Image);

            if (craf.Images != null)
            {
                var imageholder = FindViewById<RecyclerView>(Resource.Id.CraftImageViewer);

                RecyclerView.LayoutManager mLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                imageholder.SetLayoutManager(mLayoutManager2);

                SnapHelper snapHelper = new PagerSnapHelper();
                snapHelper.AttachToRecyclerView(imageholder);

                AddRessourceImage mAdapter2 = new AddRessourceImage(this, craf.Images);
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

            FindViewById<TextView>(Resource.Id.CWert).Text = "nicht erforschbar";

            FindViewById<TextView>(Resource.Id.CraftBeschreibung).Text = craf.Description;

            var Herstellung = FindViewById<RelativeLayout>(Resource.Id.RLCVerwendung);
            Herstellung.Visibility = ViewStates.Visible;

            var test = new List<Verwendung>();
            foreach (var item in CraftingTest.Alle_craft.FindAll(x => x.Hersteller.Name == craf.Name))
            {
                test.Add(new Verwendung(item.Name,item.Image, VerwendungsTyp.Craft));
            }

            var CLHer = FindViewById<ListView>(Resource.Id.CLObjekte);
            CLHer.Adapter = new AddVerwendung(this, test);
            CLHer.LayoutParameters.Height = (test.Count * Einstellungen.AdapterSpaceCalc);
            CLHer.ItemClick += (o, e) =>
            {
                var item = CLHer.Adapter as AddVerwendung;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CraftActivity));
                intent.PutExtra("Craft", ress.Name);
                this.StartActivity(intent);
            };

            var CraftLL = FindViewById<LinearLayout>(Resource.Id.CraftContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            //para.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, Einstellungen.TextSizeListOffset / Einstellungen.PageMarginTop, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            CraftLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}