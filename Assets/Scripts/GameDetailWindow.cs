using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameDetailWindow : OverridableMonoBehaviour {

	public gameSelection gselect;
	public LoadManager LoadManager;
    public RankManager rankManager;
    public DetailListHandler listHandler;
	public TextMeshProUGUI minigameNameText;
	public Transform[] characterInList;
	public TextMeshProUGUI highscoreText;
	public TextMeshProUGUI bropsText;
	public Image backgroundImage;
	public Transform starsImage;
	public TextMeshProUGUI descriptionText;
	public Sprite starsFillIcon;
	public Sprite starsEmpty;
	public ScrollRect scrollRect;
	public ScrollSnap scrollSnap;
	public GameConfig gameconfig;
    public Character[] characterList = new Character[6];
    public Image thumbnailImage;
    public Canvas characterSelection;
    public Canvas gameDetailCanvas;
    public TextMeshProUGUI characterName;
    public Image characterContainerImage;
    public GameObject[] rotatingObjects = new GameObject[6];
    private int characterSeleced;
    public float changeCharacterImageDuration;
    public Scrollbar scrollbarGameDetail;
    public ScrollRect scrollRectGameDetail;
    public Canvas MainCanvas;
    //private string loadedScene;
    public Minigames minigame;
    public int idMinigame;   
    public CanvasGroup selectCharactersGroupCanvas;
    public Canvas selectCharactersCanvas;
    public CanvasGroup gameInformationGroupCanvas;
    public Canvas gameInformationCanvas;
    public CanvasGroup rankingInformationGroupCanvas;
    public Canvas rankingInformationCanvas;
    public CanvasGroup LevelsGroupCanvas;
    public Canvas LevelsCanvas;
    public Canvas currentCanvas;
    public Canvas prevCanvas;
    public CanvasGroup prevCanvasGroup;
    public CanvasGroup currentCanvasGroup;
    public GameConfig config;
    public LevelManager levelManager;
    Level currentLevel;
    public DetailListHandler detailList;
    public Button playCharSelection;
    public TextMeshProUGUI buttonAvançarText;

    public void LoadLeveList() {
        ChangeGroupCanvas(LevelsGroupCanvas, LevelsCanvas);
        detailList.SetupMenu();
    }

    public void LoadLevelListBackButton() {
        ChangeGroupCanvas(LevelsGroupCanvas, LevelsCanvas);
    }

    public void LoadLevel() {
        currentLevel = levelManager.level;
        ChangeGroupCanvas(gameInformationGroupCanvas, gameInformationCanvas);
        scrollbarGameDetail.value = 1;
        thumbnailImage.sprite = currentLevel.iconLevel;
        minigameNameText.text = minigame.name;
        //loadedScene = minigame.sceneNameAR;
        //DBORANKING rank = gameconfig.GetGameInfo(currentLevel.idMinigame);

       // Debug.Log("Estrelas: " + minigame.stars);
        UpdateStars(levelManager.level.starAmount);

        highscoreText.text = levelManager.level.highscore.ToString();
        bropsText.text = gameconfig.BropsAmount.ToString();
        levelManager.level.GetDescription();
        descriptionText.text = currentLevel.description;
        gselect.selectedChars = -1;
        gameDetailCanvas.enabled = true;
        SetCharacter(PlayerPrefs.GetInt("characterSelected", 0));
        scrollRectGameDetail.enabled = true;

        if (levelManager.level.useCharacterSelection) {
            buttonAvançarText.SetText("Avançar");
        } else {
            buttonAvançarText.SetText("Jogar");
        }

        Canvas.ForceUpdateCanvases();
        scrollRectGameDetail.verticalScrollbar.value = 1f;
        scrollRectGameDetail.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
    }

    public void LoadMinigame(int id){

        prevCanvasGroup = gameInformationGroupCanvas;
        prevCanvas = rankingInformationCanvas;
        prevCanvas.enabled = true;
        ChangeGroupCanvasStatus(prevCanvasGroup, true);
        prevCanvasGroup.DOFade(1f, 0.3f);
        idMinigame = id;
        scrollbarGameDetail.value = 1;
        gameconfig.UpdateMinigames();
		minigame = gselect.gameConfigs.allMinigames [id];
        thumbnailImage.sprite = gselect.gameConfigs.allMinigames[id].backgroundImage;
        int tempID = id + 1;
        
        minigameNameText.text = minigame.name;

        //loadedScene = minigame.sceneNameAR;
        
        //DBORANKING rank = gameconfig.GetGameInfo(tempID);        

        Debug.Log("Estrelas: " + minigame.stars);
        UpdateStars(minigame.stars);

        highscoreText.text = gameconfig.TotalPoints.ToString();
		bropsText.text = gameconfig.BropsAmount.ToString();
        //backgroundImage.sprite = minigame.backgroundImage;

        DBOMINIGAMES dbm = config.openDB().GetMinigameInfo(tempID);
                
        if(dbm != null) {
            descriptionText.text = dbm.infoMinigame;
        }

		gselect.selectedChars = -1;

        gameDetailCanvas.enabled = true;
        //characterContainerImage.color = new Color(1f, 1f, 1f, 0f);

        SetCharacter(PlayerPrefs.GetInt("characterSelected", 0));        
        scrollRectGameDetail.enabled = true;
        // scrollbarGameDetail.value = 1;
        Canvas.ForceUpdateCanvases();
        scrollRectGameDetail.verticalScrollbar.value = 1f;
        scrollRectGameDetail.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
        //Invoke("UpdateScrollbar", 0.3f);
        //rankManager.StartRanking(id);
    }


	public void PlayMinigame(){


		LoadManager.LoadAsync (levelManager.level.buildLevelName);		

	}

    public void UpdateScrollbar() {
        scrollbarGameDetail.value = 1;
        scrollbarGameDetail.size = 0f;
        scrollRectGameDetail.enabled = true;
    }

    public void SetCharacter(int i) {
        PlayerPrefs.SetInt("characterSelected", i);
        //characterName.text = characterList[i].name;
        //characterContainerImage.color = Color.white;
        characterSeleced = i;
        ChangeCharacterImage();
        ChangeTextCharacterName();
        ActiveRotationSelected(i);
    }

    public void ActiveRotationSelected(int ObjectIndex) {
        for (int i = 0; i < 6; i++) {
            if (ObjectIndex == i) {
                rotatingObjects[i].SetActive(true);
            } else {
                if (rotatingObjects[i].activeInHierarchy) {
                    rotatingObjects[i].SetActive(false);
                }
            }
        }
    }

    public void DeactiveAllSelection() {
        for (int i = 0; i < 6; i++) {
            if (rotatingObjects[i].activeInHierarchy) {
                rotatingObjects[i].SetActive(false);
            }
        }
    }

    public void ChangeCharacterImage() {
        config.levelManager.currentCharacter = characterList[characterSeleced];
        DOTween.Kill(0, false);
        Sequence changeCharacter = DOTween.Sequence();
        if (characterContainerImage.sprite != null) {
            changeCharacter.Append(characterContainerImage.DOFade(0f, changeCharacterImageDuration / 2));
        }
        changeCharacter.AppendCallback(ChangeCharacterSprite);
        changeCharacter.Append(characterContainerImage.DOFade(1f, changeCharacterImageDuration / 2));
        changeCharacter.SetId(0);
        DOTween.Play(changeCharacter);
    }

    public void ChangeTextCharacterName() {
        DOTween.Kill(1, false);
        Sequence changeTextname = DOTween.Sequence();
        changeTextname.Append(characterName.DOFade(0f, changeCharacterImageDuration / 2));
        changeTextname.AppendCallback(ChangeTextName);
        changeTextname.Append(characterName.DOFade(1f, changeCharacterImageDuration / 2));
        changeTextname.SetId(1);
        DOTween.Play(changeTextname);
    }

    public void ChangeCharacterSprite() {
        characterContainerImage.sprite = characterList[characterSeleced].spriteImage;
    }

    public void ChangeTextName() {
        characterName.text = characterList[characterSeleced].name;
    }

    public void AdvanceBtn() {
        if (levelManager.level.useCharacterSelection) {
            OpenCharacterSelection();
        } else {
            PlayMinigame();
        }
    }

    public void OpenCharacterSelection() {

        //characterSelection.enabled = true;
        //gameDetailCanvas.enabled = false;
        ChangeGroupCanvas(selectCharactersGroupCanvas, selectCharactersCanvas);
        playCharSelection.enabled = true;
        SetCharacter(0);
    }

    public void CloseCharacterSelection() {
        //gameDetailCanvas.enabled = true;
        //characterSelection.enabled = false;    
        ChangeGroupCanvas(gameInformationGroupCanvas, gameInformationCanvas);
        playCharSelection.enabled = false;
    }

    public void LoadArScene() {
        LoadManager.LoadAsync(levelManager.currentCategory.ArSceneName);
    }

    public void UpdateStars(int _startAmount) {

        if(_startAmount == 1) {
            StarsSet(1);
        } else if (_startAmount == 2) {
            StarsSet(2);
        } else if(_startAmount == 3) {
            StarsSet(3);
        } else {
            StarsSet(0);
        }
    }

    public void StarsSet(int starAmount) {
        for (int i = 0; i < starsImage.childCount; i++) {
            if (i < starAmount) {
                starsImage.GetChild(i).GetComponent<Image>().sprite = starsFillIcon;
            } else {
                starsImage.GetChild(i).GetComponent<Image>().sprite = starsEmpty;
            }
        }
    }

    public void ChangeGroupCanvas(CanvasGroup toGroup, Canvas toCanvas) {
        currentCanvas = toCanvas;
        currentCanvasGroup = toGroup;

        Sequence Transition = DOTween.Sequence();
        if (prevCanvasGroup != null && prevCanvasGroup != toCanvas) {
            Transition.Append(prevCanvasGroup.DOFade(0f, 0.3f));        
            Transition.AppendCallback(() => ChangeGroupCanvasStatus(prevCanvasGroup, false));
        }
        if (prevCanvas != null && prevCanvas != toCanvas) {
            Transition.AppendCallback(() => ChangeCanvasStatus(prevCanvas, false));
        }
        Transition.AppendCallback(() => ChangeCanvasStatus(currentCanvas, true));
        Transition.Append(currentCanvasGroup.DOFade(1f, 0.3f));
        Transition.AppendCallback(() => ChangeGroupCanvasStatus(currentCanvasGroup, true));
        Transition.AppendCallback(() => UpdatePrevious(toGroup, toCanvas));
        Transition.Play();

        //prev canvasGroup to alpha 0f;
        //prev canvasGroup Interactable false;
        //prev canvasGroup blockraycast false;
        //prev canvsOff
        //current canvasOn;
        //current canvsGroup to alpha 1f;
        //current canvasGroup Interactable true;
        //current canvasGroup blockraycast true;

    }

    public void ChangeAllStatus() {
        ChangeGroupCanvasStatus(prevCanvasGroup, false);
        ChangeCanvasStatus(prevCanvas, false);
        ChangeCanvasStatus(currentCanvas, true);
        ChangeGroupCanvasStatus(currentCanvasGroup, true);
    }

    public void ChangeGroupCanvas(CanvasGroup toGroup) {

        Sequence CanvasGroupTransition = DOTween.Sequence();
        CanvasGroupTransition.Append(prevCanvasGroup.DOFade(0f, 0.3f));
        CanvasGroupTransition.AppendCallback(() => ChangeGroupCanvasStatus(prevCanvasGroup, false));
        CanvasGroupTransition.AppendCallback(() => ChangeGroupCanvasStatus(toGroup, true));
        CanvasGroupTransition.Append(toGroup.DOFade(1f, 0.3f));
        CanvasGroupTransition.AppendCallback(() => ChangePrevCanvasGroup(toGroup));
        CanvasGroupTransition.Play();
        

    }

    public void UpdatePrevious(CanvasGroup newPrevCanvas, Canvas newCanvas) {
        prevCanvasGroup = newPrevCanvas;
        prevCanvas = newCanvas;
    }

    public void ChangePrevCanvasGroup(CanvasGroup newPrevCanvas) {
        prevCanvasGroup = newPrevCanvas;
    }

    public void ChangePrevCanvas(Canvas newCanvas) {
        prevCanvas = newCanvas;

    }

    public void ChangePrevs() {
        prevCanvasGroup = currentCanvasGroup;
        prevCanvas = currentCanvas;
    }

    public void ChangeCanvasStatus(Canvas newCanvas, bool _enable) {
        newCanvas.enabled = _enable;
    }


    public void ChangeGroupCanvasStatus(CanvasGroup CurrentCanvasGroup, bool _enable) {
        CurrentCanvasGroup.blocksRaycasts = _enable;
        CurrentCanvasGroup.interactable = _enable;
    }

}
