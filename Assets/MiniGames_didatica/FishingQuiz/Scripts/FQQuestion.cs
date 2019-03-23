using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "others/fishingQ")]
public class FQQuestion : SerializedScriptableObject {

    public string quetionText;
    public string[] correctOnes;
    public string[] wrongOnes;
    public int AnoLetivo;
	
    public bool ContainsCorrect(string _text) {
        
        int tempCount = correctOnes.Length;
        for (int i = 0; i < tempCount; i++) {
            if(correctOnes[i] == _text) {
                return true;
            }
        }

        return false;
    }

    public string ReturnRandomOne() {
        bool isCorrect = Random.Range(0, 2) == 1 ? true : false;
        if (isCorrect) {
            return correctOnes[Random.Range(0, correctOnes.Length)];
        } else {
            return wrongOnes[Random.Range(0, wrongOnes.Length)];
        }
    }

}
