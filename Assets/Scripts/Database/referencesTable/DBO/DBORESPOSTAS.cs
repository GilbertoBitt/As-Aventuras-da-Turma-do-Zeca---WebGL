#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBORESPOSTAS {

	[PrimaryKey, Unique]
	public int idResposta {get; set;}
	public int idPergunta {get; set;}
	public string textoResposta {get; set;}
	public int correta {get; set;}
	public string dataInsert {get; set;}
	public string dataUpdate {get; set;}
	public int ativo {get; set;}
    public int audio { get; set; }
    public int downloaded { get; set; }

    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Resposta - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Pergunta - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Texto Resposta - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>Correta - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>Downloaded - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
			idResposta, 
			idPergunta, 
			textoResposta,
			correta,
            downloaded);
	}

}
