using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmInserirObjeto3D : Form
    {
        public List<int> Pontos;
        public List<int> Faces;
        public int OrigemX { get; set; }
        public int OrigemY { get; set; }

        public frmInserirObjeto3D()
        {
            InitializeComponent();
            Pontos = new List<int>();
            Faces = new List<int>();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            OrigemX = Convert.ToInt32(numX.Value);
            OrigemY = Convert.ToInt32(numY.Value);
            Regex rxVertices = new Regex(@"([-]?\d+)\s([-]?\d+)\s([-]?\d+)", RegexOptions.Compiled);
            MatchCollection matchesVertices = rxVertices.Matches(txtVertices.Text);
            Regex rxFaces = new Regex(@"([-]?\d+)+", RegexOptions.Compiled);
            MatchCollection matchesFaces = rxFaces.Matches(txtFaces.Text);
            foreach (Match m in matchesVertices)
            {
                Pontos.Add(int.Parse(m.Groups[1].Value));
                Pontos.Add(int.Parse(m.Groups[2].Value));
                Pontos.Add(int.Parse(m.Groups[3].Value));
            }
            foreach (Match m in matchesFaces)
            {
                Faces.Add(int.Parse(m.Value));
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
