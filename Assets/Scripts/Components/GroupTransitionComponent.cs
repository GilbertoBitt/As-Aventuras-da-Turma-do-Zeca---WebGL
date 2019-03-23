using UnityEngine;
using DG.Tweening;

public class GroupTransitionComponent : OverridableMonoBehaviour {

    public CanvasGroup canvasGroupComponent;

    public void Start() {
        if(canvasGroupComponent == null) {
            this.GetComponent<CanvasGroup>();
        }
    }

    public Tweener FadeCanvasTweener(float _alpha, float _duration) {
        return canvasGroupComponent.DOFade(_alpha, _duration);
    }

    public void OnValidate() {
        Start();
    }

    private void SetActiveCanvasGroup(bool _active) {
        canvasGroupComponent.interactable = _active;
        canvasGroupComponent.blocksRaycasts = _active;
    }

    public TweenCallback EnableActive(bool _enable) {
        return () => SetActiveCanvasGroup(_enable);
    }
}
