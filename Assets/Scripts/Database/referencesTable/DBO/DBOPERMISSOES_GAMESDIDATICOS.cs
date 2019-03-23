#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif
public class DBOPERMISSOES_GAMESDIDATICOS {

    public int idGameDidatico { get; set; }
    public int idLivro { get; set; }
    public int idAnoLetivo { get; set; }
    public int idTurma { get; set; }
    public int acesso { get; set; }

    public override string ToString() {
        return string.Format("" +
            "[<color=#ffffff> ID Game Didatico =</color><color=#4286f4>{0}</color>]" +
            "[<color=#ffffff> ID Game =</color><color=#4286f4>{1}</color>]" +
            "[<color=#ffffff> ID Livro =</color><color=#4286f4>{2}</color>]" +
            "[<color=#ffffff> ID Minigame =</color><color=#4286f4>{3}</color>]" +
            "[<color=#ffffff> ID Habilidade =</color><color=#4286f4>{4}</color>]",
            idGameDidatico,            
            idLivro,
            idAnoLetivo,
            idTurma,
            acesso);
    }

}
