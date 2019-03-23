using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "LevelSystem/Level")]
[System.Serializable]
public class Level : SerializedScriptableObject {
    [BoxGroup("Livro", true, true)]
    [ReadOnly]
    [LabelText("Livro:")]
    [GUIColor(1.0f, 1.0f, 0.0f, 1.0f)]
    public string bookName;
    [BoxGroup("Main", true, true)]
    [GUIColor(1.0f, 1.0f, 1.0f, 1.0f)]
    public Sprite iconLevel;
    [BoxGroup("Main")]
    [GUIColor(1.0f, 1.0f, 1.0f, 1.0f)]
    public string nameLevel;
    [BoxGroup("Main")]
    [LabelText("Level Scene File")]
    public SceneReference buildLevelName;
    [BoxGroup("Main")]
    [ShowIf("levelType", LevelType.completo, true)]
    public int idMinigame;
    [BoxGroup("Main")]
    [ShowIf("levelType", LevelType.didatico, true)]
    public int idGameDidatico; 
    [BoxGroup("Main")]
    public int starAmount;
    [BoxGroup("Main")]
    public int highscore;
    [BoxGroup("Main")]
    public bool useCharacterSelection;
    [BoxGroup("Main")]
    [TextArea(1,50)]
    public string description;
    [BoxGroup("Main")]
    public int idHabilidade;
    [BoxGroup("Main")]
    public LevelType levelType;
    [BoxGroup("Main")]
    [ShowIf("levelType", LevelType.completo, true)]
    public Level LevelDidatico;
    [BoxGroup("Limitation Config", true, true)]
    public LevelLimitationType limitationType;
    [BoxGroup("Limitation Config")]
    [ShowIf("limitationType", LevelLimitationType.Minimum, true)]
    public int minAnoLetivo;
    [BoxGroup("Limitation Config")]
    [ListDrawerSettings(DraggableItems = false, HideAddButton = true, ShowIndexLabels = true)]
    [ShowIf("limitationType", LevelLimitationType.Specific, true)]
    public bool[] anosLetivos = new bool[9];
    [BoxGroup("Override Configs", true, true)]
    public int[] turmasLocked = new int[0];
    [BoxGroup("Override Configs")]
    public int[] anosLetivosLocked = new int[0];
    [BoxGroup("Others", true, true)]
    public GameConfig config;
    [BoxGroup("Others")]
    public LevelManager levelManager;
    [BoxGroup("Requirements")]
    public List<Level> requeredLevels = new List<Level>();
    [BoxGroup("Requirements")]
    public bool alreadyPlayed = false;

    public int idGameDidaticoCurrent {
        get {
            if(levelType == LevelType.completo) {
                if (LevelDidatico != null) {
                    return LevelDidatico.idGameDidatico;
                } else {
                    return -1;
                }
            } else {
                return idGameDidatico;
            }
        }
/*
        set {
            Debug.Log("Not Possible Use the Inspector to applied");
        }
*/
    }

    public void AddTurmaBloqued(int _idTurma) {
        int tempCount = turmasLocked.Length;
        int[] tempLock = turmasLocked;
        turmasLocked = new int[tempLock.Length + 1];
        for (int i = 0; i < tempCount; i++) {
            turmasLocked[i] = tempLock[i];
        }
        turmasLocked[tempLock.Length] = _idTurma;
    }

    public void UnlockTurma(int _idTurma) {
        int indexTurma = -1;
        int tempCount = turmasLocked.Length;
        for (int i = 0; i < tempCount; i++) {
            if (turmasLocked[i] == _idTurma) {
                indexTurma = i;
            }
        }
        if (indexTurma > -1) {
            turmasLocked = turmasLocked.RemoveAt(indexTurma);
        }
    }

    public void AddLockAnoLetivo(int _anoLetivo) {
        int tempCount = anosLetivosLocked.Length;
        int[] tempLock = anosLetivosLocked;
        anosLetivosLocked = new int[tempLock.Length + 1];
        for (int i = 0; i < tempCount; i++) {
            anosLetivosLocked[i] = tempLock[i];
        }
        anosLetivosLocked[tempLock.Length] = _anoLetivo;
    }

    public void UnlockAnoLetiv(int _anoLetivo) {
        int indexAnoLetivo = -1;
        int tempCount = anosLetivosLocked.Length;
        for (int i = 0; i < tempCount; i++) {
            if (anosLetivosLocked[i] == _anoLetivo) {
                indexAnoLetivo = i;
            }
        }
        if (indexAnoLetivo > -1) {
            anosLetivosLocked = anosLetivosLocked.RemoveAt(indexAnoLetivo);
        }
    }

    public bool IsLocked(int _anoLetivo, int _idTurma){
        return IsLocked(_anoLetivo) && IsShowByTurma(_idTurma) && IsShowByAnoLetivo(_anoLetivo) && isLockedByLevelRequerements();
    }

    public bool IsLocked(int anoLetivo) {

        if (this.limitationType == LevelLimitationType.None) {
            return false;
        } else if (this.limitationType == LevelLimitationType.Minimum) {
            if (anoLetivo > minAnoLetivo) {
                return false;
            }
        } else if (this.limitationType == LevelLimitationType.Specific) {
            int tempCount = anosLetivos.Length;
            if (anosLetivos[anoLetivo] == true) {
                return false;
            }
        }

        return true;
    }

    public bool IsShowByAnoLetivo(int _anoLetivo) {
        int tempCount = anosLetivosLocked.Length;
        for (int i = 0; i < tempCount; i++) {
            if (anosLetivosLocked[i] == _anoLetivo) {
                return false;
            }
        }
        return true;
    }

    public bool IsShowByTurma(int _idTurma) {
        int tempCount = turmasLocked.Length;
        for (int i = 0; i < tempCount; i++) {
            if(turmasLocked[i] == _idTurma) {
                return false;
            }
        }
        return true;
    }

    public bool isLockedByLevelRequerements() {
        if (alreadyPlayed) {
            return false;
        } else {
            return true;
        }
    }

    public void OnValidate() {
        if (limitationType == LevelLimitationType.Specific) {
            if (anosLetivos.Length <= 0) {
                anosLetivos = new bool[9];
            }
        } else if (limitationType == LevelLimitationType.Minimum) {

        } else {

        }
    }
    [ButtonGroup("Tools")]
    [Button("Update Description", ButtonSizes.Small)]
    public void GetDescription() {
        if (levelType == LevelType.completo) {
            DBOMINIGAMES cmini;
            cmini = levelManager.GetGameInfo(this.idMinigame);
            if (cmini == null) {
                DataService ds = config.openDB();
                cmini = ds.GetMinigameInfo(this.idMinigame);
                
            }
            this.description = cmini.infoMinigame;
            this.name = cmini.nomeMinigame;
        } else if(levelType == LevelType.didatico) {
            DBOGAMES_DIDATICOS cDidatico;
            cDidatico = levelManager.GetGameDidaticoInfo(this.idGameDidatico);
            if(cDidatico == null) {
                DataService ds = config.openDB();
                cDidatico = ds.GetGameDidatico(this.idGameDidatico);
                if(cDidatico == null) {
                    cDidatico = new DBOGAMES_DIDATICOS()
                    {
                        descGameDidatico = "NOT FOUND!",
                        nomeGameDidatico = "NOT FOUND",
                        idGameDidatico = -1
                    };
                }
            }

            this.description = cDidatico.descGameDidatico;
            this.name = cDidatico.nomeGameDidatico;
        }
    }


    [ButtonGroup("Tools")]
    [Button("Update Highscore/Stars", ButtonSizes.Small)]
    public void GetHighscore() {
        DataService ds = config.openDB();
        if (levelType == LevelType.completo) {
            DBORANKING cRank = ds.GetRanking(this.idMinigame, this.config.playerID);
            if (cRank == null) {
                cRank = new DBORANKING()
                {
                    idMinigame = this.idMinigame,
                    idUsuario = this.config.playerID,
                    estrelas = 0,
                    highscore = 0,
                    dataInsert = config.ReturnCurrentDate()
                };
                alreadyPlayed = false;
            } else {
                alreadyPlayed = true;
            }
            this.starAmount = cRank.estrelas;
            this.highscore = cRank.highscore;

        } else if (levelType == LevelType.didatico) {
            DBOGAMEDIDATICOS_PROGRESSO cDidatico = ds.GetGameDidatico_PROGRESSO(this.config.playerID, this.idGameDidatico);
            if(cDidatico == null) {
                cDidatico = new DBOGAMEDIDATICOS_PROGRESSO()
                {
                    idUsuario = this.config.playerID,
                    idGameDidatico = this.idGameDidatico,
                    estrelas = 0,
                    highscore = 0

                };
                alreadyPlayed = false;
            } else {
                alreadyPlayed = true;
            }

            this.starAmount = cDidatico.estrelas;
            this.highscore = cDidatico.highscore;
        }
    }

}
