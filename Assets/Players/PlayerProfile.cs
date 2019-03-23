using UnityEngine;

/// <summary>
/// Classe responsavel por guardar informações locais do jogador salvamento online ou offline.
/// </summary>
public class PlayerProfile : ScriptableObject {

	public int id;
	public string username;
	public string password;
	public string firstName;
	public string lastName;
	public string deviceInfo;
	public string deviceModel;
	public string deviceName;
	public string deviceUniqueIdentifier;
	public int brops;
	public int totalScore;
}
