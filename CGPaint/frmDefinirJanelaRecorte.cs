using System;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmDefinirJanelaRecorte : Form
    {
        public int InicioX { get; set; }
        public int InicioY { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int LarguraFB { get; set; }
        public int AlturaFB { get; set; }

        public frmDefinirJanelaRecorte()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InicioX = Convert.ToInt32(numX.Value);
            InicioY = Convert.ToInt32(numY.Value);
            Largura = Convert.ToInt32(numLargura.Value);
            Altura = Convert.ToInt32(numAltura.Value);
        }

        private void numX_Validated(object sender, EventArgs e)
        {
            numLargura.Maximum = (LarguraFB - 1) - numX.Value;
        }

        private void numY_Validated(object sender, EventArgs e)
        {
            numAltura.Maximum = (AlturaFB - 1) - numY.Value;
        }
    }
}
