using UnityEngine.Events;
using Sirenix.OdinInspector;

public class RainbowSwitcher : OverridableMonoBehaviour {

    public UnityEvent OnSwitchDidatica;

    [Button("Switch Didatica",ButtonSizes.Medium)]
    public void SkipToDidatica() {
        OnSwitchDidatica.Invoke();
    }


}
