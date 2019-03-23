using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "LevelSystem/Manager", order = 100)]
[System.Serializable]
public class LevelManager : ScriptableObject {

    [BoxGroup("Current Config", true, true)]
    public Level currentLevel;
    public Level level {
        get {
            if(currentLevel != null) {
                return currentLevel;
            } else {
                return ScriptableObject.CreateInstance<Level>();
            }
        }
    }
    public int IDDidatico {
        get {
            if (level != null) {
                return level.idGameDidaticoCurrent;
            } else {
                return -1;
            }
        }
    }
    [BoxGroup("Current Config")]
    public LevelCategory currentCategory;

    [BoxGroup("Current Config")]
    public Character currentCharacter;

    [BoxGroup("Categories", true, true)]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public LevelCategory[] Categories;
    private int oldSizeList;
    [BoxGroup("Levels Lists", true, true)]
    [LabelText("List de Todos os Levels")]
    public List<Level> allLevels = new List<Level>();
    [BoxGroup("Levels Lists")]
    [LabelText("List da Levels Completos")]
    public List<Level> allLevelsCompleto = new List<Level>();
    [BoxGroup("Levels Lists")]
    [LabelText("List da Levels Didaticos")]
    public List<Level> allLevelsDidatico = new List<Level>();
    private List<DBOMINIGAMES> allMinigamesInfo = new List<DBOMINIGAMES>();
    private List<DBOGAMES_DIDATICOS> allGamesDidaticosInfo = new List<DBOGAMES_DIDATICOS>();

    public Level FindLevelWithID(int _levelID) {
        int tempCount = allLevels.Count;
        for (int i = 0; i < tempCount; i++) {
            if(allLevels[i].idMinigame == _levelID) {
                return allLevels[i];
            }
        }

        return null;
    }

    public void SetStarsWithId(int levelId, int starsAmount) {
        //UpdateLevelsList();
        int tempCount = allLevels.Count;
        for (int i = 0; i < tempCount; i++) {
            if(allLevels[i].idMinigame == levelId) {
                allLevels[i].starAmount = starsAmount;
            }
        }
    }

    public void UpdateLevelsList() {
        int tempCount = Categories.Length;
        int sizeList = 0;
        if (tempCount >= 1) {
            for (int i = 0; i < tempCount; i++) {
                if (Categories[i] != null && Categories[i].sceneLevels != null) {
                    sizeList += Categories[i].sceneLevels.Length;
                }
            }
        }

        if(sizeList != oldSizeList) {
            allLevels.Clear();
            tempCount = Categories.Length;
            for (int i = 0; i < tempCount; i++) {
                if (Categories[i] != null && Categories[i].sceneLevels != null) {
                    int tempCount2 = Categories[i].sceneLevels.Length;
                    for (int j = 0; j < tempCount2; j++) {
                        if (!allLevels.Contains(Categories[i].sceneLevels[j])) {
                            allLevels.Add(Categories[i].sceneLevels[j]);
                        }
                    }
                }
            }
            oldSizeList = sizeList;
        }

        UpdateCompletoLevelList();
        UpdateDidaticoLevelList();
    }

    public void UpdateCompletoLevelList() {
        allLevelsCompleto.Clear();
        int tempCount = allLevels.Count;
        for (int i = 0; i < tempCount; i++) {
            if(allLevels[i].levelType == LevelType.completo) {
                allLevelsCompleto.Add(allLevels[i]);
            }
        }
    }

    public void UpdateDidaticoLevelList() {
        allLevelsDidatico.Clear();
        int tempCount = allLevels.Count;
        for (int i = 0; i < tempCount; i++) {
            if (allLevels[i].levelType == LevelType.didatico) {
                allLevelsDidatico.Add(allLevels[i]);
            }
        }
    }

    [Button("Call Validate")]
    public void OnValidate() {
        UpdateLevelsList();
    }

    public void UpdateInformationList(List<DBOMINIGAMES> _list, List<DBOGAMES_DIDATICOS> _listDidaticos) {
        allMinigamesInfo = _list;
        allGamesDidaticosInfo = _listDidaticos;
    }

    public DBOMINIGAMES GetGameInfo(int _IdMinigame) {
        int tempCount = allMinigamesInfo.Count;
        for (int i = 0; i < tempCount; i++) {
            if(allMinigamesInfo[i].idMinigames == _IdMinigame) {
                return allMinigamesInfo[i];
            }
        }
        return null;
    }

    public DBOGAMES_DIDATICOS GetGameDidaticoInfo(int _idGameDidatico) {
        int tempCount = allGamesDidaticosInfo.Count;
        for (int i = 0; i < tempCount; i++) {
            if (allGamesDidaticosInfo[i].idGameDidatico == _idGameDidatico) {
                return allGamesDidaticosInfo[i];
            }
        }
        return null;
    }

    public void UpdateLevelsInformation() {
        int tempCount = allLevels.Count;
        for (int i = 0; i < tempCount; i++) {
            allLevels[i].GetDescription();
            allLevels[i].GetHighscore();
        }
    }

    public void SetCurrentCategory(LevelCategory _category) {
        currentCategory = _category;
    }
}