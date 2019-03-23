#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOMINIGAMES_LOGS {
    [PrimaryKey]
    public int ID { get; set; }
    public int idUsuario {get; set;}
	public int idMinigame {get; set;}
	public int pontosLudica {get; set;}
	public int pontosPedagogica {get; set;}
	public int pontosInteragindo {get; set;}
	public string personagem {get; set;}
	public string dataAcesso {get; set;}
	public int tempoLudica {get; set;}
	public int tempoDidatica {get; set;}
	public int faseLudica {get; set;}
	public string deviceID {get; set;}
    public int online { get; set; }


    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Usuario - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Minigame - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Pontos Ludica - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>Pontos Pedagogica - </color><color=#4286f4>{3}</color>\n" +
			"<color=#ffffff>Pontos Interagindo - </color><color=#4286f4>{4}</color>\n" +
			"<color=#ffffff>Personagem - </color><color=#4286f4>{5}</color>\n" + 
			"<color=#ffffff>Data Acesso - </color><color=#4286f4>{6}</color>\n" + 
			"<color=#ffffff>Tempo Ludica - </color><color=#4286f4>{7}</color>\n" +
			"<color=#ffffff>tempo Didatica - </color><color=#4286f4>{8}</color>\n" +
			"<color=#ffffff>Fase Ludica - </color><color=#4286f4>{9}</color>\n" +
			"<color=#ffffff>Device ID - </color><color=#4286f4>{10}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idUsuario, 
			idMinigame, 
			pontosLudica,
			pontosPedagogica,
			pontosInteragindo,
			personagem,
			dataAcesso,
			tempoLudica,
			tempoDidatica,
			faseLudica,
			deviceID);
	}


}
