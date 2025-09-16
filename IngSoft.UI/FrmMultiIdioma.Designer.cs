namespace IngSoft.UI
{
    partial class FrmMultiIdioma
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
            this.LblTituloMultiIdioma = new System.Windows.Forms.Label();
            this.cboIdiomas = new System.Windows.Forms.ComboBox();
            this.LblIdioma = new System.Windows.Forms.Label();
            this.gridTraduccion = new System.Windows.Forms.DataGridView();
            this.btnGuardarTraduccion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraduccion)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTituloMultiIdioma
            // 
            this.LblTituloMultiIdioma.AutoSize = true;
            this.LblTituloMultiIdioma.Location = new System.Drawing.Point(318, 19);
            this.LblTituloMultiIdioma.Name = "LblTituloMultiIdioma";
            this.LblTituloMultiIdioma.Size = new System.Drawing.Size(41, 15);
            this.LblTituloMultiIdioma.TabIndex = 0;
            this.LblTituloMultiIdioma.Text = "label1";
            this.LblTituloMultiIdioma.Click += new System.EventHandler(this.label1_Click);
            // 
            // cboIdiomas
            // 
            this.cboIdiomas.FormattingEnabled = true;
            this.cboIdiomas.Location = new System.Drawing.Point(27, 84);
            this.cboIdiomas.Name = "cboIdiomas";
            this.cboIdiomas.Size = new System.Drawing.Size(121, 24);
            this.cboIdiomas.TabIndex = 1;
            this.cboIdiomas.SelectedIndexChanged += new System.EventHandler(this.cboIdiomas_SelectedIndexChanged);
            // 
            // LblIdioma
            // 
            this.LblIdioma.AutoSize = true;
            this.LblIdioma.Location = new System.Drawing.Point(24, 47);
            this.LblIdioma.Name = "LblIdioma";
            this.LblIdioma.Size = new System.Drawing.Size(41, 15);
            this.LblIdioma.TabIndex = 2;
            this.LblIdioma.Text = "label1";
            // 
            // gridTraduccion
            // 
            this.gridTraduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTraduccion.Location = new System.Drawing.Point(27, 141);
            this.gridTraduccion.Name = "gridTraduccion";
            this.gridTraduccion.RowHeadersWidth = 51;
            this.gridTraduccion.Size = new System.Drawing.Size(737, 255);
            this.gridTraduccion.TabIndex = 3;
            this.gridTraduccion.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridTraduccion_DataBindingComplete);
            // 
            // btnGuardarTraduccion
            // 
            this.btnGuardarTraduccion.Location = new System.Drawing.Point(658, 402);
            this.btnGuardarTraduccion.Name = "btnGuardarTraduccion";
            this.btnGuardarTraduccion.Size = new System.Drawing.Size(106, 36);
            this.btnGuardarTraduccion.TabIndex = 4;
            this.btnGuardarTraduccion.Text = "button1";
            this.btnGuardarTraduccion.UseVisualStyleBackColor = true;
            this.btnGuardarTraduccion.Click += new System.EventHandler(this.btnGuardarTraduccion_Click);
            // 
            // FrmMultiIdioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGuardarTraduccion);
            this.Controls.Add(this.gridTraduccion);
            this.Controls.Add(this.LblIdioma);
            this.Controls.Add(this.cboIdiomas);
            this.Controls.Add(this.LblTituloMultiIdioma);
            this.Name = "FrmMultiIdioma";
            this.Text = "FrmMultiIdioma";
            this.Load += new System.EventHandler(this.FrmMultiIdioma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTraduccion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblTituloMultiIdioma;
        private System.Windows.Forms.ComboBox cboIdiomas;
        private System.Windows.Forms.Label LblIdioma;
        private System.Windows.Forms.DataGridView gridTraduccion;
        private System.Windows.Forms.Button btnGuardarTraduccion;
    }
}