﻿using System;
namespace CABASUS.Modelos
{
    public class usuarios
    {
        public string id_usuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public string foto { get; set; }
        public string id_dispositivo { get; set; }
        public string SO { get; set; }
        public string tokenFB { get; set; }
        public string fecha_nacimiento { get; set; }
    }

    public class ConsultarEmail
    {
        public string email { get; set; }
        public string contrasena { get; set; }
    }
}
