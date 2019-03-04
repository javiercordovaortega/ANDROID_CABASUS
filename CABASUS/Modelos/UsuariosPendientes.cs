using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CABASUS.Modelos
{
    public class UsuariosPendientes
    {
        public string id_usuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public string foto { get; set; }
        public string id_dispositivo { get; set; }
        public string SO { get; set; }
        public string tokenFB { get; set; }
        public string fecha_nacimiento { get; set; }
        public bool Pending { get; set; }
    }
}