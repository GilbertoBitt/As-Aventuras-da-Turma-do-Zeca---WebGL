#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOGAMES_LOGS {
    [PrimaryKey]
    public int ID { get; set; }
    public int idUsuario { get; set; }
    public int idGame { get; set; }
    public string dataAcesso { get; set; }
    public string versao { get; set; }
    public string deviceID { get; set; }
    public int online { get; set; }

    public override string ToString() {
        return string.Format(
            "<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>ID Usuario - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>ID Game - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>dataAcesso - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>online - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
            idUsuario,
            idGame,
            dataAcesso,
            online);
    }


}
