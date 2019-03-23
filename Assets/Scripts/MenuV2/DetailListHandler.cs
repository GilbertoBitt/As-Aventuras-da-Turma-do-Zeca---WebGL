using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DetailListHandler : OverridableMonoBehaviour {

    public Transform listParentTransform;
    public GameObject prefabContent;
    public int poolSize;
    public Queue<LevelContainer> Poolcontainer = new Queue<LevelContainer>();
    public List<LevelContainer> conteinerOnScene = new List<LevelContainer>();
    public LevelManager levelSystem;
    public GameConfig config;
    public GameDetailWindow detailWindow;
    public TextMeshProUGUI titleBarText;

    public void Start() {
        PoolSetup();
        //SetupMenu();
        //config.UpdateAll();
    }

    public void PoolSetup() {
        Poolcontainer = new Queue<LevelContainer>();
        for (int i = 0; i < poolSize; i++) {
            Poolcontainer.Enqueue(AddContainerToPool());
        }

        Debug.Log("PoolSize [ " + Poolcontainer.Count + " ]");
    }

    public LevelContainer AddContainerToPool() {
        GameObject obj = Instantiate(prefabContent, listParentTransform) as GameObject;
        LevelContainer _level = obj.GetComponent<LevelContainer>();
        _level.detailList = this;
        _level.DisableContainer();
        return _level;
    }

    public LevelContainer GetContainer() {        
        LevelContainer _l = AddContainerToPool();
        conteinerOnScene.Add(_l);
        return _l;
    }

    public void SetupMenu() {
        titleBarText.SetText(levelSystem.currentCategory.categoryName);
        int tempCount = levelSystem.currentCategory.sceneLevels.Length;
        if (conteinerOnScene.Count >= 1) {
            int tempCount2 = conteinerOnScene.Count;
            for (int i = 0; i < tempCount2; i++) {
                Destroy(conteinerOnScene[i]);
            }
        }
        for (int i = 0; i < tempCount; i++) {
            Level l = levelSystem.currentCategory.sceneLevels[i];
            LevelContainer lc = GetContainer();
            lc.level = l;
            lc.UpdateLevelInformation();
            lc.UpdateContainers();
            lc.EnableContainer();
            if (l.IsLocked(config.currentClass.idAnoLetivo, config.currentClass.idTurma)) {
                lc.LockContainer();
            } else {
                lc.UnlockContainer();
            }
        }
    }


    public void ClearMenu() {
        int tempCount = conteinerOnScene.Count;
        for (int i = 0; i < tempCount; i++) {
            conteinerOnScene[0].DisableContainer();
            Poolcontainer.Enqueue(conteinerOnScene[0]);
            conteinerOnScene.Remove(conteinerOnScene[0]);
        }
    }

	public void OpenGameWidow() {
        detailWindow.LoadLevel();
    }
}
