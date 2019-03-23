using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ContainerBubblesBQ : OverridableMonoBehaviour {

    public bool isCorrect;
    public Sprite spriteAnswer;
    public Image imageComponent;
    public FloatingComp_BQ floating;
    public Rigidbody2D rigidbodyComponent;
    public float initXPos;
    public bool canBeClicked = false;
    //public ManagerBQ manager;

    public void UpdateSprite(Sprite _sprite) {
        spriteAnswer = _sprite;
        imageComponent.sprite = spriteAnswer;
    }

    public void Start() {

        initXPos = floating.itemTransform.localPosition.x;
    }

    public void EnableClicked() {
        canBeClicked = true;
    }

    public void SetKinematic(bool _enable) {
        if (_enable) {
            rigidbodyComponent.bodyType = RigidbodyType2D.Kinematic;
            rigidbodyComponent.velocity = Vector2.zero;
            rigidbodyComponent.angularVelocity = 0f;
        } else {
            rigidbodyComponent.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    [ButtonGroup("main")]
    [Button("Reset Position")]
    public void ResetPosToFall() {
        canBeClicked = false;
        floating.itemTransform.localPosition = new Vector3(initXPos, 510f , floating.itemTransform.localPosition.z);
    }

    [ButtonGroup("main")]
    [Button("Move to Start Position")]
    public void DownToStart() {
        Sequence DownToStart = DOTween.Sequence();
        DownToStart.Append(floating.itemTransform.DOLocalMoveY(100f, 2f, false));
        DownToStart.AppendCallback(() => floating.StartFloat());
        DownToStart.AppendCallback(() => EnableClicked());
        DownToStart.Play().SetId(006);
    }

    [ButtonGroup("main")]
    [Button("Fall When Wrong")]
    public void FallWW() {
        SetFloating(false);
        floating.itemTransform.DOLocalMoveY(-520, 2f, false);
    }

    [ButtonGroup("main")]
    [Button("Go To Center")]
    public void GoToCenter() {
        floating.itemTransform.DOLocalMove(new Vector3(0f, 30f, floating.itemTransform.localPosition.z), 1f);
        floating.itemTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void SetFloating(bool _enable) {
        if (_enable) {
            floating.StartFloat();
        } else {
            floating.StopFloat();
        }
    }



}
