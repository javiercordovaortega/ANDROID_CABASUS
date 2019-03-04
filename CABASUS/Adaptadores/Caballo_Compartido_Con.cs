using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CABASUS.Actividades;
using CABASUS.Modelos;

namespace CABASUS.Adaptadores
{
    public class Caballo_Compartido_Con : BaseAdapter<UsuariosPendientes>
    {
        private Perfil_Caballo perfil_Caballo;
        List<UsuariosPendientes> listaUsuarios;

        public Caballo_Compartido_Con(Perfil_Caballo perfil_Caballo, List<UsuariosPendientes> listaUsuarios)
        {
            this.perfil_Caballo = perfil_Caballo;
            this.listaUsuarios = listaUsuarios;
        }

        public override UsuariosPendientes this[int position] { get { return listaUsuarios[position]; } }

        public override int Count { get { return listaUsuarios.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = listaUsuarios[position];
            View view = convertView;
            view = perfil_Caballo.LayoutInflater.Inflate(Resource.Layout.RowSharedWith, null);
            view.FindViewById<TextView>(Resource.Id.txtNombreUsuarioSharedWith).Text = item.nombre;
            if (item.Pending)
            {
                view.SetBackgroundColor(new Color(209, 209, 209, 106));
            }
            return view;
        }
    }
}