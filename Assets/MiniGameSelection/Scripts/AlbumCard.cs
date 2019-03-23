using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

[System.Serializable]
public class AlbumCard : OverridableMonoBehaviour{

    public int id;
    public Image cardImageComponent;
    public Image newIconImageComponent;
    private GameObject newIconGameObject;
    public Sprite defaultSprite;
    public Sprite defaultSpriteEmpty;
    public StoreItem storeItem;
    public string itemNameText;
    public TextMeshProUGUI itemNameTextComponent;
    public int cardPage;    
    public bool imageAlreadyLoaded = false;
    public bool isRecent;
    public StoreData store;


    public void Start() {
        string parentName = this.transform.parent.name;
        parentName = parentName.Replace("Page","");
        cardPage = int.Parse(parentName);
        newIconGameObject = newIconImageComponent.gameObject;
    }

    /// <summary>
    /// Muda o status da carta.
    /// </summary>
    /// <param name="hasCard">
    /// true = o usuario ja tem a carta 
    /// false = o usuario não tem a carta.
    /// </param>
    public void SetCardStatus(bool hasCard) {
        if (storeItem != null) {
            itemNameText = storeItem.itemName;
        } else {
            itemNameText = "";
        }

        if (hasCard) {
            cardImageComponent.sprite = defaultSprite;            
            itemNameTextComponent.enabled = false;
        } else {
            cardImageComponent.sprite = defaultSpriteEmpty;
            itemNameTextComponent.text = itemNameText;
            itemNameTextComponent.enabled = true;
        }
    }

    private bool showCardEditor;
    [ButtonGroup("GroupCard")]
    [Button("Show Cards")]
    public void ShowCard() {        
        storeItem.itemIcon = store.LoadPNGbyID(id);
        defaultSprite = Sprite.Create(storeItem.itemIcon, new Rect(Vector2.zero, new Vector2(storeItem.itemIcon.width, storeItem.itemIcon.height)), Vector2.one * 0.5f, 100, 0, SpriteMeshType.FullRect);
        //storeItem = store.GetItem(id);
        SetCardStatus(true);


    }
    [ButtonGroup("GroupCard")]
    [Button("Hide Cards")]
    public void HideCard() {
        SetCardStatus(false);
    }

    public void ActiveRecentIcon(bool _enable) {
       if(newIconGameObject == null) {
            newIconGameObject = newIconImageComponent.gameObject;
       }
        newIconGameObject.SetActive(_enable);
    }

}
