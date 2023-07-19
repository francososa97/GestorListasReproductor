using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorListasReproductor.Models
{
    public class Video
    {
        public int Duracion { get; set; }
        public string Autor { get;set; }
        public string Titulo{ get; set; }
        public string CuandoSePublico { get; set; }
    }
}
