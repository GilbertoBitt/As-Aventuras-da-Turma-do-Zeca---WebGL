using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
[CreateAssetMenu(fileName = "Item_AumDimi", menuName = "others/Item_AumDimi")]
public class Item_AumDimi :ScriptableObject {

    [BoxGroup("ItemInfo", false, false)]
    public string Ini;
    [BoxGroup("ItemInfo", false, false)]
    public string dimitutivo;
    [BoxGroup("ItemInfo", false, false)]
    public string normal;
    [BoxGroup("ItemInfo", false, false)]
    public string aumentativo;
    [BoxGroup("ItemInfo", false, false)]
    public string dimiResposta;
    [BoxGroup("ItemInfo", false, false)]
    public string normalResposta;
    [BoxGroup("ItemInfo", false, false)]
    public string aumeResposta;


    public void OnValidate() {
        dimiResposta = Ini + dimitutivo;
        normalResposta = Ini + normal;
        aumeResposta = Ini + aumentativo;
    }

}

