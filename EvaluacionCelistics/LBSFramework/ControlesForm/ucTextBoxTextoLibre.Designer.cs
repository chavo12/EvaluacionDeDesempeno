namespace LBSFramework.ControlesForm
{
    partial class ucTextBoxTextoLibre
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
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTexto.Location = new System.Drawing.Point(0, 0);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(250, 20);
            this.txtTexto.TabIndex = 0;
            this.txtTexto.TextChanged += new System.EventHandler(this.txtTexto_TextChanged);
            this.txtTexto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTexto_KeyPress);
            this.txtTexto.Validating += new System.ComponentModel.CancelEventHandler(this.txtTexto_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucTextBoxTextoLibre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTexto);
            this.Name = "ucTextBoxTextoLibre";
            this.Size = new System.Drawing.Size(250, 20);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
