using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class AnswerBQ {

    [BoxGroup("Item")]
    public Sprite spriteAnswer;
    [HideInInspector]
    public bool showBoolProperty = true;
    [BoxGroup("Item")]
    [ShowIf("showBoolProperty", true,true)]
    public bool isCorrect;
    	
}
