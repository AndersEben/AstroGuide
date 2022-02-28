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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class CrafterActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.craft);

            var craf = CraftingTest.FindCrafter(Intent.GetStringExtra("Crafter"));

            if(craf.Rezept != null)
            {
                var LHer = FindViewById<ListView>(Resource.Id.CLHerstellung);
                LHer.Adapter = new AddRezept(this, craf.Rezept);
                LHer.LayoutParameters.Height = (craf.Rezept.Count * Einstellungen.ListItemHeight);
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
                FindViewById<TextView>(Resource.Id.RLCraftRezept).Visibility = ViewStates.Gone;
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
                FindViewById<TextView>(Resource.Id.RLCraftHerstellungsort).Visibility = ViewStates.Gone;
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
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);

            }

            FindViewById<TextView>(Resource.Id.CWert).Text = "nicht erforschbar";

            FindViewById<TextView>(Resource.Id.CraftBeschreibung).Text = craf.Description;

            var Herstellung = FindViewById<RelativeLayout>(Resource.Id.LayoutCrafterHerstellung);
            Herstellung.Visibility = ViewStates.Visible;

            var test = new List<Verwendung>();
            foreach (var item in CraftingTest.Alle_craft.FindAll(x => x.Hersteller.Name == craf.Name))
            {
                test.Add(new Verwendung(item.Name,item.Image, VerwendungsTyp.Craft));
            }

            var CLHer = FindViewById<ListView>(Resource.Id.CLObjekte);
            CLHer.Adapter = new AddVerwendung(this, test);
            CLHer.LayoutParameters.Height = (test.Count * Einstellungen.ListItemHeight);
            CLHer.ItemClick += (o, e) =>
            {
                var item = CLHer.Adapter as AddVerwendung;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(CraftActivity));
                intent.PutExtra("Craft", ress.Name);
                this.StartActivity(intent);
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}