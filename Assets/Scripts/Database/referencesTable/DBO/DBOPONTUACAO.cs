#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOPONTUACAO {

    [PrimaryKey, Unique]
    public int idUsuario {get; set;}
	public int brops {get; set;}
	public int pontuacaoTotal {get; set;}
    public int BropsDevice { get; set; }
    public int PontuacaoTotalDevice { get; set; }
    public string dataUpdate {get; set;}
    public int online { get; set; }

    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Usuario - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>Brops - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Pontuação Total - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>Brops Device - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>Pontuação Total Device - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>Data Update - </color><color=#4286f4>{5}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idUsuario, 
			brops,
			pontuacaoTotal,
            BropsDevice,
            PontuacaoTotalDevice,
            dataUpdate);
	}

    public DBOPONTUACAO() {
        idUsuario = 0;
        brops = 0;
        pontuacaoTotal = 0;
    }

}
