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
    public class ResActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.ressource);

            var res = MaterialTest.FindRessource(Intent.GetStringExtra("Ressource"));

            if(res.Type != ResType.naturalResource && res.Type != ResType.naturalMineral && res.Type != ResType.atmoRessource && res.Type != ResType.sonstigeRessource)
            {
                FindViewById<TextView>(Resource.Id.LabelVorkommen).Text = "Rezept :";

                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddRezept(this, res.Rezept);
                LVork.LayoutParameters.Height = (res.Rezept.Count * Einstellungen.ListItemHeight);
                LVork.ItemClick += (o, e) =>
                {
                    var item = LVork.Adapter as AddRezept;
                    var ress = item[e.Position];

                    Intent intent = new Intent(this, typeof(ResActivity));
                    intent.PutExtra("Ressource", ress.Name);
                    this.StartActivity(intent);
                };

                //FindViewById<TextView>(Resource.Id.RHerstellung).Text = res.Hersteller.ToString();

                var txtHersteller = FindViewById<TextView>(Resource.Id.RHerstellung);
                txtHersteller.Text = res.Hersteller.ToString();
                txtHersteller.Click += (o, e) =>
                {
                    if (res.Hersteller != Herstellung.None)
                    {
                        Intent intent = new Intent(this, typeof(CraftActivity));
                        intent.PutExtra("Craft", res.Hersteller.ToString());
                        this.StartActivity(intent);
                    }
                };

                FindViewById<RelativeLayout>(Resource.Id.LayoutHerstellung).Visibility = ViewStates.Visible;

            }
            else if(res.Type == ResType.atmoRessource)
            {
                var planets = PlanetenTest.Alle_Planeten.FindAll(x => x.Ress.FindAll(y => y.Ress.Name == res.Name).Count > 0);

                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddVorkommen(this, planets);
                LVork.LayoutParameters.Height = (planets.Count * Einstellungen.ListItemHeight);
                LVork.ItemClick += (o, e) =>
                {
                    var item = LVork.Adapter as AddVorkommen;
                    var plan = item[e.Position];

                    Intent intent = new Intent(this, typeof(PlanetActivity));
                    intent.PutExtra("Planet", plan.Name);
                    this.StartActivity(intent);
                };

                var LHer = FindViewById<ListView>(Resource.Id.RLHerstellung);
                LHer.Adapter = new AddRezept(this, res.Rezept);
                LHer.LayoutParameters.Height = (res.Rezept.Count * Einstellungen.ListItemHeight);
                LHer.ItemClick += (o, e) =>
                {
                    var item = LHer.Adapter as AddRezept;
                    var ress = item[e.Position];

                    Intent intent = new Intent(this, typeof(ResActivity));
                    intent.PutExtra("Ressource", ress.Name);
                    this.StartActivity(intent);
                };

                //FindViewById<TextView>(Resource.Id.RHerstellung).Text = res.Hersteller.ToString();

                var txtHersteller = FindViewById<TextView>(Resource.Id.RHerstellung);
                txtHersteller.Text = res.Hersteller.ToString();
                txtHersteller.Click += (o, e) =>
                {
                    if (res.Hersteller != Herstellung.None)
                    {
                        Intent intent = new Intent(this, typeof(CraftActivity));
                        intent.PutExtra("Craft", res.Hersteller.ToString());
                        this.StartActivity(intent);
                    }
                };

                FindViewById<RelativeLayout>(Resource.Id.LayoutHerstellung).Visibility = ViewStates.Visible;
                FindViewById<RelativeLayout>(Resource.Id.LayoutLHerstellung).Visibility = ViewStates.Visible;

            }
            else if (res.Type == ResType.sonstigeRessource)
            {

                var txtHersteller = FindViewById<TextView>(Resource.Id.RHerstellung);
                txtHersteller.Text = res.Hersteller.ToString();
                txtHersteller.Click += (o, e) =>
                {
                    if(res.Hersteller != Herstellung.None)
                    {
                        Intent intent = new Intent(this, typeof(CraftActivity));
                        intent.PutExtra("Craft", res.Hersteller.ToString());
                        this.StartActivity(intent);
                    }
                };

                FindViewById<RelativeLayout>(Resource.Id.LayoutLVorkommen).Visibility = ViewStates.Gone;
                FindViewById<RelativeLayout>(Resource.Id.LayoutVerwendung).Visibility = ViewStates.Gone;
                FindViewById<RelativeLayout>(Resource.Id.LayoutHerstellung).Visibility = ViewStates.Visible;
                FindViewById<RelativeLayout>(Resource.Id.LayoutLHerstellung).Visibility = ViewStates.Gone;

            }
            else
            {
                var planets = PlanetenTest.Alle_Planeten.FindAll(x => x.Ress.FindAll(y => y.Ress.Name == res.Name).Count > 0);
                
                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddVorkommen(this, planets);
                LVork.LayoutParameters.Height = (planets.Count * Einstellungen.ListItemHeight);
                LVork.ItemClick += (o, e) =>
                {
                    var item = LVork.Adapter as AddVorkommen;
                    var plan = item[e.Position];

                    Intent intent = new Intent(this, typeof(PlanetActivity));
                    intent.PutExtra("Planet", plan.Name);
                    this.StartActivity(intent);
                };
            }


            var test = new List<Verwendung>();

            foreach (var item in MaterialTest.Alle_nat_Ressourcen.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name,item.Image, VerwendungsTyp.Ressource));
            } 
            foreach (var item in MaterialTest.Alle_raff_Ressourcen.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Ressource));
            } 
            foreach (var item in MaterialTest.Alle_komp_Ressourcen.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Ressource));
            } 
            foreach (var item in MaterialTest.Alle_gase_Ressourcen.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Ressource));
            }
            foreach (var item in MaterialTest.Alle_sonstigen_Ressourcen.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Ressource));
            }

            foreach (var item in CraftingTest.Alle_crafter.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image,VerwendungsTyp.Crafter));
            }
            foreach (var item in CraftingTest.Alle_craft.FindAll(x => x.Rezept.FindAll(y => y.Name == res.Name).Count > 0))
            {
                test.Add(new Verwendung(item.Name, item.Image, VerwendungsTyp.Craft));
            }


            var test2 = new List<Trade>();

            foreach (var item in MaterialTest.Alle_nat_Ressourcen.FindAll(x => x.Tauschen.FindAll(y => y.Item == res.Name).Count > 0))
            {
                test2.Add(new Trade(item.Name, item.Image, item.Tauschen.Find(x => x.Item == res.Name).Ratio, item.TauschObj));
            }
            foreach (var item in MaterialTest.Alle_raff_Ressourcen.FindAll(x => x.Tauschen.FindAll(y => y.Item == res.Name).Count > 0))
            {
                test2.Add(new Trade(item.Name, item.Image, item.Tauschen.Find(x => x.Item == res.Name).Ratio, item.TauschObj));
            }
            foreach (var item in MaterialTest.Alle_komp_Ressourcen.FindAll(x => x.Tauschen.FindAll(y => y.Item == res.Name).Count > 0))
            {
                test2.Add(new Trade(item.Name, item.Image, item.Tauschen.Find(x => x.Item == res.Name).Ratio, item.TauschObj));
            }
            foreach (var item in MaterialTest.Alle_gase_Ressourcen.FindAll(x => x.Tauschen.FindAll(y => y.Item == res.Name).Count > 0))
            {
                test2.Add(new Trade(item.Name, item.Image, item.Tauschen.Find(x => x.Item == res.Name).Ratio, item.TauschObj));
            }
            foreach (var item in MaterialTest.Alle_sonstigen_Ressourcen.FindAll(x => x.Tauschen.FindAll(y => y.Item == res.Name).Count > 0))
            {
                test2.Add(new Trade(item.Name, item.Image, item.Tauschen.Find(x => x.Item == res.Name).Ratio, item.TauschObj));
            }


            if(test.Count > 0)
            {
                var LL = FindViewById<ListView>(Resource.Id.RLVerwendung);
                LL.Adapter = new AddVerwendung(this, test);
                LL.LayoutParameters.Height = (test.Count * Einstellungen.ListItemHeight);
                LL.ItemClick += (o, e) =>
                {
                    var verw = LL.Adapter as AddVerwendung;
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
                            break;
                        case VerwendungsTyp.Pflanze:
                            break;
                    }
                };
            }
            else
            {
                FindViewById<RelativeLayout>(Resource.Id.LayoutVerwendung).Visibility = ViewStates.Gone;
            }


            if(test2.Count > 0)
            {
                FindViewById<TextView>(Resource.Id.RTWert).Text = test2[0].Ratio + " " + test2[0].Obj.ToString() + " / Unit";
                FindViewById<RelativeLayout>(Resource.Id.LayoutTauschwert).Visibility = ViewStates.Visible;
            }

            FindViewById<TextView>(Resource.Id.RessourceName).Text = res.Name;
            FindViewById<ImageView>(Resource.Id.RessourceBild).SetImageResource(res.Image);
            //FindViewById<TextView>(Resource.Id.RessourceType).Text = res.Type.ToString();
            FindViewById<TextView>(Resource.Id.RessourceType).Text = Funktionen.ShowEnumLabel(res.Type);
            //FindViewById<TextView>(Resource.Id.RVorkommen).Text = "Sylva";

            if(res.Forschungswert <= 0)
            {
                FindViewById<TextView>(Resource.Id.RWert).Text = "nicht erforschbar";
            }
            else
            {
                FindViewById<TextView>(Resource.Id.RWert).Text = res.Forschungswert.ToString() + " Bytes";
            }

            if(res.Tauschen.Count > 0)
            {

                FindViewById<RelativeLayout>(Resource.Id.LayoutTrade).Visibility = ViewStates.Visible;

                var LVork = FindViewById<ListView>(Resource.Id.RLTrade);
                LVork.Adapter = new AddTrade(this, res.Tauschen);
                LVork.LayoutParameters.Height = (res.Tauschen.Count * Einstellungen.ListItemHeight);
                LVork.ItemClick += (o, e) =>
                {

                };
            }

            if(res.Images != null)
            {
                var imageholder = FindViewById<RecyclerView>(Resource.Id.ResImageViewer);

                RecyclerView.LayoutManager mLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                imageholder.SetLayoutManager(mLayoutManager2);

                SnapHelper snapHelper = new PagerSnapHelper();
                snapHelper.AttachToRecyclerView(imageholder);

                AddRessourceImage mAdapter2 = new AddRessourceImage(this, res.Images);
                imageholder.SetAdapter(mAdapter2);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}