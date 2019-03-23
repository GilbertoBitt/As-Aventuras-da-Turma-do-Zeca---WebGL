using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class question {

	public string questionString;
    public AudioClip audioClip;
	public List<Alternatives> alternatives = new List<Alternatives>();
	public string rightAlternative;
	public int rightAlternativeInt;
	public int idHabilidade;
	public int idDificuldade;
    public int idPergunta;
    public int idLivro;
}
