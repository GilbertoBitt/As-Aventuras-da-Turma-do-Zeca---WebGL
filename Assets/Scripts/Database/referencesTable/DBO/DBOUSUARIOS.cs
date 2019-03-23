#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

[System.Serializable]
public class DBOUSUARIOS {

	[PrimaryKey, Unique]
	public int idUsuario {get; set;}
	public int idCliente {get; set;}
	public int tipoUsuario {get; set;}
	public int idTurma {get; set;}
    public string nomeJogador {get; set;}
	public string login {get; set;}
	public string senha {get; set;}
	public string token {get; set;}
	public string dataUpdate {get; set;}
	public int ativo {get; set;}


	public override string ToString ()
	{
		return string.Format (
			"<color=#ffffff> -------------------------------------------------------------</color>\n" +
			"<color=#ffffff>ID Usuario - </color><color=#4286f4>{0}</color>\n" +
			"<color=#ffffff>ID Cliente - </color><color=#4286f4>{1}</color>\n" +
			"<color=#ffffff>Tipo de Usuário - </color><color=#4286f4>{2}</color>\n" +
			"<color=#ffffff>ID Turma - </color><color=#4286f4>{3}</color>\n" +
			"<color=#ffffff>Nome Jogador - </color><color=#4286f4>{4}</color>\n" +
			"<color=#ffffff>Login - </color><color=#4286f4>{5}</color>\n" +
			"<color=#ffffff>Senha - </color><color=#4286f4>{6}</color>\n" +
			"<color=#ffffff>TOKEN - </color><color=#4286f4>{7}</color>" +
			"<color=#ffffff> -------------------------------------------------------------</color>\n",
			idUsuario, 
			idCliente,
			tipoUsuario,
			idTurma,
			nomeJogador,
			login,
			senha,
			token);
	}
}
