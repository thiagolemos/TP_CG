namespace CGPaint
{
    partial class frmEntradaRotacao
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
            this.label1 = new System.Windows.Forms.Label();
            this.numAngulo = new System.Windows.Forms.NumericUpDown();
            this.gbEixo = new System.Windows.Forms.GroupBox();
            this.rdZ = new System.Windows.Forms.RadioButton();
            this.rdY = new System.Windows.Forms.RadioButton();
            this.rdX = new System.Windows.Forms.RadioButton();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numZ = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAngulo)).BeginInit();
            this.gbEixo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ângulo:";
            // 
            // numAngulo
            // 
            this.numAngulo.Location = new System.Drawing.Point(61, 12);
            this.numAngulo.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numAngulo.Name = "numAngulo";
            this.numAngulo.Size = new System.Drawing.Size(87, 20);
            this.numAngulo.TabIndex = 1;
            // 
            // gbEixo
            // 
            this.gbEixo.Controls.Add(this.rdZ);
            this.gbEixo.Controls.Add(this.rdY);
            this.gbEixo.Controls.Add(this.rdX);
            this.gbEixo.Location = new System.Drawing.Point(12, 129);
            this.gbEixo.Name = "gbEixo";
            this.gbEixo.Size = new System.Drawing.Size(136, 49);
            this.gbEixo.TabIndex = 3;
            this.gbEixo.TabStop = false;
            this.gbEixo.Text = "Eixo";
            this.gbEixo.Visible = false;
            // 
            // rdZ
            // 
            this.rdZ.AutoSize = true;
            this.rdZ.Location = new System.Drawing.Point(93, 23);
            this.rdZ.Name = "rdZ";
            this.rdZ.Size = new System.Drawing.Size(32, 17);
            this.rdZ.TabIndex = 2;
            this.rdZ.TabStop = true;
            this.rdZ.Text = "Z";
            this.rdZ.UseVisualStyleBackColor = true;
            // 
            // rdY
            // 
            this.rdY.AutoSize = true;
            this.rdY.Location = new System.Drawing.Point(55, 23);
            this.rdY.Name = "rdY";
            this.rdY.Size = new System.Drawing.Size(32, 17);
            this.rdY.TabIndex = 1;
            this.rdY.TabStop = true;
            this.rdY.Text = "Y";
            this.rdY.UseVisualStyleBackColor = true;
            // 
            // rdX
            // 
            this.rdX.AutoSize = true;
            this.rdX.Checked = true;
            this.rdX.Location = new System.Drawing.Point(17, 23);
            this.rdX.Name = "rdX";
            this.rdX.Size = new System.Drawing.Size(32, 17);
            this.rdX.TabIndex = 0;
            this.rdX.TabStop = true;
            this.rdX.Text = "X";
            this.rdX.UseVisualStyleBackColor = true;
            // 
            // btnAplicar
            // 
            this.btnAplicar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAplicar.Location = new System.Drawing.Point(44, 184);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar.TabIndex = 4;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pivô:";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(61, 51);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(87, 20);
            this.numX.TabIndex = 6;
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(61, 77);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(87, 20);
            this.numY.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Y:";
            // 
            // numZ
            // 
            this.numZ.Location = new System.Drawing.Point(61, 103);
            this.numZ.Name = "numZ";
            this.numZ.Size = new System.Drawing.Size(87, 20);
            this.numZ.TabIndex = 10;
            this.numZ.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Z:";
            this.label5.Visible = false;
            // 
            // frmEntradaRotacao
            // 
            this.AcceptButton = this.btnAplicar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 217);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numZ);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.gbEixo);
            this.Controls.Add(this.numAngulo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEntradaRotacao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rotação";
            this.Load += new System.EventHandler(this.frmEntradaRotacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numAngulo)).EndInit();
            this.gbEixo.ResumeLayout(false);
            this.gbEixo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numAngulo;
        private System.Windows.Forms.GroupBox gbEixo;
        private System.Windows.Forms.RadioButton rdZ;
        private System.Windows.Forms.RadioButton rdY;
        private System.Windows.Forms.RadioButton rdX;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numZ;
        private System.Windows.Forms.Label label5;
    }
}