namespace LBSFramework.ControlesForm
{
    partial class ucTextBoxEmail
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
            this.txtNumerico = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumerico
            // 
            this.txtNumerico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumerico.Location = new System.Drawing.Point(0, 0);
            this.txtNumerico.Name = "txtNumerico";
            this.txtNumerico.Size = new System.Drawing.Size(212, 20);
            this.txtNumerico.TabIndex = 0;
            this.txtNumerico.TextChanged += new System.EventHandler(this.txtNumerico_TextChanged);
            this.txtNumerico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumerico_KeyPress);
            this.txtNumerico.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumerico_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucTextBoxEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNumerico);
            this.Name = "ucTextBoxEmail";
            this.Size = new System.Drawing.Size(212, 20);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumerico;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
