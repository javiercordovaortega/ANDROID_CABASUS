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
    public class Horsefollow_list : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_horsesfollow);
            var lista_horses = FindViewById<ListView>(Resource.Id.list_hoses);
            var listacaballos = new List<Caballos>();
            for (int i = 0; i < 10; i++)
            {
                listacaballos.Add(new Caballos {
                    nombre = "caballo " + i
                });
            }
            lista_horses.Adapter = new Adaptadores.Adaptadores_horsesfollow(this, listacaballos);
        }
    }
}