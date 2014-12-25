using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace WorkBase.UI
{   
    public partial class ValidatorControls : Component
    {
        #region Variables
        private Color propBackColorError = Color.MistyRose;
        private Color propForeColorError = SystemColors.WindowText;
        private Color propBackColorOK = Color.Honeydew;
        private Color propForeColorOK = SystemColors.WindowText;
        private ValidatorItemCollection propControles = null;

        private Color backColorAnterior = Color.Empty;
        private Color foreColorAnterior = Color.Empty;
        private System.Windows.Forms.Panel pnlMensajes = null;
        #endregion

        #region Constructor
        public ValidatorControls()
        {
            InitializeComponent();
        }
        #endregion

        #region Propiedades
        public Color BackColorError
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
        }

        public ValidatorItemCollection Controles
        {
            get
            {
                if (this.propControles == null)
                {
                    this.propControles = new ValidatorItemCollection();
                }

                return this.propControles;
            }
        }
        #endregion

        #region Metodos
        public void Validar()
        {
            foreach (ValidatorItem item in this.propControles)
            {
                
            }
        }

        public void ValidarControl()
        {

        }
        #endregion
    }
}
