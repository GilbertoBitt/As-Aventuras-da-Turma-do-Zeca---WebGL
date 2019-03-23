using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class ItemDraggable_EF02LP33 : OverridableMonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    [Required]
    public TextMeshProUGUI textComponent;
    private Vector3 initPosition;
    private Transform _transformComponent;
    private Transform _initParentTransform;
    public Transform InitParentTransform {
        get {
            if(_initParentTransform == null) {
                _initParentTransform = TransformComponent.parent;
            }
            return _initParentTransform;
        }
        set {
            _initParentTransform = value;
        }
    }

    private RectTransform _RectTransformComponent;
    public RectTransform RectTransformComponent {
        get {
            if (_RectTransformComponent == null) {
                _RectTransformComponent = this.GetComponent<RectTransform>();
            }
            return _RectTransformComponent;
        }
        set {
            _RectTransformComponent = value;
        }
    }

    [Required]
    public Camera mainCamera;

    [Required]
    public VerticalLayoutGroup verticalLayoutComponent;

    public Transform TransformComponent {
        get {
            if(_transformComponent == null) {
                _transformComponent = this.transform;
            }
            return _transformComponent;
        }
        set {
            _transformComponent = value;
        }
    }    

    public Item_EF02LP33 item;
    public DropArea_EF02LP33 droppedArea;

    [Required]
    public CanvasGroup canvasGroupComponent;

    public Canvas canvasComponent;
    public GraphicRaycaster canvasRaycasterComponent;

    public bool isBeenDrag;
    public bool hasValidDrop;
    public bool hasBeenDrop;
    public bool dragAvaible = false;

    public Text textComponentTe;
    public void Start() {
        initPosition = TransformComponent.localPosition;
        if (_initParentTransform == null) {
            _initParentTransform = TransformComponent.parent;
        }
        _RectTransformComponent = this.GetComponent<RectTransform>();

        canvasComponent = this.GetComponent<Canvas>();
        canvasRaycasterComponent = this.GetComponent<GraphicRaycaster>();

        isBeenDrag = false;
        hasValidDrop = false;
        hasBeenDrop = false;
    }

    public void UpdateText() {

       
        //textComponent.maxVisibleCharacters = 0;
        Sequence changeText = DOTween.Sequence();
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 0, .3f));
        changeText.AppendCallback(() => textComponent.SetText(item.firstSyllable));
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 100, .6f));
        changeText.Play();

        //textComponent(textComponent.text.Length, 3.0f).Play();
    }

    public Sequence UpdateTextSequence() {


        //textComponent.maxVisibleCharacters = 0;
        Sequence changeText = DOTween.Sequence();
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 0, .3f));
        changeText.AppendCallback(() => textComponent.SetText(item.firstSyllable));
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 100, .6f));
        //changeText.Play();
        return changeText;
        //textComponent(textComponent.text.Length, 3.0f).Play();
    }

    public void OnBeginDrag(PointerEventData e) {
        if (dragAvaible) {
            if (TransformComponent.parent != InitParentTransform) {
                TransformComponent.SetParent(InitParentTransform);
            }

            if (verticalLayoutComponent.isActiveAndEnabled) {
                verticalLayoutComponent.enabled = false;
            }

            if (canvasGroupComponent.blocksRaycasts) {
                canvasGroupComponent.blocksRaycasts = false;
            }

            canvasComponent.overrideSorting = true;
            canvasComponent.sortingOrder = 1;

            if (!isBeenDrag) { isBeenDrag = true; };

            if (droppedArea != null) {
                droppedArea.currentItem = null;
                droppedArea.droppedItem = null;
                hasBeenDrop = false;
                droppedArea = null;
            }
        }
    }

    public void OnDrag(PointerEventData e) {
        if (dragAvaible) {
            Vector3 toValue = Input.mousePosition;
            toValue.z = TransformComponent.position.z;
            TransformComponent.position = toValue;
        }
    }    

    public void OnEndDrag(PointerEventData e) {
        EndDrag();
    }

    public void EndDrag() {
        canvasComponent.overrideSorting = false;
        canvasComponent.sortingOrder = 0;

        if (droppedArea == null && !hasBeenDrop) {
            hasValidDrop = false;
        }

        if (!hasValidDrop && !hasBeenDrop) {
            Debug.Log("No Valid Drop End Drag!");
            if (TransformComponent.parent != InitParentTransform) {
                TransformComponent.SetParent(InitParentTransform);
            }
            //TransformComponent.localPosition = initPosition;
            TransformComponent.DOLocalMove(initPosition, .5f);
            //RectTransformComponent.SetPivot(PivotPresets.MiddleRight);
            //RectTransformComponent.SetAnchor(AnchorPresets.MiddleRight, 0, 0);
            //TransformComponent.localPosition = Vector3.right;
            ///RectTransformComponent.offsetMax = new Vector2(0f, RectTransformComponent.offsetMax.y);
        }

        if (!canvasGroupComponent.blocksRaycasts) {
            canvasGroupComponent.blocksRaycasts = true;
        }

        if (isBeenDrag) { isBeenDrag = false; };

        if (droppedArea != null && hasBeenDrop == false) {
            droppedArea.droppedItem = null;
            droppedArea.tempDrag = null;
            droppedArea = null;
        };

    }
      


}
