namespace DevO2.UI
{
    partial class ErrorManagerForm
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
            this.picImagen = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.btnEnviarCorreo = new System.Windows.Forms.Button();
            this.btnNoEnviar = new System.Windows.Forms.Button();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // picImagen
            // 
            this.picImagen.Image = global::DevO2.UI.Properties.Resources.error;
            this.picImagen.Location = new System.Drawing.Point(8, 8);
            this.picImagen.Name = "picImagen";
            this.picImagen.Size = new System.Drawing.Size(48, 48);
            this.picImagen.TabIndex = 0;
            this.picImagen.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Origen:";
            // 
            // lblOrigen
            // 
            this.lblOrigen.Location = new System.Drawing.Point(104, 88);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(360, 16);
            this.lblOrigen.TabIndex = 7;
            this.lblOrigen.Text = "#";
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(392, 112);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnEnviarCorreo
            // 
            this.btnEnviarCorreo.Location = new System.Drawing.Point(248, 112);
            this.btnEnviarCorreo.Name = "btnEnviarCorreo";
            this.btnEnviarCorreo.Size = new System.Drawing.Size(139, 23);
            this.btnEnviarCorreo.TabIndex = 1;
            this.btnEnviarCorreo.Text = "Enviar informe de errores";
            this.btnEnviarCorreo.UseVisualStyleBackColor = true;
            this.btnEnviarCorreo.Visible = false;
            this.btnEnviarCorreo.Click += new System.EventHandler(this.btnEnviarCorreo_Click);
            // 
            // btnNoEnviar
            // 
            this.btnNoEnviar.Location = new System.Drawing.Point(392, 112);
            this.btnNoEnviar.Name = "btnNoEnviar";
            this.btnNoEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnNoEnviar.TabIndex = 0;
            this.btnNoEnviar.Text = "No enviar";
            this.btnNoEnviar.UseVisualStyleBackColor = true;
            this.btnNoEnviar.Visible = false;
            this.btnNoEnviar.Click += new System.EventHandler(this.btnNoEnviar_Click);
            // 
            // txtMensaje
            // 
            this.txtMensaje.BackColor = System.Drawing.SystemColors.Control;
            this.txtMensaje.Location = new System.Drawing.Point(64, 8);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensaje.Size = new System.Drawing.Size(400, 72);
            this.txtMensaje.TabIndex = 4;
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.Location = new System.Drawing.Point(8, 112);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(112, 23);
            this.btnVerDetalle.TabIndex = 3;
            this.btnVerDetalle.Text = "Ver detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Visible = false;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // txtDetalle
            // 
            this.txtDetalle.BackColor = System.Drawing.SystemColors.Control;
            this.txtDetalle.Location = new System.Drawing.Point(8, 145);
            this.txtDetalle.Multiline = true;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.ReadOnly = true;
            this.txtDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalle.Size = new System.Drawing.Size(456, 96);
            this.txtDetalle.TabIndex = 5;
            // 
            // ErrorManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 145);
            this.ControlBox = false;
            this.Controls.Add(this.txtDetalle);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.btnNoEnviar);
            this.Controls.Add(this.btnEnviarCorreo);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.lblOrigen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picImagen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ErrorManagerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "#";
            this.Load += new System.EventHandler(this.fErrorManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImagen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Button btnEnviarCorreo;
        private System.Windows.Forms.Button btnNoEnviar;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
    }
}