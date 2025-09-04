namespace IngSoft.UI
{
    partial class FrmBitacora
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridBitacora = new System.Windows.Forms.DataGridView();
            this.btnAddMessage = new System.Windows.Forms.Button();
            this.btnAddWarning = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.txtBusquedaBitacora = new System.Windows.Forms.TextBox();
            this.lblBitacora = new System.Windows.Forms.Label();
            this.cboTipoEvento = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkFiltroDate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBitacora
            // 
            this.gridBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBitacora.Location = new System.Drawing.Point(37, 87);
            this.gridBitacora.Name = "gridBitacora";
            this.gridBitacora.RowHeadersWidth = 51;
            this.gridBitacora.RowTemplate.Height = 24;
            this.gridBitacora.Size = new System.Drawing.Size(814, 483);
            this.gridBitacora.TabIndex = 0;
            // 
            // btnAddMessage
            // 
            this.btnAddMessage.Location = new System.Drawing.Point(857, 50);
            this.btnAddMessage.Name = "btnAddMessage";
            this.btnAddMessage.Size = new System.Drawing.Size(154, 31);
            this.btnAddMessage.TabIndex = 1;
            this.btnAddMessage.Text = "Agregar Message";
            this.btnAddMessage.UseVisualStyleBackColor = true;
            this.btnAddMessage.Click += new System.EventHandler(this.btnAddMessage_Click);
            // 
            // btnAddWarning
            // 
            this.btnAddWarning.Location = new System.Drawing.Point(857, 123);
            this.btnAddWarning.Name = "btnAddWarning";
            this.btnAddWarning.Size = new System.Drawing.Size(154, 31);
            this.btnAddWarning.TabIndex = 2;
            this.btnAddWarning.Text = "Agregar Warning";
            this.btnAddWarning.UseVisualStyleBackColor = true;
            this.btnAddWarning.Click += new System.EventHandler(this.btnAddWarning_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(857, 187);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(154, 31);
            this.btnError.TabIndex = 3;
            this.btnError.Text = "Agregar Error";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // txtBusquedaBitacora
            // 
            this.txtBusquedaBitacora.Location = new System.Drawing.Point(37, 44);
            this.txtBusquedaBitacora.Multiline = true;
            this.txtBusquedaBitacora.Name = "txtBusquedaBitacora";
            this.txtBusquedaBitacora.Size = new System.Drawing.Size(530, 30);
            this.txtBusquedaBitacora.TabIndex = 4;
            this.txtBusquedaBitacora.TextChanged += new System.EventHandler(this.txtBusquedaBitacora_TextChanged);
            // 
            // lblBitacora
            // 
            this.lblBitacora.AutoSize = true;
            this.lblBitacora.Location = new System.Drawing.Point(37, 13);
            this.lblBitacora.Name = "lblBitacora";
            this.lblBitacora.Size = new System.Drawing.Size(73, 15);
            this.lblBitacora.TabIndex = 5;
            this.lblBitacora.Text = "BITACORAS";
            // 
            // cboTipoEvento
            // 
            this.cboTipoEvento.FormattingEnabled = true;
            this.cboTipoEvento.Items.AddRange(new object[] {
            "Todos",
            "Message",
            "Warning",
            "Error"});
            this.cboTipoEvento.Location = new System.Drawing.Point(582, 50);
            this.cboTipoEvento.Name = "cboTipoEvento";
            this.cboTipoEvento.Size = new System.Drawing.Size(117, 24);
            this.cboTipoEvento.TabIndex = 6;
            this.cboTipoEvento.SelectedIndexChanged += new System.EventHandler(this.cboTipoEvento_SelectedIndexChanged);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(713, 50);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(138, 20);
            this.dtpFecha.TabIndex = 7;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(590, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tipo Evento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(710, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha Creacion";
            // 
            // chkFiltroDate
            // 
            this.chkFiltroDate.AutoSize = true;
            this.chkFiltroDate.Location = new System.Drawing.Point(809, 31);
            this.chkFiltroDate.Name = "chkFiltroDate";
            this.chkFiltroDate.Size = new System.Drawing.Size(18, 17);
            this.chkFiltroDate.TabIndex = 10;
            this.chkFiltroDate.UseVisualStyleBackColor = true;
            this.chkFiltroDate.CheckedChanged += new System.EventHandler(this.chkFiltroDate_CheckedChanged);
            // 
            // FrmBitacora
            // 
            this.ClientSize = new System.Drawing.Size(1041, 597);
            this.Controls.Add(this.chkFiltroDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.cboTipoEvento);
            this.Controls.Add(this.lblBitacora);
            this.Controls.Add(this.txtBusquedaBitacora);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnAddWarning);
            this.Controls.Add(this.btnAddMessage);
            this.Controls.Add(this.gridBitacora);
            this.Name = "FrmBitacora";
            this.Load += new System.EventHandler(this.FrmBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridBitacora;
        private System.Windows.Forms.Button btnAddMessage;
        private System.Windows.Forms.Button btnAddWarning;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.TextBox txtBusquedaBitacora;
        private System.Windows.Forms.Label lblBitacora;
        private System.Windows.Forms.ComboBox cboTipoEvento;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkFiltroDate;
    }
}
