using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBlocos : MonoBehaviour {
    int x;

    void Start () {
        do {
            x = Random.Range(0, 300);
            Debug.Log("pass");
        } while (x % 2 == 0);
        Debug.Log(x);
	}
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(KeyCode.Space)) {
            do {
                x = Random.Range(0, 300);
                Debug.Log("pass");
            } while (x % 2 == 0);
            Debug.Log(x);
        }

    }
}
