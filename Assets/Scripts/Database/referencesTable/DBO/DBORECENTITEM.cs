#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBORECENTITEM {

    public int userId { get; set; }
    public int itemId { get; set; }

    public override string ToString() {
        return string.Format(
            "<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>idItem - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>idCategoriaItem - </color><color=#4286f4>{1}</color>\n" +            
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
            userId,
            itemId);
    }
}