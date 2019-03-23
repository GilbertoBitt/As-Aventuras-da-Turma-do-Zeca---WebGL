using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Net;
using System.IO;
using System.Collections.Generic;
using MEC;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Events;
using TMPro;
using Sirenix.OdinInspector;

public class gameSelection : OverridableMonoBehaviour {


	public int bropsAmount = 0;

	public Transform listViewPort;
	public LoadManager loadManager;

	public int selectedMinigame = 0;
	public menuState stateMenu;
	private menuState oldStateMenu;
	public Canvas[] Panels;

	public GameConfig gameConfigs;
	public Toggle[] Toggles;

	public Text bropsText;

	public StatusTexts texts;
	public Text ProgressText;
	public Text profileNameText;
	public Image AvatarImage;
	public GameObject syncButton;
    public TextMeshProUGUI usernameText;
	public UnityEvent onAdministratorLoggin;
    public TextMeshProUGUI[] textsUpdate;

	[Header("CharSelections")]
	public int selectedChars = -1;
	public ScrollSnap charScroll;
	public Text CharSelectedText;
	public GameDetailWindow gameDetailWindow;

    [Header("Store")]
    public StoreManager storeManager;
    public Canvas storePanel;
    public GameObject btsLogo;
    public StoreData storeData;

    public Text texBrops;
    public Text texPoints;
    public Text texNameAluno;
    public Text textSchool;
    public Text textClass;
    public Text textYear;
    public GameObject[] textNomeGame;
	public Text textComponentBuildVersion;
    public Text textComponentCliente;

    [BoxGroup]
    public LevelManager levelManager;

    public CatHandler[] catHandlers = new CatHandler[0];

    protected override void Awake() {
        base.Awake();
        Timing.RunCoroutine(LoadStoreData());
    }

    public void TrocaUsu() {
        gameConfigs.UpdateAll(gameConfigs.playerID);
        texBrops.text = "" + gameConfigs.BropsAmount;
        texPoints.text = "" + gameConfigs.TotalPoints;
        texNameAluno.text = "" + gameConfigs.namefull;
        textClass.text = gameConfigs.currentClass.descTurma;
        textSchool.text = gameConfigs.currentSchool.nomeEscola;
        textYear.text = "" + gameConfigs.currentClass.idAnoLetivo + " ANO";
    }

    public void ActiveNomeGame(bool check) {

        if (check) {
            for (int i = 0; i < textNomeGame.Length; i++) {
                textNomeGame[i].SetActive(true);
            }
        } 
        else {
            for (int i = 0; i < textNomeGame.Length; i++) {
                textNomeGame[i].SetActive(false);
            }
        }
       

    }

    IEnumerator<float> waitToStart(){
		yield return Timing.WaitForSeconds(1f);
        gameConfigs.BropsAmount = gameConfigs.currentScore.brops;
        gameConfigs.TotalPoints = gameConfigs.currentScore.pontuacaoTotal;
        usernameText.text = gameConfigs.currentUser.nomeJogador;
        UpdateCatLockSystem();
        stateMenu = menuState.main;
		selectedChars = -1;
		UpdateState (stateMenu);
		BropsTextUpdate ();
        for (int i = 0; i < textsUpdate.Length; i++) {
            textsUpdate[i].ForceMeshUpdate();
            textsUpdate[i].UpdateFontAsset();
            textsUpdate[i].SetAllDirty();
        }

        //gameConfigs.AddOrReplateMinigame
        List<DBOMINIGAMES> dboMinigames = new List<DBOMINIGAMES>();
        dboMinigames = gameConfigs.openDB().GetAllMinigames();

        int tempCount = dboMinigames.Count;
        for (int i = 0; i < tempCount; i++) {
            gameConfigs.AddOrReplateMinigame(new MinigameStruct() {
                idMinigame = dboMinigames[i].idMinigames,
                idLivro = dboMinigames[i].idLivro,
                infoMinigame = dboMinigames[i].infoMinigame,
                nomeMinigame = dboMinigames[i].nomeMinigame,
                ativo = dboMinigames[i].ativo
            });
        }

        yield return Timing.WaitForSeconds(0.1f);
        LoadMyOwnRank();

    }

    // Use this for initialization
    public void StartLate () {
        TrocaUsu();
        gameConfigs.currentScore = gameConfigs.openDB().GetScore(gameConfigs.currentUser.idUsuario);
        

		Timing.RunCoroutine(waitToStart(), MEC.Segment.SlowUpdate);
        Resources.UnloadUnusedAssets();
		textComponentBuildVersion.text = "Versão do Jogo: " + Application.version;

        textComponentCliente.text = "Cliente: " + gameConfigs.ClientName;

    }

	public void  SairGame () {
        PlayerPrefs.SetString("PlayerLastLogin", "");
        PlayerPrefs.DeleteKey("PlayerProfile");
		PlayerPrefs.DeleteAll();
		Application.Quit();
        Debug.Log("exit!");
	}

    public void changeUser() {
        PlayerPrefs.SetString("PlayerLastLogin", "");
        PlayerPrefs.DeleteKey("PlayerProfile");
        PlayerPrefs.DeleteAll();
    }

	

	public void changeScene(string nameScene){
		SceneManager.LoadScene (nameScene);
	}

	public void changeScene(int scene){
		SceneManager.LoadScene (scene);
	}

	public void buttonShowMinigame(int minigameID){
		gameDetailWindow.LoadMinigame (minigameID);
		//selectedMinigame = minigameID;
		//UpdateState (menuState.selected);
	}

	public void UpdateSelectChar(){
		selectedChars = charScroll.CurrentPage ();
		CharSelectedText.text = GetNameCharacter (selectedChars);
		PlayerPrefs.SetInt("characterSelected", selectedChars);
		//Save character selec;
	}


	public void backFromConfig(){
		gameConfigs.SavePrefs ();
        UpdateCatLockSystem();
        UpdateState (menuState.main);
	}

    public void SetSelectedMenu() {
        UpdateState(menuState.selected);
    }

	public void UpdateState(menuState state){

		switch (state) {
		case menuState.selected:
			PanelsActivation((int)state);
			selectedChars = -1;
			break;
		case menuState.loginScreen:
			PanelsActivationOver((int)state);
			break;
		default:
			PanelsActivation((int)state);
			break;
		}

	}

	/// <summary>
	/// 0 - Tela Principal.
	/// 1 - Tela de Configuração.
	/// 2 - Tela do MiniJogo Selecionado.
	/// 3 - Tela de Login/Acesso.
	/// </summary>
	/// <param name="i">The other Collider involved in this collision.</param>
	public void	PanelsActivation(int panelIndex){
		for (int i = 0; i < Panels.Length; i++)
		{
			if(Panels[i] == Panels[panelIndex]){
				Panels[i].enabled  = true;
			} else {
				Panels[i].enabled  = false;
			}
		}
	}

	public void	PanelsActivationOver(int panelsIndex){
		for (int i = 0; i < Panels.Length; i++){
			if(Panels[i] == Panels[panelsIndex]){
				Panels[i].enabled  = true;
			}
		}
	}


	public enum menuState
	{
		main,
		config,
		selected,
		loginScreen,
	}

	public void backgroundMusicValidate(){

		if (Toggles [0].isOn) {
			gameConfigs.isAudioOn = false;
		} else {
			gameConfigs.isAudioOn = true;
		}

	}


	public void soundFXValidate(){

		if (Toggles [1].isOn) {
			gameConfigs.isAudioFXOn = false;
		} else {
			gameConfigs.isAudioFXOn = true;
		}

	}

	public void VoicesValidate(){

		if (Toggles [2].isOn) {
			gameConfigs.isAudioVoiceOn = false;
		} else {
			gameConfigs.isAudioVoiceOn = true;
		}

	}

	public void OpenConfig(){

		if (gameConfigs.isAudioOn) {
			Toggles [0].isOn = false;
		} else {
			Toggles [0].isOn = true;
		}

		if (gameConfigs.isAudioFXOn) {
			Toggles [1].isOn = false;
		} else {
			Toggles [1].isOn = true;
		}

		if (gameConfigs.isAudioVoiceOn) {
			Toggles [2].isOn = false;
		} else {
			Toggles [2].isOn = true;
		}

		UpdateState (menuState.config);

	}

	public void BropsTextUpdate(){

		bropsText.text = "B$: " + bropsAmount;

	}

	public void backFromSelect(){
		stateMenu = menuState.main;
		UpdateState (menuState.main);
	}

	public string GetNameCharacter(int character){
		string name = "";

		switch (character) {
		case 0:
			name = "Zeca";
			return name;
		case 1:
			name = "Tati";
			return name;
		case 2:
			name = "Paulo";
			return name;
		case 3:
			name = "Manu";
			return name;
		case 4:
			name = "João";
			return name;
		case 5:
			name = "Bia";
			return name;
		default:
			name = "Error";
			return name;
		}

	}

	public enum CharName
	{
		zeca,
		manu,
		paulo,
		joao,
		bia,
		tati
	}

	public void networkVeryfier(){
		gameConfigs.isVerifingNetwork = true;

		switch (Application.internetReachability){
			case NetworkReachability.NotReachable:
				gameConfigs.isOnline = false;
				gameConfigs.logHandler.LogShow("Game is Offline! Line: " + (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString(), "#ba0e0e");
				break;
			case NetworkReachability.ReachableViaCarrierDataNetwork:
				gameConfigs.isOnline = true;
				break;
			case NetworkReachability.ReachableViaLocalAreaNetwork:
				gameConfigs.isOnline = true;
				break;
			default:
				gameConfigs.isOnline = false;
				break;
		}
		gameConfigs.isVerifingNetwork = false;
	}

	public string GetHtmlFromUri(string resource){
     string html = string.Empty;
     HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
     try
     {
         using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
         {
             bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
             if (isSuccess)
             {
                 using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                 {
                     //We are limiting the array to 80 so we don't have
                     //to parse the entire html document feel free to 
                     //adjust (probably stay under 300)
                     char[] cs = new char[80];
                     reader.Read(cs, 0, cs.Length);
                     foreach(char ch in cs)
                     {
                         html +=ch;
                     }
                 }
             }
         }
     }
     catch
     {
         return "";
     }
     return html;
 }

 	 /// <summary>
	 /// Callback sent to all game objects before the application is quit.
	 /// </summary>
	 void OnApplicationQuit()
	 {
		 /*gameConfigs.jogador = new DBJogador();
		 gameConfigs.jogador.IdJogador = -1;*/
	 }

	 void OnEnable(){
	#if UNITY_EDITOR
		EditorApplication.playmodeStateChanged += StateChange;
	#endif
	}
	#if UNITY_EDITOR
	void StateChange(){
		if (EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying){
			/*gameConfigs.jogador = new DBJogador();
		 	gameConfigs.jogador.IdJogador = -1;*/
		}
	}
	#endif

    public void StoreItens(){
        
        storePanel.enabled = true;
        storeManager.enabled = true;
        btsLogo.SetActive(false);
        storeManager.OpenStore();
    }

    public void LoadMyOwnRank() {
        List<DBORANKING> myRanks = gameConfigs.openDB().GetAllUserRanks(gameConfigs.currentUser.idUsuario);

        for (int i = 0; i < myRanks.Count; i++) {
            gameConfigs.allMinigames[i].highscore = myRanks[i].highscore;
        }
    }


    public IEnumerator<float> LoadInventoryData() {
        storeData.buyedItens.Clear();
        DBOINVENTARIO[] _inventory = gameConfigs.openDB().GetInventory(gameConfigs.GetCurrentUserID());
        yield return Timing.WaitForSeconds(0.2f);
        int tempCount = _inventory.Length;
        for (int i = 0; i < tempCount; i++) {
            storeData.buyedItens.Add(_inventory[i].idItem);
        }
    }

    public IEnumerator<float> LoadStoreData() {

        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadInventoryData()));
        Debug.Log("Loading Store Data.");
        storeData.SetDataService(gameConfigs.openDB());
        List<DBOITENS> _itensOnStore = gameConfigs.openDB().GetItensStoreList();
        //yield return Timing.WaitForSeconds(0.2f);
        int tempCount = _itensOnStore.Count;
        Debug.Log("Itens To Load On Store:" + tempCount);
        storeData.itensOnStore.Clear();
        //List<int> DeleteFromList = new List<int>();
        /* for (int i = 0; i < tempCount; i++) {
             if (File.Exists(config.fullPatchItemIcon + _itensOnStore[i].idItem + ".png") == false) {
                 DeleteFromList.Add(i);
             }
         }*/
        StringFast streamingAssetPatch = new StringFast();
        //Loop nos itens da loja.
        for (int i = 0; i < tempCount; i++) {
            if (_itensOnStore[i].ativo == 1) {
                streamingAssetPatch.Clear();
                //Converter Informações do SQLITE em Classe StoreItem.
                StoreItem item = storeData.dboItensToStoreItens(_itensOnStore[i]);

                //Verificação da imagem do item dentro do streaming assets.
                var persistentPathItemIcon = string.Format("{0}{1}.png", gameConfigs.fullPatchItemIcon, _itensOnStore[i].idItem);

                //Verificar se arquivo de imagem existe no PersistentPath.
                //Adicionar FLAG de verificação de download. [ ] [WARNING]
                if (File.Exists(persistentPathItemIcon)) {
                    item.itemIcon = StoreData.LoadPNG(persistentPathItemIcon);
                } else {

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_STANDALONE 
                    // PC/Unity Editor Streaming Asset.
                    streamingAssetPatch.Clear();
                    //Gerarando caminho do streaming assets folder para PC.
                    streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itensOnStore[i].idItem).Append(".png");
                    Debug.LogWarning(streamingAssetPatch.ToString(), this);
                    //Carregando bytes do arquivo de imagem em streaming assets.
                    var itemIconBytesLoaded = File.ReadAllBytes(streamingAssetPatch.ToString());
                    //Salvando bytes de streaming asset em PersistentDataPath.
                    File.WriteAllBytes(persistentPathItemIcon, itemIconBytesLoaded);
#elif UNITY_ANDROID
                    //Android Streaming Asset.
                    streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itensOnStore[i].idItem).Append(".png");
                    Debug.LogWarning(streamingAssetPatch.ToString(), this);

                    //Carregar do StreamingAssets 'apk' arquivo de imagem.
                    var loadDb = new WWW(streamingAssetPatch.ToString());
                    while (!loadDb.isDone) { }
                    //Copiar dados carregados do streaming assets 
                    File.WriteAllBytes(persistentPathItemIcon, loadDb.bytes);
                    // Android Android Ended.
#elif UNITY_IOS
                    streamingAssetPatch.Clear();
                    streamingAssetPatch.Append(Application.streamingAssetsPath).Append("/medias/item/").Append(_itensOnStore[i].idItem).Append(".png");
                    Debug.LogWarning(streamingAssetPatch.ToString(), this);
                    var ItemIconPath = streamingAssetPatch.ToString();
                    File.Copy(ItemIconPath, persistentPathItemIcon);
#endif
                    //Carrega o item agora no PersistentData.
                    item.itemIcon = StoreData.LoadPNG(persistentPathItemIcon);

                }

                streamingAssetPatch.Clear();
                streamingAssetPatch.Append("Item Name: ").Append(item.itemID).Append(" | Item File: ").Append(gameConfigs.fullPatchItemIcon).Append(_itensOnStore[i].idItem).Append(".png");
                Debug.Log(streamingAssetPatch.ToString(), this);
                storeData.itensOnStore.Add(item);
            }
        }


        /* for (int i = 0; i < tempCount; i++) {
             if (_itensOnStore[i].ativo == 1) {
                 StoreItem item = storeData.dboItensToStoreItens(_itensOnStore[i]);
                 item.itemIcon = StoreData.LoadPNG(config.fullPatchItemIcon + _itensOnStore[i].idItem + ".png");
                 Debug.Log("Item Name: " + item.itemID + " | Item File: " + config.fullPatchItemIcon + _itensOnStore[i].idItem + ".png");
                 storeData.itensOnStore.Add(item);
             }
         }*/

        LoadStoreCategoryItem();
    }

    public void LoadStoreCategoryItem() {
        storeData.itensCategory = gameConfigs.openDB().GetCategoryItem();
    }

    public void UpdateCatLockSystem() {
        int tempCount = catHandlers.Length;
        for (int i = 0; i < tempCount; i++) {
            catHandlers[i].LockVerify();
        }
    }

}
