using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour { 

    public StoreData storeData;
    public GameConfig configGame;  
    public GameObject mainPanel;
    public Canvas thisPanel;
    public gameSelection gameSelectionScript;
    public Text bropsText;
    public Text buyConfirmText;
    public Text buyConfirmValueText;
    public RawImage buyConfirmSpriteIcon;
    public GameObject buyConfirmPanel;
    public StoreItem storeItemSeleced;
    public StoreItemBox[] storeItensBox = new StoreItemBox[10];
    public StoreItemBox selectedItemBox;
    public Material defaultMaterial;
    public Material grayscaleMaterial;
    private bool storeHasLoaded = false;
    [Header("Info Box Window")]
    public GameObject infoBoxWindow;
    public Text infoBoxItemName;
    public RawImage infoBoxIconItem;
    public GameObject itemBoxPrefab;
    public Transform parentListTransform;
    //private StringFast stringFast = new StringFast();
    public ScrollRect scrollRectStore;
    public RectTransform scrollRectTransform;
    public Text buildVersionText;
    public Text clientText;

    public void OpenStore() {
        //mainPanel.SetActive(false);
        gameSelectionScript.enabled = false;
        BuildStore();
        UpdateStore();
        thisPanel = this.GetComponent<Canvas>();
        Canvas.ForceUpdateCanvases();
        scrollRectStore.horizontalNormalizedPosition = 1f;
        scrollRectTransform.localPosition = new Vector3(1462.098f, 0f, 0f);
        Canvas.ForceUpdateCanvases();
    }

    public void BuildStore() {
        if (!storeHasLoaded) {
            int count = storeData.itensOnStore.Count;

            int sizeItens = 0;
            for (int i = 0; i < count; i++) {                
                if(storeData.itensOnStore[i].itemIcon != null) {
                    sizeItens++;
                }
            }

            storeItensBox = new StoreItemBox[sizeItens];
            for (int i = 0; i < sizeItens; i++) {
                GameObject storeBoxItem = Instantiate(itemBoxPrefab, parentListTransform) as GameObject;
                storeItensBox[i] = storeBoxItem.GetComponent<StoreItemBox>();
                storeItensBox[i].storeManager = this;
            }
            storeHasLoaded = true;
        }
    }

    public void OpenConfirmWindow(StoreItem _item, StoreItemBox _itemBox) {
        selectedItemBox = _itemBox;
        storeItemSeleced = _item;
        StringFast _string = new StringFast();
        _string.Append("Deseja comprar ").Append(_item.itemName).Append("?");
        buyConfirmText.text = _string.ToString();
        buyConfirmValueText.text = _item.itemBuyValue.ToString();
        buyConfirmSpriteIcon.texture = _item.itemIcon;
        buyConfirmPanel.SetActive(true);
    }

    public void OpenInfoWindow(StoreItem _item) {
        infoBoxWindow.SetActive(true);
        infoBoxIconItem.texture = _item.itemIcon;
        infoBoxItemName.text = _item.itemName;
    }

    public void CloseInfoWindow() {
        infoBoxWindow.SetActive(false);
    }

    public void CloseConfirmWindow() {
        buyConfirmPanel.SetActive(false);
    }

    public void BuyItemConfirmation() {
        configGame.BropsAmount -= storeItemSeleced.itemBuyValue;
        //configGame.BropsDeviceAmount -= storeItemSeleced.itemBuyValue;

        storeData.buyedItens.Add(storeItemSeleced.itemID);
        storeData.SaveItemInventory(storeItemSeleced);
        UpdateStore();
        CloseConfirmWindow();
    }

    public void CloseStoreItem() {
        gameSelectionScript.btsLogo.SetActive(true);
        //mainPanel.SetActive(true);
        //gameSelectionScript.enabled = true;
        thisPanel.enabled = false;
        this.enabled = false;
    }
    public void SelectGame() {
       // gameSelectionScript.btsLogo.SetActive(true);
        mainPanel.SetActive(true);
        gameSelectionScript.enabled = true;
        thisPanel.enabled = false;
        this.enabled = false;
    }

    public void OpenConfig() {
        buildVersionText.text =  " Versão do Jogo: " + Application.version;
        clientText.text = "Cliente: " + configGame.ClientName;
        mainPanel.SetActive(true);
        gameSelectionScript.enabled = true;
        gameSelectionScript.OpenConfig();
        thisPanel.enabled = false;
        this.enabled = false;
    }

    public void UpdateStore() {
        int count = storeData.itensOnStore.Count;

        int sizeItens = 0;
        for (int i = 0; i < count; i++) {
            if (storeData.itensOnStore[i].itemIcon != null) {
                sizeItens++;
            }
        }

        for (int i = 0; i < sizeItens; i++) {

            if (storeData.itensOnStore[i].itemIcon != null) {
                storeItensBox[i].UpdateItem(storeData.itensOnStore[i]);
            }
        }

        bropsText.text = configGame.BropsAmount.ToString();

        /*Canvas.ForceUpdateCanvases();
        //scrollRectStore.horizontalScrollbar.value = 1f;
        scrollRectStore.horizontalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();*/
    }

    public bool CanBuyItem(int value) {
        return configGame.BropsAmount >= value;
    }



}
