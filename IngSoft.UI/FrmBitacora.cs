using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Domain;
using IngSoft.Domain.Enums;
using IngSoft.UI.DTOs;

namespace IngSoft.UI
{
    public partial class FrmBitacora : Form
    {
        private readonly IBitacoraServices _bitacoraServices;
        private List<Bitacora> _bitacoras;
        public FrmBitacora()
        {
            InitializeComponent();
            _bitacoraServices = ServicesFactory.CreateBitacoraServices();
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            CargarBitacora();
        }

        private void btnAddMessage_Click(object sender, EventArgs e)
        {
            var bitacora = new Bitacora {
                Id = Guid.NewGuid(),
                Usuario = new Usuario { IdUsuario = 1},
                Fecha = DateTime.Now,
                Descripcion = "Bitacora creada del tipo Message",
                Origen = "FrmBitacora",
                TipoEvento = TipoEvento.Message
            };

            _bitacoraServices.GuardarBitacora(bitacora);
            CargarBitacora();
        }

        private void btnAddWarning_Click(object sender, EventArgs e)
        {

            var bitacora = new Bitacora
            {
                Id = Guid.NewGuid(),
                Usuario = new Usuario { IdUsuario = 2 },
                Fecha = DateTime.Now,
                Descripcion = "Bitacora creada del tipo Warning",
                Origen = "FrmBitacora",
                TipoEvento = TipoEvento.Warning
            };

            _bitacoraServices.GuardarBitacora(bitacora);
            CargarBitacora();
        }

        private void btnError_Click(object sender, EventArgs e)
        {

            var bitacora = new Bitacora
            {
                Id = Guid.NewGuid(),
                Usuario = new Usuario { IdUsuario = 1 },
                Fecha = DateTime.Now,
                Descripcion = "Bitacora creada del tipo Error",
                Origen = "FrmBitacora",
                TipoEvento = TipoEvento.Error
            };

            _bitacoraServices.GuardarBitacora(bitacora);
            CargarBitacora();
        }

        private void txtBusquedaBitacora_TextChanged(object sender, EventArgs e)
        {
            var filtro = txtBusquedaBitacora.Text.Trim();

            if (filtro.Length > 3)
            {
                CargarBitacora(filtro);
            }
            else 
            {
                CargarBitacora();
            }
        }

        private void cboTipoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTipoEvento = cboTipoEvento.SelectedItem.ToString();

            if(selectedTipoEvento != "Todos") 
            {
                var filteredBitacoras = _bitacoras.Where(b => b.TipoEvento.ToString() == selectedTipoEvento).ToList();

                gridBitacora.DataSource = BuildBitacorasDataGridView(filteredBitacoras);
            }
            else 
            {
                CargarBitacora();
            }            
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaHasta.Enabled = true;

            var fechaDesde = dtpFechaDesde.Value.Date;
            var fechaHasta = dtpFechaHasta.Value.Date;

            if (fechaHasta < fechaDesde)
            {
                MessageBox.Show("La fecha hasta no puede ser menor a la fecha desde.", "Error de fecha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaHasta.Value = fechaDesde;
                CargarBitacora();
            }

            var filteredBitacoras = _bitacoras.Where(b => b.Fecha.Date >= fechaDesde && b.Fecha.Date <= fechaHasta).ToList();
            gridBitacora.DataSource = BuildBitacorasDataGridView(filteredBitacoras);
        }

        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            var fechaDesde = dtpFechaDesde.Value.Date;
            var fechaHasta = dtpFechaHasta.Value.Date;

            if(fechaHasta < fechaDesde)
            {
                MessageBox.Show("La fecha hasta no puede ser menor a la fecha desde.", "Error de fecha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaHasta.Value = fechaDesde;
                CargarBitacora();
            }

            var filteredBitacoras = _bitacoras.Where(b => b.Fecha.Date >= fechaDesde && b.Fecha.Date <= fechaHasta).ToList();
            gridBitacora.DataSource = BuildBitacorasDataGridView(filteredBitacoras);
        }

        private void chkFiltroDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltroDate.Checked)
            {
                dtpFechaDesde.Enabled = true;
            }
            else
            {
                dtpFechaDesde.Enabled = false;
                CargarBitacora();
            }
        }

        private void CargarBitacora(string filtro = null)
        {
            if (filtro != null)
            {
                _bitacoras = _bitacoraServices.ObtenerBitacorasFiltradas(filtro);
            }
            else
            {
                _bitacoras = _bitacoraServices.ObtenerBitacoras();
            }

            gridBitacora.DataSource = BuildBitacorasDataGridView(_bitacoras);
        }

        private List<BitacoraGridDto> BuildBitacorasDataGridView(List<Bitacora> bitacoras) 
        {
            return bitacoras.Select(b => new BitacoraGridDto
            {
                Fecha = b.Fecha,
                Descripcion = b.Descripcion,
                Origen = b.Origen,
                TipoEvento = b.TipoEvento.ToString(),
                Usuario = b.Usuario.UserName
            })
            .OrderByDescending(b => b.Fecha)
            .ToList();
        }

        private void gridBitacora_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gridBitacora.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }        
    }
}
