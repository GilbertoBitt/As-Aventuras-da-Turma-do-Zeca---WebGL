#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif
[System.Serializable]
public class DBOINVENTARIO {

    [PrimaryKey, Unique, NotNull]
    public int idUsuario { get; set; }
    [PrimaryKey, Unique, NotNull]
    public int idItem { get; set; }
    public int deviceQuantity { get; set; }
    public int quantidade { get; set; }
    public string dataUpdate { get; set; }
    public string dataInsert { get; set; }
    public int ativo { get; set; }
    public int online { get; set; }

    public override string ToString() {
        return string.Format(
            "<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>idUsuario - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>idItem - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>quantidade - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>dataUpdate - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>dataInsert - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>ativo - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
            idUsuario,
            idItem,
            quantidade,
            dataUpdate,
            dataInsert,
            ativo);
    }

}