using UnityEngine;

[System.Serializable]
public class Minigames {

	public string name;
	public int highscore;
	public int stars;
	public bool[] CharactersAvaible;
	public Sprite backgroundImage;
    public string sceneNameAR;
	[TextArea()]
	public string description;
	public int OldCharacterSelected;
	public string SceneName;
    public Vector3 limit;
    public int idLivro;
}
