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
    class CraftActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.craft);

            var craf = CraftingTest.FindCraft(Intent.GetStringExtra("Craft"));

            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            if(craf.Name.Length < 16)
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            }
            else if(craf.Name.Length < 25)
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementL);
            }
            else
            {
                TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            }
            
            TBText.Text = craf.Name;

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


            FindViewById<TextView>(Resource.Id.CraftType).Text = Funktionen.ShowEnumLabel(craf.Typ);

            var cName = FindViewById<TextView>(Resource.Id.CraftName);
            cName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            cName.Text = craf.Name;



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
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }

            FindViewById<TextView>(Resource.Id.CraftBeschreibung).Text = craf.Description;


            if(craf.Forschungskosten <= 0)
            {
                FindViewById<TextView>(Resource.Id.CWert).Text = "nicht erforschbar";
            }
            else
            {
                FindViewById<TextView>(Resource.Id.CWert).Text = craf.Forschungskosten.ToString() + " Bytes";
            }

            var CraftLL = FindViewById<LinearLayout>(Resource.Id.CraftContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            //para.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            CraftLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}