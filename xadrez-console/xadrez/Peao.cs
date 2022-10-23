using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "P";
        }
        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.TabPeca(pos);
            return p != null && p.Cor != Cor;
        }
        private bool Livre(Posicao pos)
        {
            return Tab.TabPeca(pos) == null;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);
            if (Cor == Cor.Branca)
            {
                //Acima
                pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linhas, pos.Colunas] = true;
                }
                pos.DefinirValores(Posicao.Linhas - 2, Posicao.Colunas);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linhas, pos.Colunas] = true;
                }
                pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linhas, pos.Colunas] = true;
                }
                pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linhas, pos.Colunas] = true;
                }
            }
            else
            {
                if (Cor == Cor.Preta)
                {
                    //Acima
                    pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas);
                    if (Tab.PosicaoValida(pos) && Livre(pos))
                    {
                        mat[pos.Linhas, pos.Colunas] = true;
                    }
                    pos.DefinirValores(Posicao.Linhas + 2, Posicao.Colunas);
                    if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                    {
                        mat[pos.Linhas, pos.Colunas] = true;
                    }
                    pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas + 1);
                    if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linhas, pos.Colunas] = true;
                    }
                    pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas - 1);
                    if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linhas, pos.Colunas] = true;
                    }
                }
            }
            return mat;
        }
    }
}
