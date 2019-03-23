using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class DropArea_EF02LP33 : OverridableMonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public ItemDraggable_EF02LP33 tempDrag = null;
    public ItemDraggable_EF02LP33 currentItem = null;
    public ItemDraggable_EF02LP33 droppedItem = null;
    public Item_EF02LP33 item;
    public Transform parentDropper;
    private bool runningDropAction;
    public TextMeshProUGUI textComponent;
    public Manager_EF02LP33 manager;
    public Transform itemImageTransformComponent;
    public Button itemImageButtonComponent;
    public GraphicRaycaster itemImageGraphicRaycasterComp;
    public Camera mainCameraComponent;
    public Canvas itemImageCanvasComponent;
    public Image itemImageComponent;
    public GameObject BlockerInteractionComponent;
    public Image BlockerImageComponent;
    public Vector3 initPosImageItem;
    public Vector3 initScaleImageItem;
    public Image TickImageComponent;
    public bool isZoomIn;

    public void Start() {
        droppedItem = null;
        tempDrag = null;
        isZoomIn = false;
        runningDropAction = false;
        mainCameraComponent = Camera.main;
        initPosImageItem = itemImageTransformComponent.position;
        initScaleImageItem = itemImageTransformComponent.localScale;
        
    }

    [ButtonGroup("Zoom")]
    [Button("ZoomIn")]
    public void ZoomIn() {
        if (isZoomIn || DOTween.IsTweening(005, true)) return;
        initPosImageItem = itemImageTransformComponent.position;
        initScaleImageItem = itemImageTransformComponent.localScale;
        EnableImageItemOverriding(true);
        Vector3 centerOnScreenPos = mainCameraComponent.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Sequence ZoomIn = DOTween.Sequence();
        ZoomIn.SetId(005);
        ZoomIn.AppendCallback(() => EnableCanvasBlocker(true));
        ZoomIn.Append(itemImageTransformComponent.DOMove(centerOnScreenPos, .5f, false));
        ZoomIn.Join(itemImageTransformComponent.DOScale(Vector3.one * 3f, .5f));
        ZoomIn.Join(BlockerImageComponent.DOFade(.5f, .6f));
        ZoomIn.AppendCallback(() => EnableGraphicRaycasterItemImage(false));
        ZoomIn.AppendCallback(() => EnableButtonImageItem(false));
        ZoomIn.AppendCallback(() => EnableImageRaycastTarget(false));
        ZoomIn.AppendCallback(() => SetIsZoomIn(true));
        ZoomIn.Play();
    }

    [ButtonGroup("Zoom")]
    [Button("ZoomOut")]
    public void ZoomOut() {
        //itemImageCanvasComponent.sortingOrder = 2;
        if (!isZoomIn || DOTween.IsTweening(006, true)) return;
        Sequence ZoomOut = DOTween.Sequence();
        ZoomOut.SetId(006);
        ZoomOut.Append(itemImageTransformComponent.DOMove(initPosImageItem, .5f, false));
        ZoomOut.Join(itemImageTransformComponent.DOScale(Vector3.one, .5f));
        ZoomOut.Join(BlockerImageComponent.DOFade(0f, .6f));
        ZoomOut.AppendCallback(() => EnableCanvasBlocker(false));
        ZoomOut.AppendCallback(() => EnableImageItemOverriding(false));
        ZoomOut.AppendCallback(() => EnableGraphicRaycasterItemImage(true));
        ZoomOut.AppendCallback(() => EnableButtonImageItem(true));
        ZoomOut.AppendCallback(() => EnableImageRaycastTarget(true));
        ZoomOut.AppendCallback(() => SetIsZoomIn(false));
        ZoomOut.Play();
    }

    public void SetIsZoomIn(bool _enable) {
        isZoomIn = _enable;
    }

    public void EnableCanvasBlocker(bool _enable) {
        BlockerInteractionComponent.SetActive(_enable);
    }

    public void ChangeImageItemSortingOrder(int _order) {
        itemImageCanvasComponent.sortingOrder = _order;
    }

    public void EnableImageItemOverriding(bool _enable) {
        itemImageCanvasComponent.overrideSorting = _enable;
    }

    public void EnableGraphicRaycasterItemImage(bool _enable) {
        itemImageGraphicRaycasterComp.enabled = _enable;
    }

    public void EnableButtonImageItem(bool _enable) {
        itemImageButtonComponent.interactable = _enable;
    }

    public void EnableImageRaycastTarget(bool _enable) {
        itemImageComponent.raycastTarget = _enable;
    }

    public void UpdateText() {
        //textComponent.SetText(item.restOfWord);
        Sequence changeText = DOTween.Sequence();
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 0, .5f));
        changeText.AppendCallback(() => textComponent.SetText(item.restOfWord));
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 100, 1f));
        changeText.Play();
    }

    public Sequence UpdateTextSequence() {
        //textComponent.SetText(item.restOfWord);
        Sequence changeText = DOTween.Sequence();
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 0, .5f));
        changeText.AppendCallback(() => textComponent.SetText(item.restOfWord));
        changeText.Append(DOTween.To(() => textComponent.maxVisibleCharacters, x => textComponent.maxVisibleCharacters = x, 100, 1f));
        return changeText;
        //changeText.Play();
    }

    public void OnDrop(PointerEventData e) {
        droppedItem = tempDrag;
        tempDrag = null;
        runningDropAction = true;
        if (currentItem == null) {
            if (droppedItem != null) {
                //currentItem = droppedItem;
                Debug.Log("Item Dropped! " + e.pointerDrag.name);
            }
            manager.ManagerSound.startSoundFX(manager.SoundClips[3]);
            SetDragItemInfo(droppedItem, this);
            manager.VerificationToRelease();
            /*droppedItem.TransformComponent.SetParent(parentDropper);
            droppedItem.RectTransformComponent.SetPivot(PivotPresets.MiddleRight);
            droppedItem.RectTransformComponent.SetAnchor(AnchorPresets.MiddleRight, 0, 0);
            droppedItem.TransformComponent.localPosition = Vector3.right;
            droppedItem.RectTransformComponent.offsetMax = new Vector2(0f, droppedItem.RectTransformComponent.offsetMax.y);
            droppedItem.hasBeenDrop = true;
            droppedItem.droppedArea = this;*/

        } else {
            if (droppedItem != null) {
                droppedItem.hasValidDrop = false;
                droppedItem.hasBeenDrop = false;
                if(droppedItem.droppedArea != null) {
                    droppedItem.droppedArea.currentItem = null;
                    droppedItem.droppedArea.droppedItem = null;
                }
                droppedItem.EndDrag();
                Debug.Log("Reset all!");
            }
        }


        runningDropAction = false;
    }
    
    public void SetDragItemInfo(ItemDraggable_EF02LP33 _itemDrag, DropArea_EF02LP33 _drop) {        
        _drop.currentItem = _itemDrag;
        _itemDrag.hasBeenDrop = true;
        if(_itemDrag.droppedArea != null) {
            _itemDrag.droppedArea.currentItem = null;
        }
        _itemDrag.TransformComponent.SetParent(_drop.parentDropper);
        _itemDrag.RectTransformComponent.SetPivot(PivotPresets.MiddleRight);
        _itemDrag.RectTransformComponent.SetAnchor(AnchorPresets.MiddleRight, 0, 0);
        _itemDrag.TransformComponent.localPosition = Vector3.right;
        _itemDrag.RectTransformComponent.offsetMax = new Vector2(0f, _itemDrag.RectTransformComponent.offsetMax.y);        
        _itemDrag.droppedArea = _drop;
        _drop.droppedItem = null;
    }

    public void OnPointerEnter(PointerEventData e) {

        if (e.pointerDrag != null) {
            tempDrag = e.pointerDrag.GetComponent<ItemDraggable_EF02LP33>();
        }

        if (tempDrag != null) {
            tempDrag.hasValidDrop = true;
            
        }       

    }

    public Tweener ShowTicker(bool isRight) {
        TickImageComponent.sprite = isRight ? manager.CorrectTick : manager.WrongTick;
        return TickImageComponent.DOFade(1f, .3f);
    }

    public void HideTicker() {
        TickImageComponent.DOFade(0f, .3f);
    }

    public void OnPointerExit(PointerEventData e) {
        if(tempDrag != null) {            
            tempDrag.hasValidDrop = false;
            tempDrag = null;
            Debug.Log("Exiting Possible Drop!");
        }

        
    }    

}
