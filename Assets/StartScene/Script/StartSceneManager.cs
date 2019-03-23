using DG.Tweening;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour {

    #region variaveis
    public GameConfig config;
    public LoadManager loadManager;
    public StoreData storeData;
    public Text textSystemInfo;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configurações do Preloader")]
#endif
    public Image preloaderImage;
    private bool isPreloadRotating = false;
    public float preloadRotationTimeOffset;
    public float preloadRotationSpeed;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configurações Botão Jogar")]
#endif
    public float crossFadeDuration;
    public Button buttonPlay;
    public Text playButtonText;
    public int sceneToLoad;

    //deixar sempre por ultimo
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("COnfigurações Status")]
#endif
    public TextMeshProUGUI messageStatusTextComp;
    [TextAreaAttribute]
    public List<string> statusMessagesString = new List<string>();
    public Image statusOnNOfText;
    public GameObject statusOnNOfComp;
    public Text textOnnOff;
    public Color onlineColor;
    public Color offlineColor;
    public Sprite onSprite;
    public Sprite offSprite;

    /*#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Configuração Botões de Acesso")]
	#endif
	public GameObject playWithLogin;
	public GameObject playWithoutLogin;
    public GameObject quitButtonGame;*/

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("LoginPass Config")]
#endif
    public TMP_InputField loginInputField;
    public Outline loginOutlineField;
    public GameObject loginCredentialsPanel;
    public TMP_InputField passwordInputField;
    public Outline passwordOutlineField;
    public Color defaultColorOutline;
    public Color WrongColorOutline;
    public TextMeshProUGUI textWrong;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("SelectData")]
#endif
    public GameObject chooseData;

    public DBOESCOLA currentSelectedEscola;
    public DBOANOLETIVO currentSelectedAnoLetivo;
    public DBOTURMA currentSelectedTurma;
    public DBOUSUARIOS currentSelectedUser;

    public Text clientTextName;

    public Dropdown schoolListDrop;
    List<DBOESCOLA> escolasList = new List<DBOESCOLA>();

    public Dropdown yearDropDown;
    List<DBOANOLETIVO> anoLetivoList = new List<DBOANOLETIVO>();

    public Dropdown classDropDown;
    List<DBOTURMA> classList = new List<DBOTURMA>();

    public Dropdown userDropDown;
    List<DBOUSUARIOS> userList = new List<DBOUSUARIOS>();
    #endregion

    DataService db;
    public networkHelper netHelper;
    public LoginSmallTabNavigation loginTabNavigation;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Password PopUP")]
#endif
    public GameObject panelPasswordAsk;
    public InputField passwordInput;
    public Outline outlinePassword;
    public GameObject wrongPassText;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("URIs")]
#endif
    [TextArea]
    public string tokenRequest;
    [TextArea]
    
    public string syncDB2;
    [TextArea]
    public string syndDB;

    [TextArea]
    public string md5Test;
    // Use this for initialization

    public int textureLimit;
    //private StringFast stringFast = new StringFast();

    public static event Action<bool, string, string> LoginFailedEvent;

    [Header("Teste")]
    public float durationTransition;

    public Image[] ButtonsImage;
    public TextMeshProUGUI[] ButtonsText;

    [Header("LoginDefault")]
    public Image[] LoginDefaultImageComp;
    public TextMeshProUGUI[] LoginDefaultTextMeshComp;
    public CanvasGroup canvasGroupLogin;
    //public TMP_InputField[] LoginDefaultTextInputComp;

    [Header("Start Fix")]
    public GameObject[] activeAll;
    public Text buildVersionTextComp;
    
    [ContextMenu("Update ComponentList")]
    public void UpdateAllChilds() {

        int tempCount = LoginDefaultImageComp.Length;
        for (int i = 0; i < tempCount; i++) {
            Color colorTemp = LoginDefaultImageComp[i].color;
            colorTemp.a = 0f;
            LoginDefaultImageComp[i].color = colorTemp;
            LoginDefaultImageComp[i].enabled = false;
        }

        tempCount = LoginDefaultTextMeshComp.Length;
        for (int i = 0; i < tempCount; i++) {
            Color colorTemp = LoginDefaultTextMeshComp[i].color;
            colorTemp.a = 0f;
            LoginDefaultTextMeshComp[i].color = colorTemp;
            LoginDefaultTextMeshComp[i].enabled = false;
        }
    }

    public void ShowCredentials() {
        canvasGroupLogin.DOFade(1f, 0.3f);
        canvasGroupLogin.blocksRaycasts = true;
        canvasGroupLogin.interactable = true;
    }

    public void HideCredentials() {
        canvasGroupLogin.DOFade(0f, 0.3f);
        canvasGroupLogin.blocksRaycasts = false;
        canvasGroupLogin.interactable = false;
    }

   
    [ContextMenu("startScene Test")]
    public void StartScene() {
        StartDelayed();
    }

    public void ActiveOnStart() {
        int tempCount = activeAll.Length;
        for (int i = 0; i < tempCount; i++) {
            activeAll[i].SetActive(true);
        }
    }
    [ContextMenu("Delay start")]
    public void StartDelayed() {

        //activate or Nor the dataservice removeLinq;
        loginTabNavigation.enabled = false;
        buildVersionTextComp.text = "Cliente: " + config.ClientName + " | Versão do jogo: " + Application.version + "| Usos: " + config.usageCounter;

        ActiveOnStart();

        Application.targetFrameRate = 30;
        config.sessionID = UniqueID.getID();

        //textSystemInfo.text = "M: " + SystemInfo.systemMemorySize.ToString() + " G: " + SystemInfo.graphicsMemorySize.ToString() + " OS: " + SystemInfo.operatingSystem + "MT: " + SystemInfo.maxTextureSize.ToString();

        if (SystemInfo.systemMemorySize <= 1200) {
            QualitySettings.masterTextureLimit = 1;
        } else {
            QualitySettings.masterTextureLimit = 0;
        }
        Resources.UnloadUnusedAssets();

        loginInputField.text = PlayerPrefs.GetString("PlayerLastLogin", string.Empty);
        if (!String.IsNullOrEmpty(loginInputField.text)) {
            loginInputField.placeholder.enabled = false;
        }
        config.ShowDestinationItemImage();

        StartCoroutine(IsDeviceConnectAsync());
    }

    public void ChangeTextureLimite() {
        if (textureLimit >= 4) {
            textureLimit = 0;
        } else {
            textureLimit++;
        }
        QualitySettings.masterTextureLimit = textureLimit;
    }

    // Update is called once per frame
    void Update() {
        RotatePreload();

        if (!config.isOnline && statusOnNOfText.sprite == onSprite) {
            statusOnNOfText.sprite = offSprite;
            textOnnOff.text = "Offline";
            textOnnOff.color = offlineColor;
        } else if (config.isOnline && statusOnNOfText.sprite == offSprite) {
            textOnnOff.text = "Online";
            statusOnNOfText.sprite = onSprite;
            textOnnOff.color = onlineColor;
        }

    }

    IEnumerator IsDeviceConnectAsync() {

        MessageStatus("Verificando Conexão");

        isPreloadRotating = true;
        netHelper = config.NetHelper;
        //netHelper = (new GameObject("SomeObjName")).AddComponent<networkHelper>();
        netHelper.startScene = this;
        netHelper.NetworkVeryfier(config);
        //config.NetHelper = netHelper;
        //netHelper.config = config;


        //config.ShowDestinationItemImage();
        yield return new WaitWhile(() => config.isVerifingNetwork);
        statusOnNOfComp.SetActive(true);
        if (config.isOnline == false) {
            string lastLogin = PlayerPrefs.GetString("PlayerLastLogin", string.Empty);
            DBOUSUARIOS user = openedDB().GetUser(lastLogin);

            if (lastLogin != string.Empty && user != null && !config.isOnline && config.usageCounter <= config.usageLimit && config.hasAchieveLimitDate()) {
                isPreloadRotating = true;
                OfflineAcess(true, user.login, user.senha);
            } else {
                statusOnNOfText.sprite = offSprite;
                textOnnOff.text = "Offline";
                textOnnOff.color = offlineColor;
                ShowPlayButtons();
                isPreloadRotating = false;
                HideMessageStatus();
            }
        } else {
            textOnnOff.text = "Online";
            statusOnNOfText.sprite = onSprite;
            textOnnOff.color = onlineColor;
            Directory.CreateDirectory(config.fullPatchItemIcon);

            Timing.RunCoroutine(BeforeLogin());
            
        }

    }

    IEnumerator<float> BeforeLogin() {
        yield return Timing.WaitForSeconds(0.5f);

        if (config.isOnline) {
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.DBOSYNCDOWN(syndDB, config.clientID, config.gameID))); 
        }
        //yield return Timing.WaitForSeconds(0.5f);
        if (config.isOnline) {
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.GettingDBOSBeforeLogin(config.clientID, config.openDB(), syncDB2, config.gameID)));
        }
        //yield return Timing.WaitForSeconds(0.1f);
        if (config.isOnline) {
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.DelsSync(config.clientID)));
        }

        AutoLogin();

    }

    public void AutoLogin() {
        string lastLogin = PlayerPrefs.GetString("PlayerLastLogin", string.Empty);
        if (config.usageCounter < config.usageLimit && !config.hasAchieveLimitDate()) {
            lastLogin = string.Empty;
        }
        DBOUSUARIOS user = openedDB().GetUser(lastLogin);
        MessageStatus("Acessando com Usuário da Última Sessão");        
        if (config.usageCounter < config.usageLimit && config.hasAchieveLimitDate()) {
            if (!String.IsNullOrEmpty(lastLogin) && user != null && config.isOnline) {
                Timing.RunCoroutine(OnlineAcess(user.login, user.senha));
            } else if (lastLogin != string.Empty && user != null && !config.isOnline) {
                OfflineAcess(true, user.login, user.senha);
            } else {
                ShowPlayButtons();
                isPreloadRotating = false;
                ShowMessageStatus(false);
            }
        } else {
            PlayerPrefs.SetString("PlayerLastLogin", string.Empty);
            ShowPlayButtons();
            isPreloadRotating = false;
            ShowMessageStatus(false);
        }
    }

    public void RunAllQueue() {
        int count = config.queueLog.Count;
        for (int i = 0; i < count; i++) {
            config.queueLog[i]();
            //Debug.Log("queue running" + i.ToString());
        }
    }

    void RotatePreload() {
        if (isPreloadRotating) {
            if (preloaderImage.enabled == false) {
                preloaderImage.color = new Vector4(1f, 1f, 1f, 0f);
                preloaderImage.enabled = true;
                preloaderImage.DOFade(1f, durationTransition);
            }
            Vector3 rotation = new Vector3(0f, 0f, 1f);
            preloaderImage.transform.Rotate(rotation, preloadRotationSpeed * Time.deltaTime);
        } else {
            if (preloaderImage.enabled) {

                preloaderImage.DOFade(1f, durationTransition).OnComplete(() => PreloadImage(false));
                //preloaderImage.enabled = false;
            }
        }
    }

    public void PreloadImage(bool test) {
        preloaderImage.enabled = test;
    }

    public void IForgetMyPassOrUser() {
        Application.OpenURL(config.ForgetUserOrPasswordLink);
    }

    /*void playButtons(bool _enable){
		playWithLogin.SetActive(_enable);
		playWithoutLogin.SetActive(_enable);
        quitButtonGame.SetActive(_enable);

    }*/

    void ShowPlayButtons() {
        for (int i = 0; i < 3; i++) {
            Color tempColorImage = ButtonsImage[i].color;
            tempColorImage.a = 0f;
            ButtonsImage[i].color = tempColorImage;
            Color tempColorText = ButtonsText[i].color;
            tempColorText.a = 0f;
            ButtonsText[i].color = tempColorText;
            ButtonsImage[i].enabled = true;
            ButtonsText[i].enabled = true;
            ButtonsImage[i].DOFade(1f, durationTransition);
            ButtonsText[i].DOFade(1f, durationTransition);
        }
    }

    void HidePlayButtons() {
        for (int i = 0; i < 3; i++) {
            if (i == 0) {
                ButtonsImage[i].DOFade(0f, durationTransition).OnComplete(DeactiveAll);
            } else {
                ButtonsImage[i].DOFade(0f, durationTransition);
            }
            ButtonsText[i].DOFade(0f, durationTransition);

        }
    }

    public void OpenChooseData() {
        HidePlayButtons();
        chooseData.SetActive(true);
        LoadSchollOfClient();
    }

    public void DeactiveAll() {
        for (int i = 0; i < 3; i++) {
            ButtonsImage[i].enabled = false;
            ButtonsText[i].enabled = false;
        }
    }

    public void ToggleText(TextMeshProUGUI textComp, bool set) { textComp.enabled = set; }

    void LoadClientName() {
        clientTextName.text = openedDB().GetClientName(config.clientID);
    }

    void LoadSchollOfClient() {
        List<string> optionSchool = new List<string>();
        escolasList.Clear();
        escolasList = openedDB().GetSchoolList(config.clientID);
        int countTemp = escolasList.Count;
        optionSchool.Add("-");
        for (int i = 0; i < countTemp; i++) {
            optionSchool.Add(escolasList[i].nomeEscola);
        }
        schoolListDrop.ClearOptions();
        schoolListDrop.AddOptions(optionSchool);
        schoolListDrop.RefreshShownValue();
    }

    void LoadYearsOfSchool() {
        List<string> optionYears = new List<string>();
        anoLetivoList.Clear();
        anoLetivoList = openedDB().GetYearsList();
        int countTemp = anoLetivoList.Count;
        optionYears.Add("-");
        for (int i = 0; i < countTemp; i++) {
            optionYears.Add(anoLetivoList[i].descAnoLetivo);
        }
        yearDropDown.ClearOptions();
        yearDropDown.AddOptions(optionYears);
        yearDropDown.RefreshShownValue();
    }

    void LoadClassOfYears() {
        List<string> optionYears = new List<string>();
        classList.Clear();
        classList = openedDB().GetClassList(currentSelectedAnoLetivo.idAnoLetivo);
        int countTemp = classList.Count;
        optionYears.Add("-");
        for (int i = 0; i < countTemp; i++) {
            optionYears.Add(classList[i].descTurma);
        }
        classDropDown.ClearOptions();
        classDropDown.AddOptions(optionYears);
        classDropDown.RefreshShownValue();
    }

    void LoadUserFromClass() {
        List<string> optionYears = new List<string>();
        userList.Clear();
        userList = openedDB().GetUserList(currentSelectedTurma.idTurma);
        int countTemp = userList.Count;
        optionYears.Add("-");
        for (int i = 0; i < countTemp; i++) {
            optionYears.Add(userList[i].nomeJogador);
        }
        userDropDown.ClearOptions();
        userDropDown.AddOptions(optionYears);
        userDropDown.RefreshShownValue();
    }

    DataService openedDB() {
        if (db == null) {
            db = config.openDB();
        }
        return db;
    }

    public void OnValidateSchoolDropDown() {
        if (schoolListDrop.value >= 1) {
            currentSelectedEscola = escolasList[schoolListDrop.value - 1];
            yearDropDown.interactable = true;
            classDropDown.value = 0;
            userDropDown.value = 0;
            yearDropDown.value = 0;
            classDropDown.interactable = false;
            userDropDown.interactable = false;
            LoadYearsOfSchool();
        } else {
            yearDropDown.interactable = false;
        }
    }

    public void OnValidadeYearsDropDown() {
        if (yearDropDown.value >= 1) {
            currentSelectedAnoLetivo = anoLetivoList[yearDropDown.value - 1];
            userDropDown.value = 0;
            classDropDown.value = 0;
            classDropDown.interactable = true;
            userDropDown.interactable = false;
            LoadClassOfYears();
        } else {
            classDropDown.interactable = false;
        }
    }

    public void OnValidadeClassDropDown() {
        if (classDropDown.value >= 1) {
            currentSelectedTurma = classList[classDropDown.value - 1];
            userDropDown.interactable = true;
            userDropDown.value = 0;
            LoadUserFromClass();
        } else {
            userDropDown.interactable = false;
        }
    }

    public void OnValidadeUser() {
        if (userDropDown.value >= 1) {
            currentSelectedUser = userList[userDropDown.value - 1];
            Debug.Log(currentSelectedUser.login);
            OpenPopUp();
        }
    }

    public void CloseChooseData() {
        chooseData.SetActive(false);
        ShowPlayButtons();
    }

    public void MessageStatus(string messageText) {
        if (messageStatusTextComp != null) {
            Sequence Transition = DOTween.Sequence();
            if (messageStatusTextComp.enabled) {
                Transition.Append(messageStatusTextComp.DOFade(0f, durationTransition));
            }
            if (messageStatusTextComp.color.a > 0f) {
                Color TempColor = messageStatusTextComp.color;
                TempColor.a = 0f;
                messageStatusTextComp.color = TempColor;
                messageStatusTextComp.text = messageText;
            }
            messageStatusTextComp.enabled = true;
            Transition.Append(messageStatusTextComp.DOFade(1f, durationTransition));
            Transition.Play();
        }
    }

    public void MessageStatus(int messageID) {
        messageStatusTextComp.enabled = true;
        messageStatusTextComp.text = statusMessagesString[messageID];
    }

    public void ShowMessageStatus(bool showMessage) {
        messageStatusTextComp.enabled = showMessage;
    }

    public void HideMessageStatus() {
        messageStatusTextComp.enabled = false;
    }

    void ShowPlayButton() {
        //crossFadeAlpha(buttonPlay.image, 1f,crossFadeDuration);
        //crossFadeAlpha(playButtonText, 1f,crossFadeDuration);
        buttonPlay.interactable = true;
    }

    void crossFadeAlpha(Image image, float alpha, float duration) {
        //StartCoroutine(CrossFadeAlphaAsync(image,alpha,duration));
    }

    void crossFadeAlpha(Text text, float alpha, float duration) {
        //StartCoroutine(CrossFadeAlphaAsync(text,alpha,duration));
    }


    IEnumerator CrossFadeAlphaAsync(Image _image, float _alpha, float _duration) {
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

        Color startColor = _image.color;
        Color endColor = startColor;
        endColor.a = _alpha;
        float times = 0.0f;
        while (times < _duration) {
            times += Time.deltaTime;
            float s = times / _duration;

            _image.color = Color.Lerp(startColor, endColor, curve.Evaluate(s));

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CrossFadeAlphaAsync(Text text, float _alpha, float _duration) {
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

        Color startColor = text.color;
        Color endColor = startColor;
        endColor.a = _alpha;
        float times = 0.0f;
        while (times < _duration) {
            times += Time.deltaTime;
            float s = times / _duration;

            text.color = Color.Lerp(startColor, endColor, curve.Evaluate(s));

            yield return new WaitForEndOfFrame();
        }
    }

    public void PlayButton() {
        LoginWithCredentials();
    }

    public void BackToButtons() {
        //loginCredentialsPanel.SetActive(false);
        HideCredentials();
        ShowPlayButtons();

    }

    public void LoginWithCredentials() {
        //loginCredentialsPanel.SetActive(true);
        ShowCredentials();
        //Debug.Log(SystemInfo.deviceUniqueIdentifier);
        HidePlayButtons();
    }

    public void LoginC() {

        if (loginInputField.text.Length < 1) {
            //Login Incompleto ou Inválido.

            loginOutlineField.effectColor = WrongColorOutline;
        } else if (passwordInputField.text.Length < 4) {
            //Insirá uma senha valida.
            passwordOutlineField.effectColor = WrongColorOutline;
        } else {
            if (config.usageCounter < config.usageLimit && config.hasAchieveLimitDate()) {
                if (config.isOnline) {
                    Timing.RunCoroutine(OnlineAcess());
                } else {
                    OfflineAcess(true);
                }
            } else {
                if (!config.isOnline) return;
                config.usageCounter = 0;
                Timing.RunCoroutine(OnlineAcess());
            }
        }
    }

    IEnumerator<float> OnlineAcess() {

        //loginCredentialsPanel.SetActive(false);
        HideCredentials();
        isPreloadRotating = true;
        string md5pass = Md5Sum(passwordInputField.text);
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.GetTokenIE(tokenRequest, config.clientID.ToString(), loginInputField.text, md5pass, SystemInfo.deviceUniqueIdentifier, this)));
        if (!config.isOnline) yield break;
        yield return Timing.WaitForSeconds(0.5f);
        LoginFailedEvent = OfflineAcess;
        //yield return Timing.WaitUntilDone(Timing.RunCoroutine(netHelper.QueueSend(LoginFailedEvent, config.jogosLog, config.rankLogs, config.statisticLog, config.gamesLogs, config.usersIDScoreToUpdate, config.usersIDInventoryUpdate)));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.SyncInfo(LoginFailedEvent)));
        yield return Timing.WaitForSeconds(0.5f);
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.SyncAfterLogin(config.clientID, config.openDB(), config.GetURL("getMetodo4"), config.NetHelper.Token, this)));
        //Timing.RunCoroutine(LoadStoreData());

    }

    IEnumerator<float> OnlineAcess(string username, string password) {

        //loginCredentialsPanel.SetActive(false);
        HideCredentials();
        isPreloadRotating = true;
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.GetTokenIE(tokenRequest, config.clientID.ToString(), username, password, SystemInfo.deviceUniqueIdentifier, this)));
        if (!config.isOnline) yield break;
        yield return Timing.WaitForSeconds(0.5f);
        LoginFailedEvent = OfflineAcess;
        //yield return Timing.WaitUntilDone(Timing.RunCoroutine(netHelper.QueueSend(LoginFailedEvent,config.jogosLog, config.rankLogs, config.statisticLog, config.gamesLogs, config.usersIDScoreToUpdate, config.usersIDInventoryUpdate)));
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.SyncInfo(LoginFailedEvent)));
        yield return Timing.WaitForSeconds(0.5f);
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(config.NetHelper.SyncAfterLogin(config.clientID, openedDB(), config.GetURL("getMetodo4"), netHelper.Token, this)));
        //Timing.RunCoroutine(LoadStoreData());
    }

    public void OnlineAcessSucess() {
        StartCoroutine(LoadStoreData());
        DBOUSUARIOS user = openedDB().GetUser(loginInputField.text) ?? new DBOUSUARIOS();
        user.idUsuario = config.NetHelper.userID;
        user.login = loginInputField.text;
        string md5pass = Md5Sum(passwordInputField.text);

        PlayerPrefs.SetString("PlayerLastLogin", user.login);
        //user.senha = md5pass;
        config.UpdateScore(user);
        if (user.token != config.NetHelper.Token) {
            user.token = config.NetHelper.Token;
            user.dataUpdate = config.ReturnCurrentDate();
            //openedDB().UpdateUser(user);
        }
        config.usageCounter++;
        loadManager.LoadAsync("selectionMinigames");
    }

    public void OnlineAcessSucess2() {
        StartCoroutine(LoadStoreData());
        DBOUSUARIOS user = currentSelectedUser ?? new DBOUSUARIOS();
        user.idUsuario = currentSelectedUser.idUsuario;
        user.login = currentSelectedUser.login;
        string md5pass = currentSelectedUser.senha;

        PlayerPrefs.SetString("PlayerLastLogin", currentSelectedUser.login);
        //user.senha = md5pass;
        config.UpdateScore(user);
        if (user != null && user.token != config.NetHelper.Token) {
            user.token = config.NetHelper.Token;
            user.dataUpdate = config.ReturnCurrentDate();
            //openedDB().UpdateUser(user);
        }
        config.usageCounter++;
        loadManager.LoadAsync("selectionMinigames");
    }

    public void OnlineAcessSucess(string username, string password) {
        StartCoroutine(LoadStoreData());
        MessageStatus("Logado com sucesso");
        StartCoroutine(LoadStoreData());
        DBOUSUARIOS user = openedDB().GetUser(username) ?? new DBOUSUARIOS() {
            idUsuario = config.NetHelper.userID,
            login = username,
            senha = password
        };
        //string md5pass = Md5Sum(password); 
        PlayerPrefs.SetString("PlayerLastLogin", user.login);
        config.UpdateScore(user);
        if (user.token != config.NetHelper.Token) {
            user.token = config.NetHelper.Token;
            user.dataUpdate = config.ReturnCurrentDate();
            //openedDB().UpdateUser(user);
        }
        config.usageCounter++;
        loadManager.LoadAsync("selectionMinigames");
    }

    public void OnlineAcessFailed() {
        Invoke("AcessFailed", 0.1f);
        //Timing.KillCoroutines("LoginRoutine");
    }

    public void AcessFailed() {
        //Timing.RunCoroutine(LoadStoreData());
        MessageStatus("Falha na autenticação");

        isPreloadRotating = false;
        //loginCredentialsPanel.SetActive(true);
        ShowCredentials();
        textWrong.enabled = true;
        textWrong.DOFade(1f, 0.3f);
        //textWrong.ena
        //loginInputField.textComponent.text = "";
        //loginInputField.placeholder.enabled = false;
        //loginInputField.textComponent.enabled = true;
        passwordInput.textComponent.text = string.Empty;
        loginOutlineField.effectColor = WrongColorOutline;
        passwordOutlineField.effectColor = WrongColorOutline;
        //PlayerPrefs.SetString("PlayerLastLogin", "");
        passwordInputField.text = string.Empty;
        MessageStatus(string.Empty);
    }

    public void OfflineAcess(bool hasCredentials) {
        MessageStatus("Autenticando usuário");
        StartCoroutine(LoadStoreData());
        DBOUSUARIOS userTemp = openedDB().GetUser(loginInputField.text);
        DBOPONTUACAO userScore = openedDB().GetScore(userTemp.idUsuario);
        string md5pass = Md5Sum(passwordInputField.text);
        MessageStatus("Autenticando usuário");
        if (userTemp.login == loginInputField.text && userTemp.senha == md5pass) {
            config.UpdateCurrent(userTemp);
            config.hasCredentials = true;
            config.NetHelper.Token = string.Empty;
            if (userScore != null) {
                config.TotalPoints = userScore.pontuacaoTotal;
                config.BropsAmount = userScore.brops;
                config.BropsDeviceAmount = userScore.BropsDevice;
                config.TotalPointsDevice = userScore.PontuacaoTotalDevice;
            } else {
                config.TotalPoints = 0;
                config.BropsAmount = 0;
                config.BropsDeviceAmount = 0;
                config.TotalPointsDevice = 0;
            }
            config.NetHelper.RegisterLogLogin(userTemp);
            config.NetHelper.UpdatePlayer(userTemp);
            config.currentUser = userTemp;
            MessageStatus("Sucesso! Entrando no Jogo!");
            PlayerPrefs.SetString("PlayerLastLogin", userTemp.login);
            //Timing.RunCoroutine(LoadStoreData());
            config.usageCounter++;
            loadManager.LoadAsync("selectionMinigames");
        } else {
            if (userTemp.login != loginInputField.text || userTemp.senha != md5pass) {
                //Login ou senha errado.
                textWrong.enabled = true;
                textWrong.DOFade(1f, 0.3f);
                loginOutlineField.effectColor = WrongColorOutline;
                passwordOutlineField.effectColor = WrongColorOutline;
                PlayerPrefs.SetString("PlayerLastLogin", string.Empty);
                passwordInputField.text = string.Empty;
                MessageStatus(string.Empty);
            }
        }
    }

    public void OfflineAcess(bool hasCredentials, string username, string password) {
        MessageStatus("Autenticando usuário");
        isPreloadRotating = true;
        StartCoroutine(LoadStoreData());
        DBOUSUARIOS userTemp = openedDB().GetUser(username);
        DBOPONTUACAO userScore = openedDB().GetScore(userTemp.idUsuario);
        //Debug.Log(md5pass);        
        if (userTemp.login == username && userTemp.senha == password) {
            config.UpdateCurrent(userTemp);
            config.hasCredentials = true;
            config.NetHelper.Token = userTemp.token;
            if (userScore != null) {
                config.TotalPoints = userScore.pontuacaoTotal;
                config.BropsAmount = userScore.brops;
                config.BropsDeviceAmount = userScore.BropsDevice;
                config.TotalPointsDevice = userScore.PontuacaoTotalDevice;
            } else {
                config.TotalPoints = 0;
                config.BropsAmount = 0;
                config.BropsDeviceAmount = 0;
                config.TotalPointsDevice = 0;
            }
            netHelper.RegisterLogLogin(userTemp);
            netHelper.UpdatePlayer(userTemp);
            config.currentUser = userTemp;
            MessageStatus("Sucesso! Entrando no Jogo!");
            PlayerPrefs.SetString("PlayerLastLogin", userTemp.login);
            config.usageCounter++;
            loadManager.LoadAsync("selectionMinigames");
        } else {
            if (userTemp.login == username && userTemp.senha == password && config.hasAchieveLimitDate() &&
                config.usageCounter <= config.usageLimit) return;
            //Login ou senha errado.
            textWrong.enabled = true;
            textWrong.DOFade(1f, 0.3f);
            loginOutlineField.effectColor = WrongColorOutline;
            passwordOutlineField.effectColor = WrongColorOutline;
            passwordInputField.text = string.Empty;
            PlayerPrefs.SetString("PlayerLastLogin", string.Empty);
            MessageStatus(string.Empty);
        }
    }


    public IEnumerator LoadInventoryData() {
        storeData.buyedItens.Clear();
        DBOINVENTARIO[] _inventory = openedDB().GetInventory(config.GetCurrentUserID());
        yield return Yielders.Get(0.2f);
        int tempCount = _inventory.Length;
        for (int i = 0; i < tempCount; i++) {
            storeData.buyedItens.Add(_inventory[i].idItem);
        }
    }

    public IEnumerator LoadStoreData() {

        yield return LoadInventoryData();
        Debug.Log("Loading Store Data.");
        storeData.SetDataService(openedDB());
        List<DBOITENS> _itensOnStore = openedDB().GetItensStoreList();
        //yield return Timing.WaitForSeconds(0.2f);
        int tempCount = _itensOnStore.Count;
        Debug.Log(string.Format("Itens To Load On Store:{0}", tempCount.ToString()));
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
            if (_itensOnStore[i].ativo != 1) continue;
            streamingAssetPatch.Clear();
            //Converter Informações do SQLITE em Classe StoreItem.
            StoreItem item = storeData.dboItensToStoreItens(_itensOnStore[i]);

            //Verificação da imagem do item dentro do streaming assets.
            var persistentPathItemIcon = string.Format("{0}{1}.png", config.fullPatchItemIcon, _itensOnStore[i].idItem);

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
            streamingAssetPatch.Append("Item Name: ").Append(item.itemID).Append(" | Item File: ").Append(config.fullPatchItemIcon).Append(_itensOnStore[i].idItem).Append(".png");
            Debug.Log(streamingAssetPatch.ToString(), this);
            storeData.itensOnStore.Add(item);
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
        storeData.itensCategory = openedDB().GetCategoryItem();
    }


    public void BackToDefaultLogin() {
        loginOutlineField.effectColor = defaultColorOutline;
        DisableWrongText();
    }

    public void BackToDefaulPass() {
        passwordOutlineField.effectColor = defaultColorOutline;
        DisableWrongText();
    }

    public void DisableWrongText() {
        textWrong.enabled = false;
        textWrong.DOFade(0f, 0.3f);
        loginInputField.placeholder.enabled = false;
        MessageStatus(string.Empty);
    }


    /// <summary>
    /// Logins the text correct.
    /// </summary>
    /// <param name="loginBreak">Login break.</param>
    /// <param name="type">Type. 0 - Formato de Exibição, 1 - Apenas a cidade, 2 - apenas a Escola, 3 - apenas a matricula</param>
    public string GetLogin(string loginBreak, int type) {

        string loginTemp = loginBreak;
        string cityTemp = string.Empty;
        string schoolTemp = string.Empty;
        string idSchool = string.Empty;
        for (int i = 0; i < 3; i++) {
            cityTemp += loginTemp[i].ToString();
        }

        for (int i = 3; i < 6; i++) {
            schoolTemp += loginTemp[i].ToString();
        }

        for (int i = 6; i < 10; i++) {
            idSchool += loginTemp[i].ToString();
        }




        string newLogin = string.Empty + cityTemp + "-" + schoolTemp + "-" + idSchool;

        switch (type){
            case 0:
                return newLogin;
            case 1:
                return cityTemp;
            case 2:
                return schoolTemp;
            case 3:
                return idSchool;
        }


        return string.Empty;
    }


#region PopUpChooseDataPassword

    public void OpenPopUp() {
        panelPasswordAsk.SetActive(true);
    }

    public void ClosePopUp() {
        panelPasswordAsk.SetActive(false);
    }

    public void BackDefaultOutline() {
        wrongPassText.SetActive(false);
        outlinePassword.effectColor = defaultColorOutline;
    }

    public void WrongPassword() {
        outlinePassword.effectColor = WrongColorOutline;
        wrongPassText.SetActive(true);
    }

    public void EnterJustPassword() {

        if (passwordInput.text.Length > 4) {
            //senha requerida pronta
            string md5pass = Md5Sum(passwordInput.text);
            if (md5pass == currentSelectedUser.senha) {
                if(config.usageCounter < config.usageLimit && config.hasAchieveLimitDate()) {
                    if (!config.isOnline) {
                        //DBOCLIENTES currentCliente = openedDB().GetClient(config.clientID);                   
                        config.UpdateCurrent(currentSelectedEscola, currentSelectedAnoLetivo, currentSelectedTurma, currentSelectedUser);
                        config.hasCredentials = true;
                        chooseData.SetActive(false);
                        PlayerPrefs.SetString("PlayerLastLogin", currentSelectedUser.login);
                        config.usageCounter++;
                        loadManager.LoadAsync("selectionMinigames");
                    } else {
                        //DBOCLIENTES currentCliente = openedDB().GetClient(config.clientID);
                        config.UpdateCurrent(currentSelectedEscola, currentSelectedAnoLetivo, currentSelectedTurma, currentSelectedUser);
                        config.hasCredentials = true;
                        chooseData.SetActive(false);
                        Debug.Log("Test  Pass: " + md5pass + " Login: " + currentSelectedUser.login);
                        StartCoroutine(OnlineAcess2(md5pass, currentSelectedUser.login));
                    }
                } else {
                    if (!config.isOnline) return;
                    config.UpdateCurrent(currentSelectedEscola, currentSelectedAnoLetivo, currentSelectedTurma, currentSelectedUser);
                    config.hasCredentials = true;
                    chooseData.SetActive(false);
                    Debug.Log("Test  Pass: " + md5pass + " Login: " + currentSelectedUser.login);
                    config.usageCounter = 0;
                    StartCoroutine(OnlineAcess2(md5pass, currentSelectedUser.login));
                }               
            } else {
                WrongPassword();
            }
        } else {
            WrongPassword();
        }

    }

    [ContextMenu("TestMD5Hash")]
    public void ToMD5Test() {
        string test = Md5Sum(md5Test);
        Debug.Log(test + " : " + md5Test);
    }

    IEnumerator OnlineAcess2(string pass, string login) {
        //loginCredentialsPanel.SetActive(false);
        HideCredentials();
        isPreloadRotating = true;
        //string md5pass = Md5Sum(passwordInputField.text);

        yield return StartCoroutine(config.NetHelper.GetTokenIE(tokenRequest, config.clientID.ToString(), login, pass, SystemInfo.deviceUniqueIdentifier, this));
        yield return Yielders.Get(0.5f);
        LoginFailedEvent = OfflineAcess;
        if (config.isOnline) {
            //yield return Timing.WaitUntilDone(Timing.RunCoroutine(netHelper.QueueSend(LoginFailedEvent, config.jogosLog, config.rankLogs, config.statisticLog, config.gamesLogs, config.usersIDScoreToUpdate, config.usersIDInventoryUpdate)));
            yield return StartCoroutine(config.NetHelper.SyncInfo(LoginFailedEvent));
            yield return Yielders.Get(0.5f);
            yield return StartCoroutine(config.NetHelper.SyncAfterLogin2(config.clientID, config.openDB(), config.GetURL("getMetodo4"), config.NetHelper.Token, this));
        }
        //Timing.RunCoroutine(LoadStoreData()); netHelper.token, this)));
        config.usageCounter++;
    }

    public string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = string.Empty;

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public void QuitGame() {
        Application.Quit();
    }

#endregion

}
