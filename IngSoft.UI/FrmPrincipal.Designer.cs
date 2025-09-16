namespace IngSoft.UI
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboIdiomas = new System.Windows.Forms.ComboBox();
            this.menuStripPrincipal = new System.Windows.Forms.MenuStrip();
            this.sesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboIdiomas
            // 
            this.cboIdiomas.FormattingEnabled = true;
            this.cboIdiomas.Location = new System.Drawing.Point(820, 2);
            this.cboIdiomas.Name = "cboIdiomas";
            this.cboIdiomas.Size = new System.Drawing.Size(69, 24);
            this.cboIdiomas.TabIndex = 4;
            this.cboIdiomas.SelectedIndexChanged += new System.EventHandler(this.cboIdiomas_SelectedIndexChanged);
            // 
            // menuStripPrincipal
            // 
            this.menuStripPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesionToolStripMenuItem,
            this.usuariosToolStripMenuItem,
            this.bitacoraToolStripMenuItem,
            this.multiIdiomaToolStripMenuItem});
            this.menuStripPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuStripPrincipal.Name = "menuStripPrincipal";
            this.menuStripPrincipal.Size = new System.Drawing.Size(901, 28);
            this.menuStripPrincipal.TabIndex = 5;
            this.menuStripPrincipal.Text = "menuStrip1";
            // 
            // sesionToolStripMenuItem
            // 
            this.sesionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesionToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem});
            this.sesionToolStripMenuItem.Name = "sesionToolStripMenuItem";
            this.sesionToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.sesionToolStripMenuItem.Text = "Sesion";
            // 
            // iniciarSesionToolStripMenuItem
            // 
            this.iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            this.iniciarSesionToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.iniciarSesionToolStripMenuItem.Text = "Iniciar Sesion";
            this.iniciarSesionToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesionToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // bitacoraToolStripMenuItem
            // 
            this.bitacoraToolStripMenuItem.Name = "bitacoraToolStripMenuItem";
            this.bitacoraToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.bitacoraToolStripMenuItem.Text = "Bitacora";
            this.bitacoraToolStripMenuItem.Click += new System.EventHandler(this.bitacoraToolStripMenuItem_Click);
            // 
            // multiIdiomaToolStripMenuItem
            // 
            this.multiIdiomaToolStripMenuItem.Name = "multiIdiomaToolStripMenuItem";
            this.multiIdiomaToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.multiIdiomaToolStripMenuItem.Text = "MultiIdioma";
            this.multiIdiomaToolStripMenuItem.Click += new System.EventHandler(this.multiIdiomaToolStripMenuItem_Click);
            // 
            // FrmPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(901, 545);
            this.Controls.Add(this.cboIdiomas);
            this.Controls.Add(this.menuStripPrincipal);
            this.MainMenuStrip = this.menuStripPrincipal;
            this.Name = "FrmPrincipal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStripPrincipal.ResumeLayout(false);
            this.menuStripPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboIdiomas;
        private System.Windows.Forms.MenuStrip menuStripPrincipal;
        private System.Windows.Forms.ToolStripMenuItem sesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiIdiomaToolStripMenuItem;
    }
}

