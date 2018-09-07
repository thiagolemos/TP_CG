using System;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmDesenharElipse : Form
    {
        public int EixoMenor { get; set; }
        public int EixoMaior { get; set; }

        public frmDesenharElipse()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EixoMaior = Convert.ToInt32(numRx.Value);
            EixoMenor = Convert.ToInt32(numRy.Value);
        }
    }
}
