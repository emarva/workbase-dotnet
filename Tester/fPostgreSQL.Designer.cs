namespace Tester
{
    partial class fPostgreSQL
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
            this.grilla = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.rut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grilla)).BeginInit();
            this.SuspendLayout();
            // 
            // grilla
            // 
            this.grilla.AllowUserToAddRows = false;
            this.grilla.AllowUserToDeleteRows = false;
            this.grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rut,
            this.nombre,
            this.direc});
            this.grilla.Location = new System.Drawing.Point(32, 16);
            this.grilla.Name = "grilla";
            this.grilla.Size = new System.Drawing.Size(528, 150);
            this.grilla.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Actualizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rut
            // 
            this.rut.DataPropertyName = "rut";
            this.rut.HeaderText = "Rut";
            this.rut.Name = "rut";
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            // 
            // direc
            // 
            this.direc.DataPropertyName = "direccion";
            this.direc.HeaderText = "Dirección";
            this.direc.Name = "direc";
            // 
            // fPostgreSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grilla);
            this.Name = "fPostgreSQL";
            this.Text = "PostgreSQL";
            ((System.ComponentModel.ISupportInitialize)(this.grilla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rut;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn direc;
    }
}