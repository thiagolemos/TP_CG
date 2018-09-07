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
    public partial class frmEntradaTranslacao : Form
    {
        public bool is3D = false;
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public frmEntradaTranslacao()
        {
            InitializeComponent();
        }

        private void frmEntradaTranslacao_Load(object sender, EventArgs e)
        {
            if (is3D)
            {
                label3.Visible = true;
                numZ.Visible = true;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            X = Convert.ToInt32(numX.Value);
            Y = Convert.ToInt32(numY.Value);
            if (is3D)
                Z = Convert.ToInt32(numZ.Value);
        }
    }
}
