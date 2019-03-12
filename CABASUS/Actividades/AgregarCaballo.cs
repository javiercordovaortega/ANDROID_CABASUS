using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using CABASUS.Adaptadores;
using CABASUS.Clases;
using Refractored.Controls;
using SQLite;
using static Android.App.DatePickerDialog;
using Uri = Android.Net.Uri;

namespace CABASUS.Actividades
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class AgregarCaballo : AppCompatActivity
    {
        CircleImageView Foto;
        Uri RutaArchivo;
        FileStream streamArchivo;
        PickerDate onDateSetListener;
        ListView textListView;
        EditText buscar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NuevoCaballo);
            new ShareInside().CopyDocuments("RazasGender.sqlite", "RazasGender.db");
            var txtHorseName = FindViewById<EditText>(Resource.Id.txtHorseName);
            Foto = FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.btnFoto);
            Foto.Click += delegate {
                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, RutaArchivo);
                StartActivityForResult(intent, 1, savedInstanceState);
            };
            var txtBirthDay = FindViewById<TextView>(Resource.Id.txtBirthDay);
            txtBirthDay.Click += delegate {
                Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
                int year = calendar.Get(Java.Util.CalendarField.Year);
                int month = calendar.Get(Java.Util.CalendarField.Month);
                int day_of_month = calendar.Get(Java.Util.CalendarField.DayOfMonth);
                DatePickerDialog dialog;

                dialog = new DatePickerDialog(this, Resource.Style.ThemeOverlay_AppCompat_Dialog_Alert,
                onDateSetListener, year, month, day_of_month);
                dialog.Show();
            };
            onDateSetListener = new PickerDate(txtBirthDay);
            var txtGender = FindViewById<TextView>(Resource.Id.txtGender);
            txtGender.Click += delegate {
                Dialog alertar = new Dialog(this, Resource.Style.Theme_Dialog_Translucent);
                alertar.RequestWindowFeature(1);
                alertar.SetCancelable(true);
                alertar.SetContentView(Resource.Layout.DialogoGender);
                List<string> consulta = new List<string>() { GetText(Resource.String.Filly), GetText(Resource.String.Gelding), GetText(Resource.String.Mare), GetText(Resource.String.Stallion) };
                textListView = alertar.FindViewById<ListView>(Resource.Id.Listagender);
                textListView.Adapter = new AdaptadorGender(this, consulta, alertar, txtGender);
                alertar.Show();
            };
            var txtBreed = FindViewById<TextView>(Resource.Id.txtBreed);
            txtBreed.Click += delegate {
                Dialog alertar = new Dialog(this, Resource.Style.Theme_Dialog_Translucent);
                alertar.RequestWindowFeature(1);
                alertar.SetCancelable(true);
                alertar.SetContentView(Resource.Layout.DialogoRazas);
                var con = new SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "RazasGender.sqlite"));
                var consulta = con.Query<Modelos.Razas>("select * from Razas", new Modelos.Razas().id_raza);
                textListView = alertar.FindViewById<ListView>(Resource.Id.ListaRazas);
                textListView.Adapter = new AdaptadorRazas(this, consulta, alertar, txtBreed);
                buscar = alertar.FindViewById<EditText>(Resource.Id.buscar);
                buscar.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    var consulta2 = con.Query<Modelos.Razas>("select * from Razas where  raza like  '" + buscar.Text + "%'", new Modelos.Razas().id_raza);
                    textListView.Adapter = new AdaptadorRazas(this, consulta2, alertar, txtBreed);
                };
                alertar.Show();
            };
            var txtHeigth = FindViewById<EditText>(Resource.Id.txtHeigthHorse);
            var txtWeigth = FindViewById<EditText>(Resource.Id.txtWeigthHorse);
            var txtOat = FindViewById<EditText>(Resource.Id.txtOatHorse);
            var btnAtras = FindViewById<ImageView>(Resource.Id.btnAtras);
            btnAtras.Click += delegate {

            };
            var btnGuardar = FindViewById<ImageView>(Resource.Id.btnGuardar);
            btnGuardar.Click += delegate {
                //if ( == "")
                //{

                //}
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

    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "Y:" + typeof(TimePickerFragment).Name.ToUpper();
        
        Action<TimeSpan> _timeSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<TimeSpan> onTimeSet)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag._timeSelectedHandler = onTimeSet;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Java.Util.Calendar c = Java.Util.Calendar.Instance;
            int hour = c.Get(Java.Util.CalendarField.HourOfDay);
            int minute = c.Get(Java.Util.CalendarField.Minute);
            bool is24HourView = true;
            TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                           this,
                                                           hour,
                                                           minute,
                                                           is24HourView);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            TimeSpan selectedTime = new TimeSpan(hourOfDay, minute, 00);
            Log.Debug(TAG, selectedTime.ToString());
            _timeSelectedHandler(selectedTime);
        }
    }

    public class PickerDate : Java.Lang.Object, IOnDateSetListener
    {
        TextView txtDate;

        public PickerDate(TextView txtDate) { this.txtDate = txtDate; }

        void IOnDateSetListener.OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            string m = (month + 1).ToString(), d = dayOfMonth.ToString();

            if (m.Length <= 1)
                m = "0" + (month + 1).ToString();
            if (d.Length <= 1)
                d = "0" + dayOfMonth.ToString();
            try
            {
                double Mes = (double.Parse(m));
                if (Mes <= 10)
                    m = "0" + Mes.ToString();
                txtDate.Text = d + "-" + m + "-" + year.ToString();
            }
            catch (System.Exception) { }
        }
    }

    public class Razas
    {
        public int Id_Raza { get; set; }
        public int id_gender { get; set; }
        public string tipo { get; set; }
        public string en { get; set; }
        public string de { get; set; }
        public string fr { get; set; }
        public string it { get; set; }
        public string no { get; set; }
        public string ar { get; set; }
        public string tr { get; set; }
        public string pt { get; set; }
        public string zu { get; set; }
        public string ru { get; set; }
        public string es { get; set; }

    }
}