using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

public class LevelContainer : OverridableMonoBehaviour {

    public Level level;
    public Image thumbnailContainer;
    public TextMeshProUGUI levelNameContainer;
    public StarsComponent starsComponent;
    public TextMeshProUGUI levelScoreContainer;
    public GameObject lockerObject;
    public LevelManager levelmanager;
    public DetailListHandler detailList;
    [HideInInspector]
    public GameObject thisGameObject;

    public void Start() {
        thisGameObject = this.gameObject;
    }

    public void SetLevel(Level _level) {
        this.level = _level;
        UpdateLevelInformation();
        UpdateContainers();
    }

    [ButtonGroup("Tools")]
    [Button("Update Containers")]
    public void UpdateContainers() {
        thumbnailContainer.sprite = level.iconLevel;
        levelNameContainer.SetText(level.nameLevel);
        starsComponent.Set(level.starAmount);
        levelScoreContainer.SetText(level.highscore.ToString());
    }

    [ButtonGroup("Tools")]
    [Button("Update Level Information")]
    public void UpdateLevelInformation() {
        //level.GetDescription();
        level.GetHighscore();
    }

    public void DisableContainer() {
        if(thisGameObject == null) {
            Start();
        }
        thisGameObject.SetActive(false);
    }

    public void EnableContainer() {
        if (thisGameObject == null) {
            Start();
        }
        thisGameObject.SetActive(true);
    }

    [ButtonGroup("Locker")]
    [Button("Unlock Containers")]
    public void UnlockContainer() {
        //liberar Container.
        lockerObject.SetActive(false);
    }
    [ButtonGroup("Locker")]
    [Button("Lock Containers")]
    public void LockContainer() {
        //Bloquear Container.
        lockerObject.SetActive(true);
    }

    public void LoadLevelWindow() {
        levelmanager.currentLevel = this.level;
        detailList.OpenGameWidow();
    }
}
