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
using CABASUS.Actividades;
using CABASUS.Modelos;

namespace CABASUS.Adaptadores
{
    public class Diarios_Perfil_Caballos : BaseAdapter<Diarios>
    {
        private Perfil_Caballo perfil_Caballo;
        private List<Diarios> listaDiarios;

        public Diarios_Perfil_Caballos(Perfil_Caballo perfil_Caballo, List<Diarios> listaDiarios)
        {
            this.perfil_Caballo = perfil_Caballo;
            this.listaDiarios = listaDiarios;
        }

        public override Diarios this[int position] { get { return listaDiarios[position]; } }

        public override int Count { get { return listaDiarios.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = listaDiarios[position];
            View view = convertView;
           // view = perfil_Caballo.LayoutInflater.Inflate(Resource.Layout.layout_Diario_Texto, null);
            //view = perfil_Caballo.LayoutInflater.Inflate(Resource.Layout.layout_Diario_Texto, null);
            return view;
        }
    }
}