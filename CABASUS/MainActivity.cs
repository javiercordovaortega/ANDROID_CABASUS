using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using CABASUS.Fragments;

namespace CABASUS
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public FragmentTransaction transaccion;
        Fragment_Caballos _Fragment_Caballos;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.nav_diary)
            {

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

            }
            else if (id == Resource.Id.nav_horses)
            {
                transaccion = FragmentManager.BeginTransaction();
                transaccion.Show(_Fragment_Caballos);
                transaccion.Commit();
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

            _Fragment_Caballos = new Fragment_Caballos(this);

            transaccion = FragmentManager.BeginTransaction();
            transaccion.Add(Resource.Id.FrameContent, _Fragment_Caballos, "Horses");
            transaccion.Hide(_Fragment_Caballos);
            transaccion.Commit();
        }
    }
}