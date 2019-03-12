using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using CABASUS.Modelos;
using CABASUS.Actividades;
namespace CABASUS.Fragments
{
    public class Fragment_Chat : Fragment
    {
        ListView ListaAlertas;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View Vista = inflater.Inflate(Resource.Layout.layout_chat, container, false);
            ListaAlertas = Vista.FindViewById<ListView>(Resource.Id.lst_chats);
            List<usuarios> usuarios = new List<usuarios>();
            for (int i = 0; i < 10; i++)
            {
                usuarios.Add(new usuarios { id_usuario = i.ToString() });
            }
            ListaAlertas.Adapter = new Adaptadores.Adapter_chat(Activity, usuarios);

            Vista.FindViewById<TextView>(Resource.Id.btncreate).Click += delegate
            {
                Activity.StartActivity(typeof(Nuevo_chat));
            };
            return Vista;
        }
    }
}