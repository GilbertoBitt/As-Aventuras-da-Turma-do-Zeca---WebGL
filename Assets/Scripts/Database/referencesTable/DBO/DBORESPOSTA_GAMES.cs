#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBORESPOSTA_GAMES {

	[PrimaryKey][Unique]
	public int IdResposta { get; set; }
	public int IdPergunta { get; set; }
	public string TextoResposta { get; set; }
	public int correta { get; set; }
	public int audio { get; set; }
	public int imagem { get; set; }
	public int downloaded { get; set; }

	public override string ToString(){
		return string.Format(
			"IdResposta: {0}, IdPergunta: {1}, TextoResposta: {2}, correta: {3}, audio: {4}, imagem: {5}, downloaded: {6}",
			IdResposta, IdPergunta, TextoResposta, correta, audio, imagem, downloaded);
	}
}
