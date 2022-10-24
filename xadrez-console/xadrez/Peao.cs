using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;
        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
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

                //Jogada especial En Passant
                if (Posicao.Linhas == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linhas, Posicao.Colunas - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.TabPeca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linhas - 1, esquerda.Colunas] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linhas, Posicao.Colunas + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.TabPeca(direita) == Partida.VulneravelEnPassant)
                    {
                        mat[direita.Linhas - 1, direita.Colunas] = true;
                    }
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

                    //Jogada especial En Passant
                    if (Posicao.Linhas == 4)
                    {
                        Posicao esquerda = new Posicao(Posicao.Linhas, Posicao.Colunas - 1);
                        if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.TabPeca(esquerda) == Partida.VulneravelEnPassant)
                        {
                            mat[esquerda.Linhas + 1, esquerda.Colunas] = true;
                        }
                        Posicao direita = new Posicao(Posicao.Linhas, Posicao.Colunas + 1);
                        if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.TabPeca(direita) == Partida.VulneravelEnPassant)
                        {
                            mat[direita.Linhas + 1, direita.Colunas] = true;
                        }
                    }
                }
            }
            return mat;
        }
    }
}
