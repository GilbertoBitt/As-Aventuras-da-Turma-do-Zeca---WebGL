#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOMINIGAMES {

    [PrimaryKey]
    public int idMinigames { get; set; }
    public int idLivro { get; set; }
    public string nomeMinigame { get; set; }
    public string infoMinigame { get; set; }
    public int ativo { get; set; }
}
