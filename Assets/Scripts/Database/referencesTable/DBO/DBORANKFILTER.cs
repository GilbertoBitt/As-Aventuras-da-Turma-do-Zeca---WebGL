#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBORANKFILTER {

    [PrimaryKey, Unique]
    public int idMinigame { get; set; }
    public int idUsuario { get; set; }
    public int highscore { get; set; }
    public int idEscola { get; set; }
    public string nomeEscola { get; set; }
    public int idTurma { get; set; }
    public string descTurma { get; set; }
    public int idCliente { get; set; }
    public string login { get; set; }


    public override string ToString() {
        return string.Format(
            "<color=#ffffff> -------------------------------------------------------------</color>\n" +
            "<color=#ffffff>ID Minigame - </color><color=#4286f4>{0}</color>\n" +
            "<color=#ffffff>ID Usuario - </color><color=#4286f4>{1}</color>\n" +
            "<color=#ffffff>Highscore - </color><color=#4286f4>{2}</color>\n" +
            "<color=#ffffff>ID Escola - </color><color=#4286f4>{3}</color>\n" +
            "<color=#ffffff>Nome Escola - </color><color=#4286f4>{4}</color>\n" +
            "<color=#ffffff>ID Turma - </color><color=#4286f4>{5}</color>\n" +
            "<color=#ffffff>Descrição Turma - </color><color=#4286f4>{6}</color>\n" +
            "<color=#ffffff>ID Cliente - </color><color=#4286f4>{7}</color>\n" +
            "<color=#ffffff>Login - </color><color=#4286f4>{8}</color>\n" +
            "<color=#ffffff> -------------------------------------------------------------</color>\n",
            idMinigame,
            idUsuario,
            highscore,
            idEscola,
            nomeEscola,
            idTurma,
            descTurma,
            idCliente,
            login);
    }
}