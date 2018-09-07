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
    public partial class frmEntradaEscala : Form
    {
        public bool is3D = false;
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public frmEntradaEscala()
        {
            InitializeComponent();
        }

        private void frmEntradaEscala_Load(object sender, EventArgs e)
        {
            if (is3D)
            {
                label3.Visible = true;
                numZ.Visible = true;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            X = Convert.ToDouble(numX.Value);
            Y = Convert.ToDouble(numY.Value);
            if (is3D)
                Z = Convert.ToDouble(numZ.Value);
        }
    }
}
