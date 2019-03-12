using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using CABASUS.Adaptadores;
using CABASUS.Modelos;

namespace CABASUS.Actividades
{
    [Activity(Label = "Perfil_Caballo")]
    public class Perfil_Caballo : AppCompatActivity
    {
        ListView CaballosCompartidosCon;
        ListView DiariosPerfilCaballo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Perfil_Caballo);
            CaballosCompartidosCon = FindViewById<ListView>(Resource.Id.lstSharedwith);
            
            List<UsuariosPendientes> ListaUsuarios = new List<UsuariosPendientes>() {
                new UsuariosPendientes(){
                    nombre = "juan",
                    Pending = true
                },
                new UsuariosPendientes(){
                    nombre = "jose",
                    Pending = false
                },
                new UsuariosPendientes(){
                    nombre = "julian",
                    Pending = true
                },
                new UsuariosPendientes(){
                    nombre = "Jonatan",
                    Pending = false
                },
                new UsuariosPendientes(){
                    nombre = "juan",
                    Pending = true
                },
            };

            CaballosCompartidosCon.LayoutParameters.Height = ((int)DpToPixels(this, 70) + ((int)DpToPixels(this, 5) * 3)) * ListaUsuarios.Count;
            
            CaballosCompartidosCon.Adapter = new Caballo_Compartido_Con(this, ListaUsuarios);

            List<Diarios> ListaDiarios = new List<Diarios>() {
                new Diarios(){},
                new Diarios(){},
                new Diarios(){},
                new Diarios(){},
                new Diarios(){}
            };

            DiariosPerfilCaballo = FindViewById<ListView>(Resource.Id.lstDiariosPerfilCaballo);
            DiariosPerfilCaballo.LayoutParameters.Height = ((int)DpToPixels(this, 70) + ((int)DpToPixels(this, 5))) * ListaDiarios.Count;

            DiariosPerfilCaballo.Adapter = new Diarios_Perfil_Caballos(this, ListaDiarios);

            var scrollView = FindViewById<ScrollView>(Resource.Id.ScrollPerfilCaballos);
            scrollView.SmoothScrollTo(0, 0);
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}