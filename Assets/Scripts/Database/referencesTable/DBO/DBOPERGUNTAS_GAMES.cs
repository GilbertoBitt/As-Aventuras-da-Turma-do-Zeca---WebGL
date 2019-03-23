#if SQLITENET
using SQLite;
#else
using SQLite4Unity3d;
#endif

public class DBOPERGUNTAS_GAMES{

	[PrimaryKey, Unique]
	public int idPergunta{ get; set; }
	public int idHabilidade{ get; set; }
	public int idCliente{ get; set; }
	public int idDificuldade{ get; set; }
	public int idAnoLetivo{ get; set; }
	public string textoPergunta{ get; set; }
	public int layout{ get; set; }
	public int imagem{ get; set; }
	public int audio{ get; set; }
	public int ativo{ get; set; }
	public int downloaded{ get; set; }
	public override string ToString(){
		return string.Format("idPergunta: {0}, idHabilidade: {1}, idCliente: {2}, idDificuldade: {3}, textoPergunta: {4}, layout: {5}, imagem: {6}, audio: {7}, ativo: {8}, downloaded: {9}", idPergunta, idHabilidade, idCliente, idDificuldade, textoPergunta, layout, imagem, audio, ativo, downloaded);
	}

	public DBOPERGUNTAS_GAMES(){
	}

	public DBOPERGUNTAS_GAMES(int idPergunta, int idHabilidade, int idCliente, int idDificuldade, int idAnoLetivo, string textoPergunta, int layout, int imagem, int audio, int ativo, int downloaded){
		this.idPergunta = idPergunta;
		this.idHabilidade = idHabilidade;
		this.idCliente = idCliente;
		this.idDificuldade = idDificuldade;
		this.idAnoLetivo = idAnoLetivo;
		this.textoPergunta = textoPergunta;
		this.layout = layout;
		this.imagem = imagem;
		this.audio = audio;
		this.ativo = ativo;
		this.downloaded = downloaded;
	}
}

	