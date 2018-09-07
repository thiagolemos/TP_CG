using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmEntradaReflexao : Form
    {
        public int Angulo { get; set; }
        public int PivoX { get; set; }
        public int PivoY { get; set; }

        public frmEntradaReflexao()
        {
            InitializeComponent();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            PivoX = Convert.ToInt32(numX.Value);
            PivoY = Convert.ToInt32(numY.Value);
        }
    }
}
