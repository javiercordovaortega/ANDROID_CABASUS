using System;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using System.IO;
using Uri = Android.Net.Uri;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CABASUS.Modelos;
using CABASUS.Clases;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CABASUS.Actividades
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class Registrar_Usuario : Activity
    {
        ProgressBar progress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_registrarusuario);
            var login = FindViewById<TextView>(Resource.Id.btnlogin);
            var signin = FindViewById<TextView>(Resource.Id.btnsignin);
            var username = FindViewById<EditText>(Resource.Id.txtusername);
            var email = FindViewById<EditText>(Resource.Id.txtemail);
            var password = FindViewById<EditText>(Resource.Id.txtpw);
            var confirmpassword = FindViewById<EditText>(Resource.Id.txtconfirmpw);
            progress = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progress.IndeterminateDrawable.SetColorFilter(Android.Graphics.Color.Rgb(203, 30, 30), Android.Graphics.PorterDuff.Mode.Multiply);

            login.Click += delegate
            {
                try { StartActivity(typeof(Iniciar_Sesion)); }
                catch (Exception) { }
            };
            signin.Click += async delegate
            {
                var contenido = "";
                try
                {
                    if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(confirmpassword.Text))
                    {

                        Toast.MakeText(this, "Empty field", ToastLength.Short).Show();
                    }
                    else if (!validar_email(email))
                    {
                        Toast.MakeText(this, Resource.String.This_email_is_not_valid, ToastLength.Short).Show();
                    }
                    else if (password.Text != confirmpassword.Text)
                    {
                        Toast.MakeText(this, "Password doesn't match", ToastLength.Short).Show();
                    }
                    else
                    {
                        progress.Visibility = Android.Views.ViewStates.Visible;
                        Window.AddFlags(Android.Views.WindowManagerFlags.NotTouchable);
                        string url = "http://192.168.1.74:5001/api/Account/registrar";
                        string formato = "application/json";
                        usuarios usuarios = new usuarios()
                        {
                            nombre = username.Text,
                            email = email.Text,
                            contrasena = password.Text,
                            id_dispositivo = Build.Serial,
                            SO = "Android",
                            tokenFB = await new ShareInside().GenerarTokenFirebase(),
                            fecha_nacimiento = ""
                        };
                        var json = new StringContent(JsonConvert.SerializeObject(usuarios), Encoding.UTF8, formato);
                        HttpClient cliente = new HttpClient();
                        cliente.Timeout = TimeSpan.FromSeconds(20);
                        if (new ShareInside().HayConexion())
                        {
                            var respuesta = await cliente.PostAsync(url, json);
                            contenido = await respuesta.Content.ReadAsStringAsync();
                            respuesta.EnsureSuccessStatusCode();
                            if (respuesta.IsSuccessStatusCode)
                            {
                                var cont = JsonConvert.DeserializeObject<Token>(contenido);
                                usuarios.foto = "";
                                usuarios.id_usuario = Obtener_idusuario(cont.token);
                                new ShareInside().GuardarToken(cont);
                                new ShareInside().Guardar_DatosUsuario(usuarios);
                                new ShareInside().Guardar_Email_Contrasena(email.Text, password.Text);
                                progress.Visibility = Android.Views.ViewStates.Invisible;
                                Window.ClearFlags(Android.Views.WindowManagerFlags.NotTouchable);
                                StartActivity(typeof(MainActivity));
                            }
                            else
                            {
                                Toast.MakeText(this, contenido, ToastLength.Short).Show();
                                progress.Visibility = Android.Views.ViewStates.Invisible;
                                Window.ClearFlags(Android.Views.WindowManagerFlags.NotTouchable);
                            }
                        }
                        else
                            Toast.MakeText(this, Resource.String.No_internet_connection, ToastLength.Short).Show();
                    }
                }
                catch (Exception) { }
            };
            
        }
        private bool validar_email(EditText txt_email)
        {
            Regex email = new Regex(@"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                           @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                           @")+" +
                                           @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");
            if (email.IsMatch(txt_email.Text))
                return true;
            else
                return false;
        }

        private string Obtener_idusuario(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "id").Value;
            return jti;
        }
    }
}