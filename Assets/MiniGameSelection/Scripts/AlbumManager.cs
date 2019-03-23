using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlbumManager : OverridableMonoBehaviour {

    public Canvas albumCanvas;
    public Canvas mainCanvas;
    public BookPro bookProComponent;
    public StoreData storeData;
    public Inventory inventory;

    public List<int> hasCardCollection = new List<int>();
    public int itemCategoryId;
    public List<AlbumCard> albumCards = new List<AlbumCard>();
    public AutoFlip autoFlipController;

    //buttons References.
    public List<GameObject> buttonsZoomIn = new List<GameObject>();
    //int buttonsZoomInCount;

    //Zoom Transform and Pos;
    public RectTransform bookRectTransform;
    public Vector2 inicialBookPosMax;
    public Vector2 inicialBookPosMin;
    public Vector2 zoom2xBookPosL;
    public Vector2 zoom2xBookPosMinL;
    public Vector2 zoom2xBookPosR;
    public Vector2 zoom2xBookPosMinR;
    public Vector2 startPosition;
    public Vector2 startLocalPosition;
    public Vector2 startPositionParent;

    //rectScroll
    public ScrollRect scrollrectController;

    //scrollRectParent
    public RectTransform scrollRectTransform;
    public RectTransform bookBorderRectTransform;

    public GameObject rightSpotObject;
    public GameObject leftSpotObject;
    public GameObject rightButtonZoomOut;
    public GameObject leftButtonZoomOut;

    public bool onZoom;

    public bool alreadyLoaded = false;

    public void Start() {
        //buttonsZoomInCount = buttonsZoomIn.Count;
        startPosition = bookRectTransform.position;
        startLocalPosition = bookRectTransform.localPosition;
        startPositionParent = scrollRectTransform.position;
        inventory.LoadRecentItens();
        onZoom = false;

    }

    public void UpdateRecentCardsOnFlip() {
        int tempCount = albumCards.Count;
        for (int i = 0; i < tempCount; i++) {
            if(albumCards[i].isRecent) {
                albumCards[i].isRecent = false;
                inventory.RemoveRecentItem(albumCards[i].id);
                albumCards[i].ActiveRecentIcon(albumCards[i].isRecent);
            }
        }
    }

    /// <summary>
    /// Ativa ou Desativa todos os botões de zoomIn.
    /// </summary>
    /// <param name="_enable">True ou False</param>
    public void SetActiveButtonsZoomIn(bool _enable) {
        int buttonsZoomInCount = buttonsZoomIn.Count;
        for (int i = 0; i < buttonsZoomInCount; i++) {
            buttonsZoomIn[i].SetActive(_enable);
        }
    }

    /// <summary>
    /// Ativa o Zoom 2x na pagina esquerda do livro.
    /// </summary>
    public void Zoom2XLeftPage() {
        scrollrectController.enabled = true;
        bookProComponent.interactable = false;
        Sequence transitionToZoomR = DOTween.Sequence();
        transitionToZoomR.Append(bookRectTransform.DOScale(new Vector3(2f, 2f, 2f), 1f)).
        Join(bookRectTransform.DOMoveX(zoom2xBookPosL.x, 1f)).
        Join(bookBorderRectTransform.DOMoveX(zoom2xBookPosL.x, 1f)).
        Join(bookBorderRectTransform.DOScale(Vector2.one * 2f, 1f));
        SetActiveButtonsZoomIn(false);
        rightSpotObject.SetActive(false);
        leftButtonZoomOut.SetActive(true);
        leftSpotObject.SetActive(false);
        transitionToZoomR.Play();
        onZoom = true;
    }

    /// <summary>
    /// Ativa o Zoom 2x na pagina direita do livro.
    /// </summary>
    public void Zoom2XRightPage() {
        scrollrectController.enabled = true;
        bookProComponent.interactable = false;
        Sequence transitionToZoomL = DOTween.Sequence();
        transitionToZoomL.Append(bookRectTransform.DOScale(new Vector3(2f, 2f, 2f), 1f)).
        Join(bookRectTransform.DOMoveX(zoom2xBookPosR.x, 1f)).
        Join(bookBorderRectTransform.DOMoveX(zoom2xBookPosR.x, 1f)).
        Join(bookBorderRectTransform.DOScale(Vector2.one * 2f, 1f));
        SetActiveButtonsZoomIn(false);
        rightSpotObject.SetActive(false);
        rightButtonZoomOut.SetActive(true);
        leftSpotObject.SetActive(false);
        transitionToZoomL.Play();
        onZoom = true;
    }

    /// <summary>
    /// Reseta a visualização removendo o zoom do livro.
    /// </summary>
    public void ResetViewToDefault() {
        scrollrectController.enabled = false;
        bookProComponent.interactable = true;
        Sequence transitionToZoomReset = DOTween.Sequence();
        transitionToZoomReset.Append(bookRectTransform.DOScale(new Vector3(1f, 1f, 1f), 1f)).
        Join(bookBorderRectTransform.DOScale(Vector2.one, 1f)).
        Join(bookRectTransform.DOMoveX(0f, 1f)).
        Join(bookRectTransform.DOMoveY(startPosition.y, 1f)).
        Join(bookBorderRectTransform.DOMoveX(0f, 1f)).
        Join(bookRectTransform.DOLocalMoveY(startLocalPosition.y, 1f)).
        Join(scrollRectTransform.DOMoveY(startPositionParent.y, 1f));
        SetActiveButtonsZoomIn(true);
        rightSpotObject.SetActive(true);
        rightButtonZoomOut.SetActive(false);
        leftButtonZoomOut.SetActive(false);
        leftSpotObject.SetActive(true);
        transitionToZoomReset.Play();
        onZoom = false;
    }

    /// <summary>
    /// Pegar a carta certa do album usando apenas o ID do item.
    /// </summary>
    /// <param name="id"></param>
    public AlbumCard GetAlbumCard(int id) {
        return albumCards.Find(card => card.id == id);
    }

    /// <summary>
    /// Verificação de item foi comprado ou não.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool hasItemOnInventory(int id) {
        return hasCardCollection.Contains(id);
    }

    /// <summary>
    /// Configura sistema da loja para atualizar itens comprados ou não de acordo com o usuario.
    /// </summary>
    public void LoadAlbumCards() {

        onZoom = false;
        hasCardCollection = storeData.buyedItens;
        int countTemp = albumCards.Count;
        inventory.LoadRecentItens();
        for (int i = 0; i < countTemp; i++) {
            //AlbumCard card = albumCards[i];
            
            albumCards[i].storeItem = storeData.GetItem(albumCards[i].id);
            if (!albumCards[i].imageAlreadyLoaded) {
                Texture2D texture = storeData.LoadPNGbyID(albumCards[i].id);
                albumCards[i].defaultSprite = Sprite.Create(texture,new Rect(Vector2.zero,new Vector2(texture.width,texture.height)),Vector2.one * 0.5f,100,0,SpriteMeshType.FullRect);
                albumCards[i].imageAlreadyLoaded = true;
            }
            //albumCards[i].defaultSprite
            albumCards[i].isRecent = inventory.isItemRecent(albumCards[i].id);
            albumCards[i].ActiveRecentIcon(albumCards[i].isRecent);
            if (hasItemOnInventory(albumCards[i].id)) {               
                albumCards[i].SetCardStatus(true);
            } else {
                albumCards[i].SetCardStatus(false);
            }
        }
       
    }

    /// <summary>
    /// Abre a tela do album sem animação.
    /// </summary>
    public void OpenAlbum_noAnimation() {
        albumCanvas.enabled = true;
        mainCanvas.enabled = false;       
        LoadAlbumCards();
    }

    /// <summary>
    /// Fecha a tela do album sem animação.
    /// </summary>
    public void CloseAlbum_noAnimation() {
        UpdateRecentCardsOnFlip();
        mainCanvas.enabled = true;
        albumCanvas.enabled = false;
    }

    public void ZoomLeftPageEvent() {
        if (onZoom) {
            ResetViewToDefault();
        } else {
            Zoom2XLeftPage();
        }
    }

    public void ZoomRightPageEvent() {
        if (onZoom) {
            ResetViewToDefault();
        } else {
            Zoom2XRightPage();
        }
    }
}
