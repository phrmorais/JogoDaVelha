# C#: API do Jogo da Velha.

API feito em C# .net core 3.1 que simula o Jogo da Velha, suas funcoes sao:

Apos os dois jogadores entrarem na partida, o tabuleiro e iniciado
A jogada deve ser feita através de indexacao de matriz no Array de duas dimensoes
E preciso fornecer um indice para a linha e outro para a coluna
Apos o tabuleiro ser preenchido pela mesma opção na vertical, horizontal ou diagonal, um dos jogadores sera declarado vencedor.
Apos existir o campeao, o jogo e encerrado.
Se nao houver uma combinacao de 3 elementos iguais, o jogo informa que nao houve um empate

Nao foi usada nenhuma biblioteca externa para criacao da API.

# GET - /game
Retorna quantidade de jogos que estão em andamento

# POST - /game
Retorna o id da partida, qual opção inicia e a opção do player atual;

reposta:

    {
        "id": "b7212f48-01f4-4354-8faf-944e2e6a6193",
        "firstPlayer": "O",
        "yourPlayer": "X"
    }

# POST - /game/{id}/movement
Faz as movimentações de acordo com o indexacao de matrix enviada;

Validacoes:
    "Movimento não informado": O corpo não foi enviado para a requisicao
    "Movimento fora do tabuleiro": A indexacao estáva fora dos valores permitidos
    "movimento diferente do Id da URI": o ID da url e o Id que é enviado no corpo estão diferentes
    "Partida não encontrada": O id não retornou uma partida valida
    "Partida aguardando segundo jogador": Somente um jogador acessou a plataforma, execute um post para /game novamente para continuar
    "Partida não finalizada": A partida foi encerrada em outro momento
    "Não é turno do jogador": O outro jogador ainda nao realizou a jogada.
    "Movimento já executado": o movimento tentou sobrescrever um item que já havia sido escolhido.

Corpo:

    {
       "id" : "b7212f48-01f4-4354-8faf-944e2e6a6193",
       "player": "o",
       "position": {
          "x": 2,
          "y": 2
        }
    }


