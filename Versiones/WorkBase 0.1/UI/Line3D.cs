using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WorkBase.UI
{
    public partial class Line3D : UserControl
    {
        #region Variables
        private Color propColorSuperior = Color.FromKnownColor(KnownColor.ControlDark);
        private Color propColorInferior = Color.FromKnownColor(KnownColor.ControlLightLight);
        private int propIndentValue;
        #endregion

        #region Constructor
        public Line3D()
        {
            InitializeComponent();

            this.propIndentValue = 4;

            lblLineaInferior.SendToBack();

            lblLineaSuperior.BackColor = this.propColorSuperior;
            lblLineaSuperior.Height = 1;
            lblLineaSuperior.Top = 0;
            lblLineaSuperior.Left = this.propIndentValue;

            lblLineaInferior.BackColor = this.propColorInferior;
            lblLineaInferior.Height = 1;
            lblLineaInferior.Top = lblLineaSuperior.Top + 1;
            lblLineaInferior.Left = this.propIndentValue;
        }
        #endregion

        #region Propiedades
        public Color ColorSuperior
        {
            get { return this.propColorSuperior; }
            set 
            { 
                this.propColorSuperior = value;
                lblLineaSuperior.BackColor = value;
            }
        }

        public Color ColorInferior
        {
            get { return this.propColorInferior; }
            set
            {
                this.propColorInferior = value;
                lblLineaInferior.BackColor = value;
            }
        }

        public int IndentValue
        {
            get { return this.propIndentValue; }
            set 
            { 
                this.propIndentValue = value;
                // Reajustar tamaño de las líneas
                Line3D_Resize(null, null);
            }
        }
        #endregion

        private void Line3D_Resize(object sender, EventArgs e)
        {
            this.Height = 2;
            lblLineaSuperior.Left = this.propIndentValue;
            lblLineaSuperior.Width = this.Width - (this.propIndentValue * 2);
            lblLineaInferior.Left = this.propIndentValue;
            lblLineaInferior.Width = this.Width - (this.propIndentValue * 2);
        }

    }
}
