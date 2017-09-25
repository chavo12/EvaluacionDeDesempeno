using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using LBSFramework.Entitys;


namespace LBSFramework.ControlesWPF
{
    /// <summary>
    /// Lógica de interacción para ucCombo.xaml
    /// </summary>
    public partial class ucCombo : UserControl
    {
        public event EventHandler ucSelectedChange;
        public event EventHandler ucLostFocus;

        #region Propiedades
        /********************************************************/

        private string _TextoConElementos = "Seleccione una opción";
        private string _TextoSinElementos = "No hay opciones";

        private string _displayMember = "Descripcion";
        private string _valueMember = "Id";
        List<Items> _list = new List<Items>();
        private object _idAux = -1;

        public string TextoSinElementos { 
            
            get{return _TextoSinElementos; }

            set { _TextoSinElementos = value; }
        
        }

        public string TextoConElementos
        {

            get { return _TextoConElementos; }

            set { _TextoConElementos = value; }

        }


        public ComboBox oComboBox
        {

            get { return cmbCombo; }

            set { cmbCombo = value; }

        }
        

        /// <summary>
        /// Obtiene o establece el Indice del elemento seleccionado
        /// </summary>
        public int SelectedIndex
        {
            get { return _SelectedIndex(); }
            set { _SelectedIndex(value); }
        }

        public object SelectedValue
        {
            get { return _SelectedValue(); }
            set { _SelectedValue(value); }
        }

        public Items SelectedItem
        {
            get { return (Items)cmbCombo.SelectedItem; }
            set { cmbCombo.SelectedItem = (Items)value; }
        }

        /********************************************************/
        #endregion Fin Propiedades


        public ucCombo()
        {
            InitializeComponent();
           
        }


        /// <summary>
        /// Carga el Combo con la Lista Recibida
        /// </summary>
        /// <param name="oLista"></param>
        public void CargarCombo(List<Items> oLista)
        {
            try
            {
                cmbCombo.SelectedValuePath = _valueMember;
                cmbCombo.DisplayMemberPath = _displayMember;
                if (oLista != null)
                {
                    //Lleno el Combo   
                    if (oLista.Count <= 500)
                        cmbCombo.ItemsSource = oLista;
                }
                else 
                {
                    cmbCombo.ItemsSource = oLista;
                }
                
                _list = oLista;

                cmbCombo.SelectedIndex = -1;
                _idAux = -1;
                AsignarTextoCombo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        /// <summary>
        /// Evento que cuando se cambia la Seleccion retorna el Objeto Seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ucSelectedChange != null)
                ucSelectedChange(cmbCombo.SelectedValue, e);
        }

        /// <summary>
        /// Establece el Index seleccionado
        /// </summary>
        /// <param name="index"></param>
        private void _SelectedIndex(int index)
        {
            cmbCombo.SelectedIndex = index;
        }

        /// <summary>
        /// Retorna el Indice Seleccionado, si no hay seleccion retorna -1
        /// </summary>
        /// <returns></returns>
        private int _SelectedIndex()
        {
            return cmbCombo.SelectedIndex;
        }

        /// <summary>
        /// Establece el item seleccionado con el value recibido
        /// </summary>
        /// <param name="id"></param>
        private void _SelectedValue(object id)
        {
            if (id != null)
            {
                _idAux = id;
                if (_list != null && _list.Count > 500)
                {
                    cmbCombo.ItemsSource = _list.Where(i => (int)i.Id == (int)id).ToList();
                }
                switch (id.GetType().ToString())
                {
                    case "System.Int32":
                        cmbCombo.SelectedValue = (int)id;
                        break;
                    case "System.Boolean":
                        break;
                    case "System.Char":
                        break;
                    case "System.DateTime":
                        break;
                    case "System.Decimal":
                        break;
                    case "System.Double":
                        break;
                    case "System.Guid":
                        cmbCombo.SelectedValue = (Guid)id;
                        break;
                    case "System.String":
                        cmbCombo.SelectedValue = (string)id;
                        break;
                    default:
                        break;
                }//Fin Switch
               if (cmbCombo.SelectedItem != null) lblTexto.Content = ((Items)cmbCombo.SelectedItem).Descripcion;
            }
            else
            {
                limpiar();
            }
        }

        private void limpiar()
        {
            cmbCombo.SelectedIndex = -1;
            cmbCombo.ItemsSource = _list;
            AsignarTextoCombo();
        }

        private object _SelectedValue()
        {
            return cmbCombo.SelectedValue;
        }

        /// <summary>
        /// Borra la selección del combo y la deja en selectValue = null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_click(object sender, EventArgs e)
        {
            limpiar();
        }

        #region filtroCombo


        public static void FilterCombo(System.Windows.Controls.ComboBox combo, List<Items> list)
        {
            if (list != null)
            {
                string Prefix = combo.Text;
                if (((Prefix.Length > 2) && (list.Count > 500)) || (list.Count <= 500))
                {
                    combo.ItemsSource = list.Where(i => (!string.IsNullOrEmpty(i.Descripcion)) && i.Descripcion.ToLower().IndexOf(Prefix.ToLower()) > -1).ToList();
                }
                else if (list.Count > 500) combo.ItemsSource = null;
                else combo.ItemsSource = list;

            }
            else combo.ItemsSource = list;
        }

        #endregion

        private void cmbCombo_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key != System.Windows.Input.Key.Escape &&
                   e.Key != System.Windows.Input.Key.Down &&
                   e.Key != System.Windows.Input.Key.Up &&
                   e.Key != System.Windows.Input.Key.Left &&
                   e.Key != System.Windows.Input.Key.Right &&
                   e.Key != System.Windows.Input.Key.Tab &&
                   e.Key != System.Windows.Input.Key.Enter)
            {
               cmbCombo.IsDropDownOpen = true;

               FilterCombo(cmbCombo,_list);
            }
        }

        private void cmbCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ucSelectedChange != null && sender != null)
                if (((ComboBox)sender).SelectedItem != null)
                    ucSelectedChange(((Entitys.Items)((ComboBox)sender).SelectedItem).Id, e);
                else ucSelectedChange(((ComboBox)sender).SelectedValue, e);
                    
        }
       

        private void AsignarTextoCombo()
        {
            if (cmbCombo.Items.Count == 0)
            {
                cmbCombo.Text = TextoSinElementos;
            }
            else
            {
                cmbCombo.Text = TextoConElementos;
            }
        }

        private void cmbCombo_LostFocus(object sender, RoutedEventArgs e)
        {

            if (ucLostFocus != null && sender != null)
                if (((ComboBox)sender).SelectedItem != null)
                    ucLostFocus(((Entitys.Items)((ComboBox)sender).SelectedItem).Id, e);
                else ucLostFocus(((ComboBox)sender).SelectedValue, e);

        }

        private void ucComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            cmbCombo.ItemsSource = _list;
            if (cmbCombo.SelectedValue == null)
            {
                AsignarTextoCombo();
            }
        }

        private void ucComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (cmbCombo.SelectedValue == null)
            {
                AsignarTextoCombo();
            }
        }

        private void ucComboBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (!(bool)e.NewValue)
                {
                    pnLabel.Visibility = System.Windows.Visibility.Visible;
                    cmbCombo.Visibility = System.Windows.Visibility.Collapsed;
                    btnCancelar.Visibility = System.Windows.Visibility.Collapsed;
                    lblTexto.Content = (cmbCombo.SelectedItem == null) ? cmbCombo.Text : ((Items)cmbCombo.SelectedItem).Descripcion;
                }
                else
                {
                    pnLabel.Visibility = System.Windows.Visibility.Collapsed;
                    cmbCombo.Visibility = System.Windows.Visibility.Visible;
                    btnCancelar.Visibility = System.Windows.Visibility.Visible;
                }

            }
            catch (Exception)
            {
                pnLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}