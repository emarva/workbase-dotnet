using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DevO2.Shared;

namespace DevO2.UI
{
    public class ComboBoxNuevoItem
    {
        #region Variables
        private string propNombre;
        private object propItemData;
        #endregion

        #region Constructores
        public ComboBoxNuevoItem()
        {
            this.propNombre = string.Empty;
            this.propItemData = -1;
        }

        public ComboBoxNuevoItem(string nombre, object itemData)
        {
            this.propNombre = nombre;
            this.propItemData = itemData;
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return this.propNombre; }
            set { this.propNombre = value; }
        }

        public object ItemData
        {
            get { return this.propItemData; }
            set { this.propItemData = value; }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.propNombre;
        }
        #endregion
    }

    public partial class ComboBoxEx : System.Windows.Forms.ComboBox
    {
        //#region Constantes
        // Constantes para controlar el copiado y pegado
        //private const int WM_RBUTTONUP = 0x205;
        //private const int WM_COPY = 0x301;
        //private const int WM_PASTE = 0x302;
        //#endregion

        //#region Varibales
        //private Color propBackColorError = Color.MistyRose;
        //private Color propForeColorError = SystemColors.WindowText;
        //private Color propBackColorOK = Color.Honeydew;
        //private Color propForeColorOK = SystemColors.WindowText;
        //private string propMensajeErrorLargoCero;
        //private bool propControlarLargoCero;
        //private Control propSaltarA;
        //private bool propActivarProteccion;
        //private string propMensajeProteccion;

        //private Color backColorAnterior = Color.Empty;
        //private Color foreColorAnterior = Color.Empty;
        //#endregion

        #region Constructor
        public ComboBoxEx()
        {
            InitializeComponent();
        }
        #endregion

        #region Propiedades
        [DesignOnly(true),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ParenthesizePropertyName(true),
        Editor(typeof(AboutEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public object Acerca { get { return null; } }

        /*public Color BackColorError
        {
            get { return this.propBackColorError; }
            set { this.propBackColorError = value; }
        }

        public Color ForeColorError
        {
            get { return this.propForeColorError; }
            set { this.propForeColorError = value; }
        }

        public Color BackColorOK
        {
            get { return this.propBackColorOK; }
            set { this.propBackColorOK = value; }
        }

        public Color ForeColorOK
        {
            get { return this.propForeColorOK; }
            set { this.propForeColorOK = value; }
        }*/

        /*public string MensajeErrorLargoCero
        {
            get { return this.propMensajeErrorLargoCero; }
            set { this.propMensajeErrorLargoCero = value; }
        }*/

        /*public bool ControlarLargoCero
        {
            get { return this.propControlarLargoCero; }
            set
            {
                if (!value)
                {
                    if (this.backColorAnterior != Color.Empty)
                    {
                        this.BackColor = this.backColorAnterior;
                        this.ForeColor = this.foreColorAnterior;

                        this.backColorAnterior = Color.Empty;
                        this.foreColorAnterior = Color.Empty;
                    }
                }

                this.propControlarLargoCero = value;
            }
        }*/

        /*public Control SaltarA
        {
            get { return this.propSaltarA; }
            set
            {
                if (value != null)
                {
                    if (this.Name == value.Name)
                    {
                        MessageBox.Show("No se puede seleccionar a sí mismo el control");
                        this.propSaltarA = null;
                    }
                    else
                    {
                        this.propSaltarA = value;
                    }
                }
                else
                {
                    this.propSaltarA = null;
                }
            }
        }*/

        /*public bool ActivarProteccion
        {
            get { return this.propActivarProteccion; }
            set { this.propActivarProteccion = value; }
        }

        public string MensajeProteccion
        {
            get { return this.propMensajeProteccion; }
            set { this.propMensajeProteccion = value; }
        }*/
        #endregion

        #region Metodos
        /*private void LargoCero()
        {
            if (this.propControlarLargoCero)
            {
                if (this.backColorAnterior == Color.Empty)
                {
                    this.backColorAnterior = this.BackColor;
                    this.foreColorAnterior = this.ForeColor;
                }

                if (this.SelectedIndex != -1)
                {
                    this.BackColor = this.propBackColorOK;
                    this.ForeColor = this.propForeColorOK;
                }
                else
                {
                    this.BackColor = this.propBackColorError;
                    this.ForeColor = this.propForeColorError;

                    if (this.propMensajeErrorLargoCero != null)
                        if (this.propMensajeErrorLargoCero.Length != 0)
                            MessageBox.Show(this.propMensajeErrorLargoCero);
                }
            }
            else
            {
                if (this.backColorAnterior != Color.Empty)
                {
                    this.BackColor = this.backColorAnterior;
                    this.ForeColor = this.foreColorAnterior;
                    this.backColorAnterior = Color.Empty;
                    this.foreColorAnterior = Color.Empty;
                }
            }
        }*/

        public void AgregarItem(int indice, string nombre, object itemData)
        {
            ComboBoxNuevoItem nuevoItem = new ComboBoxNuevoItem(nombre, itemData);

            if (indice == -1)
                this.Items.Add(nuevoItem);
            else
                this.Items.Insert(indice, nuevoItem);
        }

        public void AgregarItem(string nombre, object itemData)
        {
            this.AgregarItem(-1, nombre, itemData);
        }

        /*public void CargarDesdeDataStorage(WorkBase.DataLayer.DataStorage dataStorage, string campoNombre, string campoItemData)
        {
            try
            {
                if (dataStorage.TieneFilas)
                {
                    this.Items.Clear();

                    while (dataStorage.Leer())
                    {
                        if (campoItemData != null)
                            this.AgregarItem(dataStorage[campoNombre].ToString(), dataStorage[campoItemData]);
                        else
                            this.Items.Add(dataStorage[campoNombre].ToString());
                    }

                    dataStorage.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        /*public void CargarDesdeDataStorage(WorkBase.DataLayer.DataStorage dataStorage, string campoNombre)
        {
            this.CargarDesdeDataStorage(dataStorage, campoNombre, null);
        }*/

        public void CargarDesdeDataTable(System.Data.DataTable dataTable, string campoNombre, string campoItemData)
        {
            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    this.Items.Clear();
                    foreach (System.Data.DataRow fila in dataTable.Rows)
                    {
                        if (campoItemData != null)
                            this.AgregarItem(fila[campoNombre].ToString(), fila[campoItemData]);
                        else
                            this.Items.Add(fila[campoNombre].ToString());
                    }
                    dataTable.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarDesdeDataTable(System.Data.DataTable dataTable, string campoNombre)
        {
            this.CargarDesdeDataTable(dataTable, campoNombre, null);
        }

        public Object2 ItemData()
        {
            ComboBoxNuevoItem obj = null;

            Type tipo = this.SelectedItem.GetType();

            if (tipo.Name == "ComboBoxNuevoItem")
                obj = (ComboBoxNuevoItem)this.SelectedItem;

            return obj != null ? new Object2(obj.ItemData) : null;
        }

        public int Buscar(string textoBuscar)
        {
            for (int i = 0; i <= this.Items.Count - 1; i++)
            {
                if (this.Items[i].ToString() == textoBuscar)
                {
                    return i;
                }
            }

            return -1;
        }

        // ESTA FUNCION NO ESTA FUNCIONANDO BIEN
        public void SeleccionarItem(string item)
        {
            for (int i = 0; i <= this.Items.Count - 1; i++)
            {
                if (this.Items[i].ToString() == item)
                {
                    this.SelectedIndex = i;
                }
            }

            return;
        }

        public void SeleccionarItem(object itemData)
        {
            for (int i = 0; i <= this.Items.Count - 1; i++)
            {
                if (((ComboBoxNuevoItem)this.Items[i]).ItemData.ToString() == itemData.ToString())
                {
                    this.SelectedIndex = i;
                }
            }

            return;
        }
        #endregion

        //#region Overrides
        /*protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (this.Enabled)
            {
                base.OnKeyPress(e);

                if (e.KeyChar == 13 && this.propSaltarA != null)
                {
                    this.propSaltarA.Focus();

                    // Evitar el pitido
                    e.Handled = true;
                 }
            }
        }*/

        /*protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.Enabled)
            {
                base.OnSelectedIndexChanged(e);

                // Controlar largo cero
                this.LargoCero();
            }
        }*/

        /*protected override void OnLeave(EventArgs e)
        {
            if (this.Enabled)
            {
                base.OnLeave(e);

                // Controlar largo cero
                this.LargoCero();
            }
        }*/

        /*protected override void WndProc(ref Message m)
        {
            if (propActivarProteccion)
            {
                if (m.Msg == WM_COPY)
                {
                    if (this.propMensajeProteccion != null)
                        if (this.propMensajeProteccion.Length != 0)
                            MessageBox.Show(this.propMensajeProteccion);
                    return;
                }
                if (m.Msg == WM_RBUTTONUP || m.Msg == WM_PASTE)
                    return;
            }

            base.WndProc(ref m);
        }*/
        //#endregion
    }
}
