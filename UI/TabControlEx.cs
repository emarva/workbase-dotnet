using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using DevO2.Shared;

namespace DevO2.UI
{
    public partial class TabControlEx : TabControl
    {
        #region Variables
        private Keys propTeclaTabAnterior;
        private Keys propTeclaTabSiguiente;
        private ToolStrip propToolStrip;

        private ToolStrip toolStripOriginal;
        private ImageList iconos;
        #endregion

        #region Constructor
        public TabControlEx()
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
        [DesignOnly(true),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ParenthesizePropertyName(true),
        Editor(typeof(AboutEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public object Acerca { get { return null; } }

        public Keys TeclaTabAnterior
        {
            get { return this.propTeclaTabAnterior; }
            set { this.propTeclaTabAnterior = value; }
        }

        public Keys TeclaTabSiguiente
        {
            get { return this.propTeclaTabSiguiente; }
            set { this.propTeclaTabSiguiente = value; }
        }

        public ToolStrip ToolStrip
        {
            get { return this.propToolStrip; }
            set { this.propToolStrip = value; }
        }
        #endregion

        #region Metodos
        public bool ExisteFormulario(Form formulario, bool mostrarForm)
        {
            foreach (TabPageForm tfp in this.TabPages)
            {
                if (tfp.Formulario.Name == formulario.Name)
                {
                    // Muestra el formulario
                    if (mostrarForm)
                        this.SelectTab(tfp);

                    return true;
                }
            }

            return false;
        }

        public bool ExisteFormulario(Form formulario)
        {
            return this.ExisteFormulario(formulario, false);
        }

        public void AgregarFormulario(Form formulario, bool mostrarIcono, Color backColor, bool backColorRandom)
        {
            try
            {
                TabPageForm tfpForm = new TabPageForm();

                if (mostrarIcono)
                {
                    // Verifico si se le asigno un ImageList al TabFrame
                    if (this.ImageList == null)
                    {
                        iconos = new ImageList();
                        iconos.ColorDepth = ColorDepth.Depth16Bit;
                        this.ImageList = iconos;
                    }

                    if (!this.ImageList.Images.ContainsKey(formulario.Name))
                        this.ImageList.Images.Add(formulario.Name, formulario.Icon);

                    tfpForm.ImageIndex = this.ImageList.Images.IndexOfKey(formulario.Name);
                }

                tfpForm.Formulario = formulario;

                // Restaura la ToolStrip a su estado original
                if (this.toolStripOriginal != null)
                    this.propToolStrip = this.toolStripOriginal;

                // Verifica si el TabControlEx tiene un ToolStrip asignado, 
                // de ser asi si lo asigna al TabPageForm
                if (this.propToolStrip != null)
                {                    
                    tfpForm.ToolStrip = this.propToolStrip;
                 
                    // Guarda una copia del ToolStrip original,
                    // para poder cargar de forma correcta el ToolStrip al cambiar de pestaña
                    toolStripOriginal = this.propToolStrip; 
                }

                tfpForm.BackColorForm = backColor;
                tfpForm.BackColorFormRandom = backColorRandom;
                tfpForm.EstaActivoForm += new TabPageForm.EstaActivoFormEventHandler(tfpForm_EstaActivoForm);
                tfpForm.SeCerroForm += new TabPageForm.SeCerroFormEventHandler(tfpForm_SeCerroForm);
                tfpForm.Ver();

                // Le agrego la imagen a la pestaña

                // Agrega el formulario y lo como pestaña activa
                this.TabPages.Add(tfpForm);
                this.SelectTab(this.TabPages.Count - 1);
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorManager em = new ErrorManager(ex);
            }
        }
                
        public void AgregarFormulario(Form formulario)
        {
            this.AgregarFormulario(formulario, true, Color.FromKnownColor(KnownColor.ButtonFace), false);
        }

        public void AgregarFormulario(Form formulario, bool mostrarIcono)
        {
            this.AgregarFormulario(formulario, mostrarIcono, Color.FromKnownColor(KnownColor.ButtonFace), false);
        }

        public void AgregarFormulario(Form formulario, bool mostrarIcono, Color backColor)
        {
            this.AgregarFormulario(formulario, mostrarIcono, backColor, false);
        }

        public Form ObtenerFormActual()
        {
            if (this.SelectedTab == null)
                return null;

            TabPageForm tfp = (TabPageForm)this.SelectedTab;

            return tfp.Formulario;
        }
        
        private void TabControlEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*this.propToolStrip = this.toolStripOriginal;

            if (((TabPageForm)SelectedTab) != null)
                ((TabPageForm)SelectedTab).Formulario.Activate();*/
        }

        private void tfpForm_EstaActivoForm()
        {
            ((TabPageForm)SelectedTab).Formulario.Text = "Activo";
            MessageBox.Show("Activo.");
        }

        private void tfpForm_SeCerroForm()
        {
            if (this.SelectedIndex != -1)
            {
                int indiceTab = this.SelectedIndex;

                this.TabPages.Remove(this.SelectedTab);

                if (this.TabCount <= indiceTab)
                {
                    indiceTab -= 1;
                }

                if (indiceTab != -1)
                {
                    this.SelectTab(indiceTab);
                }

                // Restaura la ToolStrip a su estado original
                this.propToolStrip = this.toolStripOriginal;
            }
        }        

        private void TabFrame_KeyDown(object sender, KeyEventArgs e)
        {          
            if ((e.Modifiers + e.KeyValue) == this.propTeclaTabAnterior) // Atrás
            {
                if (this.SelectedIndex != 0)
                {
                    this.SelectTab(this.SelectedIndex - 1);
                }
            }

            if ((e.Modifiers + e.KeyValue) == this.propTeclaTabSiguiente) // Siguiente
            {
                if (this.SelectedIndex != this.TabPages.Count - 1)
                {
                    this.SelectTab(this.SelectedIndex + 1);
                }
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
                this.SelectedTab.Refresh();
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
