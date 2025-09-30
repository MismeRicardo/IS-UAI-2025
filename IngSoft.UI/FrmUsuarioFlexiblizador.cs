using IngSoft.ApplicationServices;
using IngSoft.ApplicationServices.Factory;
using IngSoft.Domain;
using IngSoft.Services;
using IngSoft.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IngSoft.UI
{
    internal static class FrmUsuarioFlexiblizador
    {
        // Delegado para registrar en la bitácora
        private static void RegistrarEnBitacora(Usuario usuario, string descripcion, string origen, TipoEvento tipoEvento)
        {
            IBitacoraServices bitacoraServices = ServicesFactory.CreateBitacoraServices();
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

        internal static void TextBoxCreator(string param, Point position)
        {
            Label labelUsuario = new Label
            {
                Name = $"lbl{param}",
                Location = new Point(position.X, position.Y - 20),
                Size = new Size(200, 20),
                Text = param,
                Visible = true
            };
            TextBox txtUsuario = new TextBox
            {
                Name = $"txt{param}",
                Location = new Point(position.X, position.Y),
                Size = new Size(200, 30),
                Text = "",
                Visible = true,
                Enabled = true,
                ReadOnly = false,
                Font = new Font("Arial", 10)
            };

            if (FrmUsuario.ActiveForm.Controls.Find(txtUsuario.Name, true).Length == 0)
            {
                FrmUsuario.ActiveForm.Controls.Add(labelUsuario);
                FrmUsuario.ActiveForm.Controls.Add(txtUsuario);
            }
            else if (FrmUsuario.ActiveForm.Controls.Find(txtUsuario.Name, true).Length > 0)
            {
                var existingTextBox = FrmUsuario.ActiveForm.Controls.Find(txtUsuario.Name, true).FirstOrDefault() as TextBox;
                var existingLabel = FrmUsuario.ActiveForm.Controls.Find(labelUsuario.Name, true).FirstOrDefault() as Label;
                FrmUsuario.ActiveForm.Controls.Remove(existingTextBox);
                FrmUsuario.ActiveForm.Controls.Remove(existingLabel);
                FrmUsuario.ActiveForm.Controls.Add(labelUsuario);
                FrmUsuario.ActiveForm.Controls.Add(txtUsuario);


            }
        }

        private static void btnGuardar_Click(object sender, EventArgs e)
        {
            // Lógica para manejar el evento de clic del botón Guardar Usuario
            IUsuarioServices usuarioServices = ServicesFactory.CreateUsuarioServices();
            // Inyecta el delegado al servicio
           usuarioServices.SetRegistradoBitacora(RegistrarEnBitacora);
            try
            {
                usuarioServices.GuardarUsuario(new Usuario
                {
                    IdUsuario = 0,
                    Username = FrmUsuario.ActiveForm.Controls.Find("txtUsuario", true).FirstOrDefault() is TextBox txtUsuario ? txtUsuario.Text : string.Empty,
                    Nombre = FrmUsuario.ActiveForm.Controls.Find("txtNombre", true).FirstOrDefault() is TextBox txtNombre ? txtNombre.Text : string.Empty,
                    Apellido = FrmUsuario.ActiveForm.Controls.Find("txtApellido", true).FirstOrDefault() is TextBox txtApellido ? txtApellido.Text : string.Empty,
                    Email = FrmUsuario.ActiveForm.Controls.Find("txtEmail", true).FirstOrDefault() is TextBox txtEmail ? txtEmail.Text : string.Empty,
                    Contrasena = FrmUsuario.ActiveForm.Controls.Find("txtContraseña", true).FirstOrDefault() is TextBox txtContraseña ? txtContraseña.Text : string.Empty
                });
                MessageBox.Show("Usuario guardado con éxito.");
                new FrmUsuario().EliminarControlesAdicionalesUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el usuario: {ex.Message}");
            }
        }
        private static void BtnLogin_Click(object sender, EventArgs e)
        {
            IUsuarioServices usuarioServices = ServicesFactory.CreateUsuarioServices();
            // Inyecta el delegado al servicio
            usuarioServices.SetRegistradoBitacora(RegistrarEnBitacora);


            Usuario mUsuarioActual = new Usuario
            {
                Username = FrmUsuario.ActiveForm.Controls.Find("txtUsuario", true).FirstOrDefault() is TextBox txtUsuario ? txtUsuario.Text : string.Empty,
                Contrasena = FrmUsuario.ActiveForm.Controls.Find("txtContraseña", true).FirstOrDefault() is TextBox txtContraseña ? txtContraseña.Text : string.Empty
            };
            try
            {
                usuarioServices.LoginUser(mUsuarioActual);
                MessageBox.Show($"Iniciado sesion: {SessionManager.GetUsuario().UserName}");
                new FrmUsuario().EliminarControlesAdicionalesUsuario();
            }
            catch(UnauthorizedAccessException UnAcExc)
            {
                MessageBox.Show($"Error de autenticación: {UnAcExc.Message}");
            }
            catch (Exception ex) {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
            }
        }
        internal static void GuardarUsuarioButtonCreator()
        {
            Button btnGuardar = new Button
            {
                Name = "btnGuardarUsuario",
                Location = new Point((FrmUsuario.ActiveForm.Width / 2) - 100, (FrmUsuario.ActiveForm.Height / 2 + FrmUsuario.ActiveForm.Height / 4)),
                Size = new Size(200, 30),
                Text = "Guardar Usuario",
                Visible = true,
                Enabled = true
            };
            btnGuardar.Click += btnGuardar_Click;
            if (FrmUsuario.ActiveForm.Controls.Find(btnGuardar.Name, true).Length == 0)
            {
                FrmUsuario.ActiveForm.Controls.Add(btnGuardar);
            }
            else if (FrmUsuario.ActiveForm.Controls.Find(btnGuardar.Name, true).Length > 0)
            {
                var existingButton = FrmUsuario.ActiveForm.Controls.Find(btnGuardar.Name, true).FirstOrDefault() as Button;
                FrmUsuario.ActiveForm.Controls.Remove(existingButton);
                FrmUsuario.ActiveForm.Controls.Add(btnGuardar);
            }
        }
        internal static void LoginButtonCreator()
        {
            Button btnLogin = new Button
            {
                Name = "btnLogin",
                Location = new Point((FrmUsuario.ActiveForm.Width / 2) - 100, (FrmUsuario.ActiveForm.Height / 2 + FrmUsuario.ActiveForm.Height / 4)),
                Size = new Size(200, 30),
                Text = "Login",
                Visible = true,
                Enabled = true
            };
            btnLogin.Click += BtnLogin_Click;
            if (FrmUsuario.ActiveForm.Controls.Find(btnLogin.Name, true).Length == 0)
            {
                FrmUsuario.ActiveForm.Controls.Add(btnLogin);
            }
            else if (FrmUsuario.ActiveForm.Controls.Find(btnLogin.Name, true).Length > 0)
            {
                var existingButton = FrmUsuario.ActiveForm.Controls.Find(btnLogin.Name, true).FirstOrDefault() as Button;
                FrmUsuario.ActiveForm.Controls.Remove(existingButton);
                FrmUsuario.ActiveForm.Controls.Add(btnLogin);
            }
        }
        internal static void DataGridViewUsuarioCreator(List<Usuario> pUsuarios)
        {
            DataGridView dataGridViewUsuarios = new DataGridView
            {
                Name = "dataGridViewUsuarios",
                Location = new Point(FrmUsuario.ActiveForm.Width / 8, FrmUsuario.ActiveForm.Height / 8),
                Size = new Size(FrmUsuario.ActiveForm.Width / 2 + FrmUsuario.ActiveForm.Width / 4, FrmUsuario.ActiveForm.Height / 2 + FrmUsuario.ActiveForm.Height / 4),
                Visible = true,
                Enabled = true,
            };


            //transformar la lista de usuarios en un DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Guid));
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("Contrasena", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Nombre", typeof(string));
            dataTable.Columns.Add("Apellido", typeof(string));
            dataTable.Columns.Add("Bloqueado", typeof(bool));
            dataTable.Columns.Add("CantidadIntentos", typeof(int));
            foreach (var usuario in pUsuarios)
            {
                dataTable.Rows.Add(usuario.Id, usuario.Username, usuario.Contrasena, usuario.Email, usuario.Nombre, usuario.Apellido, usuario.Bloqueado, usuario.CantidadIntentos);
            }

            dataGridViewUsuarios.DataSource = dataTable;

            //dataGridViewUsuarios.DataSource = null; 
            //dataGridViewUsuarios.DataSource = pUsuarios;

            dataGridViewUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsuarios.MultiSelect = false;
            dataGridViewUsuarios.ReadOnly = true;
            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.AllowUserToDeleteRows = false;
            dataGridViewUsuarios.AllowUserToOrderColumns = true;
            dataGridViewUsuarios.RowHeadersVisible = true;

            //var columna = dataGridViewUsuarios.Columns;

            //dataGridViewUsuarios.Columns["IdBitacora"].Visible = false;
            var dataGridView = FrmUsuario.ActiveForm.Controls.Find(dataGridViewUsuarios.Name, true);
            if (dataGridView.Length == 0)
            {
                FrmUsuario.ActiveForm.Controls.Add(dataGridViewUsuarios);
            }
            else if (dataGridView.Length > 0)
            {
                var existingDataGridView = dataGridView.FirstOrDefault() as DataGridView;
                FrmUsuario.ActiveForm.Controls.Remove(existingDataGridView);
                FrmUsuario.ActiveForm.Controls.Add(dataGridViewUsuarios);
            }
        }

    }
}
