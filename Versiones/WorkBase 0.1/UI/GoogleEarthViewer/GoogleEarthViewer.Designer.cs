namespace WorkBase.UI
{
    partial class GoogleEarthViewer
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            this.wbbGE = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbbGE
            // 
            this.wbbGE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbbGE.IsWebBrowserContextMenuEnabled = false;
            this.wbbGE.Location = new System.Drawing.Point(0, 0);
            this.wbbGE.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbbGE.Name = "wbbGE";
            this.wbbGE.ScriptErrorsSuppressed = true;
            this.wbbGE.ScrollBarsEnabled = false;
            this.wbbGE.Size = new System.Drawing.Size(407, 277);
            this.wbbGE.TabIndex = 0;
            this.wbbGE.WebBrowserShortcutsEnabled = false;
            this.wbbGE.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbbGE_DocumentCompleted);
            // 
            // GoogleEarthViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wbbGE);
            this.Name = "GoogleEarthViewer";
            this.Size = new System.Drawing.Size(407, 277);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbbGE;
    }
}
