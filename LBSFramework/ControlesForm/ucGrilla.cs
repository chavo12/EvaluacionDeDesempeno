using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using LBSFramework.Entitys;

namespace LBSFramework.ControlesForm
{
    [Serializable]
    public partial class ucGrilla : UserControl
    {
        
        public event EventHandler ucEditar;
        public event EventHandler ucBorrar;


        public ucGrilla()
        {
            InitializeComponent();
        }

        public void CargarGrilla(IList oLista)
        {
            //No muestra columna de seleccion de Fila
            dtgGrilla.RowHeadersVisible = false;

            DataGridViewImageColumn cEditar = new DataGridViewImageColumn();
            cEditar.Name = "Editar";
            cEditar.HeaderText = "";
            cEditar.Frozen = true;
            cEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dtgGrilla.Columns.Add(cEditar);


            //Imagen como boton Eliminar
            DataGridViewImageColumn Del = new DataGridViewImageColumn();
            Del.Name = "Borrar";
            Del.HeaderText = "";
            Del.Frozen = true;
            Del.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dtgGrilla.Columns.Add(Del);

            //Lleno la Lista
            dtgGrilla.DataSource = oLista;
            AgregarEstiloColumna();
            ActualizarOperacionesCeldas();
        }

        private void ActualizarOperacionesCeldas()
        {

            foreach (DataGridViewRow  r in dtgGrilla.Rows)
            {
                //Imagen como boton Editar
                DataGridViewImageCell cell = (DataGridViewImageCell)r.Cells["Editar"];
                
                if ((bool)r.Cells["Editable"].Value)
                    cell.Value= Properties.Resources.EditarEnable16;
                else
                    cell.Value = Properties.Resources.EditarDisable16;

                //Imagen como boton Editar
                DataGridViewImageCell cellDel = (DataGridViewImageCell)r.Cells["Borrar"];

                if ((bool)r.Cells["Borrable"].Value)
                    cellDel.Value = Properties.Resources.Borrar16;
                else
                    cellDel.Value = Properties.Resources.Borrar16Disable;
                                
            }
        }

        public void AgregarEstiloColumna()
        {
            dtgGrilla.Columns["Editable"].Visible = false;
            dtgGrilla.Columns["Borrable"].Visible = false;
            dtgGrilla.Columns["Identificador"].Visible = false;

       
        }

        public void EstiloColumna(string oNombreCol, bool oVisible=true, string oNombreVisible="", DataGridViewAutoSizeColumnMode oAncho= DataGridViewAutoSizeColumnMode.None)
        {
            if (dtgGrilla.Columns[oNombreCol] != null)
            {
                dtgGrilla.Columns[oNombreCol].Visible = oVisible;
                dtgGrilla.Columns[oNombreCol].HeaderText = (oNombreVisible =="")? oNombreCol : oNombreVisible;
                dtgGrilla.Columns[oNombreCol].AutoSizeMode = oAncho;
            }
        }

        private void dtgGrilla_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if ((bool)dtgGrilla["Editable", e.RowIndex].Value == false)
                { 
                    
                }
            }
        }

        private void dtgGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             //Obtengo el indice de la columna con la imagen y evaluo si fue la columna que selecciono
            if ((dtgGrilla.Columns["Editar"].Index == e.ColumnIndex) && (bool)dtgGrilla.CurrentRow.Cells["Editable"].Value)
            {
               //BonoEmitido bon =  (BonoEmitido)dtgListaBonos.CurrentRow.DataBoundItem;
                if (ucEditar != null)
                {
                    ucEditar(dtgGrilla.CurrentRow.DataBoundItem, e);
                }
            }

             //Obtengo el indice de la columna con la imagen y evaluo si fue la columna que selecciono
            if (dtgGrilla.Columns["Borrar"].Index == e.ColumnIndex && (bool)dtgGrilla.CurrentRow.Cells["Borrable"].Value)
            {
                //BonoEmitido bon =  (BonoEmitido)dtgListaBonos.CurrentRow.DataBoundItem;
                if (ucBorrar != null)
                {
                    ucBorrar(dtgGrilla.CurrentRow.DataBoundItem, e);
                }
            }
        }

    }

}
