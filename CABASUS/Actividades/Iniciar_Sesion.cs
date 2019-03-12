using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CABASUS.Clases;
using CABASUS.Modelos;

namespace CABASUS.Actividades
{
    
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar"/*, MainLauncher = true*/)]
    public class Iniciar_Sesion : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_iniciarsesion);
            var btnregistrar = FindViewById<TextView>(Resource.Id.btncreate);
            var btnlogin = FindViewById<TextView>(Resource.Id.btnlogin);
            var correo = FindViewById<EditText>(Resource.Id.txtusername);
            var contrasena = FindViewById<EditText>(Resource.Id.txtpw);
            var progress = FindViewById<ProgressBar>(Resource.Id.progressBar);
            if (File.Exists(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Token.xml")))
            {
                StartActivity(typeof(MainActivity));
            }
            correo.Text = "ami@g.com";
            contrasena.Text = "12345";
            progress.IndeterminateDrawable.SetColorFilter(Android.Graphics.Color.Rgb(203, 30, 30), Android.Graphics.PorterDuff.Mode.Multiply);

            btnregistrar.Click += delegate 
            {
                try { StartActivity(typeof(Registrar_Usuario)); }
                catch (Exception) { }
            };
            btnlogin.Click += async delegate
            {
                try
                {
                    //progress.Visibility = Android.Views.ViewStates.Visible;
                    //Window.AddFlags(Android.Views.WindowManagerFlags.NotTouchable);

                    //if (!string.IsNullOrWhiteSpace(contrasena.Text) && !string.IsNullOrWhiteSpace(correo.Text))
                    //{
                    //    login log = new login()
                    //    {
                    //        usuario = correo.Text,
                    //        contrasena = contrasena.Text,
                    //        id_dispositivo = Build.Serial,
                    //        SO = "Android",
                    //        TokenFB = await new ShareInside().GenerarTokenFirebase()
                    //    };
                    //    var mensaje = await new ShareInside().LogearUsuario(log);
                    //    if (mensaje == "Logeado")
                    //    {
                            StartActivity(typeof(MainActivity));
                            Finish();
                    //    }
                    //    else
                    //        Toast.MakeText(this, mensaje, ToastLength.Short).Show();
                    //}
                    //else
                    //    Toast.MakeText(this, Resource.String.There_are_empty_fields, ToastLength.Short).Show();
                    //progress.Visibility = Android.Views.ViewStates.Invisible;
                    //Window.ClearFlags(Android.Views.WindowManagerFlags.NotTouchable);
                }
                catch (Exception)
                {
                    progress.Visibility = Android.Views.ViewStates.Invisible;
                    Window.ClearFlags(Android.Views.WindowManagerFlags.NotTouchable);
                }
            };
        }
    }
}