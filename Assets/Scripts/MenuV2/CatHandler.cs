using UnityEngine;
using Sirenix.OdinInspector;

public class CatHandler : OverridableMonoBehaviour {

    public GameObject defaultComponent;
    public GameObject lockedComponent;
    public LevelCategory category;
    public GameConfig config;

    [ButtonGroup("LockerSystem")]
    [Button("Unlock Book")]
    public void UnlockComponent() {
        defaultComponent.SetActive(true);
        lockedComponent.SetActive(false);
    }

    [ButtonGroup("LockerSystem")]
    [Button("Lock Book")]
    public void LockComponent() {
        lockedComponent.SetActive(true);
        defaultComponent.SetActive(false);
    }

    public void LockVerify() {
        if(category.IsLocked(config.currentClass.idAnoLetivo) && category.IsShowByAnoLetivo(config.currentClass.idAnoLetivo) && category.IsShowByTurma(config.currentClass.idTurma)) {
            LockComponent();
        } else {
            UnlockComponent();
        }
    }
	
}
