namespace Tester
{
    partial class fPermisos
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
            this.components = new System.ComponentModel.Container();
            this.permissionsManager1 = new DevO2.UI.PermissionsManager();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // permissionsManager1
            // 
            this.permissionsManager1.BaseDatos = null;
            this.permissionsManager1.BDContrasena = null;
            this.permissionsManager1.BDHost = null;
            this.permissionsManager1.BDPuerto = 0;
            this.permissionsManager1.BDUsuario = null;
            //this.permissionsManager1.TipoConnector = DevO2.DataLayer.TipoConnector.PostgreSQL;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 414);
            this.Controls.Add(this.button1);
            this.Name = "fPermisos";
            this.Text = "fPermisos";
            this.ResumeLayout(false);

        }

        #endregion

        private DevO2.UI.PermissionsManager permissionsManager1;
        private System.Windows.Forms.Button button1;
    }
}