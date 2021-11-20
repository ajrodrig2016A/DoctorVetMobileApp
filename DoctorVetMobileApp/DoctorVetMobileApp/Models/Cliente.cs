using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorVetMobileApp.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string numeroDocumento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string numeroContacto { get; set; }

    }
}
