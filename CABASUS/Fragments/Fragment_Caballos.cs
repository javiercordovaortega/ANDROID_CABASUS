using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CABASUS.Modelos;
using System.Linq;
using Android.Content;

namespace CABASUS.Fragments
{
    public class Fragment_Caballos : Fragment
    {
        ListView ListViewCaballos;
        public MainActivity mainActivity;

        public Fragment_Caballos(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            List<Caballos> caballos = new List<Caballos>();
            caballos.Add(new Caballos() {
                id = "1",
                nombre = "Caballo propio",
                compartido = false
            });
            caballos.Add(new Caballos()
            {
                id = "2",
                nombre = "Caballo compartido",
                compartido = true
            });
            caballos.Add(new Caballos()
            {
                id = "3",
                nombre = "Caballo propio",
                compartido = false
            });
            caballos.Add(new Caballos()
            {
                id = "4",
                nombre = "Caballo propio",
                compartido = false
            });
            caballos.Add(new Caballos()
            {
                id = "5",
                nombre = "Caballo compartido",
                compartido = true
            });
            caballos.Add(new Caballos()
            {
                id = "6",
                nombre = "Caballo propio",
                compartido = false
            });
            caballos.Add(new Caballos()
            {
                id = "7",
                nombre = "Caballo propio",
                compartido = false
            });
            caballos.Add(new Caballos()
            {
                id = "8",
                nombre = "Caballo compartido",
                compartido = true
            });

            caballos = caballos.OrderBy(x => x.compartido.Equals(true)).ThenBy(x => x.compartido.Equals(false)).ToList();
            ListViewCaballos.Adapter = new Adaptadores.Adaptador_Caballos(caballos, mainActivity);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View Vista = inflater.Inflate(Resource.Layout.LayoutFragmentHorses, container, false);

            ListViewCaballos = Vista.FindViewById<ListView>(Resource.Id.lstCaballos);

            Vista.FindViewById<LinearLayout>(Resource.Id.bntNuevoCaballo).Click += delegate {
                Intent intent = new Intent(mainActivity, typeof(Actividades.AgregarCaballo));
                mainActivity.StartActivity(intent);
            };

            return Vista;
        }
    }
}