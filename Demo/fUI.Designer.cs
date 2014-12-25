namespace Demo
{
    partial class fUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlEx1 = new DevO2.UI.TabControlEx();
            this.SuspendLayout();
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Location = new System.Drawing.Point(8, 8);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(712, 424);
            this.tabControlEx1.TabIndex = 0;
            this.tabControlEx1.TeclaTabAnterior = System.Windows.Forms.Keys.None;
            this.tabControlEx1.TeclaTabSiguiente = System.Windows.Forms.Keys.None;
            this.tabControlEx1.ToolStrip = null;
            // 
            // fUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 441);
            this.Controls.Add(this.tabControlEx1);
            this.Name = "fUI";
            this.Text = "fUI";
            this.Load += new System.EventHandler(this.fUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevO2.UI.TabControlEx tabControlEx1;
    }
}