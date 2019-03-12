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
using CABASUS.Fragments;
using CABASUS.Modelos;

namespace CABASUS.Adaptadores
{
    public class Adaptador_Caballos : BaseAdapter<Caballos>
    {
        private List<Caballos> caballos;
        MainActivity mainActivity;
        List<string> Selecccion = new List<string>();
        public Adaptador_Caballos(List<Caballos> caballos, MainActivity mainActivity)
        {
            this.caballos = caballos;
            this.mainActivity = mainActivity;
        }

        public override Caballos this[int position] { get { return caballos[position]; } }

        public override int Count { get { return caballos.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = caballos[position];
            View view = convertView;
            if (item.compartido)
            {
                view = mainActivity.LayoutInflater.Inflate(Resource.Layout.RowCaballoCompartido, null);
                view.FindViewById<TextView>(Resource.Id.txtNombreCaballoCompartido).Text = item.nombre;
                view.LongClick += delegate
                {
                    if (Selecccion.Count == 1)
                    {
                        if (Selecccion.Contains(item.id))
                        {
                            view.SetBackgroundColor(new Color(255, 255, 255));
                            Selecccion.RemoveAll(x => x == item.id);
                        }
                    }
                    else
                    {
                        view.SetBackgroundColor(new Color(209, 209, 209, 106));
                        Selecccion.Add(item.id);
                    }
                };
                view.Click += delegate
                {
                    if (Selecccion.Count != 0)
                    {
                        if (Selecccion.Contains(item.id))
                        {
                            view.SetBackgroundColor(new Color(255, 255, 255));
                            Selecccion.RemoveAll(x => x == item.id);
                        }
                    }
                };
            }
            else
            {
                view = mainActivity.LayoutInflater.Inflate(Resource.Layout.RowCaballoPropio, null);
                view.FindViewById<TextView>(Resource.Id.txtNombreCaballo).Text = item.nombre;
                view.LongClick += delegate
                {
                    if (Selecccion.Count == 1)
                    {
                        if (Selecccion.Contains(item.id))
                        {
                            view.SetBackgroundColor(new Color(255, 255, 255));
                            Selecccion.RemoveAll(x => x == item.id);
                        }
                    }
                    else
                    {
                        view.SetBackgroundColor(new Color(209, 209, 209, 106));
                        Selecccion.Add(item.id);
                    }
                };
                view.Click += delegate
                {
                    if (Selecccion.Count != 0)
                    {
                        if (Selecccion.Contains(item.id))
                        {
                            view.SetBackgroundColor(new Color(255, 255, 255));
                            Selecccion.RemoveAll(x => x == item.id);
                        }
                    }
                    else
                    {
                        try
                        {
                            Intent intent = new Intent(view.Context, typeof(Actividades.Perfil_Caballo));
                            view.Context.StartActivity(intent);
                        }
                        catch (Exception ex)
                        {
                            var a = "";
                        }
                    }
                };
            }

            if (Selecccion.Contains(item.id))
            {
                view.SetBackgroundColor(new Color(209, 209, 209, 106));
            }

            return view;
        }
    }
}