#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOANOLETIVO {

	[PrimaryKey, Unique]
	public int idAnoLetivo {get; set;}
	public string descAnoLetivo {get; set;}
	public string dataInsert {get; set;}
	public string dataUpdate {get; set;}
	public int ativo {get; set;}

	public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Ano Letivo - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>Descrição Ano Letivo - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idAnoLetivo,
			descAnoLetivo);
	}
}
