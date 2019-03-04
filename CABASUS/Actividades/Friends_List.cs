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
    public class Friends_List : Activity
    {
        List<usuarios> lista_usuarios= new List<usuarios>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_friendslist);
            var btnregresar = FindViewById<ImageView>(Resource.Id.btnvolver);
            var lista = FindViewById<ListView>(Resource.Id.list_frieds);
            lista_usuarios.Add(new usuarios
            {
                nombre = "",
                id_usuario = "00000000"
            });

            lista_usuarios.Add(new usuarios {
                nombre="Mary",
                id_usuario="53s4f56"
            });
            lista_usuarios.Add(new usuarios
            {
                nombre = "Franklyn",
                id_usuario = "53s4+6f56"
            });
            lista_usuarios.Add(new usuarios
            {
                nombre = "",
                id_usuario = "11111111"
            });

            lista_usuarios.Add(new usuarios
            {
                nombre = "Chuy",
                id_usuario = "53as4f56"
            });
            lista_usuarios.Add(new usuarios
            {
                nombre = "Javo",
                id_usuario = "53453s4+6f56"
            });
            lista_usuarios.Add(new usuarios
            {
                nombre = "Alex",
                id_usuario = "53435s4f56"
            });
            lista_usuarios.Add(new usuarios
            {
                nombre = "Cami",
                id_usuario = "53s4+6fpkñl56"
            });
            lista.Adapter = new Adaptadores.Adapter_friends(this,lista_usuarios);

            btnregresar.Click += delegate
            {
                
            };
        }
    }
}