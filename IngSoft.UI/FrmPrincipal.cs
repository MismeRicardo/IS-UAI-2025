using System;
using System.Linq;
using System.Windows.Forms;
using IngSoft.Abstractions;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Domain;
using IngSoft.Services;

namespace IngSoft.UI
{
    public partial class FrmPrincipal : Form, IObserver
    {
        private readonly IMultiIdiomaServices _multiIdiomaServices;
        public FrmPrincipal()
        {
            InitializeComponent();
            _multiIdiomaServices = ServicesFactory.CreateMultiIdiomaServices();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarIdiomas();
            EstablecerIdiomaPorDefecto();
        }

        private void CargarIdiomas()
        {
            var idiomas = _multiIdiomaServices.ObtenerIdiomas();
            cboIdiomas.DataSource = idiomas;
            cboIdiomas.DisplayMember = "Codigo";
            cboIdiomas.ValueMember = null;
        }

        private void cboIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!(cboIdiomas.SelectedItem is Idioma))
                return;

            var idiomaSeleccionado = (Idioma)cboIdiomas.SelectedValue;

            if(MultiIdiomaManager.Idioma != null)
                MultiIdiomaManager.Idioma.Desuscribir(this);

            MultiIdiomaManager.SetIdioma(idiomaSeleccionado);

            idiomaSeleccionado.Suscribir(this);

            idiomaSeleccionado.NotificarObservers();
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

        private void multiIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmMultiIdioma = new FrmMultiIdioma();
            frmMultiIdioma.ShowDialog();
        }

        private void EstablecerIdiomaPorDefecto()
        {
            var idiomaPorDefecto = _multiIdiomaServices.ObtenerIdiomaPorDefecto();
            if (idiomaPorDefecto == null) return;
            cboIdiomas.SelectedItem = idiomaPorDefecto;
            MultiIdiomaManager.SetIdioma(idiomaPorDefecto);
            idiomaPorDefecto.Suscribir(this);
            var controles = _multiIdiomaServices.ObtenerControlesPorIdioma(idiomaPorDefecto.Id)
                .Cast<IControlIdioma>().ToList();
            MultiIdiomaManager.CambiarIdiomaControles(this, controles);
        }

        public void Actualizar()
        {
            if (MultiIdiomaManager.Idioma != null)
            {
                var controles = _multiIdiomaServices.ObtenerControlesPorIdioma(MultiIdiomaManager.Idioma.Id)
                    .Cast<IControlIdioma>().ToList();

                MultiIdiomaManager.CambiarIdiomaControles(this, controles);
            }
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
