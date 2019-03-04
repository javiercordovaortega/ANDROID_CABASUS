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
using CABASUS.Clases;
using CABASUS.Actividades;

namespace CABASUS.Fragments
{
    public class Fragment_perfil : Fragment
    {
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View Vista = inflater.Inflate(Resource.Layout.layout_perfil, container, false);
            var email = Vista.FindViewById<TextView>(Resource.Id.txtemail);
            var nombre = Vista.FindViewById<TextView>(Resource.Id.txtusername);
            var btnfriends = Vista.FindViewById<TextView>(Resource.Id.btnfriends);
            var btnhorses = Vista.FindViewById<TextView>(Resource.Id.btnhorses);
            email.Text = new ShareInside().Consultar_DatosUsuario().email;
            nombre.Text= new ShareInside().Consultar_DatosUsuario().nombre;
            btnfriends.Click += delegate 
            {
              Activity.StartActivity(typeof(Friends_List));
            };
            btnhorses.Click += delegate
            {
                Activity.StartActivity(typeof(Horsefollow_list));
            };
            return Vista;
        }
    }
}