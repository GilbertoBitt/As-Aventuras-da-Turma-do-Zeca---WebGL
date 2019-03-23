using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
[CreateAssetMenu(fileName = "newItemEF02LP33", menuName = "others/EF02LP33")]
public class Item_EF02LP33 : ScriptableObject {

    [BoxGroup("ItemInfo",false,false)]
    public string firstSyllable;
    [BoxGroup("ItemInfo", false, false)]
    public string restOfWord;
    [BoxGroup("ItemInfo", false, false)]
    [ReadOnly]
    public string entireWorld;

    public void OnValidate() {
        entireWorld = firstSyllable + restOfWord;
    }
	
}
