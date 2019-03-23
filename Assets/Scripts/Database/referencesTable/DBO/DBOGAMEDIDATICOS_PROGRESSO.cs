#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOGAMEDIDATICOS_PROGRESSO {
    [PrimaryKey, Unique]
    public int idGameDidatico { get; set; }
    public int idUsuario { get; set; }
    public int highscore { get; set; }
    public int estrelas { get; set; }
    public int online { get; set; }

    public override string ToString() {
        return string.Format("[<color=#ffffff> ID Game Didatico =</color><color=#4286f4>{0}</color>]" +
            "[<color=#ffffff> ID Usuario =</color><color=#4286f4>{1}</color>]" +
            "[<color=#ffffff> Highscore =</color><color=#4286f4>{2}</color>]" +
            "[< color =#ffffff> Estrelas =</color><color=#4286f4>{3}</color>]" +
            "[<color=#ffffff> Online =</color><color=#4286f4>{4}</color>]",
            idGameDidatico,
            idUsuario,
            highscore,
            estrelas,
            online);
    }
}
