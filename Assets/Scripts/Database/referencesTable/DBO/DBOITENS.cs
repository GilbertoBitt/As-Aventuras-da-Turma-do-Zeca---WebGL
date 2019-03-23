#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOITENS {

	[PrimaryKey, Unique, NotNull]
	public int idItem {get; set;}
    public int idCliente { get; set; }
    public int idCategoriaItem {get; set;}
	public string nomeItem {get; set;}
	public string infoItem {get; set;}
    public int valor { get; set; }	
	public int ativo {get; set;}
    public string dataUpdate { get; set; }
    public int downloaded { get; set; }

    public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>idItem - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>idCategoriaItem - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>nomeItem - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>infoItem - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>valor - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>ativo - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff>dataUpdate - </color><color=#4286f4>{6}</color>\n" +
            "< color =#ffffff>downloaded - </color><color=#4286f4>{7}</color>\n" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
            idItem,
            idCategoriaItem,
            nomeItem,
            infoItem,
            valor,
            ativo,
            dataUpdate,
            downloaded);
	}
}