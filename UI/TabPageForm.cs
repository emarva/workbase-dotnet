using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Reflection;
using DevO2.Shared;

namespace DevO2.UI
{
    public partial class TabPageForm : TabPage
    {
        #region Variables
        private Form propForm;
        private ToolStrip propToolStrip;
        private Color propBackColorForm;
        private bool propBackColorFormRandom;
        #endregion

        #region Delegados
        //public delegate void CambioTituloFormEventHandler(string titulo);
        //public delegate void CerrarFormEventHandler();
        public delegate void EstaActivoFormEventHandler();
        public delegate void SeCerroFormEventHandler();
        #endregion

        #region Eventos
        //public event CambioTituloFormEventHandler CambioTituloForm;
        //public event CerrarFormEventHandler CerrarForm;
        public event EstaActivoFormEventHandler EstaActivoForm;
        public event SeCerroFormEventHandler SeCerroForm;
        #endregion

        #region Constructor
        public TabPageForm()
        {
            InitializeComponent();

            // Activamos el DoubleBuffering
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Activamos el evento OnNotifyMessage, asi tenemos la oportunidad
            // de filtrar los mensajes de Windows antes de que lleguen al WndProc
            // del formulario
            SetStyle(ControlStyles.EnableNotifyMessage, true);
        }      
        #endregion

        #region Propiedades
        public Form Formulario
        {
            get { return this.propForm; }
            set { this.propForm = value; }
        }

        public ToolStrip ToolStrip
        {
            get { return this.propToolStrip; }
            set { this.propToolStrip = value; }
        }

        public Color BackColorForm
        {
            get { return this.propBackColorForm; }
            set
            {
                this.propBackColorForm = value;
                this.propForm.BackColor = this.propBackColorForm;
            }
        }

        public bool BackColorFormRandom
        {
            get { return this.propBackColorFormRandom; }
            set { this.propBackColorFormRandom = value; }
        }
        #endregion

        #region Metodos
        public void Ver()
        {
            try
            {
                this.Text = this.propForm.Text;

                if (this.propBackColorFormRandom == true)
                {
                    Random r = new Random();
                    this.propForm.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                }

                this.propForm.TextChanged += new EventHandler(propForm_TextChanged);
                this.propForm.Activated += new EventHandler(propForm_Activated);
                this.propForm.FormClosing += new FormClosingEventHandler(propForm_FormClosing);

                // Verifica si el formulario tiene una propiedad llamada ToolStrip
                // de ser asi le asigna el ToolStrip del TabPageForm siempre que no sea nulo
                if (this.ToolStrip != null)
                {
                    foreach (PropertyInfo propiedad in this.Formulario.GetType().GetProperties())
                    {
                        if (propiedad.Name == "ToolStrip")
                        {
                            propiedad.SetValue(this.Formulario, this.ToolStrip, null);
                            this.Formulario.GetType().GetMethod("CargarToolStrip").Invoke(this.Formulario, null);
                        }
                    }
                }

                this.propForm.TopLevel = false;
                this.propForm.FormBorderStyle = FormBorderStyle.None;
                this.propForm.Dock = DockStyle.Fill;
                this.Controls.Add(this.propForm);
                this.propForm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorManager em = new ErrorManager(ex);
            }
        }       

        public void propForm_TextChanged(object sender, EventArgs e)
        {
            this.Text = this.propForm.Text;            
        }

        private void propForm_Activated(object sender, EventArgs e)
        {
            if (EstaActivoForm != null)
                EstaActivoForm();
        }

        public void propForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (SeCerroForm != null)
            {
                SeCerroForm();
            }
        }
        #endregion

        #region Overrides
        protected override void OnResize(EventArgs e)
        {
            try
            {
                // Evita el parpadeo al cambiar de tamaño
                base.OnResize(e);
                this.Refresh();
            }
            catch { }
        }

        protected override void OnNotifyMessage(Message m)
        {
            // Filtramos el mensaje WM_ERASEBKGND
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
        #endregion
    }
}
