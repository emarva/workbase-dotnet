namespace WorkBase.UI
{
    partial class AboutDialogForm
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.picIcono = new System.Windows.Forms.PictureBox();
            this.lblApp = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblAutorizacion = new System.Windows.Forms.Label();
            this.line3D1 = new WorkBase.UI.Line3D();
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(136, 104);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // picIcono
            // 
            this.picIcono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIcono.Location = new System.Drawing.Point(8, 8);
            this.picIcono.Name = "picIcono";
            this.picIcono.Size = new System.Drawing.Size(48, 48);
            this.picIcono.TabIndex = 3;
            this.picIcono.TabStop = false;
            // 
            // lblApp
            // 
            this.lblApp.AutoSize = true;
            this.lblApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp.Location = new System.Drawing.Point(72, 8);
            this.lblApp.Name = "lblApp";
            this.lblApp.Size = new System.Drawing.Size(36, 13);
            this.lblApp.TabIndex = 2;
            this.lblApp.Text = "#app";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(72, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "#version";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(72, 40);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(131, 13);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Copyright (c) #ano #autor.";
            // 
            // lblAutorizacion
            // 
            this.lblAutorizacion.AutoSize = true;
            this.lblAutorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutorizacion.Location = new System.Drawing.Point(72, 72);
            this.lblAutorizacion.Name = "lblAutorizacion";
            this.lblAutorizacion.Size = new System.Drawing.Size(228, 13);
            this.lblAutorizacion.TabIndex = 6;
            this.lblAutorizacion.Text = "Se autoriza el uso de este software a #usuario.";
            // 
            // line3D1
            // 
            this.line3D1.ColorInferior = System.Drawing.SystemColors.ControlLightLight;
            this.line3D1.ColorSuperior = System.Drawing.SystemColors.ControlDark;
            this.line3D1.IndentValue = 8;
            this.line3D1.Location = new System.Drawing.Point(0, 96);
            this.line3D1.Name = "line3D1";
            this.line3D1.Size = new System.Drawing.Size(344, 2);
            this.line3D1.TabIndex = 1;
            // 
            // AboutDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 137);
            this.Controls.Add(this.lblAutorizacion);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblApp);
            this.Controls.Add(this.picIcono);
            this.Controls.Add(this.line3D1);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acerca de #app";
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private Line3D line3D1;
        private System.Windows.Forms.PictureBox picIcono;
        private System.Windows.Forms.Label lblApp;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblAutorizacion;
        public System.Windows.Forms.Label lblVersion;
    }
}