using System.Drawing;

namespace CGPaint
{
    class FrameBuffer
    {
        private int _altura;
        private int _largura;
        private Color[,] _framebuffer;

        public FrameBuffer(int x, int y)
        {
            _largura = x;
            _altura = y;
            _framebuffer = new Color[x, y];
        }

        /*
         * Função usada para definir um ponto.
         * O ponto de origem (0, 0) está no canto superior esquerdo da tela.
         */
        public void setPonto(Ponto p)
        {
            int x = p.getX();
            int y = p.getY();
            if ((x < _largura && y < _altura) && (x >= 0 && y >= 0))
                _framebuffer[x, y] = p.getCor();
        }

        //Função para obter um ponto.
        public Color getPonto(int x, int y)
        {
            return _framebuffer[x, y];
        }

        public void limparPontos()
        {
            _framebuffer = new Color[_largura, _altura];
        }
    }
}
