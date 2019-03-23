#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOSINCRONIZACAO {

	[PrimaryKey, Unique]
	public int idCliente {get; set;}
    public string escola { get; set; }
    public string turma {get; set;}
	public string usuarios {get; set;}
    public string minigames {get; set;}
	public string perguntas {get; set;}
	public string respostas {get; set;}
    public string itens { get; set; }
    public string itens_categorias { get; set; }
    public int idDelPergunta { get; set; }
    public int idDelResposta { get; set; }
    public string ranking { get; set; }
    public string gamesdidaticos { get; set; }
	public string perguntasGames { get; set; }
	public string respostasGames { get; set; }



    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Cliente - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>Turma - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>Usuario - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>Perguntas - </color><color=#4286f4>{3}</color>\n" +
			"<color=#ffffff>Respostas - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>Ranking - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff>Escola - </color><color=#4286f4>{6}</color>\n" +
            "<color=#ffffff>ID Del Pergunta - </color><color=#4286f4>{7}</color>\n" +
            "<color=#ffffff>ID Del Resposta - </color><color=#4286f4>{8}</color>\n" +
            "<color=#ffffff>Minigames - </color><color=#4286f4>{8}</color>\n" +
            "<color=#ffffff>Games Didaticos - </color><color=#4286f4>{9}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
			idCliente,  
			turma,
            usuarios,
			perguntas,
			respostas,
            ranking,
            escola,
            idDelPergunta,
            idDelResposta,
            minigames,
            gamesdidaticos);
	}



    public DBOSINCRONIZACAO() {
        idCliente = 0;
        turma = "2017-01-01 01:01:01.001";
        usuarios = "2017-01-01 01:01:01.001";
        perguntas = "2017-01-01 01:01:01.001";
        respostas = "2017-01-01 01:01:01.001";
        ranking = "2017-01-01 01:01:01.001";
        escola = "2017-01-01 01:01:01.001";
        minigames = "2017-01-01 01:01:01.001";
        gamesdidaticos = "2017-01-01 01:01:01.001";
        idDelPergunta = 0;
        idDelResposta = 0;
    }

}
