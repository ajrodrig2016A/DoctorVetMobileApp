﻿using DoctorVetMobileApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorVetMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewActualizarCliente : ContentPage
    {
        public viewActualizarCliente(Cliente datos)
        {
            InitializeComponent();
            //llenar datos en controles
            txtIdCliente.Text = Convert.ToString(datos.idCliente);
            txtNumeroDocumento.Text = datos.numeroDocumento;
            txtNombre.Text = datos.nombres;
            txtApellido.Text = datos.apellidos;
            dtpFechaRegistro.SetValue(DatePicker.DateProperty, datos.fechaRegistro);
            txtDireccion.Text = datos.direccion;
            txtEmail.Text = datos.email;
            txtNumeroContacto.Text = datos.numeroContacto;
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientePage());
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cliente objCli = new Cliente();
                objCli.idCliente = Convert.ToInt32(txtIdCliente.Text);
                objCli.numeroDocumento = txtNumeroDocumento.Text;
                objCli.nombres = txtNombre.Text;
                objCli.apellidos = txtApellido.Text;
                objCli.fechaRegistro = dtpFechaRegistro.Date;
                objCli.email = txtEmail.Text;
                objCli.direccion = txtDireccion.Text;
                objCli.numeroContacto = txtNumeroContacto.Text;

                var client = new RestClient("https://doctorvetwebapi.azurewebsites.net/api/Cliente");
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(objCli);

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    DisplayAlert("Alerta", "Actualizacion correcta", "OK");
                    limpiarCampos();

                }
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void limpiarCampos()
        {
            txtIdCliente.Text = String.Empty;
            txtNumeroDocumento.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            //dtpFechaRegistro.SetValue(DatePicker.DateProperty, String.Empty);
            txtDireccion.Text = String.Empty; ;
            txtEmail.Text = String.Empty; ;
            txtNumeroContacto.Text = String.Empty;
        }
    }
}