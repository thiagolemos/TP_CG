using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGPaint
{
    class Ponto
    {
        private int x;
        private int y;
        private Color cor;

        public Ponto(int x, int y, Color cor)
        {
            this.x = x;
            this.y = y;
            this.cor = cor;
        }

        public void setCoordenada(int novoX, int novoY)
        {
            x = novoX;
            y = novoY;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setCor(Color novaCor)
        {
            cor = novaCor;
        }

        public Color getCor()
        {
            return cor;
        }

        public int[,] getCoordenadaHomogenea()
        {
            int[,] coord = { { x },
                             { y },
                             { 1 } };
            return coord;
        }

        public void setCoordenadaHomogenea(int[,] coord)
        {
            setCoordenada(coord[0, 0],
                          coord[1, 0]);
        }

        public override string ToString()
        {
            return ("Ponto: (" + x + ", " + y + ")");
        }
    }

    class Reta
    {
        private Ponto inicio;
        private Ponto fim;
        private Color cor;
        private HashSet<Ponto> pontos;
        private string tipo;

        public Reta(Ponto inicio, Ponto fim, Color cor, string tipo)
        {
            this.inicio = inicio;
            this.fim = fim;
            this.cor = cor;
            this.tipo = tipo;
            pontos = new HashSet<Ponto>();

            if (tipo == "DDA")
                RetaDda();
            else
                RetaBresenham();
        }

        public Ponto getPontoInicial()
        {
            return inicio;
        }

        public Ponto getPontoFinal()
        {
            return fim;
        }

        public Color getCor()
        {
            return cor;
        }

        public int getYMinimo()
        {
            return Math.Min(inicio.getY(), fim.getY());
        }

        public int getYMaximo()
        {
            return Math.Max(inicio.getY(), fim.getY());
        }

        public int getXPontoMaximo()
        {
            if (inicio.getY() < fim.getY())
                return inicio.getX();
            else
                return fim.getX();
        }

        public HashSet<Ponto> getPontos()
        {
            pontos = new HashSet<Ponto>();

            if (tipo == "DDA")
                RetaDda();
            else
                RetaBresenham();

            return pontos;
        }

        void RetaBresenham()
        {
            int x0 = inicio.getX();
            int y0 = inicio.getY();
            int x1 = fim.getX();
            int y1 = fim.getY();

            int x, y, desvioX, desvioY;
            double deltax, deltay, e, m;
            bool trocaXY = false;
            bool trocaX = false;
            bool trocaY = false;

            desvioX = x0;
            desvioY = y0;
            x0 -= desvioX;
            x1 -= desvioX;
            y0 -= desvioY;
            y1 -= desvioY;

            deltax = x1 - x0;
            deltay = y1 - y0;
            m = deltay / deltax;

            //Reflexão(p1, p2)
            if (m > 1 || m < -1)
            {
                int aux = x0;
                x0 = y0;
                y0 = aux;
                aux = x1;
                x1 = y1;
                y1 = aux;
                trocaXY = true;
            }
            if (x0 > x1)
            {
                x0 = -x0;
                x1 = -x1;
                trocaX = true;
            }
            if (y0 > y1)
            {
                y0 = -y0;
                y1 = -y1;
                trocaY = true;
            }

            x = x0;
            y = y0;
            deltax = x1 - x0;
            deltay = y1 - y0;
            m = deltay / deltax;
            e = m - 0.5;
            pontos.Add(new Ponto(x + desvioX, y + desvioY, cor));

            while (x < x1)
            {
                if (e >= 0)
                {
                    y += 1;
                    e -= 1;
                }
                x += 1;
                e += m;

                //Reflexão()-1
                int xTemp = x;
                int yTemp = y;
                if (trocaY)
                {
                    yTemp = -yTemp;
                }
                if (trocaX)
                {
                    xTemp = -xTemp;
                }
                if (trocaXY)
                {
                    int aux = xTemp;
                    xTemp = yTemp;
                    yTemp = aux;
                }
                pontos.Add(new Ponto(xTemp + desvioX, yTemp + desvioY, cor));
            }
        }

        void RetaDda()
        {
            //Pontos Iniciais
            float x0 = inicio.getX();
            float y0 = inicio.getY();
            int x1 = fim.getX();
            int y1 = fim.getY();
            int pasos = 0;

            float x, y;
            double deltax, deltay, e, m;

            deltax = x1 - x0;
            deltay = y1 - y0;

            if (Math.Abs(deltax) > Math.Abs(deltay))
                pasos = (int)Math.Abs(deltax);
            else
                pasos = (int)Math.Abs(deltax);

            x = (float)deltax / pasos;
            y = (float)deltay / pasos;

            pontos.Add(new Ponto((int)x0, (int)y0, cor));

            for (int k = 1; k < pasos + 1; k++)
            {
                x0 = x0 + x;
                y0 = y0 + y;
                pontos.Add(new Ponto((int)x0, (int)y0, cor));
            }


        }

        public override string ToString()
        {
            return ("Reta: (" + inicio.getX() + ", " + inicio.getY() + ") a (" +
                fim.getX() + ", " + fim.getY() + ")");
        }
    }

    class Circulo
    {
        private Ponto centro;
        private int raio;
        private Color cor;
        private HashSet<Ponto> pontos;

        public Circulo(int x, int y, int r, Color cor)
        {
            centro = new Ponto(x, y, cor);
            raio = r;
            this.cor = cor;
            pontos = new HashSet<Ponto>();
            CirculoPontoMedio();
        }

        public Ponto getCentro()
        {
            return centro;
        }

        public void setCentro(Ponto p)
        {
            centro = p;
        }

        public HashSet<Ponto> getPontos()
        {
            pontos = new HashSet<Ponto>();
            CirculoPontoMedio();
            return pontos;
        }

        private void CirculoPontoMedio()
        {
            int x = centro.getX();
            int y = centro.getY();
            int r = raio;
            HashSet<Tuple<int, int>> tmpPontos = new HashSet<Tuple<int, int>>();
            int desvioX, desvioY, p;
            desvioX = x;
            desvioY = y;
            x -= desvioX;
            y -= desvioY;
            //Ponto médio
            x = 0;
            y = r;
            p = 1 - r;
            tmpPontos.Add(Tuple.Create(x, y));
            tmpPontos.Add(Tuple.Create(-x, y));
            tmpPontos.Add(Tuple.Create(x, -y));
            tmpPontos.Add(Tuple.Create(-x, -y));
            tmpPontos.Add(Tuple.Create(y, x));
            tmpPontos.Add(Tuple.Create(-y, x));
            tmpPontos.Add(Tuple.Create(y, -x));
            tmpPontos.Add(Tuple.Create(-y, -x));
            while (x < y)
            {
                x++;
                if (p < 0)
                    p += 2 * x + 3;
                else
                {
                    y--;
                    p += 2 * x - 2 * y + 5;
                }
                tmpPontos.Add(Tuple.Create(x, y));
                tmpPontos.Add(Tuple.Create(-x, y));
                tmpPontos.Add(Tuple.Create(x, -y));
                tmpPontos.Add(Tuple.Create(-x, -y));
                tmpPontos.Add(Tuple.Create(y, x));
                tmpPontos.Add(Tuple.Create(-y, x));
                tmpPontos.Add(Tuple.Create(y, -x));
                tmpPontos.Add(Tuple.Create(-y, -x));
            }

            foreach (Tuple<int, int> t in tmpPontos)
                pontos.Add(new Ponto(t.Item1 + desvioX, t.Item2 + desvioY, cor));
        }

        public override string ToString()
        {
            return ("Circulo: (" + centro.getX() + ", " + centro.getY() + ") com raio " + raio);
        }
    }

    class Elipse
    {
        private Ponto centro;
        private int eixoMenor;
        private int eixoMaior;
        private Color cor;
        private HashSet<Ponto> pontos;

        public Elipse(int x, int y, int rx, int ry, Color cor)
        {
            centro = new Ponto(x, y, cor);
            eixoMenor = ry;
            eixoMaior = rx;
            this.cor = cor;
            pontos = new HashSet<Ponto>();
            ElipsePontoMedio();
        }

        public Ponto getCentro()
        {
            return centro;
        }

        public void setCentro(Ponto p)
        {
            centro = p;
        }

        public HashSet<Ponto> getPontos()
        {
            pontos = new HashSet<Ponto>();
            ElipsePontoMedio();
            return pontos;
        }

        private void ElipsePontoMedio()
        {
            int x = centro.getX();
            int y = centro.getY();
            int a = eixoMaior;
            int a2 = a * a;
            int b = eixoMenor;
            int b2 = b * b;
            HashSet<Tuple<int, int>> tmpPontos = new HashSet<Tuple<int, int>>();
            int desvioX, desvioY;
            desvioX = x;
            desvioY = y;
            x -= desvioX;
            y -= desvioY;

            //Ponto médio
            float d1, d2;
            x = 0;
            y = b;
            d1 = b2 - a2 * b + a2 / 4;
            tmpPontos.Add(Tuple.Create(x, y));
            tmpPontos.Add(Tuple.Create(x, -y));
            tmpPontos.Add(Tuple.Create(-x, y));
            tmpPontos.Add(Tuple.Create(-x, -y));
            // Região 1
            while (a2 * (y - 1 / 2) > b2 * (x + 1))
            {
                if (d1 < 0)
                {
                    d1 += b2 * (2 * x + 3);
                    x++;
                }
                else
                {
                    d1 += b2 * (2 * x + 3) + a2 * (-2 * y + 2);
                    x++;
                    y--;
                }
                tmpPontos.Add(Tuple.Create(x, y));
                tmpPontos.Add(Tuple.Create(x, -y));
                tmpPontos.Add(Tuple.Create(-x, y));
                tmpPontos.Add(Tuple.Create(-x, -y));
            }
            // Região 2
            d2 = b2 * (x + 1 / 2) * (x + 1 / 2) + a2 * (y - 1) * (y - 1) - a2 * b2;
            while (y > 0)
            {
                if (d2 < 0)
                {
                    d2 += b2 * (2 * x + 2) + a2 * (-2 * y + 3);
                    x++;
                    y--;
                }
                else
                {
                    d2 += 2 * (-2 * y + 3);
                    y--;
                }
                tmpPontos.Add(Tuple.Create(x, y));
                tmpPontos.Add(Tuple.Create(x, -y));
                tmpPontos.Add(Tuple.Create(-x, y));
                tmpPontos.Add(Tuple.Create(-x, -y));
            }

            foreach (Tuple<int, int> t in tmpPontos)
                pontos.Add(new Ponto(t.Item1 + desvioX, t.Item2 + desvioY, cor));
        }

        public override string ToString()
        {
            return ("Elipse: (" + centro.getX() + ", " + centro.getY() + ") com eixo maior " +
                eixoMaior + " e eixo menor " + eixoMenor);
        }
    }

    class Poligono
    {
        private List<Ponto> vertices;
        private HashSet<Ponto> pontos;

        public Poligono()
        {
            vertices = new List<Ponto>();
            pontos = new HashSet<Ponto>();
        }

        public HashSet<Ponto> getPontos()
        {
            Reta tmpReta;
            pontos.Clear();
            for (int i = 0; i < vertices.Count; i++)
            {
                tmpReta = new Reta(vertices[i], vertices[(i + 1) % vertices.Count], vertices[i].getCor(), "");
                foreach (Ponto p in tmpReta.getPontos())
                    pontos.Add(p);
            }
            return pontos;
        }

        public void adicionarVertice(Ponto p)
        {
            vertices.Add(p);
        }

        public List<Ponto> getVertices()
        {
            return vertices;
        }

        public void setVertices(List<Ponto> vertices)
        {
            this.vertices = vertices;
        }

        public List<Reta> getRetas()
        {
            List<Reta> retas = new List<Reta>();
            for (int i = 0; i < vertices.Count; i++)
            {
                retas.Add(new Reta(vertices[i], vertices[(i + 1) % vertices.Count], vertices[i].getCor(), ""));
            }
            return retas;
        }

        public override string ToString()
        {
            return ("Polígono: " + vertices.Count + " vértices, iniciando em (" +
                vertices[0].getX() + ", " + vertices[0].getY() + ")");
        }
    }

    class Ponto3D
    {
        private int x;
        private int y;
        private int z;

        public Ponto3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void setCoordenada(int novoX, int novoY, int novoZ)
        {
            x = novoX;
            y = novoY;
            z = novoZ;
        }

        public int[,] getCoordenadaHomogenea()
        {
            int[,] coord = { { x },
                             { y },
                             { z },
                             { 1 } };
            return coord;
        }

        public void setCoordenadaHomogenea(int[,] coord)
        {
            setCoordenada(coord[0, 0],
                          coord[1, 0],
                          coord[2, 0]);
        }

        public override string ToString()
        {
            return ("(" + x + ", " + y + ", " + z + ")");
        }
    }

    class Poligono3D
    {
        private List<Ponto3D> vertices;
        private List<List<int>> faces;
        private int origemX;
        private int origemY;

        public Poligono3D()
        {
            vertices = new List<Ponto3D>();
            faces = new List<List<int>>();
        }

        public void adicionarVertice(Ponto3D p)
        {
            vertices.Add(p);
        }

        public List<Ponto3D> getVertices()
        {
            return vertices;
        }

        public void setVertices(List<Ponto3D> vertices)
        {
            this.vertices = vertices;
        }

        public void adicionarFace(List<int> l)
        {
            faces.Add(l);
        }

        public List<List<int>> getFaces()
        {
            return faces;
        }

        public void setOrigem(int x, int y)
        {
            this.origemX = x;
            this.origemY = y;
        }

        public Ponto getOrigem()
        {
            return new Ponto(origemX, origemY, Color.Black);
        }

        public override string ToString()
        {
            return ("Polígono 3D: Origem em (" + origemX + ", " + origemY + ")");
        }
    }
}
