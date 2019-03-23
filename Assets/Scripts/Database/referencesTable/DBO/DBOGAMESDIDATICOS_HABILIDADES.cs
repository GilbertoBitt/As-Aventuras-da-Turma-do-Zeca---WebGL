#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOGAMESDIDATICOS_HABILIDADES {

	[PrimaryKey, Unique]
	public int IdGameDidatico{ get; set; }
	public int IdHabilidade{ get; set; }

	public override string ToString(){
		return string.Format("IdGameDidatico: {0}, idHabilidade: {1}", IdGameDidatico, IdHabilidade);
	}
}

