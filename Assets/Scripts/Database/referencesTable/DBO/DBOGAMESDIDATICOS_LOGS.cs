#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOGAMESDIDATICOS_LOGS {
    [PrimaryKey, Unique, AutoIncrement]
    public int id { get; set; }
    public int idUsuario { get; set; }
    public int idGameDidatico { get; set; }
    public int pontos { get; set; }
    public string personagem { get; set; }
    public int tempo { get; set; }
    public int fase { get; set; }
    public string deviceID { get; set; }
    public string dataAcesso { get; set; }
    public int online { get; set; }

    public override string ToString() {
        return string.Format("" +
            "[<color=#ffffff> ID Usuário =</color><color=#4286f4>{0}</color>]" +
            "[<color=#ffffff> ID Game Didatico =</color><color=#4286f4>{1}</color>]" +
            "[<color=#ffffff> Pontos =</color><color=#4286f4>{2}</color>]" +
            "[<color=#ffffff> Personagem =</color><color=#4286f4>{3}</color>]" +
            "[<color=#ffffff> Tempo =</color><color=#4286f4>{4}</color>]" +
            "[<color=#ffffff> Fase =</color><color=#4286f4>{5}</color>]" +
            "[<color=#ffffff> Device ID =</color><color=#4286f4>{6}</color>]" +
            "[<color=#ffffff> Data Acesso =</color><color=#4286f4>{7}</color>]" +
            "[<color=#ffffff> Online =</color><color=#4286f4>{8}</color>]",
            idUsuario,
            idGameDidatico,
            pontos,
            personagem,
            tempo,
            fase,
            deviceID,
            dataAcesso,
            online);
    }

}
