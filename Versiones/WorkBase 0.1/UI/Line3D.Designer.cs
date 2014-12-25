namespace WorkBase.UI
{
    partial class Line3D
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
            this.lblLineaSuperior = new System.Windows.Forms.Label();
            this.lblLineaInferior = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineaSuperior
            // 
            this.lblLineaSuperior.Location = new System.Drawing.Point(0, 0);
            this.lblLineaSuperior.Name = "lblLineaSuperior";
            this.lblLineaSuperior.Size = new System.Drawing.Size(200, 1);
            this.lblLineaSuperior.TabIndex = 0;
            // 
            // lblLineaInferior
            // 
            this.lblLineaInferior.Location = new System.Drawing.Point(0, 0);
            this.lblLineaInferior.Name = "lblLineaInferior";
            this.lblLineaInferior.Size = new System.Drawing.Size(200, 1);
            this.lblLineaInferior.TabIndex = 1;
            this.lblLineaInferior.Text = "label1";
            // 
            // Line3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLineaInferior);
            this.Controls.Add(this.lblLineaSuperior);
            this.Name = "Line3D";
            this.Size = new System.Drawing.Size(200, 2);
            this.Resize += new System.EventHandler(this.Line3D_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLineaSuperior;
        private System.Windows.Forms.Label lblLineaInferior;
    }
}
