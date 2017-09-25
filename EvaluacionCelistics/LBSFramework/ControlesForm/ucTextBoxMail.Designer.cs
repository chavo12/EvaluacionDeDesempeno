namespace LBSFramework.ControlesForm
{
    partial class ucTextBoxMail
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
            this.txtMail = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMail
            // 
            this.txtMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMail.Location = new System.Drawing.Point(0, 0);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(315, 20);
            this.txtMail.TabIndex = 0;
            this.txtMail.TextChanged += new System.EventHandler(this.txtMail_TextChanged);
            this.txtMail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMail_KeyPress);
            this.txtMail.Validating += new System.ComponentModel.CancelEventHandler(this.txtMail_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucTextBoxMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMail);
            this.Name = "ucTextBoxMail";
            this.Size = new System.Drawing.Size(315, 20);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
