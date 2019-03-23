using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class StarsComponent : OverridableMonoBehaviour {

    public Sprite starSprite;
    public Sprite starHoleSprite;
    public Image[] starsComponent;
    private int starsCount;

    [Button("Update starsComponent")]
    private void Start() {
        Transform thisTransform = this.transform;
        starsComponent = thisTransform.GetComponentsInChildren<Image>();
        starsCount = starsComponent.Length;
    }

    public void Clear() {
        for (int i = 0; i < starsCount; i++) {
            starsComponent[i].sprite = starHoleSprite;
        }
    }

    public void GetAll() {
        for (int i = 0; i < starsCount; i++) {
            starsComponent[i].sprite = starSprite;
        }
    }

    public void Set(int starsAmount) {
        Clear();
        if (starsAmount < starsCount) {
            for (int i = 0; i < starsAmount; i++) {
                starsComponent[i].sprite = starSprite;
            }
        } else {
            GetAll();
        }        
    }


}
