using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CGPaint
{
    public partial class frmPrincipal : Form
    {
        static int tamanhoPonto = 2;
        FrameBuffer frameBuffer;
        List<object> formas;
        Bitmap visualizacao;
        int tamanhoX;
        int tamanhoY;
        private const short DENTRO = 0;      // 0000
        private const short ESQUERDA = 1;    // 0001
        private const short DIREITA = 2;     // 0010
        private const short ABAIXO = 4;      // 0100
        private const short ACIMA = 8;       // 1000
        bool recorteAtivado = false;
        int xMin;
        int yMin;
        int xMax;
        int yMax;
        bool mostraMalha = false;
        bool criandoLinha = false;
        Ponto linhaInicio;
        Ponto linhaFim;
        bool criandoPoligono = false;
        Ponto verticeInicial;
        Poligono tmpPoligono;

        public frmPrincipal()
        {
            InitializeComponent();
            int x = 450;// pnlTela.Width / tamanhoPonto;
            int y = 220;// pnlTela.Height / tamanhoPonto;
            criarTela(x, y);
            desenharTela();
        }

        private void criarTela(int x, int y)
        {
            // Tamanho da tela
            tamanhoX = x;
            tamanhoY = y;
            // Limites da janela de recorte
            xMin = 0;
            yMin = 0;
            xMax = tamanhoX - 1;
            yMax = tamanhoY - 1;

            int largura = 1 + (tamanhoX * tamanhoPonto);
            int altura = 1 + (tamanhoY * tamanhoPonto);
            visualizacao = new Bitmap(largura, altura);
            pbMonitor.Image = visualizacao;
            pbMonitor.Height = altura;
            pbMonitor.Width = largura;
            pbMonitor.Left = (ClientSize.Width - pbMonitor.Width) / 2;
            pbMonitor.Top = (ClientSize.Height - pbMonitor.Height) / 2;
            frameBuffer = new FrameBuffer(tamanhoX, tamanhoY);
            formas = new List<object>();
        }

        private void desenharTela()
        {
            using (Graphics g = Graphics.FromImage(visualizacao))
            {
                for (int i = 0; i < tamanhoX; i++)
                    for (int j = 0; j < tamanhoY; j++)
                    {
                        if (!frameBuffer.getPonto(i, j).IsEmpty)
                            g.FillRectangle(new SolidBrush(frameBuffer.getPonto(i, j)), i * tamanhoPonto, j * tamanhoPonto, tamanhoPonto, tamanhoPonto);
                        else
                            g.FillRectangle(new SolidBrush(Color.White), i * tamanhoPonto, j * tamanhoPonto, tamanhoPonto, tamanhoPonto);
                        if (mostraMalha)
                            g.DrawRectangle(Pens.Black, i * tamanhoPonto, j * tamanhoPonto, tamanhoPonto, tamanhoPonto);
                        if (recorteAtivado)
                            g.DrawRectangle(Pens.Red, xMin * tamanhoPonto, yMin * tamanhoPonto, (xMax - xMin + 1) * tamanhoPonto, (yMax - yMin + 1) * tamanhoPonto);
                    }
            }
            pbMonitor.Refresh();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCriarNovo frmCriarNovo = new frmCriarNovo();
            frmCriarNovo.ShowDialog(this);

            if (frmCriarNovo.DialogResult == DialogResult.OK)
            {
                criarTela(frmCriarNovo.Largura, frmCriarNovo.Altura);
                desenharTela();
            }
        }

        private void btnMostraMalha_CheckedChanged(object sender, EventArgs e)
        {
            mostraMalha = btnMostraMalha.Checked ? true : false;
            desenharTela();
        }

        private void pbMonitor_MouseMove(object sender, MouseEventArgs e)
        {
            lblPosicaoAtual.Text = String.Format("Posição: ({0}, {1})", e.X / tamanhoPonto, e.Y / tamanhoPonto);
        }

        private void pbMonitor_MouseClick(object sender, MouseEventArgs e)
        {
            int selX = e.X / tamanhoPonto;
            int selY = e.Y / tamanhoPonto;

            if (btnDesenharPonto.Checked)
            {
                Ponto p = new Ponto(selX, selY, Color.Black);
                formas.Add(p);
                processarRecorte(p);
                desenharTela();
            }
            if (btnDesenharLinha.Checked)
            {
                if (!criandoLinha)
                {
                    criandoLinha = true;
                    linhaInicio = new Ponto(selX, selY, Color.Black);
                }
                else
                {
                    linhaFim = new Ponto(selX, selY, Color.Black);
                    Reta reta = new Reta(linhaInicio, linhaFim, Color.Black);
                    formas.Add(reta);
                    processarRecorte(reta);
                    desenharTela();
                    linhaInicio = null;
                    linhaFim = null;
                    criandoLinha = false;
                }
            }
            if (btnDesenharCirculo.Checked)
            {
                frmDesenharCirculo frmDesenharCirculo = new frmDesenharCirculo();
                frmDesenharCirculo.numRaio.Maximum = Math.Min(tamanhoX - 1, tamanhoY - 1);
                frmDesenharCirculo.numRaio.Minimum = 0;
                frmDesenharCirculo.ShowDialog(this);
                if (frmDesenharCirculo.DialogResult == DialogResult.OK)
                {
                    Circulo circulo = new Circulo(selX, selY, frmDesenharCirculo.Raio, Color.Black);
                    formas.Add(circulo);
                    processarRecorte(circulo);
                    desenharTela();
                }
            }
            if (btnDesenharElipse.Checked)
            {
                frmDesenharElipse frmDesenharElipse = new frmDesenharElipse();
                frmDesenharElipse.numRx.Maximum = tamanhoX - 1;
                frmDesenharElipse.numRx.Minimum = 0;
                frmDesenharElipse.numRy.Maximum = tamanhoY - 1;
                frmDesenharElipse.numRy.Minimum = 0;
                frmDesenharElipse.ShowDialog(this);
                if (frmDesenharElipse.DialogResult == DialogResult.OK)
                {
                    Elipse elipse = new Elipse(selX, selY, frmDesenharElipse.EixoMaior, frmDesenharElipse.EixoMenor, Color.Black);
                    formas.Add(elipse);
                    processarRecorte(elipse);
                    desenharTela();
                }
            }
            if (btnDesenharPoligono.Checked)
            {
                Ponto pontoSel = new Ponto(selX, selY, Color.Black);
                if (!criandoPoligono)
                {
                    criandoPoligono = true;
                    verticeInicial = pontoSel;
                    linhaInicio = verticeInicial;
                    tmpPoligono = new Poligono();
                    tmpPoligono.adicionarVertice(verticeInicial);
                }
                else
                {
                    linhaFim = pontoSel;
                    Reta tmpReta = new Reta(linhaInicio, linhaFim, Color.Black);
                    foreach (Ponto p in tmpReta.getPontos())
                        frameBuffer.setPonto(p);
                    desenharTela();
                    linhaInicio = linhaFim;
                    linhaFim = null;
                    if (verticeInicial.getX() == pontoSel.getX() && verticeInicial.getY() == pontoSel.getY())
                    {
                        criandoPoligono = false;
                        formas.Add(tmpPoligono);
                        processarRecorte(tmpPoligono);
                        tmpPoligono = null;
                    }
                    else
                    {
                        tmpPoligono.adicionarVertice(pontoSel);
                    }
                }
            }
            if (btnPreencherRecursivo.Checked)
            {
                preencherRecursivamente(selX, selY);
                desenharTela();
            }
            if (btnPreencherVarredura.Checked)
            {
                List<Reta> retas = new List<Reta>();
                foreach (Poligono p in formas.OfType<Poligono>())
                {
                    retas.AddRange(p.getRetas());
                }
                for (int y = 0; y < tamanhoY; y++)
                {
                    List<int> xIntersecoes = new List<int>();
                    foreach (Reta r in retas)
                    {
                        //Se é um ponto máximo ou mínimo, adicionar duas vezes o vértice.
                        if (y == r.getPontoInicial().getY())
                        {
                            xIntersecoes.Add(r.getPontoInicial().getX());
                            xIntersecoes.Add(r.getPontoInicial().getX());
                        }
                        if (y == r.getPontoFinal().getY())
                        {
                            xIntersecoes.Add(r.getPontoFinal().getX());
                            xIntersecoes.Add(r.getPontoFinal().getX());
                        }
                        //Se não é, adicionar somente uma vez.
                        if (y > r.getYMinimo() && y < r.getYMaximo())
                        {
                            int intersecao = shIntersecaoX(xMin, y, xMax, y,
                                r.getPontoInicial().getX(), r.getPontoInicial().getY(),
                                r.getPontoFinal().getX(), r.getPontoFinal().getY());
                            xIntersecoes.Add(intersecao);
                        }
                    }
                    xIntersecoes.Sort();
                    for (int i = 0; i < xIntersecoes.Count - 1; i+=2)
                    {
                        for (int x = xIntersecoes[i]; x <= xIntersecoes[i + 1]; x++)
                        {
                            frameBuffer.setPonto(new Ponto(x, y, Color.Black));
                        }
                    }
                    desenharTela();
                }
            }
        }

        private void preencherRecursivamente(int i, int j)
        {
            if (frameBuffer.getPonto(i, j) == Color.Black)
                return;
            Ponto p = new Ponto(i, j, Color.Black);
            frameBuffer.setPonto(p);
            if (isCoordenadaValida(i - 1, j))
                preencherRecursivamente(i - 1, j);
            if (isCoordenadaValida(i + 1, j))
                preencherRecursivamente(i + 1, j);
            if (isCoordenadaValida(i, j - 1))
                preencherRecursivamente(i, j - 1);
            if (isCoordenadaValida(i, j + 1))
                preencherRecursivamente(i, j + 1);
        }

        private bool isCoordenadaValida(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < tamanhoX && j < tamanhoY)
                return true;
            else
                return false;
        }

        private void btnRecortarDefinirJanela_Click(object sender, EventArgs e)
        {
            frmDefinirJanelaRecorte frmDefinirJanelaRecorte = new frmDefinirJanelaRecorte();
            frmDefinirJanelaRecorte.LarguraFB = tamanhoX;
            frmDefinirJanelaRecorte.AlturaFB = tamanhoY;
            frmDefinirJanelaRecorte.numX.Minimum = 0;
            frmDefinirJanelaRecorte.numX.Maximum = tamanhoX - 1;
            frmDefinirJanelaRecorte.numY.Minimum = 0;
            frmDefinirJanelaRecorte.numY.Maximum = tamanhoY - 1;
            frmDefinirJanelaRecorte.numLargura.Minimum = 0;
            frmDefinirJanelaRecorte.numLargura.Maximum = tamanhoX - 1;
            frmDefinirJanelaRecorte.numAltura.Minimum = 0;
            frmDefinirJanelaRecorte.numAltura.Maximum = tamanhoY - 1;
            frmDefinirJanelaRecorte.numX.Value = xMin;
            frmDefinirJanelaRecorte.numY.Value = yMin;
            frmDefinirJanelaRecorte.numLargura.Value = xMax - xMin;
            frmDefinirJanelaRecorte.numAltura.Value = yMax - yMin;
            frmDefinirJanelaRecorte.ShowDialog(this);
            if (frmDefinirJanelaRecorte.DialogResult == DialogResult.OK)
            {
                xMin = frmDefinirJanelaRecorte.InicioX;
                yMin = frmDefinirJanelaRecorte.InicioY;
                xMax = xMin + frmDefinirJanelaRecorte.Largura;
                yMax = yMin + frmDefinirJanelaRecorte.Altura;
                if (recorteAtivado)
                    aplicarRecorte();
                else
                    desenharTela();
            }
        }

        private void btnRecortarAplicar_Click(object sender, EventArgs e)
        {
            recorteAtivado = btnRecortarAplicar.Checked ? true : false;
            aplicarRecorte();
        }

        private void aplicarRecorte()
        {
            frameBuffer.limparPontos();
            foreach (Ponto p in formas.OfType<Ponto>())
                processarRecorte(p);
            foreach (Reta r in formas.OfType<Reta>())
                processarRecorte(r);
            foreach (Circulo c in formas.OfType<Circulo>())
                processarRecorte(c);
            foreach (Elipse c in formas.OfType<Elipse>())
                processarRecorte(c);
            foreach (Poligono p in formas.OfType<Poligono>())
                processarRecorte(p);
            foreach (Poligono3D p in formas.OfType<Poligono3D>())
                processarRecorte(p);
            desenharTela();
        }

        private void processarRecorte(Ponto p)
        {
            if (recorteAtivado)
            {
                if (p.getX() >= xMin && p.getX() <= xMax && p.getY() >= yMin && p.getY() <= yMax)
                    frameBuffer.setPonto(p);
            }
            else
            {
                frameBuffer.setPonto(p);
            }
        }

        private void processarRecorte(Reta r)
        {
            if (recorteAtivado)
            {
                //Cohen-Sutherland
                bool aceito = false;
                short p1 = csGeraCodigo(r.getPontoInicial().getX(), r.getPontoInicial().getY());
                short p2 = csGeraCodigo(r.getPontoFinal().getX(), r.getPontoFinal().getY());
                int x0 = r.getPontoInicial().getX();
                int y0 = r.getPontoInicial().getY();
                int x1 = r.getPontoFinal().getX();
                int y1 = r.getPontoFinal().getY();

                while (aceito == false)
                {
                    // Totalmente dentro? Aceitar.
                    if ((p1 | p2) == DENTRO)
                    {
                        aceito = true;
                        break;
                    }
                    // Totalmente fora? Rejeitar.
                    else if ((p1 & p2) != DENTRO)
                    {
                        break;
                    }
                    // Não está totalmente dentro, nem totalmente fora.
                    else
                    {
                        // m = deltaY / deltaX
                        // x = x0 + (1/m) * y - y0
                        // y = y0 + m * (x - x0)
                        int x = 0;
                        int y = 0;
                        short pFora = (p1 != DENTRO) ? p1 : p2;
                        if ((pFora & ACIMA) != 0)
                        {
                            x = x0 + (x1 - x0) * (yMax - y0) / (y1 - y0);
                            y = yMax;
                        }
                        else if ((pFora & ABAIXO) != 0)
                        {
                            x = x0 + (x1 - x0) * (yMin - y0) / (y1 - y0);
                            y = yMin;
                        }
                        else if ((pFora & DIREITA) != 0)
                        {
                            y = y0 + (y1 - y0) * (xMax - x0) / (x1 - x0);
                            x = xMax;
                        }
                        else if ((pFora & ESQUERDA) != 0)
                        {
                            y = y0 + (y1 - y0) * (xMin - x0) / (x1 - x0);
                            x = xMin;
                        }

                        if (pFora == p1)
                        {
                            x0 = x;
                            y0 = y;
                            p1 = csGeraCodigo(x0, y0);
                        }
                        else
                        {
                            x1 = x;
                            y1 = y;
                            p2 = csGeraCodigo(x1, y1);
                        }

                    }
                }
                if (aceito)
                {
                    Reta tmpReta = new Reta(new Ponto(x0, y0, r.getCor()), new Ponto(x1, y1, r.getCor()), r.getCor());
                    foreach (Ponto p in tmpReta.getPontos())
                        frameBuffer.setPonto(p);
                }
            }
            else
            {
                foreach (Ponto p in r.getPontos())
                    frameBuffer.setPonto(p);
            }
        }

        private short csGeraCodigo(int x, int y)
        {
            short codigo = 0;
            if (x < xMin)
                codigo |= ESQUERDA;
            if (x > xMax)
                codigo |= DIREITA;
            if (y < yMin)
                codigo |= ABAIXO;
            if (y > yMax)
                codigo |= ACIMA;
            return codigo;
        }

        private void processarRecorte(Circulo c)
        {
            if (recorteAtivado)
            {
                foreach (Ponto p in c.getPontos())
                {
                    if (p.getX() >= xMin && p.getX() <= xMax && p.getY() >= yMin && p.getY() <= yMax)
                        frameBuffer.setPonto(p);
                }
            }
            else
            {
                foreach (Ponto p in c.getPontos())
                    frameBuffer.setPonto(p);
            }
        }

        private void processarRecorte(Elipse e)
        {
            if (recorteAtivado)
            {
                foreach (Ponto p in e.getPontos())
                {
                    if (p.getX() >= xMin && p.getX() <= xMax && p.getY() >= yMin && p.getY() <= yMax)
                        frameBuffer.setPonto(p);
                }
            }
            else
            {
                foreach (Ponto p in e.getPontos())
                    frameBuffer.setPonto(p);
            }
        }

        private void processarRecorte(Poligono p)
        {
            //Sutherland-Hodgman
            if (recorteAtivado)
            {
                List<Ponto> vertices = p.getVertices();
                List<Ponto> verticesP1 = shProcessaLado(vertices, ESQUERDA);
                List<Ponto> verticesP2 = shProcessaLado(verticesP1, DIREITA);
                List<Ponto> verticesP3 = shProcessaLado(verticesP2, ABAIXO);
                List<Ponto> verticesP4 = shProcessaLado(verticesP3, ACIMA);

                Poligono shPoligonoRecortado = new Poligono();
                shPoligonoRecortado.setVertices(verticesP4);
                foreach (Ponto x in shPoligonoRecortado.getPontos())
                    frameBuffer.setPonto(x);
            }
            else
            {
                foreach (Ponto x in p.getPontos())
                    frameBuffer.setPonto(x);
            }
        }

        private void processarRecorte(Poligono3D p)
        {
            //Recorte 3D não foi implementado
            List<Ponto> pontosProjecao = new List<Ponto>();
            List<Ponto3D> vertices = p.getVertices();
            List<List<int>> faces = p.getFaces();
            int origemX = p.getOrigem().getX();
            int origemY = p.getOrigem().getY();
            foreach (List<int> face in faces)
            {
                Poligono faceProjecao = new Poligono();
                for (int i = 0; i < face.Count; i++)
                {
                    Ponto3D tmpPto3D = vertices[face[i]];
                    int[,] tmpCoord = tmpPto3D.getCoordenadaHomogenea();
                    Ponto tmpPto2D;
                    if (btnObjetos3DProjecaoOrtografica.Checked)
                    {
                        int[,] matrizProjOrtograficaXY = { {1, 0, 0, 0 },
                                                           {0, 1, 0, 0 },
                                                           {0, 0, 0, 0 },
                                                           {0, 0, 0, 1 } };
                        int[,] matrizProj = Matriz.Multiplicacao(matrizProjOrtograficaXY, tmpCoord);
                        tmpPto2D = new Ponto(matrizProj[0, 0] + origemX,
                                             matrizProj[1, 0] + origemY,
                                             Color.Black);
                        faceProjecao.adicionarVertice(tmpPto2D);
                    }
                    if (btnObjetos3DProjecaoAxiometrica.Checked)
                    {
                        int anguloGraus = 30;
                        double angulo = (Math.PI / 180) * anguloGraus;
                        //Rotação em X
                        int[,] matrizProj =  Matriz.Multiplicacao(
                                new double[,] { {1, 0, 0, 0 },
                                                {0, Math.Cos(angulo), -Math.Sin(angulo), 0 },
                                                {0, Math.Sin(angulo), Math.Cos(angulo), 0 },
                                                {0, 0, 0, 1 }},
                                tmpCoord);
                        //Rotação em Y
                        matrizProj = Matriz.Multiplicacao(
                                new double[,] { {Math.Cos(angulo), 0, Math.Sin(angulo), 0},
                                                {0, 1, 0, 0},
                                                {-Math.Sin(angulo), 0, Math.Cos(angulo), 0},
                                                {0, 0, 0, 1}},
                                matrizProj
                        );
                        tmpPto2D = new Ponto(matrizProj[0, 0] + origemX,
                                             matrizProj[1, 0] + origemY,
                                             Color.Black);
                        faceProjecao.adicionarVertice(tmpPto2D);
                    }
                    if (btnObjetos3DProjecaoObliqua.Checked)
                    {
                        int anguloGraus = 30;
                        double angulo = (Math.PI / 180) * anguloGraus;
                        double fator = 1.0/2.0;
                        int[,] matrizProj = Matriz.Multiplicacao(
                            new double[,] { {1, 0, fator*Math.Cos(angulo), 0},
                                            {0, 1, fator*Math.Sin(angulo), 0},
                                            {0, 0, 0, 0},
                                            {0, 0, 0, 1} },
                            tmpCoord);
                        tmpPto2D = new Ponto(matrizProj[0, 0] + origemX,
                                             matrizProj[1, 0] + origemY,
                                             Color.Black);
                        faceProjecao.adicionarVertice(tmpPto2D);
                    }
                }
                pontosProjecao.AddRange(faceProjecao.getPontos());
            }
            foreach (Ponto x in pontosProjecao)
                frameBuffer.setPonto(x);
        }

        private List<Ponto> shProcessaLado(List<Ponto> vertices, int lado)
        {
            List<Ponto> tmpVertices = new List<Ponto>();
            for (int i = 0; i < vertices.Count; i++)
            {
                Ponto p1 = vertices[i];
                Ponto p2 = vertices[(i + 1) % vertices.Count];
                //Somente o segundo ponto está dentro (Reta entrando)
                //Adicionar interseção com a janela e segundo ponto
                if (!shIsPontoDentro(p1, lado) && shIsPontoDentro(p2, lado))
                {
                    tmpVertices.Add(shIntersecao(p1, p2, lado));
                    tmpVertices.Add(p2);
                }
                // Ambos os pontos estão dentro
                // Adicionar somente o segundo ponto
                if (shIsPontoDentro(p1, lado) && shIsPontoDentro(p2, lado))
                {
                    tmpVertices.Add(p2);
                }
                //Somente o primeiro ponto está dentro (Reta saindo)
                //Adicionar interseção com a janela somente
                if (shIsPontoDentro(p1, lado) && !shIsPontoDentro(p2, lado))
                {
                    tmpVertices.Add(shIntersecao(p1, p2, lado));
                }
                //Nenhum ponto está dentro, adicionar nenhum
                //if (!shIsPontoDentro(p1, lado) && !shIsPontoDentro(p2, lado))

                //O resultado (tmpVertices) deve ser alimentado para a proxima iteração de lado.
            }
            return tmpVertices;
        }

        private bool shIsPontoDentro(Ponto p, int lado)
        {
            bool resultado = false;
            switch (lado)
            {
                case ESQUERDA:
                    resultado = (p.getX() >= xMin);
                    break;
                case DIREITA:
                    resultado = (p.getX() <= xMax);
                    break;
                case ABAIXO:
                    resultado = (p.getY() <= yMax);
                    break;
                case ACIMA:
                    resultado = (p.getY() >= yMin);
                    break;
            }
            return resultado;
        }

        private Ponto shIntersecao(Ponto p1, Ponto p2, int lado)
        {
            int x1 = p1.getX();
            int y1 = p1.getY();
            int x2 = p2.getX();
            int y2 = p2.getY();
            int xInt = -1;
            int yInt = -1;

            switch (lado)
            {
                case ESQUERDA:
                    xInt = shIntersecaoX(xMin, yMin, xMin, yMax, x1, y1, x2, y2);
                    yInt = shIntersecaoY(xMin, yMin, xMin, yMax, x1, y1, x2, y2);
                    break;
                case DIREITA:
                    xInt = shIntersecaoX(xMax, yMin, xMax, yMax, x1, y1, x2, y2);
                    yInt = shIntersecaoY(xMax, yMin, xMax, yMax, x1, y1, x2, y2);
                    break;
                case ACIMA:
                    xInt = shIntersecaoX(xMin, yMin, xMax, yMin, x1, y1, x2, y2);
                    yInt = shIntersecaoY(xMin, yMin, xMax, yMin, x1, y1, x2, y2);
                    break;
                case ABAIXO:
                    xInt = shIntersecaoX(xMin, yMax, xMax, yMax, x1, y1, x2, y2);
                    yInt = shIntersecaoY(xMin, yMax, xMax, yMax, x1, y1, x2, y2);
                    break;
            }

            Ponto resultado = new Ponto(xInt, yInt, p1.getCor());
            return resultado;
        }

        private int shIntersecaoX(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            int num = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            int den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            return num / den;
        }

        private int shIntersecaoY(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            int num = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
            int den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            return num / den;
        }

        private void btnDesenhar_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)e.ClickedItem;
            ToolStripDropDownButton ownerItem = (ToolStripDropDownButton)clickedItem.OwnerItem;
            bool isItemMarcado = clickedItem.Checked;
            if (ownerItem != null)
                foreach (ToolStripMenuItem item in ownerItem.DropDownItems)
                    item.Checked = false;
            clickedItem.Checked = isItemMarcado ? true : false;
            foreach (ToolStripMenuItem item in btnPreencher.DropDownItems)
                item.Checked = false;
        }

        private void btnPreencher_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)e.ClickedItem;
            ToolStripDropDownButton ownerItem = (ToolStripDropDownButton)clickedItem.OwnerItem;
            bool isItemMarcado = clickedItem.Checked;
            if (ownerItem != null)
                foreach (ToolStripMenuItem item in ownerItem.DropDownItems)
                    item.Checked = false;
            clickedItem.Checked = isItemMarcado ? true : false;
            foreach (ToolStripMenuItem item in btnDesenhar.DropDownItems)
                item.Checked = false;
        }

        private int selecionarForma()
        {
            frmSelecionarForma frmSelecionarForma = new frmSelecionarForma();
            frmSelecionarForma.Formas = formas;
            frmSelecionarForma.ShowDialog(this);
            if (frmSelecionarForma.DialogResult == DialogResult.OK)
            {
                return frmSelecionarForma.FormaSelecionada;
            }
            return -1;
        }

        private bool isForma3D(object o)
        {
            return (o.GetType() == typeof(Poligono3D));
        }

        private void btnTransformarTranslacao_Click(object sender, EventArgs e)
        {
            int sel = selecionarForma();
            if (sel == -1)
                return;
            bool Forma3D = isForma3D(formas[sel]);
            if (Forma3D)
            {
                frmEntradaTranslacao frmEntradaTranslacao = new frmEntradaTranslacao();
                frmEntradaTranslacao.is3D = true;
                frmEntradaTranslacao.ShowDialog(this);
                if (frmEntradaTranslacao.DialogResult == DialogResult.OK)
                {
                    int[,] matrizTranslacao = { { 1, 0, 0, frmEntradaTranslacao.X},
                                                { 0, 1, 0, frmEntradaTranslacao.Y},
                                                { 0, 0, 1, frmEntradaTranslacao.Z},
                                                { 0, 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Poligono3D))
                    {
                        List<Ponto3D> vertices = ((Poligono3D)formas[sel]).getVertices();
                        foreach (Ponto3D v in vertices)
                        {
                            int[,] novoPonto = Matriz.Multiplicacao(matrizTranslacao, v.getCoordenadaHomogenea());
                            v.setCoordenadaHomogenea(novoPonto);
                        }
                        ((Poligono3D)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
            else
            {
                frmEntradaTranslacao frmEntradaTranslacao = new frmEntradaTranslacao();
                frmEntradaTranslacao.is3D = false;
                frmEntradaTranslacao.ShowDialog(this);
                if (frmEntradaTranslacao.DialogResult == DialogResult.OK)
                {
                    int[,] matrizTranslacao = { { 1, 0, frmEntradaTranslacao.X},
                                                { 0, 1, frmEntradaTranslacao.Y},
                                                { 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Ponto))
                    {
                        int[,] novoPonto = Matriz.Multiplicacao(matrizTranslacao, ((Ponto)formas[sel]).getCoordenadaHomogenea());
                        ((Ponto)formas[sel]).setCoordenadaHomogenea(novoPonto);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Reta))
                    {
                        int[,] novoInicio = Matriz.Multiplicacao(matrizTranslacao, ((Reta)formas[sel]).getPontoInicial().getCoordenadaHomogenea());
                        ((Reta)formas[sel]).getPontoInicial().setCoordenadaHomogenea(novoInicio);
                        int[,] novoFim = Matriz.Multiplicacao(matrizTranslacao, ((Reta)formas[sel]).getPontoFinal().getCoordenadaHomogenea());
                        ((Reta)formas[sel]).getPontoFinal().setCoordenadaHomogenea(novoFim);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Circulo))
                    {
                        Ponto centro = ((Circulo)formas[sel]).getCentro();
                        int[,] novoCentro = Matriz.Multiplicacao(matrizTranslacao, centro.getCoordenadaHomogenea());
                        centro.setCoordenadaHomogenea(novoCentro);
                        ((Circulo)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Elipse))
                    {
                        Ponto centro = ((Elipse)formas[sel]).getCentro();
                        int[,] novoCentro = Matriz.Multiplicacao(matrizTranslacao, centro.getCoordenadaHomogenea());
                        centro.setCoordenadaHomogenea(novoCentro);
                        ((Elipse)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Poligono))
                    {
                        List<Ponto> vertices = ((Poligono)formas[sel]).getVertices();
                        foreach (Ponto v in vertices)
                        {
                            int[,] novoPonto = Matriz.Multiplicacao(matrizTranslacao, v.getCoordenadaHomogenea());
                            v.setCoordenadaHomogenea(novoPonto);
                        }
                        ((Poligono)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
        }

        private void btnTransformarRotacao_Click(object sender, EventArgs e)
        {
            int sel = selecionarForma();
            if (sel == -1)
                return;
            bool Forma3D = isForma3D(formas[sel]);
            if (Forma3D)
            {
                frmEntradaRotacao frmEntradaRotacao = new frmEntradaRotacao();
                frmEntradaRotacao.is3D = true;
                frmEntradaRotacao.ShowDialog(this);
                if (frmEntradaRotacao.DialogResult == DialogResult.OK)
                {
                    double angulo = (Math.PI / 180) * frmEntradaRotacao.Angulo;
                    int x = frmEntradaRotacao.PivoX;
                    int y = frmEntradaRotacao.PivoY;
                    int z = frmEntradaRotacao.PivoZ;
                    int[,] matrizTranslacaoIda = { {1, 0, 0, -x },
                                                   {0, 1, 0, -y },
                                                   {0, 0, 1, -z },
                                                   {0, 0, 0, 1 } };
                    int[,] matrizTranslacaoVolta = { {1, 0, 0, x },
                                                     {0, 1, 0, y },
                                                     {0, 0, 1, z },
                                                     {0, 0, 0, 1 } };
                    double[,] matrizRotacaoX = { { 1, 0, 0, 0},
                                                 { 0, Math.Cos(angulo), -Math.Sin(angulo), 0},
                                                 { 0, Math.Sin(angulo), Math.Cos(angulo), 0},
                                                 { 0, 0, 0, 1}};
                    double[,] matrizRotacaoY = { { Math.Cos(angulo), 0, Math.Sin(angulo), 0},
                                                 { 0, 1, 0, 0},
                                                 { -Math.Sin(angulo), 0, Math.Cos(angulo), 0},
                                                 { 0, 0, 0, 1}};
                    double[,] matrizRotacaoZ = { {Math.Cos(angulo), -Math.Sin(angulo), 0, 0},
                                                 {Math.Sin(angulo), Math.Cos(angulo), 0, 0},
                                                 { 0, 0, 1, 0},
                                                 { 0, 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Poligono3D))
                    {
                        List<Ponto3D> vertices = ((Poligono3D)formas[sel]).getVertices();
                        foreach (Ponto3D v in vertices)
                        {
                            if (frmEntradaRotacao.EixoX)
                            {
                                int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, v.getCoordenadaHomogenea());
                                int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacaoX, ptoOrigem);
                                int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                                v.setCoordenadaHomogenea(resultado);
                            }
                            if (frmEntradaRotacao.EixoY)
                            {
                                int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, v.getCoordenadaHomogenea());
                                int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacaoY, ptoOrigem);
                                int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                                v.setCoordenadaHomogenea(resultado);
                            }
                            if (frmEntradaRotacao.EixoZ)
                            {
                                int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, v.getCoordenadaHomogenea());
                                int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacaoZ, ptoOrigem);
                                int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                                v.setCoordenadaHomogenea(resultado);
                            }
                        }
                        ((Poligono3D)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
            else
            {
                frmEntradaRotacao frmEntradaRotacao = new frmEntradaRotacao();
                frmEntradaRotacao.is3D = false;
                frmEntradaRotacao.ShowDialog(this);
                if (frmEntradaRotacao.DialogResult == DialogResult.OK)
                {
                    double angulo = (Math.PI / 180) * frmEntradaRotacao.Angulo;
                    int x = frmEntradaRotacao.PivoX;
                    int y = frmEntradaRotacao.PivoY;
                    int[,] matrizTranslacaoIda = { {1, 0, -x },
                                                   {0, 1, -y },
                                                   {0, 0, 1 } };
                    int[,] matrizTranslacaoVolta = { {1, 0, x },
                                                     {0, 1, y },
                                                     {0, 0, 1 } };
                    double[,] matrizRotacao = { { Math.Cos(angulo), -Math.Sin(angulo), 0},
                                                { Math.Sin(angulo), Math.Cos(angulo), 0},
                                                { 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Ponto))
                    {
                        int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, ((Ponto)formas[sel]).getCoordenadaHomogenea());
                        int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacao, ptoOrigem);
                        int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                        ((Ponto)formas[sel]).setCoordenadaHomogenea(resultado);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Reta))
                    {
                        int[,] ptoOrigemInicio = Matriz.Multiplicacao(matrizTranslacaoIda, ((Reta)formas[sel]).getPontoInicial().getCoordenadaHomogenea());
                        int[,] ptoRotacaoInicio = Matriz.Multiplicacao(matrizRotacao, ptoOrigemInicio);
                        int[,] resultadoInicio = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacaoInicio);
                        ((Reta)formas[sel]).getPontoInicial().setCoordenadaHomogenea(resultadoInicio);
                        int[,] ptoOrigemFim = Matriz.Multiplicacao(matrizTranslacaoIda, ((Reta)formas[sel]).getPontoFinal().getCoordenadaHomogenea());
                        int[,] ptoRotacaoFim = Matriz.Multiplicacao(matrizRotacao, ptoOrigemFim);
                        int[,] resultadoFim = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacaoFim);
                        ((Reta)formas[sel]).getPontoFinal().setCoordenadaHomogenea(resultadoFim);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Circulo))
                    {
                        Ponto centro = ((Circulo)formas[sel]).getCentro();
                        int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, centro.getCoordenadaHomogenea());
                        int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacao, ptoOrigem);
                        int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                        centro.setCoordenadaHomogenea(resultado);
                        ((Circulo)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Elipse))
                    {
                        Ponto centro = ((Elipse)formas[sel]).getCentro();
                        int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, centro.getCoordenadaHomogenea());
                        int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacao, ptoOrigem);
                        int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                        centro.setCoordenadaHomogenea(resultado);
                        ((Elipse)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Poligono))
                    {
                        List<Ponto> vertices = ((Poligono)formas[sel]).getVertices();
                        foreach (Ponto v in vertices)
                        {
                            int[,] ptoOrigem = Matriz.Multiplicacao(matrizTranslacaoIda, v.getCoordenadaHomogenea());
                            int[,] ptoRotacao = Matriz.Multiplicacao(matrizRotacao, ptoOrigem);
                            int[,] resultado = Matriz.Multiplicacao(matrizTranslacaoVolta, ptoRotacao);
                            v.setCoordenadaHomogenea(resultado);
                        }
                        ((Poligono)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
        }

        private void btnTransformarEscala_Click(object sender, EventArgs e)
        {
            int sel = selecionarForma();
            if (sel == -1)
                return;
            bool Forma3D = isForma3D(formas[sel]);
            if (Forma3D)
            {
                frmEntradaEscala frmEntradaEscala = new frmEntradaEscala();
                frmEntradaEscala.is3D = true;
                frmEntradaEscala.ShowDialog(this);
                if (frmEntradaEscala.DialogResult == DialogResult.OK)
                {
                    double[,] matrizEscala = { { frmEntradaEscala.X, 0, 0, 0},
                                               { 0, frmEntradaEscala.Y, 0, 0},
                                               { 0, 0, frmEntradaEscala.Z, 0},
                                               { 0, 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Poligono3D))
                    {
                        List<Ponto3D> vertices = ((Poligono3D)formas[sel]).getVertices();
                        foreach (Ponto3D v in vertices)
                        {
                            int[,] novoPonto = Matriz.Multiplicacao(matrizEscala, v.getCoordenadaHomogenea());
                            v.setCoordenadaHomogenea(novoPonto);
                        }
                        ((Poligono3D)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
            else
            {
                frmEntradaEscala frmEntradaEscala = new frmEntradaEscala();
                frmEntradaEscala.is3D = false;
                frmEntradaEscala.ShowDialog(this);
                if (frmEntradaEscala.DialogResult == DialogResult.OK)
                {
                    double[,] matrizEscala = { { frmEntradaEscala.X, 0, 0},
                                               { 0, frmEntradaEscala.Y, 0},
                                               { 0, 0, 1}};
                    Type t = formas[sel].GetType();
                    if (t == typeof(Ponto))
                    {
                        int[,] novoPonto = Matriz.Multiplicacao(matrizEscala, ((Ponto)formas[sel]).getCoordenadaHomogenea());
                        ((Ponto)formas[sel]).setCoordenadaHomogenea(novoPonto);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Reta))
                    {
                        int[,] novoInicio = Matriz.Multiplicacao(matrizEscala, ((Reta)formas[sel]).getPontoInicial().getCoordenadaHomogenea());
                        ((Reta)formas[sel]).getPontoInicial().setCoordenadaHomogenea(novoInicio);
                        int[,] novoFim = Matriz.Multiplicacao(matrizEscala, ((Reta)formas[sel]).getPontoFinal().getCoordenadaHomogenea());
                        ((Reta)formas[sel]).getPontoFinal().setCoordenadaHomogenea(novoFim);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Circulo))
                    {
                        Ponto centro = ((Circulo)formas[sel]).getCentro();
                        int[,] novoCentro = Matriz.Multiplicacao(matrizEscala, centro.getCoordenadaHomogenea());
                        centro.setCoordenadaHomogenea(novoCentro);
                        ((Circulo)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Elipse))
                    {
                        Ponto centro = ((Elipse)formas[sel]).getCentro();
                        int[,] novoCentro = Matriz.Multiplicacao(matrizEscala, centro.getCoordenadaHomogenea());
                        centro.setCoordenadaHomogenea(novoCentro);
                        ((Elipse)formas[sel]).setCentro(centro);
                        aplicarRecorte();
                        return;
                    }
                    if (t == typeof(Poligono))
                    {
                        List<Ponto> vertices = ((Poligono)formas[sel]).getVertices();
                        foreach (Ponto v in vertices)
                        {
                            int[,] novoPonto = Matriz.Multiplicacao(matrizEscala, v.getCoordenadaHomogenea());
                            v.setCoordenadaHomogenea(novoPonto);
                        }
                        ((Poligono)formas[sel]).setVertices(vertices);
                        aplicarRecorte();
                        return;
                    }
                }
                desenharTela();
            }
        }

        private void btnObjetos3DInserir_Click(object sender, EventArgs e)
        {
            frmInserirObjeto3D frmInserirObjeto3D = new frmInserirObjeto3D();
            frmInserirObjeto3D.ShowDialog(this);
            if (frmInserirObjeto3D.DialogResult == DialogResult.OK)
            {
                Poligono3D novoPoligono = new Poligono3D();
                novoPoligono.setOrigem(frmInserirObjeto3D.OrigemX, frmInserirObjeto3D.OrigemY);
                List<Ponto3D> vertices = new List<Ponto3D>();
                int i;
                for (i = 0; i < frmInserirObjeto3D.Pontos.Count; i+=3)
                {
                    vertices.Add(new Ponto3D(frmInserirObjeto3D.Pontos[i],
                                             frmInserirObjeto3D.Pontos[i+1],
                                             frmInserirObjeto3D.Pontos[i+2]));
                }
                novoPoligono.setVertices(vertices);
                i = 0;
                while (i < frmInserirObjeto3D.Faces.Count)
                {
                    List<int> novaFace = new List<int>();
                    while (frmInserirObjeto3D.Faces[i] != -1)
                    {
                        novaFace.Add(frmInserirObjeto3D.Faces[i]);
                        i++;
                    }
                    novoPoligono.adicionarFace(novaFace);
                    i++;
                }
                formas.Add(novoPoligono);
                aplicarRecorte();
            }
        }

        private void btnObjetos3D_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)e.ClickedItem;
            if (clickedItem != btnObjetos3DInserir)
            {
                ToolStripDropDownButton ownerItem = (ToolStripDropDownButton)clickedItem.OwnerItem;
                bool isItemMarcado = clickedItem.Checked;
                if (ownerItem != null)
                    foreach (ToolStripMenuItem item in ownerItem.DropDownItems.OfType<ToolStripMenuItem>())
                        item.Checked = false;
                clickedItem.Checked = isItemMarcado ? true : false;
            }
        }
    }
}
