#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBORANKING {

    [PrimaryKey]
	public int idMinigame {get; set;}
    public int idUsuario {get; set;}
	public int highscore {get; set;}
    public int posicao { get; set; }
    public int estrelas { get; set; }
    public int online { get; set; }
    public string dataInsert {get; set;}
	public string dataUpdate {get; set;}

	public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Minigame - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Usuario - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Highscore - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>Posição - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>Estrelas - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
			idMinigame, 
			idUsuario, 
			highscore,
            posicao,
            estrelas);
	}

}
