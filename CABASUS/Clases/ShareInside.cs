using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Firebase.Iid;
using Java.Lang;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using SQLite;
using CABASUS.Modelos;
namespace CABASUS.Clases
{
    public class ShareInside
    {
        public bool HayConexion()
        {
            string CheckUrl = "https://www.google.com/";
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;
            }
            catch (WebException ex)
            {
                // Console.WriteLine (".....no connection..." + ex.ToString ());
                return false;
            }
        }

        public void Guardar_Email_Contrasena(string email, string contrasena)
        {
            var guardartoken = new ConsultarEmail();
            guardartoken.email = email;
            guardartoken.contrasena = contrasena;
            var serializador = new XmlSerializer(typeof(ConsultarEmail));
            var Escritura = new StreamWriter(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ConsultarEmail.xml"));
            serializador.Serialize(Escritura, guardartoken);
            Escritura.Close();
        }

        private ConsultarEmail Consultar_Email_Contrasena()
        {
            var serializador = new XmlSerializer(typeof(ConsultarEmail));
            var Lectura = new StreamReader(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ConsultarEmail.xml"));
            var datos = (ConsultarEmail)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos;
        }

        public void GuardarToken(Token tokens)
        {
            var guardartoken = new Token();
            guardartoken.token = tokens.token;
            guardartoken.expiration = tokens.expiration;
            var serializador = new XmlSerializer(typeof(Token));
            var Escritura = new StreamWriter(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Token.xml"));
            serializador.Serialize(Escritura, guardartoken);
            Escritura.Close();
        }

        private string ConsultarExpiracion()
        {
            var serializador = new XmlSerializer(typeof(Token));
            var Lectura = new StreamReader(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Token.xml"));
            var datos = (Token)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos.expiration;
        }

        public async Task<string> ConsultarToken()
        {
            try
            {
                var expiracion = Convert.ToDateTime(ConsultarExpiracion());
                var fechaactual = DateTime.Now;
                if (fechaactual >= expiracion)
                {
                    login log = new login()
                    {
                        usuario = Consultar_Email_Contrasena().email,
                        contrasena = Consultar_Email_Contrasena().contrasena,
                        TokenFB = "dsfsdf",
                        SO = "Android",
                        id_dispositivo = Build.Serial
                    };
                    if (HayConexion())
                    {
                        await LogearUsuario(log);
                    }
                }
                var serializador = new XmlSerializer(typeof(Token));
                var Lectura = new StreamReader(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Token.xml"));
                var datos = (Token)serializador.Deserialize(Lectura);
                Lectura.Close();
                return datos.token;
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        public async Task<string> LogearUsuario(login log)
        {
            var contenido = "";
            try
            {
                if (HayConexion())
                {
                    string url = "http://192.168.1.74:5001/api/account/Login";
                    var json = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");
                    HttpClient cliente = new HttpClient();
                    cliente.Timeout = TimeSpan.FromSeconds(20);
                    var respuesta = await cliente.PostAsync(url, json);
                    respuesta.EnsureSuccessStatusCode();
                    if (respuesta.IsSuccessStatusCode)
                    {
                        contenido = await respuesta.Content.ReadAsStringAsync();
                        var cont = JsonConvert.DeserializeObject<Token>(contenido);
                        var datosusuario = JsonConvert.DeserializeObject<usuarios>(contenido);
                        datosusuario.contrasena = log.contrasena;
                        Guardar_DatosUsuario(datosusuario);
                        GuardarToken(cont);
                        Guardar_Email_Contrasena(log.usuario, log.contrasena);
                        return "Logeado";
                    }
                    else
                    {
                        return await respuesta.Content.ReadAsStringAsync();
                    }
                }
                else
                    return "No hay conexion";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public void Guardar_DatosUsuario(usuarios user)
        {
            var guardarususario = new usuarios();
            guardarususario.id_usuario = user.id_usuario;
            guardarususario.email = user.email;
            guardarususario.contrasena = user.contrasena;
            guardarususario.fecha_nacimiento = user.fecha_nacimiento;
            guardarususario.nombre = user.nombre;
            guardarususario.foto = user.foto;
            var serializador = new XmlSerializer(typeof(usuarios));
            var Escritura = new StreamWriter(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatosUsuario.xml"));
            serializador.Serialize(Escritura, guardarususario);
            Escritura.Close();
        }

        public usuarios Consultar_DatosUsuario()
        {
            var serializador = new XmlSerializer(typeof(usuarios));
            var Lectura = new StreamReader(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatosUsuario.xml"));
            var datos = (usuarios)serializador.Deserialize(Lectura);
            Lectura.Close();
            return datos;
        }

        public async Task<string> GenerarTokenFirebase()
        {
            HttpClient Clientelogin = new HttpClient(new System.Net.Http.HttpClientHandler());
            await Task.Delay(1000);
            string token = FirebaseInstanceId.Instance.Token;
            await Task.Delay(3000);
            Log.Debug("tag", token);
            return token;
        }

        public void CopyDocuments(string FileName, string AssetsFileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = System.IO.Path.Combine(path, FileName);

            try
            {
                if (!File.Exists(dbPath))
                {
                    using (var br = new BinaryReader(Application.Context.Assets.Open(AssetsFileName)))
                    {
                        using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}