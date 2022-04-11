using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasolinerasEspana.Models
{
    [Serializable]
    public class Gasolineras
    {
        public Gasolineras(){}
        public Gasolineras(string fecha, List<ListaEESSPrecio> _lista, string nota, string result)
        {
            this.Fecha = fecha;
            this.ListaEESSPrecio = _lista;
            this.Nota = nota;
            this.ResultadoConsulta = result;
        }


        public string Fecha { get; set; }
        public List<ListaEESSPrecio> ListaEESSPrecio { get;set;}
        public string Nota { get; set; }
        public string ResultadoConsulta { get; set; }
    }
}
