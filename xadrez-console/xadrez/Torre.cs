using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "T";
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
            //Acima
            pos.DefinirValores(Posicao.Linhas - 1, Posicao.Colunas);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
                if (Tab.TabPeca(pos) != null && Tab.TabPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linhas--;
            }
            //Direita
            pos.DefinirValores(Posicao.Linhas, Posicao.Colunas + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
                if (Tab.TabPeca(pos) != null && Tab.TabPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Colunas++;
            }
            //Abaixo
            pos.DefinirValores(Posicao.Linhas + 1, Posicao.Colunas);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
                if (Tab.TabPeca(pos) != null && Tab.TabPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linhas++;
            }
            //Esquerda
            pos.DefinirValores(Posicao.Linhas, Posicao.Colunas - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linhas, pos.Colunas] = true;
                if (Tab.TabPeca(pos) != null && Tab.TabPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Colunas--;
            }
            return mat;
        }        
    }
}