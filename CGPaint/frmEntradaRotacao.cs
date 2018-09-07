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
    public partial class frmEntradaRotacao : Form
    {
        public bool is3D = false;
        public int Angulo { get; set; }
        public int PivoX { get; set; }
        public int PivoY { get; set; }
        public int PivoZ { get; set; }
        public bool EixoX;
        public bool EixoY;
        public bool EixoZ;

        public frmEntradaRotacao()
        {
            InitializeComponent();
        }

        private void frmEntradaRotacao_Load(object sender, EventArgs e)
        {
            if (is3D)
            {
                gbEixo.Visible = true;
                label5.Visible = true;
                numZ.Visible = true;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Angulo = Convert.ToInt32(numAngulo.Value);
            PivoX = Convert.ToInt32(numX.Value);
            PivoY = Convert.ToInt32(numY.Value);
            if (is3D)
            {
                PivoZ = Convert.ToInt32(numZ.Value);
                EixoX = rdX.Checked ? true : false;
                EixoY = rdY.Checked ? true : false;
                EixoZ = rdZ.Checked ? true : false;
            }
        }
    }
}
