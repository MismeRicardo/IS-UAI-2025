using System;
using System.Linq;
using System.Windows.Forms;
using IngSoft.Abstractions;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Services;
using IngSoft.UI.DTOs;

namespace IngSoft.UI
{
    public partial class FrmMultiIdioma : Form, IObserver
    {
        private readonly IMultiIdiomaServices _multiIdiomaServices;
        public FrmMultiIdioma()
        {
            InitializeComponent();
            _multiIdiomaServices = ServicesFactory.CreateMultiIdiomaServices();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMultiIdioma_Load(object sender, EventArgs e)
        {
            CargarIdiomas();
            SuscribirseAIdiomaActual();
            AplicarIdiomaActual();
            CargarGridTraduccion();
        }
        private void gridTraduccion_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Configurar las columnas después de que se cargan los datos
            if (gridTraduccion.Columns.Count >= 2)
            {
                // Hacer las dos primeras columnas de solo lectura
                gridTraduccion.Columns["Control"].ReadOnly = true;
                gridTraduccion.Columns["TextoPorDefecto"].ReadOnly = true;

                // Opcional: Cambiar el estilo visual para indicar que son de solo lectura
                gridTraduccion.Columns["Control"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                gridTraduccion.Columns["TextoPorDefecto"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

                // Opcional: Configurar anchos de columnas
                gridTraduccion.Columns["Control"].Width = 150;
                gridTraduccion.Columns["TextoPorDefecto"].Width = 200;
                gridTraduccion.Columns["TextoTraducido"].Width = 200;

                // Opcional: Configurar headers más descriptivos
                gridTraduccion.Columns["Control"].HeaderText = "Control";
                gridTraduccion.Columns["TextoPorDefecto"].HeaderText = "Texto Original";
                gridTraduccion.Columns["TextoTraducido"].HeaderText = "Traducción";
            }
        }
        private void cboIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Validaciones para evitar el error de casteo
            if (!(cboIdiomas.SelectedValue is Guid))
                return;

            var idiomaSeleccionado = (Guid)cboIdiomas.SelectedValue;
            var controles = _multiIdiomaServices.ObtenerControlesPorIdioma(idiomaSeleccionado)
                .Cast<IControlIdioma>().ToList();

            MultiIdiomaManager.CambiarIdiomaControles(this, controles);
        }

        private void btnGuardarTraduccion_Click(object sender, EventArgs e)
        {

        }

        private void CargarIdiomas()
        {
            var idiomas = _multiIdiomaServices.ObtenerIdiomas();
            cboIdiomas.DataSource = idiomas;
            cboIdiomas.DisplayMember = "Nombre";
            cboIdiomas.ValueMember = null;
        }

        private void SuscribirseAIdiomaActual()
        {
            // Suscribirse al idioma actual si existe
            if (MultiIdiomaManager.Idioma != null)
            {
                MultiIdiomaManager.Idioma.Suscribir(this);

                // Sincronizar el ComboBox con el idioma actual
                cboIdiomas.SelectedItem = MultiIdiomaManager.Idioma;
            }
        }

        private void AplicarIdiomaActual()
        {
            // Aplicar el idioma actual al formulario
            if (MultiIdiomaManager.Idioma != null)
            {
                var controles = _multiIdiomaServices.ObtenerControlesPorIdioma(MultiIdiomaManager.Idioma.Id)
                    .Cast<IControlIdioma>().ToList();
                MultiIdiomaManager.CambiarIdiomaControles(this, controles);
            }
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

        private void CargarGridTraduccion() 
        {
            var controlesPorDefecto = _multiIdiomaServices.ObtenerControlesPorIdioma(MultiIdiomaManager.Idioma.Id);

            gridTraduccion.DataSource = controlesPorDefecto.Select( c =>
                new MultiIdiomaGridDto 
                {
                    Control = c.NombreControl,
                    TextoPorDefecto = c.TextoTraducido,
                    TextoTraducido = string.Empty
                }).ToList();

        }

        
    }
}
