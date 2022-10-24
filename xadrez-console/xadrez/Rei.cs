using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;
        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.TabPeca(pos);
            return p == null || p.Cor != Cor;
        }
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.TabPeca(pos);
            return p != null && p is Torre && QteMovimentos == 0 && p.Cor == Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Nordeste
            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Direita
            pos.DefinirValores(Posicao.Linhas, Posicao.Colunas + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Sudeste
            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Abaixo
            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Sudoeste
            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Esquerda
            pos.DefinirValores(Posicao.Linhas, Posicao.Colunas - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            //Nordeste
            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            // #Jogada especial Roque
            if (QteMovimentos == 0 && !Partida.Xeque)
            {
                // #Jogada especial Roque Pequeno
                Posicao posT1 = new Posicao(Posicao.Linhas, Posicao.Colunas + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linhas, Posicao.Colunas + 1);
                    Posicao p2 = new Posicao(Posicao.Linhas, Posicao.Colunas + 2);
                    if (Tab.TabPeca(p1) == null && Tab.TabPeca(p2) == null)
                    {
                        mat[Posicao.Linhas, Posicao.Colunas + 2] = true;
                    }
                }
                // #Jogada especial Roque Grande
                Posicao posT2 = new Posicao(Posicao.Linhas, Posicao.Colunas - 4);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linhas, Posicao.Colunas - 1);
                    Posicao p2 = new Posicao(Posicao.Linhas, Posicao.Colunas - 2);
                    Posicao p3 = new Posicao(Posicao.Linhas, Posicao.Colunas - 2);
                    if (Tab.TabPeca(p1) == null && Tab.TabPeca(p2) == null && Tab.TabPeca(p3) == null)
                    {
                        mat[Posicao.Linhas, Posicao.Colunas - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
