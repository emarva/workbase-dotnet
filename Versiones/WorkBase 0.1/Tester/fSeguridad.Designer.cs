namespace Tester
{
    partial class fSeguridad
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblResMD5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTextoMD5 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sha5rb = new System.Windows.Forms.RadioButton();
            this.sha3rb = new System.Windows.Forms.RadioButton();
            this.sha2rb = new System.Windows.Forms.RadioButton();
            this.sha1rb = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.lblResSHA = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTextoSHA = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboTipoBase64 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResB64 = new System.Windows.Forms.TextBox();
            this.txtTextoB64 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cboAlgoritmo = new System.Windows.Forms.ComboBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtEnc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControlEx1 = new WorkBase.UI.TabControlEx();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lblResMD5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTextoMD5);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MD5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generar MD5";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblResMD5
            // 
            this.lblResMD5.BackColor = System.Drawing.SystemColors.Window;
            this.lblResMD5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResMD5.Location = new System.Drawing.Point(8, 72);
            this.lblResMD5.Name = "lblResMD5";
            this.lblResMD5.Size = new System.Drawing.Size(272, 20);
            this.lblResMD5.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resultado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Texto";
            // 
            // txtTextoMD5
            // 
            this.txtTextoMD5.Location = new System.Drawing.Point(8, 32);
            this.txtTextoMD5.Name = "txtTextoMD5";
            this.txtTextoMD5.Size = new System.Drawing.Size(272, 20);
            this.txtTextoMD5.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.lblResSHA);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtTextoSHA);
            this.groupBox2.Location = new System.Drawing.Point(304, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SHA";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sha5rb);
            this.groupBox3.Controls.Add(this.sha3rb);
            this.groupBox3.Controls.Add(this.sha2rb);
            this.groupBox3.Controls.Add(this.sha1rb);
            this.groupBox3.Location = new System.Drawing.Point(328, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(104, 104);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo";
            // 
            // sha5rb
            // 
            this.sha5rb.AutoSize = true;
            this.sha5rb.Location = new System.Drawing.Point(8, 64);
            this.sha5rb.Name = "sha5rb";
            this.sha5rb.Size = new System.Drawing.Size(65, 17);
            this.sha5rb.TabIndex = 3;
            this.sha5rb.Text = "SHA512";
            this.sha5rb.UseVisualStyleBackColor = true;
            // 
            // sha3rb
            // 
            this.sha3rb.AutoSize = true;
            this.sha3rb.Location = new System.Drawing.Point(8, 48);
            this.sha3rb.Name = "sha3rb";
            this.sha3rb.Size = new System.Drawing.Size(65, 17);
            this.sha3rb.TabIndex = 2;
            this.sha3rb.Text = "SHA384";
            this.sha3rb.UseVisualStyleBackColor = true;
            // 
            // sha2rb
            // 
            this.sha2rb.AutoSize = true;
            this.sha2rb.Location = new System.Drawing.Point(8, 32);
            this.sha2rb.Name = "sha2rb";
            this.sha2rb.Size = new System.Drawing.Size(65, 17);
            this.sha2rb.TabIndex = 1;
            this.sha2rb.Text = "SHA256";
            this.sha2rb.UseVisualStyleBackColor = true;
            // 
            // sha1rb
            // 
            this.sha1rb.AutoSize = true;
            this.sha1rb.Checked = true;
            this.sha1rb.Location = new System.Drawing.Point(8, 16);
            this.sha1rb.Name = "sha1rb";
            this.sha1rb.Size = new System.Drawing.Size(53, 17);
            this.sha1rb.TabIndex = 0;
            this.sha1rb.TabStop = true;
            this.sha1rb.Text = "SHA1";
            this.sha1rb.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Generar SHA";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblResSHA
            // 
            this.lblResSHA.BackColor = System.Drawing.SystemColors.Window;
            this.lblResSHA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResSHA.Location = new System.Drawing.Point(8, 72);
            this.lblResSHA.Name = "lblResSHA";
            this.lblResSHA.Size = new System.Drawing.Size(312, 48);
            this.lblResSHA.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Resultado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Texto";
            // 
            // txtTextoSHA
            // 
            this.txtTextoSHA.Location = new System.Drawing.Point(8, 32);
            this.txtTextoSHA.Name = "txtTextoSHA";
            this.txtTextoSHA.Size = new System.Drawing.Size(272, 20);
            this.txtTextoSHA.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboTipoBase64);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtResB64);
            this.groupBox4.Controls.Add(this.txtTextoB64);
            this.groupBox4.Location = new System.Drawing.Point(8, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(288, 144);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Base64";
            // 
            // cboTipoBase64
            // 
            this.cboTipoBase64.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoBase64.FormattingEnabled = true;
            this.cboTipoBase64.Items.AddRange(new object[] {
            "Codificar",
            "Decodificar"});
            this.cboTipoBase64.Location = new System.Drawing.Point(8, 112);
            this.cboTipoBase64.Name = "cboTipoBase64";
            this.cboTipoBase64.Size = new System.Drawing.Size(128, 21);
            this.cboTipoBase64.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Codificar/Decodificar";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Procesar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Resultado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Texto ";
            // 
            // txtResB64
            // 
            this.txtResB64.Location = new System.Drawing.Point(8, 72);
            this.txtResB64.Name = "txtResB64";
            this.txtResB64.Size = new System.Drawing.Size(272, 20);
            this.txtResB64.TabIndex = 1;
            // 
            // txtTextoB64
            // 
            this.txtTextoB64.Location = new System.Drawing.Point(8, 32);
            this.txtTextoB64.Name = "txtTextoB64";
            this.txtTextoB64.Size = new System.Drawing.Size(272, 20);
            this.txtTextoB64.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.cboAlgoritmo);
            this.groupBox5.Controls.Add(this.txtClave);
            this.groupBox5.Controls.Add(this.txtEnc);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(304, 176);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(432, 208);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Encriptación por algoritmos";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(344, 176);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Desencriptar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(264, 176);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Encriptar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cboAlgoritmo
            // 
            this.cboAlgoritmo.FormattingEnabled = true;
            this.cboAlgoritmo.Items.AddRange(new object[] {
            "DES",
            "RC2",
            "Rijndael",
            "TripleDES"});
            this.cboAlgoritmo.Location = new System.Drawing.Point(8, 176);
            this.cboAlgoritmo.Name = "cboAlgoritmo";
            this.cboAlgoritmo.Size = new System.Drawing.Size(121, 21);
            this.cboAlgoritmo.TabIndex = 5;
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(8, 136);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(120, 20);
            this.txtClave.TabIndex = 4;
            // 
            // txtEnc
            // 
            this.txtEnc.Location = new System.Drawing.Point(48, 16);
            this.txtEnc.Multiline = true;
            this.txtEnc.Name = "txtEnc";
            this.txtEnc.Size = new System.Drawing.Size(376, 96);
            this.txtEnc.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Algoritmo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Clave";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Texto";
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.Location = new System.Drawing.Point(8, 296);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(240, 88);
            this.tabControlEx1.TabIndex = 4;
            this.tabControlEx1.TeclaTabAnterior = System.Windows.Forms.Keys.None;
            this.tabControlEx1.TeclaTabSiguiente = System.Windows.Forms.Keys.None;
            this.tabControlEx1.ToolStrip = null;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(256, 312);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 48);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // fSeguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tester.Properties.Resources.bgform2;
            this.ClientSize = new System.Drawing.Size(749, 396);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "fSeguridad";
            this.Text = "fSeguridad";
            this.Load += new System.EventHandler(this.fSeguridad_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblResMD5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTextoMD5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton sha2rb;
        private System.Windows.Forms.RadioButton sha1rb;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblResSHA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTextoSHA;
        private System.Windows.Forms.RadioButton sha3rb;
        private System.Windows.Forms.RadioButton sha5rb;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboTipoBase64;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtResB64;
        private System.Windows.Forms.TextBox txtTextoB64;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cboAlgoritmo;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtEnc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private WorkBase.UI.TabControlEx tabControlEx1;
        private System.Windows.Forms.Button button6;
    }
}