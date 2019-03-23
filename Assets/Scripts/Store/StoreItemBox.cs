using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemBox : MonoBehaviour {

    public StoreItem item;
    public StoreData storeData;
    public StoreManager storeManager;
    public TextMeshProUGUI textItemName;
    public RawImage imageItemIcon;
    public TextMeshProUGUI textItemValue;
    public Image bropsIcon;
    public bool buyed = false;
    public Image[] ImagesComponents;
    public Image itemValueBox;
    public Color buyedItemColor;

    public void OnClickEvent() {
        if (storeManager.CanBuyItem(item.itemBuyValue) && !buyed) {
            storeManager.OpenConfirmWindow(item, this);
        } else if(buyed) {
            storeManager.OpenInfoWindow(item);
        }
    }

    public void UpdateItem(StoreItem _item) {
        item = _item;
        textItemName.text = _item.itemName;
        imageItemIcon.texture = _item.itemIcon;
        imageItemIcon.color = Color.white;
        
        StringBuilder stringBuild = new StringBuilder();

        bool hasItem = storeData.buyedItens.Contains(_item.itemID);

        if (hasItem) {
            //Deactivate Button/Gray Item/Value = buyed;.
            stringBuild.Append("Comprado");
            textItemValue.text = stringBuild.ToString();
            bropsIcon.enabled = false;
            ChangeMaterial(storeManager.defaultMaterial);
            itemValueBox.color = buyedItemColor;
            buyed = true;
        } else if (!hasItem && !storeManager.CanBuyItem(_item.itemBuyValue)) {
            stringBuild.Append(_item.itemBuyValue);
            textItemValue.text = stringBuild.ToString();
            bropsIcon.enabled = true;
            ChangeMaterial(storeManager.grayscaleMaterial);
            buyed = false;
        } else if(!hasItem && storeManager.CanBuyItem(_item.itemBuyValue)) {
            stringBuild.Append(_item.itemBuyValue);
            textItemValue.text = stringBuild.ToString();
            ChangeMaterial(storeManager.defaultMaterial);
            bropsIcon.enabled = true;
            buyed = false;
        }

    }

    public void ChangeMaterial(Material _material) {
        for (int i = 0; i < 3; i++) {
            ImagesComponents[i].material = _material;
        }
        imageItemIcon.material = _material;
    }
	
}
