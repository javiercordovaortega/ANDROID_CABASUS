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

namespace CABASUS.Adaptadores
{
    class Adapter_friends : BaseAdapter<usuarios>
    {

        Activity context;
        private List<usuarios> usuarios;

        public Adapter_friends(Activity context,List<usuarios> _usuarios)
        {
            this.context = context;
            usuarios = _usuarios;
        }
        public override usuarios this[int position] { get { return usuarios[position]; } }

        public override int Count { get { return usuarios.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = usuarios[position];
            View view = convertView;
            if (item.id_usuario == "00000000")
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.layout_title, null);
            }
            else if (item.id_usuario == "11111111")
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.layout_title, null);
                view.FindViewById<TextView>(Resource.Id.txttitulo).Text = "Firends";
                
            }
            else
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.layout_itemrequest, null);

            }
            return view;
        }

    }

}