using System;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmCriarNovo : Form
    {
        public int Largura { get; set; }
        public int Altura { get; set; }

        public frmCriarNovo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Largura = Convert.ToInt32(numLargura.Value);
            Altura = Convert.ToInt32(numAltura.Value);
        }
    }
}
