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
    public class ResActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.ressource);

            var res = MaterialTest.FindRessource(Intent.GetStringExtra("Ressource"));

            var TBText = FindViewById<TextView>(Resource.Id.TBTextCenter);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBText.Text = res.Name;

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




            var RLRTyp = FindViewById<RelativeLayout>(Resource.Id.RLRTyp);
            var TVRTyp = FindViewById<TextView>(Resource.Id.TVRTyp);
            var RLRVorkommen = FindViewById<RelativeLayout>(Resource.Id.RLRVorkommen);
            var TVRVorkommen = FindViewById<TextView>(Resource.Id.TVRVorkommen);
            var RLRRezept = FindViewById<RelativeLayout>(Resource.Id.RLRRezept);
            var TVRRezept = FindViewById<TextView>(Resource.Id.TVRRezept);
            var RLRHerstellungsort = FindViewById<RelativeLayout>(Resource.Id.RLRHerstellungsort);
            var TVRHerstellungsort = FindViewById<TextView>(Resource.Id.TVRHerstellungsort);
            var RLRForschungswert = FindViewById<RelativeLayout>(Resource.Id.RLRForschungswert);
            var TVRForschungswert = FindViewById<TextView>(Resource.Id.TVRForschungswert);
            var RLRTauschwert = FindViewById<RelativeLayout>(Resource.Id.RLRTauschwert);
            var TVRTauschwert = FindViewById<TextView>(Resource.Id.TVRTauschwert);
            var RLRTrade = FindViewById<RelativeLayout>(Resource.Id.RLRTrade);
            var TVRTrade = FindViewById<TextView>(Resource.Id.TVRTrade);
            var RLRVerwendung = FindViewById<RelativeLayout>(Resource.Id.RLRVerwendung);
            var TVRVerwendung = FindViewById<TextView>(Resource.Id.TVRVerwendung);






            if (res.Type != ResType.naturalResource && res.Type != ResType.naturalMineral && res.Type != ResType.atmoRessource && res.Type != ResType.sonstigeRessource)
            {
                FindViewById<TextView>(Resource.Id.TVRVorkommen).Text = "Rezept :";

                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddRezept(this, res.Rezept);
                LVork.LayoutParameters.Height = res.Rezept.Count * Einstellungen.AdapterSpaceCalc;
                LVork.ItemClick += (o, e) =>
                {
                    var item = LVork.Adapter as AddRezept;
                    var ress = item[e.Position];

                    Intent intent = new Intent(this, typeof(ResActivity));
                    intent.PutExtra("Ressource", ress.Name);
                    this.StartActivity(intent);
                };


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

                FindViewById<RelativeLayout>(Resource.Id.RLRHerstellungsort).Visibility = ViewStates.Visible;

            }
            else if(res.Type == ResType.atmoRessource)
            {
                var planets = PlanetenTest.Alle_Planeten.FindAll(x => x.Ress.FindAll(y => y.Ress.Name == res.Name).Count > 0);

                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddVorkommen(this, planets);
                LVork.LayoutParameters.Height = planets.Count * Einstellungen.AdapterSpaceCalc;
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
                LHer.LayoutParameters.Height = res.Rezept.Count * Einstellungen.AdapterSpaceCalc;
                LHer.ItemClick += (o, e) =>
                {
                    var item = LHer.Adapter as AddRezept;
                    var ress = item[e.Position];

                    Intent intent = new Intent(this, typeof(ResActivity));
                    intent.PutExtra("Ressource", ress.Name);
                    this.StartActivity(intent);
                };


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

                FindViewById<RelativeLayout>(Resource.Id.RLRHerstellungsort).Visibility = ViewStates.Visible;
                FindViewById<RelativeLayout>(Resource.Id.RLRRezept).Visibility = ViewStates.Visible;

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

                FindViewById<RelativeLayout>(Resource.Id.RLRVorkommen).Visibility = ViewStates.Gone;
                FindViewById<RelativeLayout>(Resource.Id.RLRVerwendung).Visibility = ViewStates.Gone;
                FindViewById<RelativeLayout>(Resource.Id.RLRHerstellungsort).Visibility = ViewStates.Visible;
                FindViewById<RelativeLayout>(Resource.Id.RLRRezept).Visibility = ViewStates.Gone;

            }
            else
            {
                var planets = PlanetenTest.Alle_Planeten.FindAll(x => x.Ress.FindAll(y => y.Ress.Name == res.Name).Count > 0);
                
                var LVork = FindViewById<ListView>(Resource.Id.RLVorkommen);
                LVork.Adapter = new AddVorkommen(this, planets);
                LVork.LayoutParameters.Height = planets.Count * Einstellungen.AdapterSpaceCalc;
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
                LL.LayoutParameters.Height = (test.Count * Einstellungen.AdapterSpaceCalc);
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
                FindViewById<RelativeLayout>(Resource.Id.RLRVerwendung).Visibility = ViewStates.Gone;
            }


            if(test2.Count > 0)
            {
                FindViewById<TextView>(Resource.Id.RTWert).Text = test2[0].Ratio + " " + test2[0].Obj.ToString() + " / Unit";
                FindViewById<RelativeLayout>(Resource.Id.RLRTauschwert).Visibility = ViewStates.Visible;
            }

            var rName = FindViewById<TextView>(Resource.Id.RessourceName);
            rName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            rName.Text = res.Name;


            FindViewById<ImageView>(Resource.Id.RessourceBild).SetImageResource(res.Image);
            FindViewById<TextView>(Resource.Id.RessourceType).Text = Funktionen.ShowEnumLabel(res.Type);

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

                FindViewById<RelativeLayout>(Resource.Id.RLRTrade).Visibility = ViewStates.Visible;

                var LVork = FindViewById<ListView>(Resource.Id.RLTrade);
                LVork.Adapter = new AddTrade(this, res.Tauschen);
                LVork.LayoutParameters.Height = res.Tauschen.Count * Einstellungen.AdapterSpaceCalc;
                LVork.ItemClick += (o, e) =>
                {
                    var verw = LVork.Adapter as AddTrade;
                    var type = verw.GetTrade(e.Position);

                    switch (type.Typ)
                    {
                        case VerwendungsTyp.Ressource:
                            Intent intent = new Intent(this, typeof(ResActivity));
                            intent.PutExtra("Ressource", type.Item);
                            this.StartActivity(intent);
                            break;
                        case VerwendungsTyp.Crafter:
                            Intent intent2 = new Intent(this, typeof(CrafterActivity));
                            intent2.PutExtra("Crafter", type.Item);
                            this.StartActivity(intent2);
                            break;
                        case VerwendungsTyp.Craft:
                            Intent intent3 = new Intent(this, typeof(CraftActivity));
                            intent3.PutExtra("Craft", type.Item);
                            this.StartActivity(intent3);
                            break;
                        case VerwendungsTyp.Pflanze:
                            break;
                        case VerwendungsTyp.Galastropode:
                            break;
                        case VerwendungsTyp.Planet:
                            break;
                    }
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
                mAdapter2.onItemClick += (o, e) =>
                {
                    var ad = new AndroidX.AppCompat.App.AlertDialog.Builder(this).Create();
                    ad.SetView(LayoutInflater.Inflate(Resource.Layout.imagePopup, null, false));

                    ad.Show();
                    ad.FindViewById<ImageView>(Resource.Id.PopupImageView).SetImageResource(e.Picture);
                };

                imageholder.SetAdapter(mAdapter2);
            }

            var RessLL = FindViewById<LinearLayout>(Resource.Id.RessourceContentLL);
            ScrollView.LayoutParams para = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.MatchParent);
            para.SetMargins(Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0, Einstellungen.TextSizeListOffset / Einstellungen.PageMargin, 0);
            RessLL.LayoutParameters = para;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}