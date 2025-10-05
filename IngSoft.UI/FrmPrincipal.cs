﻿using System;
using System.Windows.Forms;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Services;

namespace IngSoft.UI
{
    public partial class FrmPrincipal : Form
    {
        private readonly IUsuarioServices _usuarioServices;
        public FrmPrincipal()
        {
            InitializeComponent();
            _usuarioServices = ServicesFactory.CreateUsuarioServices();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActualizarMenuSegunEstadoSesion();
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmUsuario = new FrmUsuario();
            frmUsuario.ShowDialog();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmBitacora = new FrmBitacora();
            frmBitacora.ShowDialog();
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmLogin = new FrmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                ActualizarMenuSegunEstadoSesion();
            }
        }
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogOutUser();
            ActualizarMenuSegunEstadoSesion();
        }

        private void LogOutUser()
        {
            _usuarioServices.SetRegistradoBitacora(FrmUsuarioFlexiblizador.RegistrarEnBitacora);
            _usuarioServices.LogOutUser();
        }

        private void ActualizarMenuSegunEstadoSesion()
        {
            if (SessionManager.GetInstance().IsLoggedIn())
            {
                iniciarSesionToolStripMenuItem.Enabled = false;
                cerrarSesionToolStripMenuItem.Enabled = true;
                usuariosToolStripMenuItem.Visible = true;
                bitacoraToolStripMenuItem.Visible = true;
                label1.Visible = true;
                label1.Text = $"Bienvenido: {SessionManager.GetUsuario().Nombre} {SessionManager.GetUsuario().Apellido}";
            }
            else
            {
                iniciarSesionToolStripMenuItem.Enabled = true;
                cerrarSesionToolStripMenuItem.Enabled = false;
                usuariosToolStripMenuItem.Visible = false;
                bitacoraToolStripMenuItem.Visible = false;
                label1.Visible = false;
            }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SessionManager.GetInstance().IsLoggedIn())
            {
                LogOutUser();
            }
        }
    }
}
