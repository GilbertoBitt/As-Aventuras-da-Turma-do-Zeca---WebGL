using UnityEngine;

public class ControlInstPassaro : MonoBehaviour {

	public GameObject passaroG;
	public Manager_1_2A Manager_1_2ALocal;
	public RectTransform[] posIniM;
	public RectTransform[] posPousarM;
	public RectTransform[] posFimC;
	public int numbPass;
	public int limPass;
	float timeM;
	public float[] timeMy;
	int numTemp;
	public float tamPass;
	
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (Manager_1_2ALocal.destroyPref);

		if (Time.realtimeSinceStartup - timeM > timeMy[numTemp] && numbPass<limPass && Manager_1_2ALocal.destroyPref==false) {
		//	Debug.Log ("dfdsf");
			numTemp = Random.Range (0,timeMy.Length);	
			GameObject clone = Instantiate(passaroG, transform.position, Quaternion.identity) as GameObject;
			numbPass = numbPass + 1;
			clone.GetComponent<ControlPassaro>().ControlInstPassaro = GetComponent<ControlInstPassaro>();
			clone.transform.SetParent(this.transform);
			timeM = Time.realtimeSinceStartup;
		}
	}
		
}