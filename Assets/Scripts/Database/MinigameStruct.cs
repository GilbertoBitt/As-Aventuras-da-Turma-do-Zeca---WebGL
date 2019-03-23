#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class MinigameStruct {

    public int idMinigame;
    public int idLivro;
    public string nomeMinigame;
    public string infoMinigame;
    public string dataUpdate;
    public int ativo;
}
