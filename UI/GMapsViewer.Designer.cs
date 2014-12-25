namespace DevO2.UI
{
    partial class GMapsViewer
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
            this.wbbGMaps = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbbGMaps
            // 
            this.wbbGMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbbGMaps.IsWebBrowserContextMenuEnabled = false;
            this.wbbGMaps.Location = new System.Drawing.Point(0, 0);
            this.wbbGMaps.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbbGMaps.Name = "wbbGMaps";
            this.wbbGMaps.ScriptErrorsSuppressed = true;
            this.wbbGMaps.ScrollBarsEnabled = false;
            this.wbbGMaps.Size = new System.Drawing.Size(474, 322);
            this.wbbGMaps.TabIndex = 0;
            this.wbbGMaps.WebBrowserShortcutsEnabled = false;
            this.wbbGMaps.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbbGMaps_DocumentCompleted);
            // 
            // GMapsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wbbGMaps);
            this.Name = "GMapsViewer";
            this.Size = new System.Drawing.Size(474, 322);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbbGMaps;
    }
}
