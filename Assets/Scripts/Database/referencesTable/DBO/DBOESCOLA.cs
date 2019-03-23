#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOESCOLA {

	[PrimaryKey, Unique]
	public int idEscola {get; set;}
	public int idCliente {get; set;}
	public string nomeEscola {get; set;}
	public string dataInsert {get; set;}
	public string dataUpdate {get; set;}
	public int ativo {get; set;}

	public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Escola - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Cliente - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Nome Cliente - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idEscola, 
			idCliente, 
			nomeEscola);
	}


}
