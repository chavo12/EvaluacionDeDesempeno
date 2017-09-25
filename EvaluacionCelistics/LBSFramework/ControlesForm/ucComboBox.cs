using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace LBSFramework.ControlesForm
{
    public partial class ucComboBox : UserControl
    {

        public event EventHandler ucSelectedChange;

        #region Propiedades
        /********************************************************/
	
        private string _displayMember;
        private string _valueMember="Identificador";


        public string ValueMember
        {
            get { return _valueMember; }
            set { _valueMember = value; }
        }

        /// <summary>
        /// Valor que se Muestra
        /// </summary>
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        /********************************************************/
        #endregion Fin Propiedades
		
        /// <summary>
        /// Constructor que inicializa los componentes
        /// </summary>
        public ucComboBox()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Carga el Combo con la Lista Recibida
        /// </summary>
        /// <param name="oLista"></param>
        public void CargarCombo(IList oLista)
        {
            try
            {
                cmbCombo.ValueMember = _valueMember;
                cmbCombo.DisplayMember = _displayMember;
                //Lleno el Combo            
                cmbCombo.DataSource = oLista;
                cmbCombo.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Retorna el Objeto Seleccionado
        /// </summary>
        /// <returns></returns>
        public object ItemSeleccionado()
        {
            return cmbCombo.SelectedValue;
        }

        /// <summary>
        /// Evento que cuando se cambia la Seleccion retorna el Objeto Seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if(ucSelectedChange!=null )
                ucSelectedChange(cmbCombo.SelectedValue,e);
        }
       


    }
}
