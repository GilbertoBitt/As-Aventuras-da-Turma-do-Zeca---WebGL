#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif
public class DBOCLIENTES {
	[PrimaryKey, Unique]
	public int idCliente {get; set;}
	public string nomeCliente {get; set;}
	public int dataInsert {get; set;}
	public int dataUpdate {get; set;}
	public int ativo {get; set;}

	public override string ToString ()
	{
		return string.Format ("[<color=#ffffff> ID Cliente =</color><color=#4286f4>{0}</color>, <color=#ffffff> Nome Cliente=</color><color=#4286f4>{1}</color>]", idCliente, nomeCliente);
	}
}
