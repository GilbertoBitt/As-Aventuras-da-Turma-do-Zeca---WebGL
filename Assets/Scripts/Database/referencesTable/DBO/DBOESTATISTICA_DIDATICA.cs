#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOESTATISTICA_DIDATICA {

    [PrimaryKey]
    public int ID { get; set; }
    public int idUsuario {get; set;}
	public string dataInsert {get; set;}
	public int idGameDidatico {get; set;}
    public int idHabilidade { get; set; }
    public int idDificuldade {get; set;}
	public int idLivro { get; set;}
	public int acertou {get; set;}
    public int online { get; set; }

    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Usuario - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>Data Insert - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>ID Competencia - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>ID Dificuldade - </color><color=#4286f4>{3}</color>\n" +
			"<color=#ffffff>ID Minigame - </color><color=#4286f4>{4}</color>\n" +
			"<color=#ffffff>Acertou - </color><color=#4286f4>{5}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idUsuario, 
			dataInsert,
            idGameDidatico,
			idDificuldade,
            idLivro,
			acertou);
	}

    public DBOESTATISTICA_DIDATICA() {
        idUsuario = 0;
        dataInsert = "";
        idGameDidatico = 0;
        idHabilidade = 0;
        idDificuldade = 0;
        idLivro = 0;
        acertou = 0;
        online = 0;
    }

}
