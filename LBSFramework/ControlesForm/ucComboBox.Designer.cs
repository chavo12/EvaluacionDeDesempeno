namespace LBSFramework.ControlesForm
{
    partial class ucComboBox
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
            this.cmbCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbCombo
            // 
            this.cmbCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCombo.FormattingEnabled = true;
            this.cmbCombo.Location = new System.Drawing.Point(0, 0);
            this.cmbCombo.Name = "cmbCombo";
            this.cmbCombo.Size = new System.Drawing.Size(174, 21);
            this.cmbCombo.TabIndex = 0;
            this.cmbCombo.SelectedValueChanged += new System.EventHandler(this.cmbCombo_SelectedValueChanged);
            // 
            // ucComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCombo);
            this.Name = "ucComboBox";
            this.Size = new System.Drawing.Size(174, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCombo;
    }
}
