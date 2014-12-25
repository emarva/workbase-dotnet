using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkBase.Shared;

namespace Tester
{
    public partial class fSeguridad : Form
    {
        public fSeguridad()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblResMD5.Text = Security.GenerarMD5(txtTextoMD5.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TipoSHA tipo = new TipoSHA(); ;

            if (sha1rb.Checked == true)
                tipo = TipoSHA.SHA1;
            if (sha2rb.Checked == true)
                tipo = TipoSHA.SHA256;
            if (sha3rb.Checked == true)
                tipo = TipoSHA.SHA384;
            if (sha5rb.Checked == true)
                tipo = TipoSHA.SHA512;

            lblResSHA.Text = Security.GenerarSHA(txtTextoSHA.Text, tipo);
        }

        private void fSeguridad_Load(object sender, EventArgs e)
        {
            cboTipoBase64.SelectedIndex = 0;
            cboAlgoritmo.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cboTipoBase64.SelectedIndex == 0)
            {
                txtResB64.Text = Security.Base64Codificar(txtTextoB64.Text);
            }
            else if (cboTipoBase64.SelectedIndex == 1)
            {
                txtResB64.Text = Security.Base64Decodificar(txtTextoB64.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (cboAlgoritmo.SelectedIndex)
            {
                case 0: // DES
                    txtEnc.Text = Security.Encriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.DES, FormatoCadenaEncriptada.Hexadecimal);
                    break;
                case 1: // RC2 
                    txtEnc.Text = Security.Encriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.RC2, FormatoCadenaEncriptada.Hexadecimal);
                    break;
                case 2: // Rijndael
                    txtEnc.Text = Security.Encriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.Rijndael, FormatoCadenaEncriptada.Base64);
                    break;
                case 3: // TripleDES
                    txtEnc.Text = Security.Encriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.TripleDES, FormatoCadenaEncriptada.Base64);
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (cboAlgoritmo.SelectedIndex)
            {
                case 0: // DES
                    txtEnc.Text = Security.Desencriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.DES, FormatoCadenaEncriptada.Hexadecimal);
                    break;
                case 1: // RC2 
                    txtEnc.Text = Security.Desencriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.RC2, FormatoCadenaEncriptada.Hexadecimal);
                    break;
                case 2: // Rijndael
                    txtEnc.Text = Security.Desencriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.Rijndael, FormatoCadenaEncriptada.Base64);
                    break;
                case 3: // TripleDES
                    txtEnc.Text = Security.Desencriptar(txtEnc.Text, txtClave.Text, MetodoEncriptacion.TripleDES, FormatoCadenaEncriptada.Base64);
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControlEx1.AgregarFormulario(new fMySQL());
            tabControlEx1.AgregarFormulario(new fMySQL());
        }
    }
}
