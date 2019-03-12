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
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class Nuevo_chat : Activity
    {
        ListView ListaAlertas;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_nuevochat);
            ListaAlertas = FindViewById<ListView>(Resource.Id.lst_nuevochat);
            List<usuarios> usuarios = new List<usuarios>();
            usuarios.Add(new usuarios { id_usuario = "1" });
            usuarios.Add(new usuarios { id_usuario = "1" });
            ListaAlertas.Adapter = new Adaptadores.Adapter_NuevoChat(this, usuarios);
        }
    }
}