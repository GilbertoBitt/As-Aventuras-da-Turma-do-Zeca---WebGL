using UnityEngine;

[System.Serializable]
public class FQLineFish {

    public Transform rightPos;
    public Transform leftPos;
    public Vector3 rightWorldPos;
    public Vector3 leftWorldPos;
    public bool isLocked;

    public void UpdatePos() {
        if(rightPos != null) {
            rightWorldPos = rightPos.position;
        }

        if(leftPos != null) {
            leftWorldPos = leftPos.position;
        }
    }
    
}
