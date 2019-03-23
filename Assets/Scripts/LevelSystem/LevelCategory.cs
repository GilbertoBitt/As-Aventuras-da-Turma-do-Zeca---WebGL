using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(menuName = "LevelSystem/Category", order = 100)]
[System.Serializable]
public class LevelCategory : SerializedScriptableObject {
    [BoxGroup("Main", true, true)]
    public Sprite iconSprite;
    [BoxGroup("Main")]
    public string categoryName;
    [BoxGroup("Main")]
    public int idLivro;
    [BoxGroup("Main")]
    [InfoBox("Nome da Cena do AR dessa categoria de acordo com o build settings", InfoMessageType.Warning)]
    public SceneReference ArSceneName;
    [BoxGroup("Limitation AnoLetivo",true, true)]
    public LevelLimitationType limitationType;
    [BoxGroup("Limitation Config")]
    public int minAnoLetivo;
    [BoxGroup("Limitation Config")]
    [ListDrawerSettings(DraggableItems = false, HideAddButton = true, ShowIndexLabels = true)]
    public bool[] anosLetivos = new bool[9];
    [BoxGroup("Override Configs", true, true)]
    public int[] turmasLocked = new int[0];
    [BoxGroup("Override Configs")]
    public int[] anosLetivosLocked = new int[0];
    [BoxGroup("Levels", true, true)]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public Level[] sceneLevels = new Level[0];

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
            if(turmasLocked[i] == _idTurma) {
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

    /// <summary>
    /// Passando o ano letivo para confirmar se a fasa está liberada.
    /// </summary>
    /// <param name="anoLetivo"> ano letivo do usuario.</param>
    /// <returns> 
    /// true - está bloqueada.
    /// false - está desbloqueada.
    /// </returns>
    public bool IsLocked(int anoLetivo) {

        if(this.limitationType == LevelLimitationType.None) {
            return false;
        } else if (this.limitationType == LevelLimitationType.Minimum) {
            if(anoLetivo > minAnoLetivo) {
                return false;
            }
        } else if(this.limitationType == LevelLimitationType.Specific) {
            int tempCount = anosLetivos.Length;
            if (anosLetivos[anoLetivo] == true) {
                return false;
            }
        }

        return true;
    }

    [Button("Force OnValidate")]
    public void OnValidate() {
        if (limitationType == LevelLimitationType.Specific) {
            if (anosLetivos.Length <= 0) {
                anosLetivos = new bool[9]; 
            }
        } else if(limitationType == LevelLimitationType.Minimum) {
            
        } else {
            
        }

        BuildLevelRequerements();
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
            if (turmasLocked[i] == _idTurma) {
                return false;
            }
        }
        return true;
    }

    public Level FindLevelWithID(int levelID) {    
        int tempCount = sceneLevels.Length;
        for (int i = 0; i < tempCount; i++) {
            if(sceneLevels[i].idMinigame == levelID) {
                return sceneLevels[i];
            }
        }
        return null;
    }

    public void BuildLevelRequerements() {
        sceneLevels = sceneLevels.Where(c => c != null).ToArray();
        int tempCount = sceneLevels.Length;
        if (tempCount >= 1) {
            for (int i = 0; i < tempCount; i++) {
                if (i == 0){
                    Debug.Log(sceneLevels[i].    bookName);
                    sceneLevels[i].requeredLevels.Clear();
                } else {
                    sceneLevels[i].requeredLevels.Clear();
                    sceneLevels[i].requeredLevels.Add(sceneLevels[i - 1]);
                }
                sceneLevels[i].bookName = this.categoryName;

#if UNITY_EDITOR

                EditorUtility.SetDirty(sceneLevels[i]);
                //AssetDatabase.SaveAssets();
                //AssetDatabase.Refresh();
#endif
            }
        }
    }

    [Button("Force SetDirty")]
    public void ForceSetDirty() {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
    

}
