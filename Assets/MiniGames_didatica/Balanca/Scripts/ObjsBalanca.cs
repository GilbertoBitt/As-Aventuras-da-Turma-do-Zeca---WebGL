using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
[CreateAssetMenu(fileName = "ObjsBalanca", menuName = "others/ObjsBalanca")]
public class ObjsBalanca :ScriptableObject {


    [BoxGroup("ItemInfo", false, false)]
    public Sprite objEMenor;
    [BoxGroup("ItemInfo", false, false)]
    public Sprite objEMaior;
   

    public void OnValidate() {

     //   objRMenor = objEMenor;
      //  objRMaior = objEMaior;
    }

}
