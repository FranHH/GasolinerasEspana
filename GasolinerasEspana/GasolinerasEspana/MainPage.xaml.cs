using GasolinerasEspana.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace GasolinerasEspana
{
    public partial class MainPage : ContentPage
    {

        private string urlMaritimos = "https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/PostesMaritimos/";
        private string urlTerrestres = "https://sedeaplicaciones.minetur.gob.es/ServiciosRESTCarburantes/PreciosCarburantes/EstacionesTerrestres/";
        private string geoLocal = "http://api.geonames.org/findNearbyJSON?formatted=true&lat=37.8859213&lng=-4.791809&fclass=P&fcode=PPLA&fcode=PPL&fcode=PPLC&username=frandev001&style=full"; //username=frandev001
        private string geoLocalenUso = "http://api.geonames.org/addressJSON?lat=37.8859213&lng=-4.791809&username=frandev001";
        private string urlAPIGoogle = "https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyAI38hOHYgwfIU_1RQbqWRtKut4Plen7Eg";
        private HttpClient client = new HttpClient();
        private HttpResponseMessage response = null;
        private Gasolineras oGasolinera = null;
        private GetAddress oAddress = null;
        private IList<ListaEESSPrecio> ilListaEESSPrecio = new List<ListaEESSPrecio>();
        private string jsonLista = string.Empty, jsonGeo = string.Empty;
        private CancellationTokenSource cts;

        public MainPage()
        {
            InitializeComponent();
            MostrarLista();
            #region Refrescar datos
            lvGasolineras.RefreshCommand = new Command(() =>
            {
                MostrarLista();
                lvGasolineras.IsRefreshing = false;
            });
            #endregion
        }

        private async void MostrarLista()
        {
            //Obtengo response de api
            response = await client.GetAsync(urlTerrestres);
            //Si no hay error obtengo string de gasolineras
            if (response.IsSuccessStatusCode)
            {
                 jsonLista = response.Content.ReadAsStringAsync().Result;
            }
            //Moldeo el string de gasolineras para adaptarlo a mi modelo (ListaEESSPrecio)
            jsonLista = jsonLista.Replace("C.P.", "CP")
                .Replace("Dirección", "Direccion")
                .Replace("Longitud (WGS84)","Longitud")
                .Replace("Precio Biodiesel", "PrecioBioDiesel")
                .Replace("Precio Bioetanol", "PrecioBioEtanol")
                .Replace("Precio Gas Natural Comprimido","PrecioGasNaturalComprimido")
                .Replace("Precio Gas Natural Licuado","PrecioGasNaturalLicuado")
                .Replace("Precio Gases licuados del petróleo", "PrecioGasesLicuadosDelPetroleo")
                .Replace("Precio Gasoleo A","PrecioGasoleoA")
                .Replace("Precio Gasoleo B", "PrecioGasoleoB")
                .Replace("Precio Gasoleo Premium","PrecioGasoleoPremium")
                .Replace("Precio Gasolina 95 E10","PrecioGasolina95E10")
                .Replace("Precio Gasolina 95 E5","PrecioGasolina95E5")
                .Replace("Precio Gasolina 95 E5 Premium", "PrecioGasolina95E5Premium")
                .Replace("Precio Gasolina 98 E10","PrecioGasolina98E10")
                .Replace("Precio Gasolina 98 E5","PrecioGasolina98E5")
                .Replace("Precio Hidrogeno","PrecioHidrogeno")
                .Replace("Remisión", "Remision")
                .Replace("Rótulo", "Rotulo")
                .Replace("Tipo Venta","TipoVenta")
                .Replace("% BioEtanol", "PorcentajeBioEtanol")
                .Replace("% Éster metílico", "PorcentajeEsterMetilico");

            //Deserializo listado de gasolineras
            oGasolinera = JsonConvert.DeserializeObject<Gasolineras>(jsonLista);
            //Obtengo las 100 primeras gasolineras en funcion de la ubicación del dispositivo movil y muestro en el listView la lista de gasolineras ordenadas por distancia
            lvGasolineras.ItemsSource = GetListEESSLocation100First(GetLocation(), oGasolinera.ListaEESSPrecio).Result;
        }

        #region Filtro
        private void btnFiltro(object sender, TextChangedEventArgs e)
        {
            IList<ListaEESSPrecio> ilAux = new List<ListaEESSPrecio>();

            foreach(ListaEESSPrecio item in oGasolinera.ListaEESSPrecio)
            {
                if (item.Rotulo.ToLower().Contains(bBusqueda.Text.ToLower()) || item.Provincia.ToLower().Contains(bBusqueda.Text.ToLower()))
                {
                    ilAux.Add(item);
                }
            }

            lvGasolineras.ItemsSource = ilAux;
        }
        #endregion

        #region Tapped
        private void ListGasolinerasTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        #endregion

        #region Scrolled
        private void OnListViewScrolled(object sender, ScrolledEventArgs e)
        {
            ListView listview = new ListView();
            listview.Scrolled += OnListViewScrolled;
        }


        #endregion

        private void ListGasolinerasSeleccted(object sender, SelectedItemChangedEventArgs e)
        {

        }

        #region GetLocation
        private async Task<double[]> GetLocation()
        {
            var lastLocation = await Geolocation.GetLastKnownLocationAsync();
            IList<double> listLocation = new List<double>();
            double[] aLocation = new double [2];

            if (lastLocation != null)
            {
                aLocation[0] = lastLocation.Latitude;
                aLocation[1] = lastLocation.Longitude;

                return aLocation;
            }
            else
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    aLocation[0] = location.Latitude;
                    aLocation[1] = location.Longitude;

                    return aLocation;
                }
            }
            return null;
        }
        #endregion

        #region ConvertirLocation
        private async Task<IList<ListaEESSPrecio>> GetListEESSLocation100First(Task<double[]> listLocation, IList<ListaEESSPrecio> ilEESSLocation)
        {
            ///listLocation[0] = latitud del dispositivo movil (lat1)
            ///listLocation[1] = longitud del dispositivo movil (long1)
            ///ilEESSLocation[i].Latitud = latitud de la gasolinera (lat2)
            ///ilEESSLocation[i].Longitud = longitud de la gasolinera (long2)

            double dRadio = 6371; //Radio de la tierra en km
            double dLat, dLon, dA, dC, dDistancia = 0, dDistanciaAux=dRadio;

            IList<ListaEESSPrecio> liEESSAux = new List<ListaEESSPrecio>();
            string url = "http://api.geonames.org/addressJSON?lat=" + listLocation.Result[0].ToString().Replace(",", ".") + "&lng=" + listLocation.Result[1].ToString().Replace(",", ".") + "&username=frandev001";
            //string testURL = "http://api.geonames.org/addressJSON?lat=37.8859213&lng=-4.7918054&username=frandev001";
            try
            {
                HttpResponseMessage responseLocation = client.GetAsync(url).Result;
                //Si no hay error obtengo string de localizacion
                if (responseLocation.IsSuccessStatusCode)
                {
                    jsonGeo = responseLocation.Content.ReadAsStringAsync().Result;
                }

                //Deserializo localizacion
                oAddress = JsonConvert.DeserializeObject<GetAddress>(jsonGeo);

            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "Ha ocurrido un error al intentar obtener las gasolineras de su provincia", "Aceptar");
            }

            for (int i = 0; i < oGasolinera.ListaEESSPrecio.Count && liEESSAux.Count != 100; i++)
            {
                if (!string.IsNullOrEmpty(ilEESSLocation[i].PrecioGasoleoA))
                {
                    dLat = double.Parse(ilEESSLocation[i].Latitud) * Math.PI / 180 - (listLocation.Result[0] * Math.PI / 180);
                    dLon = double.Parse(ilEESSLocation[i].Longitud) * Math.PI / 180 - (listLocation.Result[1] * Math.PI / 180);
                    dA = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(listLocation.Result[0] * Math.PI / 180) * Math.Cos(double.Parse(ilEESSLocation[i].Latitud) * Math.PI / 180) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                    dC = 2 * Math.Atan2(Math.Sqrt(dA), Math.Sqrt(1 - dA));
                    dDistancia = dRadio * dC;

                    if (oGasolinera.ListaEESSPrecio[i].Provincia.ToLower().Contains(oAddress.Address.locality.ToLower()))
                    {
                        ilEESSLocation[i].IconGasolinera = "gasolinera.png";
                        ilEESSLocation[i].PrecioGasoleoA += "€";
                        ilEESSLocation[i].PrecioGasoleoB += "€";
                        ilEESSLocation[i].PrecioGasesLicuadosDelPetroleo += "€";
                        ilEESSLocation[i].PrecioBioDiesel += "€";
                        ilEESSLocation[i].PrecioBioEtanol += "€";
                        ilEESSLocation[i].PrecioGasNaturalComprimido += "€";
                        ilEESSLocation[i].PrecioGasNaturalLicuado += "€";
                        ilEESSLocation[i].PrecioGasoleoPremium += "€";
                        ilEESSLocation[i].PrecioGasolina95E10 += "€";
                        ilEESSLocation[i].PrecioGasolina95E5 += "€";
                        ilEESSLocation[i].PrecioGasolina95E5Premium += "€";
                        ilEESSLocation[i].PrecioGasolina98E10 += "€";
                        ilEESSLocation[i].PrecioGasolina98E5 += "€";
                        ilEESSLocation[i].PrecioHidrogeno += "€";
                        ilEESSLocation[i].PorcentajeBioEtanol += "%";
                        ilEESSLocation[i].PorcentajeEsterMetilico += "%";
                        ilEESSLocation[i].Distancia = (Math.Truncate(dDistancia * 100) / 100).ToString() + " km.";
                        liEESSAux.Add(ilEESSLocation[i]);
                    }
                }
            }
            //Devuelvo la lista ordenada por distancia
            return liEESSAux.OrderBy(o => o.Distancia).ToList();

        }
        #endregion

    }
}
