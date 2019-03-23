using UnityEngine;
using UnityEngine.Events;

public class ControlPersonPesca : MonoBehaviour {

    public UnityEvent Jogarlinha;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pescar() {
        Jogarlinha.Invoke();

    }
}
