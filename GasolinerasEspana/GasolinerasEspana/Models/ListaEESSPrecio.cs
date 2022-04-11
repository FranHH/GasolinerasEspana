using System;
using System.Collections.Generic;
using System.Text;

namespace GasolinerasEspana.Models
{
    [Serializable]
    public class ListaEESSPrecio
    {
        public ListaEESSPrecio(string cp, string direccion, string horario, 
                                string latitud, string localidad, string longitud,
                                string margen, string municipio, string biodiesel, 
                                string bioetanol, string gasComprimido, string gasLicuado, 
                                string gasLicuadoPetroleo,  string gasoleoA, string gasoleoB, 
                                string gasoleoPremium, string gasolina95E10, string gasolina95e5, 
                                string gasolinaE5Premium, string gasolina98E10, string gasolina98E5,
                                string pHidrogeno, string provincia, string remision, string rotulo, 
                                string tipoVenta, string porcentajeBioEtanol, string porcentajeEsterMetilico, 
                                string idees, string idMunicipio, string idProvincia, string idCCAA, string icon, string update)
        {
            this.CP = cp;
            this.Direccion = direccion;
            this.Horario = horario;
            this.Latitud = latitud;
            this.Localidad = localidad;
            this.Longitud = longitud;
            this.Margen = margen;
            this.Municipio = municipio;
            this.PrecioBioDiesel = biodiesel + "€";
            this.PrecioBioEtanol = bioetanol + "€";
            this.PrecioGasNaturalComprimido = gasComprimido + "€";
            this.PrecioGasNaturalLicuado = gasLicuado + "€";
            this.PrecioGasesLicuadosDelPetroleo = gasLicuadoPetroleo + "€";
            this.PrecioGasoleoA = gasoleoA + "€";
            this.PrecioGasoleoB = gasoleoB + "€";
            this.PrecioGasoleoPremium = gasoleoPremium + "€";
            this.PrecioGasolina95E10 = gasolina95E10 + "€";
            this.PrecioGasolina95E5 = gasolina95e5 + "€";
            this.PrecioGasolina95E5Premium = gasolinaE5Premium + "€";
            this.PrecioGasolina98E10 = gasolina98E10 + "€";
            this.PrecioGasolina98E5 = gasolina98E5 + "€";
            this.PrecioHidrogeno = pHidrogeno + "€";
            this.Provincia = provincia;
            this.Remision = remision;
            this.Rotulo = rotulo;
            this.TipoVenta = tipoVenta;
            this.PorcentajeBioEtanol = porcentajeBioEtanol + "%";
            this.PorcentajeEsterMetilico = porcentajeEsterMetilico + "%";
            this.IDEESS = idees;
            this.IDMunicipio = idMunicipio;
            this.IDProvincia = idProvincia;
            this.IDCCAA = idCCAA;
            this.Address = direccion;
            this.Address2 = localidad + " " + provincia + "(" + cp + ")";
            this.IconGasolinera = icon;
            this.Update = update;

        }


        public string CP { get; set; }
        public string Direccion { get; set; }
        public string Horario { get; set; }
        public string Latitud { get; set; }
        public string Localidad { get; set; }
        public string Longitud { get; set; }
        public string Margen { get; set; }
        public string Municipio { get; set; }
        public string PrecioBioDiesel { get; set; }
        public string PrecioBioEtanol { get; set; }
        public string PrecioGasNaturalComprimido { get; set; }
        public string PrecioGasNaturalLicuado { get; set; }
        public string PrecioGasesLicuadosDelPetroleo { get; set; }
        public string PrecioGasoleoA { get; set; }
        public string PrecioGasoleoB { get; set; }
        public string PrecioGasoleoPremium { get; set; }
        public string PrecioGasolina95E10 { get; set; }
        public string PrecioGasolina95E5 { get; set; }
        public string PrecioGasolina95E5Premium { get; set; }
        public string PrecioGasolina98E10 { get; set; }
        public string PrecioGasolina98E5 { get; set; }
        public string PrecioHidrogeno { get; set; }
        public string Provincia { get; set; }
        public string Remision { get; set; }
        public string Rotulo { get; set; }
        public string TipoVenta { get; set; }
        public string PorcentajeBioEtanol { get; set; }
        public string PorcentajeEsterMetilico { get; set; }
        public string IDEESS { get; set; }
        public string IDMunicipio { get; set; }
        public string IDProvincia { get; set; }
        public string IDCCAA { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Distancia { get; set; }
        public string IconGasolinera { get; set; }
        public string Update { get; set; }
    }
}
