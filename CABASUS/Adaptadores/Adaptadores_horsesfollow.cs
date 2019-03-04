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
using CABASUS.Actividades;

namespace CABASUS.Adaptadores
{
  public  class Adaptadores_horsesfollow : BaseAdapter<Caballos>
    {

        Activity context;
        private List<Caballos> caballos;

        public Adaptadores_horsesfollow(Activity context, List<Caballos> _caballos)
        {
            this.context = context;
            caballos = _caballos;
        }
        
        public override Caballos this[int position] { get { return caballos[position]; } }

        public override int Count { get { return caballos.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = caballos[position];
            View view = convertView;
            view = context.LayoutInflater.Inflate(Resource.Layout.layout_itemhorsesfollow, null);
            view.FindViewById<TextView>(Resource.Id.txthorsename).Text = item.nombre;
            view.FindViewById<LinearLayout>(Resource.Id.layout_horses).Click += delegate
            {
                context.StartActivity(typeof(Horses_Diary));
            };
            return view;
        }


    }

}