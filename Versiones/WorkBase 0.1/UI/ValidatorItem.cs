using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBase.UI
{
    #region ValidatorTypeFormat    
    public enum ValidatorType
    {
        Ninguno,
        EMail,
        Fecha,
        Hora,
        Rut,
        SoloNumeros,
        SoloLetras,
        Personalizado        
    }
    #endregion

    [Serializable()]
    public sealed class ValidatorItem
    {
        #region Variables
        private System.Windows.Forms.Control propControl;
        private bool propPermitirLargoCero;
        private ValidatorType propTipoValidacion = ValidatorType.Ninguno;
        private string propExpresionRegular;
        #endregion

        #region Propiedades
        public System.Windows.Forms.Control Control
        {
            get { return this.propControl; }
            set 
            {
                if (value != null)
                {
                    Type tipo = value.GetType();

                    if (tipo.Name == "ComboBoxEx" || tipo.Name == "ComboBox" || tipo.Name == "TextBox" ||
                        tipo.Name == "MaskedTextBox")
                    {
                        this.propControl = value;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Tipo de control no valido.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.propControl = null;
                }
            }
        }

        public bool PermitirLargoCero
        {
            get { return this.propPermitirLargoCero; }
            set { this.propPermitirLargoCero = value; }
        }

        public ValidatorType TipoValidacion
        {
            get { return this.propTipoValidacion; }
            set { this.propTipoValidacion = value; }
        }

        public string ExpresionRegular
        {
            get { return this.propExpresionRegular; }
            set { this.propExpresionRegular = value; }
        }
        #endregion
    }

    [Serializable()]
    public sealed class ValidatorItemCollection : System.Collections.CollectionBase
    {
        #region Indexador
        public ValidatorItem this[int indice]
        {
            get { return (ValidatorItem)List[indice]; }
            set { List.Add(value); }
        }
        #endregion

        #region Metodos
        public void Add(ValidatorItem item)
        {
            List.Add(item);
        }

        public void Add(System.Windows.Forms.Control control, bool permitirLargoCero, ValidatorType tipoValidacion, string expresionRegular)
        {
            ValidatorItem item = new ValidatorItem();

            item.Control = control;
            item.PermitirLargoCero = permitirLargoCero;
            item.TipoValidacion = tipoValidacion;
            item.ExpresionRegular = expresionRegular;

            this.Add(item);
        }

        public void Remove(int indice)
        {
            if (indice > Count - 1 || indice < 0)
                System.Windows.Forms.MessageBox.Show("Índice no valido!");
            else
                List.RemoveAt(indice);
        }

        /*public Marcador Item(int indice)
        {
            return (Marcador)List[indice];
        }*/
        #endregion
    }
}
