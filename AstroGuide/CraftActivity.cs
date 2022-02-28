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


            var txtHersteller = FindViewById<TextView>(Resource.Id.CraftHerstellung);
            txtHersteller.Text = craf.Hersteller.Name;
            txtHersteller.Click += (o, e) =>
            {
                    Intent intent = new Intent(this, typeof(CrafterActivity));
                    intent.PutExtra("Crafter", craf.Hersteller.Name);
                    this.StartActivity(intent);
            };

            FindViewById<TextView>(Resource.Id.CraftType).Text = Funktionen.ShowEnumLabel(craf.Typ);

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

            FindViewById<TextView>(Resource.Id.CraftBeschreibung).Text = craf.Description;
            FindViewById<TextView>(Resource.Id.CWert).Text = craf.Forschungskosten.ToString() + " Bytes";

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}