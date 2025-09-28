using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Domain;
using IngSoft.Services;


namespace IngSoft.UI
{
    public partial class FrmUsuario : Form
    {
        private readonly IUsuarioServices _usuarioServices;
        private List<Usuario> _usuarios;
        public FrmUsuario()
        {
            InitializeComponent();
            _usuarioServices = ServicesFactory.CreateUsuarioServices();
        }

        private void agregarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarControlesAdicionalesUsuario();
            CrearTextBoxesUsuario();
        }

        private void CrearTextBoxesUsuario()
        {
            FrmUsuarioFlexiblizador.TextBoxCreator("Usuario", new Point(FrmUsuario.ActiveForm.Width / 2 - 230, FrmUsuario.ActiveForm.Height / 8));
            FrmUsuarioFlexiblizador.TextBoxCreator("Nombre", new Point((FrmUsuario.ActiveForm.Width / 2 - 280) + (280), FrmUsuario.ActiveForm.Height / 8));
            FrmUsuarioFlexiblizador.TextBoxCreator("Apellido", new Point((FrmUsuario.ActiveForm.Width / 2 - 230), FrmUsuario.ActiveForm.Height / 4));
            FrmUsuarioFlexiblizador.TextBoxCreator("Email", new Point((FrmUsuario.ActiveForm.Width / 2 - 280) + (280), FrmUsuario.ActiveForm.Height / 4));
            FrmUsuarioFlexiblizador.TextBoxCreator("Contraseña", new Point((FrmUsuario.ActiveForm.Width / 2 - 230), (FrmUsuario.ActiveForm.Height / 8 + FrmUsuario.ActiveForm.Height / 4)));
            FrmUsuarioFlexiblizador.TextBoxCreator("Repetir Contraseña", new Point((FrmUsuario.ActiveForm.Width / 2 - 280) + (280), (FrmUsuario.ActiveForm.Height / 8 + FrmUsuario.ActiveForm.Height / 4)));
            FrmUsuarioFlexiblizador.GuardarUsuarioButtonCreator();
        }

       
        private void verTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarControlesAdicionalesUsuario();
            _usuarios = _usuarioServices.ObtenerUsuarios();
            FrmUsuarioFlexiblizador.DataGridViewUsuarioCreator(_usuarios);
        }


        internal void EliminarControlesAdicionalesUsuario()
        {
            Control menuStrip = FrmUsuario.ActiveForm.Controls.Find("menuStrip1", true).FirstOrDefault();
            ToolStripItemCollection botonera = ((MenuStrip)menuStrip).Items;
            ToolStripItem loginToolStripButton= botonera.Find("loginToolStripMenuItem", true).FirstOrDefault();
            ToolStripItem logoutToolStripButton = botonera.Find("cerrarSesionToolStripMenuItem", true).FirstOrDefault();
            if (SessionManager.GetInstance().IsLoggedIn())
            {
                loginToolStripButton.Visible=false;
                logoutToolStripButton.Visible = true;
            }
            else
            {
                loginToolStripButton.Visible = true;
                logoutToolStripButton.Visible = false;
            }
            FrmUsuario.ActiveForm.Controls.Clear();
            FrmUsuario.ActiveForm.Controls.Add(menuStrip);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarControlesAdicionalesUsuario();
            FrmUsuarioFlexiblizador.TextBoxCreator("Usuario", new Point(FrmUsuario.ActiveForm.Width / 2 - 100, FrmUsuario.ActiveForm.Height / 4));
            FrmUsuarioFlexiblizador.TextBoxCreator("Contraseña", new Point((FrmUsuario.ActiveForm.Width / 2 - 100), FrmUsuario.ActiveForm.Height / 8 + FrmUsuario.ActiveForm.Height / 4));
            try
            {
                FrmUsuarioFlexiblizador.LoginButtonCreator();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _usuarioServices.LogOutUser();
            EliminarControlesAdicionalesUsuario();
        }

        private void FrmUsuario_Shown(object sender, EventArgs e)
        {
            EliminarControlesAdicionalesUsuario();
        }
    }
}
