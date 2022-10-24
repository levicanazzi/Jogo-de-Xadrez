using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //Jogada especial Roque Pequeno
            if (p is Rei && destino.Colunas == origem.Colunas + 2)
            {
                Posicao origemT = new Posicao(origem.Linhas, origem.Colunas + 3);
                Posicao destinoT = new Posicao(origem.Linhas, origem.Colunas + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            //Jogada especial Roque grande
            if (p is Rei && destino.Colunas == origem.Colunas - 2)
            {
                Posicao origemT = new Posicao(origem.Linhas, origem.Colunas - 4);
                Posicao destinoT = new Posicao(origem.Linhas, origem.Colunas - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);

            //Jogada especial Roque Pequeno
            if (p is Rei && destino.Colunas == origem.Colunas + 2)
            {
                Posicao origemT = new Posicao(origem.Linhas, origem.Colunas + 3);
                Posicao destinoT = new Posicao(origem.Linhas, origem.Colunas + 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            //Jogada especial Roque Grande
            if (p is Rei && destino.Colunas == origem.Colunas - 2)
            {
                Posicao origemT = new Posicao(origem.Linhas, origem.Colunas - 4);
                Posicao destinoT = new Posicao(origem.Linhas, origem.Colunas - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Tab.ColocarPeca(T, origemT);
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.TabPeca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.TabPeca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.TabPeca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.TabPeca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca r = rei(cor);
            if (r == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linhas, r.Posicao.Colunas])
                {
                    return true;
                }
            }
            return false;
        }
        public bool XequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(x.Posicao, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(x.Posicao, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        public void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab));
        }
    }
}
