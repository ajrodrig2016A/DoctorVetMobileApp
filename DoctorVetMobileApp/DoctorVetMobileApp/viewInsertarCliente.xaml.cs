using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorVetMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewInsertarCliente : ContentPage
    {
        public viewInsertarCliente()
        {
            InitializeComponent();
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientePage());
        }
        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                //parametros.Add("idCliente", Convert.ToInt32(txtIdCliente.Text)); --Se utiliza un campo PK autoincrementable para codigo de tabla cliente
                parametros.Add("numeroDocumento", txtNumeroDocumento.Text);
                parametros.Add("nombres", txtNombre.Text);
                parametros.Add("apellidos", txtApellido.Text);
                parametros.Add("fechaRegistro", Convert.ToString(dtpFechaRegistro.Date.Year) + '-' + Convert.ToString(dtpFechaRegistro.Date.Month) + '-' + Convert.ToString(dtpFechaRegistro.Date.Day));
                parametros.Add("direccion", txtDireccion.Text);
                parametros.Add("email", txtEmail.Text);
                parametros.Add("numeroContacto", txtNumeroContacto.Text);
                cliente.UploadValues("http://doctorvetwebapi.azurewebsites.net/api/Cliente", "POST", parametros);
                DisplayAlert("Alerta", "Ingreso correcto", "OK");
                limpiarCampos();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void limpiarCampos()
        {
            txtNumeroDocumento.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            //dtpFechaRegistro.SetValue(DatePicker.DateProperty, String.Empty);
            txtDireccion.Text = String.Empty; ;
            txtEmail.Text = String.Empty; ;
            txtNumeroContacto.Text = String.Empty;
        }

        private void dtpFechaRegistro_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}