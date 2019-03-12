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
using CABASUS.Modelos;

namespace CABASUS.Actividades
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class Alerts_history : Activity
    {
        ListView ListaAlertas;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_alerts);
            ListaAlertas = FindViewById<ListView>(Resource.Id.lst_alertas);
            List<usuarios> usuarios = new List<usuarios>();
            usuarios.Add(new usuarios { id_usuario="1"});
            usuarios.Add(new usuarios { id_usuario = "1" });
            ListaAlertas.Adapter = new Adaptadores.Adapter_AlertsHistory(this, usuarios);
        }
    }
}