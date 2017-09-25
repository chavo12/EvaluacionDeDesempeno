namespace LBSFramework.ControlesForm
{
    partial class ucGrilla
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
            this.dtgGrilla = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGrilla)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgGrilla
            // 
            this.dtgGrilla.AllowUserToAddRows = false;
            this.dtgGrilla.AllowUserToDeleteRows = false;
            this.dtgGrilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgGrilla.Location = new System.Drawing.Point(0, 0);
            this.dtgGrilla.Name = "dtgGrilla";
            this.dtgGrilla.ReadOnly = true;
            this.dtgGrilla.Size = new System.Drawing.Size(609, 341);
            this.dtgGrilla.TabIndex = 0;
            this.dtgGrilla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgGrilla_CellClick);
            this.dtgGrilla.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dtgGrilla_RowPrePaint);
            // 
            // ucGrilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtgGrilla);
            this.Name = "ucGrilla";
            this.Size = new System.Drawing.Size(609, 341);
            ((System.ComponentModel.ISupportInitialize)(this.dtgGrilla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgGrilla;
    }
}
