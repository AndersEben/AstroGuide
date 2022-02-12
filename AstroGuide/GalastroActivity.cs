﻿using Android.App;
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
    class GalastroActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.galastropod);

            var galast = GalastropodenTest.FindGalast(Intent.GetStringExtra("Galastropode"));

            FindViewById<TextView>(Resource.Id.GalastName).Text = galast.Name;
            FindViewById<ImageView>(Resource.Id.GalastroBild).SetImageResource(galast.Image);

            var verwendung = new List<Verwendung>();
            verwendung.Add(new Verwendung(galast.Terrarium.Element1.Name, galast.Terrarium.Element1.Image, VerwendungsTyp.Ressource));
            verwendung.Add(new Verwendung(galast.Terrarium.Element2.Name, galast.Terrarium.Element2.Image, VerwendungsTyp.Ressource));
            verwendung.Add(new Verwendung(galast.Terrarium.Samen.Name, galast.Terrarium.Samen.Image, VerwendungsTyp.Pflanze));


            var LV = FindViewById<ListView>(Resource.Id.RLVerwendung);
            LV.Adapter = new AddVerwendung(this, verwendung);
            LV.LayoutParameters.Height = (verwendung.Count * Einstellungen.ListItemHeight);
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

            

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}