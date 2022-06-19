using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E16778.Models
{
    public class Ubicaciones
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public byte[] Imagen { get; set; }

        public float Longitud { get; set; }

        public float Latitud { get; set; }

        [MaxLength(200)]
        public String Descripcion { get; set; }
    }
}
