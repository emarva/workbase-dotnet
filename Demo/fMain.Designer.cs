namespace Demo
{
    partial class fMain
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlEx1 = new DevO2.UI.TabControlEx();
            this.googleEarthViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(659, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataLayerToolStripMenuItem,
            this.uIToolStripMenuItem,
            this.googleEarthViewerToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // dataLayerToolStripMenuItem
            // 
            this.dataLayerToolStripMenuItem.Name = "dataLayerToolStripMenuItem";
            this.dataLayerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataLayerToolStripMenuItem.Text = "DataLayer";
            this.dataLayerToolStripMenuItem.Click += new System.EventHandler(this.dataLayerToolStripMenuItem_Click);
            // 
            // uIToolStripMenuItem
            // 
            this.uIToolStripMenuItem.Name = "uIToolStripMenuItem";
            this.uIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uIToolStripMenuItem.Text = "UI";
            this.uIToolStripMenuItem.Click += new System.EventHandler(this.uIToolStripMenuItem_Click);
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(0, 24);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(659, 374);
            this.tabControlEx1.TabIndex = 1;
            this.tabControlEx1.TeclaTabAnterior = System.Windows.Forms.Keys.None;
            this.tabControlEx1.TeclaTabSiguiente = System.Windows.Forms.Keys.None;
            this.tabControlEx1.ToolStrip = null;
            // 
            // googleEarthViewerToolStripMenuItem
            // 
            this.googleEarthViewerToolStripMenuItem.Name = "googleEarthViewerToolStripMenuItem";
            this.googleEarthViewerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.googleEarthViewerToolStripMenuItem.Text = "Google Earth Viewer";
            this.googleEarthViewerToolStripMenuItem.Click += new System.EventHandler(this.googleEarthViewerToolStripMenuItem_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 398);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "WorkBase Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataLayerToolStripMenuItem;
        private DevO2.UI.TabControlEx tabControlEx1;
        private System.Windows.Forms.ToolStripMenuItem uIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleEarthViewerToolStripMenuItem;
    }
}

