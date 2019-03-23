#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOITENS_CATEGORIAS {

    [PrimaryKey, Unique, NotNull]
    public int idCategoriaItem { get; set; }
    public int idGame { get; set; }
    public string nomeCategoria { get; set; }
    public string infoCategoria { get; set; }
    public int colecionaveis { get; set; }
    public int ativo { get; set; }
    public string dataUpdate { get; set; }


    public override string ToString() {
        return string.Format(
            "<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>idCategoriaItem - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>idGame - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>nomeCategoria - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>colecionaveis - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>ativo - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>dataUpdate - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
            idCategoriaItem,
            idGame,
            nomeCategoria,
            colecionaveis,
            ativo,
            dataUpdate);
    }
}