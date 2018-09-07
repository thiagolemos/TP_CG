using System;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmDesenharCirculo : Form
    {
        public int Raio { get; set; }

        public frmDesenharCirculo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Raio = Convert.ToInt32(numRaio.Value);
        }
    }
}
