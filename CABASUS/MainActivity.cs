﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using CABASUS.Clases;
using CABASUS.Fragments;

namespace CABASUS
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        FragmentTransaction transaccion;
        Fragment_perfil fragment_perfil = new Fragment_perfil();
        Fragment_Ajustes fragment_ajustes = new Fragment_Ajustes();
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.nav_profile)
            {
                transaccion = FragmentManager.BeginTransaction();
                transaccion.Add(Resource.Id.FrameContent, fragment_perfil, "Perfil");
                transaccion.Show(fragment_perfil);
                transaccion.Commit();
            }
            else if (id == Resource.Id.nav_activities)
            {

            }
            else if (id == Resource.Id.nav_calendar)
            {
            }
            else if (id == Resource.Id.nav_chat)
            {

            }
            else if (id == Resource.Id.nav_settings)
            {
                transaccion = FragmentManager.BeginTransaction();
                transaccion.Add(Resource.Id.FrameContent, fragment_ajustes, "Ajustes");
                transaccion.Show(fragment_ajustes);
                transaccion.Commit();
            }
            else if (id == Resource.Id.nav_horses)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Navigation);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            var btnMenu = FindViewById<ImageView>(Resource.Id.imgMenu);
            btnMenu.Click += delegate {
                drawer.OpenDrawer(GravityCompat.Start);
            };
            var headerView = navigationView.GetHeaderView(0);
            var nombre = headerView.FindViewById<TextView>(Resource.Id.txtusuario);
            nombre.Text = new ShareInside().Consultar_DatosUsuario().nombre;
        }
    }
}