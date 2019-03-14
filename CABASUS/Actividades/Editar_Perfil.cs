using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Refractored.Controls;
using Uri = Android.Net.Uri;

namespace CABASUS.Actividades
{
    [Activity(Label = "Editar_Perfil", WindowSoftInputMode = SoftInput.AdjustPan)]
    public class Editar_Perfil : Activity
    {
        CircleImageView Foto;
        Uri RutaArchivo;
        FileStream streamArchivo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_editarperfil);
            Foto = FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.btnFoto);
            Foto.Click += delegate {
                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, RutaArchivo);
                StartActivityForResult(intent, 1, savedInstanceState);
            };
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                RutaArchivo = Android.Net.Uri.Parse(System.IO.Path.Combine
                                (System.Environment.GetFolderPath
                                    (System.Environment.SpecialFolder.Personal), "CABASUS.jpg"));

                streamArchivo = new FileStream(System.IO.Path.Combine(System.Environment.GetFolderPath
                                                            (System.Environment.SpecialFolder.Personal),
                                                            "CABASUS.jpg"), FileMode.Create);
                var bitmapImage = (Bitmap)data.Extras.Get("data");
                bitmapImage.Compress(Bitmap.CompressFormat.Jpeg, 100, streamArchivo);
                streamArchivo.Close();
                Foto.SetImageBitmap(bitmapImage);
                GC.Collect();
            }
        }
    }
}