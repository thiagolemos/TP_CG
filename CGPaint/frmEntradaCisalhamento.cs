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
    public partial class frmEntradaCisalhamento : Form
    {
        public int Forca { get; set; }
        public int PivoX { get; set; }
        public int PivoY { get; set; }

        public frmEntradaCisalhamento()
        {
            InitializeComponent();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Forca = Convert.ToInt32(forca.Value);
            PivoX = Convert.ToInt32(numX.Value);
            PivoY = Convert.ToInt32(numY.Value);
        }
    }
}
