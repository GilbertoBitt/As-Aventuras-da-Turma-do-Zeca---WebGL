#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOPERGUNTAS {

	[PrimaryKey, Unique]
	public int idPergunta {get; set;}
	public int idHabilidade {get; set;}
	public int idLivro {get; set;}
    public int idCliente { get; set; }
    public int idDificuldade {get; set;}
	public string textoPergunta {get; set;}
	public string dataInsert {get; set;}
	public string dataUpdate {get; set;}
	public int ativo {get; set;}
    public int audio { get; set; }
    public int downloaded { get; set; }

    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Pergunta - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Habilidade - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>ID Livro - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>ID Dificuldade - </color><color=#4286f4>{3}</color>\n" +
			"<color=#ffffff>Texto Pergunta - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>downloaded - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
			idPergunta, 
			idHabilidade, 
			idLivro,
			idDificuldade,
			textoPergunta,
            downloaded);
	}

}
