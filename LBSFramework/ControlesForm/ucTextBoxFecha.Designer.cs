namespace LBSFramework.ControlesForm
{
    partial class ucTextBoxFecha
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(0, 0);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(93, 20);
            this.txtFecha.TabIndex = 0;
            this.txtFecha.TextChanged += new System.EventHandler(this.txtFecha_TextChanged);
            this.txtFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFecha_KeyPress);
            this.txtFecha.Validating += new System.ComponentModel.CancelEventHandler(this.txtFecha_Validating);
            // 
            // dtpFecha
            // 
            this.dtpFecha.AllowDrop = true;
            this.dtpFecha.Checked = false;
            this.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(93, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(20, 20);
            this.dtpFecha.TabIndex = 1;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucTextBoxFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtFecha);
            this.Name = "ucTextBoxFecha";
            this.Size = new System.Drawing.Size(114, 20);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
