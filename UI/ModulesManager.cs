using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using DevO2.Shared;

namespace DevO2.UI
{
    public partial class ModulesManager : Component
    {
        #region Variables
        private MenuStrip propMenuStrip;
        private ToolStrip propToolStrip;
        private string propRutaModulos;
        private TabControlEx propTabFrame;
       
        private ModuleCollection colModulos = new ModuleCollection();
        #endregion

        #region Constructor
        public ModulesManager()
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
        
        public MenuStrip MenuStrip
        {
            get { return this.propMenuStrip; }
            set { this.propMenuStrip = value; }
        }

        public ToolStrip ToolStrip
        {
            get { return this.propToolStrip; }
            set { this.propToolStrip = value; }
        }

        public string RutaModulos
        {
            get { return this.propRutaModulos; }
            set { this.propRutaModulos = value; }
        }

        public TabControlEx TabFrame
        {
            get { return this.propTabFrame; }
            set { this.propTabFrame = value; }
        }
        #endregion

        #region Metodos
        private void EjecutarModulo(string nombreModulo)
        {
            Module modulo = colModulos.Find(nombreModulo);
            
            if (modulo != null)
            {
                /*if (modulo.Instancia.UsaTabFrame && propTabFrame != null)
                {
                    modulo.Instancia.TabFrame = propTabFrame;
                }*/

                /*if (modulo.Instancia.CrearItemMenu)
                {
                    //this.MenuStrip.d
                }*/

                if (modulo.Instancia.CrearBoton)
                {
                    ToolStripButton tsb = new ToolStripButton();

                    //tsb.DisplayStyle = ToolStripItemDisplayStyle.
                    tsb.Text = modulo.Instancia.TextoBoton;
                    tsb.Click += new EventHandler(modulo.Instancia.Boton_Click);

                    this.ToolStrip.Items.Add(tsb);
                }
            }
        }

        private void AgregarModulo(string archivo)
        {
            Assembly modulo = Assembly.LoadFrom(archivo);
            Type[] tipos = modulo.GetTypes();

            foreach (Type tipo in tipos)
            {
                if (tipo.IsPublic)
                {
                    if (tipo.FullName.Contains(".Modulos."))
                    {
                        Module nuevoModulo = new Module();

                        nuevoModulo.RutaAssembly = tipo.FullName;

                        try
                        {
                            //object o = Activator.CreateInstance(tipo);
                            nuevoModulo.Instancia = (IModule)Activator.CreateInstance(modulo.GetType(tipo.ToString()));

                            colModulos.Add(nuevoModulo);

                            nuevoModulo = null;
                            this.EjecutarModulo(tipo.FullName);
                        }
                        catch (Exception ex)
                        {
                            modulo = null;
                            throw ex;
                            //ErrorManager em = new ErrorManager(ex);
                        }
                    }
                }
            }

            modulo = null;
        }

        public void CargarModulos()
        {
            colModulos.Clear();

            foreach (string archivoEn in Directory.GetFiles(this.propRutaModulos))
            {
                FileInfo fi = new FileInfo(archivoEn);

                if (fi.Extension.Equals(".dll"))
                {
                    this.AgregarModulo(archivoEn);                    
                }
            }
        }
        #endregion
    }
}
