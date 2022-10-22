namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }

        public Peca TabPeca(int linhas, int colunas)
        {
            return Pecas[linhas, colunas];
        }
        public Peca TabPeca(Posicao pos)
        {
            return Pecas[pos.Linhas, pos.Colunas];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return TabPeca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe peça nessa posição!");
            }
            Pecas[pos.Linhas, pos.Colunas] = p;
            p.Posicao = pos;
        }
        public Peca RetirarPeca(Posicao pos)
        {
            if (TabPeca(pos) == null)
            {
                return null;
            }
            else
            {
                Peca aux = TabPeca(pos);
                aux.Posicao = null;
                Pecas[pos.Linhas, pos.Colunas] = null;
                return aux;
            }
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linhas < 0 || pos.Linhas >= Linhas || pos.Colunas < 0 || pos.Colunas >= Colunas)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
