using UnityEngine;

public class ControlEfetcPe : MonoBehaviour {
    public GameObject partivulaPe;
    GameObject partivulaPeR;

    void Start () {
        partivulaPeR =  Instantiate(partivulaPe, transform.position, Quaternion.identity) as GameObject;
        partivulaPeR.transform.SetParent(this.transform);
        partivulaPeR.transform.localScale = new Vector3(partivulaPe.transform.localScale.x, partivulaPe.transform.localScale.y, partivulaPe.transform.localScale.z);
        partivulaPeR.SetActive(false);
    }
	
	// Update is called once per frame
	public void OnPe () {
        if (partivulaPeR != null) {
            this.partivulaPeR.SetActive(true);
        }
    }
    public void OffPe()
    {
        if (partivulaPeR != null) {
            this.partivulaPeR.SetActive(false);
        }
    }
}
