using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using CABASUS.Modelos;
using Android.Widget;

namespace CABASUS.Adaptadores
{
    class Adapter_NuevoChat : BaseAdapter<usuarios>
    {

        Activity context;
        private List<usuarios> usuarios;

        public Adapter_NuevoChat(Activity context, List<usuarios> _usuarios)
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
            view = context.LayoutInflater.Inflate(Resource.Layout.layout_itemnuevochat, null);

            return view;
        }

    }
}