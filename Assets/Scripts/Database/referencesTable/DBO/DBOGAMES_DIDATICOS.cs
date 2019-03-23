#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOGAMES_DIDATICOS {

    [PrimaryKey, Unique]
    public int idGameDidatico { get; set; }
    public int idLivro { get; set; }
    public int idHabilidade { get; set; }
    public string nomeGameDidatico { get; set; }
    public string descGameDidatico { get; set; }
    public string disciplina { get; set; }
    public int ativo { get; set; }

    public override string ToString() {
        return string.Format("" +
            "[<color=#ffffff> ID Game Didatico =</color><color=#4286f4>{0}</color>]" +
            "[<color=#ffffff> ID Livro =</color><color=#4286f4>{1}</color>]" +
            "[<color=#ffffff> ID Habilidade =</color><color=#4286f4>{2}</color>]" +
            "[<color=#ffffff> Nome Game Didatico =</color><color=#4286f4>{3}</color>]" +
            "[<color=#ffffff> Descrição Game Didatico =</color><color=#4286f4>{4}</color>]" +
            "[<color=#ffffff> Disciplina =</color><color=#4286f4>{5}</color>]" +
            "[< color =#ffffff> Ativo =</color><color=#4286f4>{6}</color>]",
            idGameDidatico,
            idLivro,
            idHabilidade,
            nomeGameDidatico,
            descGameDidatico,
            disciplina,
            ativo);
    }

}
