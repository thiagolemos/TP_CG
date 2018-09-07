namespace CGPaint
{
    partial class frmInserirObjeto3D
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
            this.txtVertices = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFaces = new System.Windows.Forms.TextBox();
            this.btnInserir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vértices:\r\nInserir vértices no formato \"x y z\" e separar cada vértice por \",\" (ví" +
    "rgula).";
            // 
            // txtVertices
            // 
            this.txtVertices.Location = new System.Drawing.Point(15, 38);
            this.txtVertices.Multiline = true;
            this.txtVertices.Name = "txtVertices";
            this.txtVertices.Size = new System.Drawing.Size(352, 120);
            this.txtVertices.TabIndex = 1;
            this.txtVertices.Text = "0 0 0,\r\n10 0 0,\r\n10 10 0,\r\n0 10 0,\r\n0 0 -10,\r\n10 0 -10,\r\n10 10 -10,\r\n0 10 -10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Faces:\r\nIndicar vértices pelo seu índice, separando por \",\" (vírgula).\r\nSeparar c" +
    "ada face por linhas, terminar linha com -1.";
            // 
            // txtFaces
            // 
            this.txtFaces.Location = new System.Drawing.Point(15, 203);
            this.txtFaces.Multiline = true;
            this.txtFaces.Name = "txtFaces";
            this.txtFaces.Size = new System.Drawing.Size(352, 116);
            this.txtFaces.TabIndex = 3;
            this.txtFaces.Text = "0 1 2 3 -1\r\n1 5 6 2 -1\r\n0 4 7 3 -1\r\n0 1 5 4 -1\r\n3 2 6 7 -1\r\n4 5 6 7 -1";
            // 
            // btnInserir
            // 
            this.btnInserir.Location = new System.Drawing.Point(112, 374);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(75, 23);
            this.btnInserir.TabIndex = 4;
            this.btnInserir.Text = "Inserir";
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(193, 374);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ponto de origem:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "X:";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(35, 338);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(80, 20);
            this.numX.TabIndex = 8;
            this.numX.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(144, 338);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(80, 20);
            this.numY.TabIndex = 9;
            this.numY.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Y:";
            // 
            // frmInserirObjeto3D
            // 
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(379, 409);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.txtFaces);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVertices);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInserirObjeto3D";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inserir objeto 3D";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVertices;
        private System.Windows.Forms.TextBox txtFaces;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label5;
    }
}