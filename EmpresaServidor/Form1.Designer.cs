
namespace EmpresaServidor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxCombustible = new System.Windows.Forms.ComboBox();
            this.numericPrecio = new System.Windows.Forms.NumericUpDown();
            this.btnPrecio = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrecio)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxCombustible
            // 
            this.comboBoxCombustible.FormattingEnabled = true;
            this.comboBoxCombustible.Location = new System.Drawing.Point(6, 22);
            this.comboBoxCombustible.Name = "comboBoxCombustible";
            this.comboBoxCombustible.Size = new System.Drawing.Size(121, 23);
            this.comboBoxCombustible.TabIndex = 0;
            this.comboBoxCombustible.Text = "Combustible";
            this.comboBoxCombustible.SelectedIndexChanged += new System.EventHandler(this.comboBoxCombustible_SelectedIndexChanged);
            // 
            // numericPrecio
            // 
            this.numericPrecio.Location = new System.Drawing.Point(7, 51);
            this.numericPrecio.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericPrecio.Name = "numericPrecio";
            this.numericPrecio.Size = new System.Drawing.Size(120, 23);
            this.numericPrecio.TabIndex = 1;
            // 
            // btnPrecio
            // 
            this.btnPrecio.Location = new System.Drawing.Point(7, 80);
            this.btnPrecio.Name = "btnPrecio";
            this.btnPrecio.Size = new System.Drawing.Size(120, 23);
            this.btnPrecio.TabIndex = 2;
            this.btnPrecio.Text = "Cambiar Precio";
            this.btnPrecio.UseVisualStyleBackColor = true;
            this.btnPrecio.Click += new System.EventHandler(this.btnPrecio_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.Location = new System.Drawing.Point(7, 109);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(120, 23);
            this.btnReporte.TabIndex = 3;
            this.btnReporte.Text = "Solicitar Reporte";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCombustible);
            this.groupBox1.Controls.Add(this.btnReporte);
            this.groupBox1.Controls.Add(this.numericPrecio);
            this.groupBox1.Controls.Add(this.btnPrecio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 142);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnConectar);
            this.groupBox2.Controls.Add(this.textBoxIp);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 88);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(16, 51);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(95, 23);
            this.btnConectar.TabIndex = 1;
            this.btnConectar.Text = "Iniciar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(7, 22);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(120, 23);
            this.textBoxIp.TabIndex = 0;
            this.textBoxIp.Text = "IP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(161, 261);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericPrecio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCombustible;
        private System.Windows.Forms.NumericUpDown numericPrecio;
        private System.Windows.Forms.Button btnPrecio;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox textBoxIp;
    }
}

