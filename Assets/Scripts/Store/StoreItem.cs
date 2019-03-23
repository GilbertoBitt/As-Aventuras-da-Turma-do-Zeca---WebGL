using UnityEngine;

[System.Serializable]
public class StoreItem {

    public string itemName;
    public int itemBuyValue;
    public Texture2D itemIcon;
    public int itemCategoryID;
    public int itemID;

    public enum Type {
        none,
        collectibles,
    }


	
}
