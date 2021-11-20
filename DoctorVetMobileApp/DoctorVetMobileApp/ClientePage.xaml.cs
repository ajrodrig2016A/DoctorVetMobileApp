using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoctorVetMobileApp.Models;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace DoctorVetMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientePage : ContentPage
    {
        private const string Url = "http://doctorvetwebapi.azurewebsites.net/api/Cliente";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Cliente> _post;
        private Cliente datosSelected;
        public ClientePage()
        {
            InitializeComponent();
            GetDatos();
        }

        private async void GetDatos()
        {
            var content = await client.GetStringAsync(Url);
            List<Cliente> posts = JsonConvert.DeserializeObject<List<Cliente>>(content);
            _post = new ObservableCollection<Cliente>(posts);

            clienteListView.ItemsSource = _post;
        }

        private async void btnPost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewInsertarCliente());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        private async void btnPut_Clicked(object sender, EventArgs e)
        {
            Cliente datos = datosSelected;

            if (datos == null)
            {
                await DisplayAlert("Mensaje informativo", "Favor seleccionar un registro del ListView", "OK");
                return;
            }
            await Navigation.PushAsync(new viewActualizarCliente(datos));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cliente datos = datosSelected;

                if (datos == null)
                {
                    await DisplayAlert("Mensaje informativo", "Favor seleccionar un registro del ListView", "OK");
                    return;
                }

                var action = await DisplayAlert("Mensaje de confirmación", "Usted está seguro de eliminar el registro", "Aceptar", "Cancelar");

                if (action.Equals(true))
                {
                    var client = new RestClient("https://doctorvetwebapi.azurewebsites.net/api/Cliente?idCliente="+ datos.idCliente);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.DELETE);
                    request.AddHeader("Accept", "application/json");
                    var body = @"";
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        await DisplayAlert("Alerta", "Eliminacion correcta", "OK");
                        GetDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void clienteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objDatos = ((Xamarin.Forms.ListView)sender).SelectedItem as Cliente;

            if (objDatos == null)
            {
                return;
            }
            datosSelected = objDatos;
        }
    }
}