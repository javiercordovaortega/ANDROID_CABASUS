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
    public class Caballos
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string genero { get; set; }
        public string raza { get; set; }
        public double altura { get; set; }
        public double peso { get; set; }
        public double avena { get; set; }
        public bool compartido { get; set; }
    }
}