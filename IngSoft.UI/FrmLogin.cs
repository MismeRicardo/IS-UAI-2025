﻿using System;
using System.Windows.Forms;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Domain;
using IngSoft.Domain.Enums;
using IngSoft.Services;

namespace IngSoft.UI
{
    public partial class FrmLogin : Form
    {
        private readonly IUsuarioServices usuarioServices;
        private readonly IBitacoraServices bitacoraServices;
        public FrmLogin()
        {
            InitializeComponent();
            usuarioServices = ServicesFactory.CreateUsuarioServices();
            bitacoraServices = ServicesFactory.CreateBitacoraServices();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            usuarioServices.SetRegistradoBitacora(RegistrarEnBitacora);

            try
            {
                var mUsuarioActual = new Domain.Usuario
                {
                    UserName = txtUsuario.Text,
                    Contrasena = txtContrasena.Text
                };

                usuarioServices.LoginUser(mUsuarioActual);
                MessageBox.Show($"Iniciado sesion: {SessionManager.GetUsuario().UserName}");
                this.DialogResult = DialogResult.OK;
                //new FrmUsuario().EliminarControlesAdicionalesUsuario();
                this.Close();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"Credenciales Incorrectas");
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
                this.DialogResult = DialogResult.None;
            }
        }

        private void RegistrarEnBitacora(Usuario usuario, string descripcion, string origen, TipoEvento tipoEvento)
        {            
            var bitacora = new Bitacora
            {
                Id = Guid.NewGuid(),
                Usuario = usuario,
                Fecha = DateTime.Now,
                Descripcion = descripcion,
                Origen = origen,
                TipoEvento = tipoEvento
            };
            bitacoraServices.GuardarBitacora(bitacora);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
