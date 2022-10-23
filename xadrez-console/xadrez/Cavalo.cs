using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "C";
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.TabPeca(pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);


            pos.DefinirValores(Posicao.Linhas - 2, Posicao.Colunas - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            pos.DefinirValores(Posicao.Linhas - 2, Posicao.Colunas + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas - 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas + 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            pos.DefinirValores(Posicao.Linhas + 2, Posicao.Colunas + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            pos.DefinirValores(Posicao.Linhas + 2, Posicao.Colunas - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }

            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas + 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas - 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
            }
            return mat;
        }
    }
}