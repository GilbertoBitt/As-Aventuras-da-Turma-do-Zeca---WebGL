using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class LoginManager : MonoBehaviour {

	[HeaderAttribute("References")]
	public GameConfig config;
	public StatusTexts messagesText;
	public GameDetailWindow gameDetails;
	public Button loginButton;
	public Button closeButton;
	public InputField usernameInput;
	public InputField passwordInput;
	public Text MessageStatus;
	public Text ErroMessage;
	public List<GameObject> DefaultPanel = new List<GameObject>();
	EventSystem system;
	List<Selectable> selectableItems = new List<Selectable>();
	int currentSelectable;
	public acessState currentState = acessState.Default;
	public Color defaultColorInput;
	public Color errorColorInput;

	// Use this for initialization
	void Start () {
		//Debug.Log(Application.persistentDataPath);
		system = EventSystem.current;
		selectableItems.Add(loginButton.GetComponent<Selectable>());
		selectableItems.Add(closeButton.GetComponent<Selectable>());
		selectableItems.Add(usernameInput.GetComponent<Selectable>());
		selectableItems.Add(passwordInput.GetComponent<Selectable>());
		UpdateStatus(acessState.Default);

	}
	
	// Update is called once per frame
	

	public void startLogin(){
		if (config != null){
			gameDetails.PlayMinigame();
		} else {
				//Debug.Log("Error: Digite sua senha.");
		currentState = acessState.NetworkCheck;
		if(usernameInput.text != "" && usernameInput.text != null){
			if(passwordInput.text != "" && passwordInput.text != null){
				StartCoroutine(loginBegin());
			} else {
				//Debug.Log("Error: Digite sua senha.");
				UpdateStatus(acessState.ErrorPassNotTyped);
			}
		} else {
			//Debug.Log("Error: Digite um nome de usuário/login.");
			UpdateStatus(acessState.ErroUserNotTyped);
		}
	}
	}

	IEnumerator loginBegin(){
		networkHelper helper = (new GameObject("NetworkHelper")).AddComponent<networkHelper>();
        UpdateStatus(acessState.NetworkCheck);
		helper.NetworkVeryfier(config);
		yield return new WaitWhile(()=> config.isVerifingNetwork);
		/*if(config.isOnline == false){
			//startOfflineAcess();
		} else	{
			//startOnlineAcess();
		}*/
	}

	/*ublic void startOfflineAcess(){
		UpdateStatus(acessState.ValidatingUserName);
		//DataService dbConnection = new DataService("localDatabase.db3");
		DataService dbConnection = config.openDB();
		DBJogador player = dbConnection.GetJogadorWithLogin(usernameInput.text);		
		if(player != null && player.login == usernameInput.text && player.senha == passwordInput.text){
			//Debug.Log(player.ToString());	
			config.jogador = player;
			config.BropsAmount = player.brops;
			config.TotalPoints = player.pontuacaoTotal;
			config.ReturnAll(player.IdJogador);
			gameDetails.PlayMinigame();
		} else {
			//Debug.Log("usuário ou senha inválida!.");
			UpdateStatus(acessState.ErroPasswordOrUser);
		}

	}*/

	/*public IEnumerator startOnlineAcess(){
		UpdateStatus(acessState.DownloadingProfile);
	}*/

	void TabNavigation(){

		if(Input.GetKeyDown(KeyCode.Tab)){			
			if(currentSelectable >= selectableItems.Count-1){
				currentSelectable = 0;
				system.SetSelectedGameObject(selectableItems[currentSelectable].gameObject);
			} else {
				currentSelectable++;
				system.SetSelectedGameObject(selectableItems[currentSelectable].gameObject);
			}
		}
	}

	public enum acessState
	{
		Default,
		NetworkCheck,
		ValidatingUserName,
		ErroPasswordOrUser,
		DownloadingProfile,
		ErrorDownloadingProfile,
		ErroUserNotTyped,
		ErrorPassNotTyped,
	}

	public void UpdateStatus(acessState stateCurrent){
		currentState = stateCurrent;
		switch (currentState){
			case acessState.Default:
				loginDefault(true);
				hideMessageStatus();
				break;
			case acessState.NetworkCheck:
				loginDefault(false);
				showMessageStatus(8);
				break;
			case acessState.ValidatingUserName:
				loginDefault(false);
				showMessageStatus(1);
				break;
			case acessState.DownloadingProfile:
				loginDefault(false);
				showMessageStatus(2);
				break;
			case acessState.ErroPasswordOrUser:
				loginDefault(true);
				hideMessageStatus();
				errorMessage(3);
				break;
			case acessState.ErroUserNotTyped:
				loginDefault(true);
				hideMessageStatus();
				usernameInput.gameObject.GetComponent<Outline>().effectColor = errorColorInput;
				errorMessage(4);
				break;
			case acessState.ErrorPassNotTyped:
				loginDefault(true);
				hideMessageStatus();
				passwordInput.gameObject.GetComponent<Outline>().effectColor = errorColorInput;
				errorMessage(5);
				break;
			default:
				break;
		}
	}

	public void loginDefault(bool isHide){
		for (int i = 0; i < DefaultPanel.Count; i++)
		{
			DefaultPanel[i].SetActive(isHide);
		}
	}

	public void showMessageStatus(int message){
		MessageStatus.text = messagesText.statusMessages[message];
		MessageStatus.gameObject.SetActive(true);
	}

	public void hideMessageStatus(){
		MessageStatus.gameObject.SetActive(false);
	}

	public void errorMessage(int message){
		ErroMessage.text = messagesText.statusMessages[message];
		ErroMessage.gameObject.SetActive(true);
	}

	public void errorMessage(){
		ErroMessage.gameObject.SetActive(false);
	}

	public void resetUsernameInput(){
		usernameInput.gameObject.GetComponent<Outline>().effectColor = defaultColorInput;
		errorMessage();
	}

	public void resetPasswordInput(){
		passwordInput.gameObject.GetComponent<Outline>().effectColor = defaultColorInput;
		errorMessage();
	}

	/// <summary>
	/// Callback sent to all game objects before the application is quit.
	/// </summary>
	void OnApplicationQuit()
	{
		//TODO 
		//config.jogador.IdJogador = -1;
	}

}
