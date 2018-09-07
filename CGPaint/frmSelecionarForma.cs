using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmSelecionarForma : Form
    {
        public int FormaSelecionada { get; set; }

        public List<object> Formas { get; set; }

        public frmSelecionarForma()
        {            
            InitializeComponent();
        }

        private void frmSelecionarForma_Load(object sender, EventArgs e)
        {
            foreach (object o in Formas)
            {
                listFormas.Items.Add(o);
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            FormaSelecionada = listFormas.SelectedIndex;
        }
    }
}
