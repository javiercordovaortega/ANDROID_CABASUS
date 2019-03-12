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
using CABASUS.Actividades;

namespace CABASUS.Fragments
{
    public class Fragment_Salud : Fragment
    {
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View Vista = inflater.Inflate(Resource.Layout.layout_salud, container, false);
            Vista.FindViewById<TextView>(Resource.Id.btncreate).Click += delegate {
                Activity.StartActivity(typeof(Alerts_history));
            };
            return Vista;
        }
    }
}