using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;

namespace CABASUS
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            throw new System.NotImplementedException();
        }
        //Conexion con git
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
        }
    }
}