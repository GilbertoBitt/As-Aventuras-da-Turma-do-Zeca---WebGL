
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaInst : MonoBehaviour {

    Rigidbody2D rigBola;
    public float forcB;
	void Start () {
        rigBola = GetComponent<Rigidbody2D>();
        rigBola.AddForce(transform.up * 100 * forcB);
      // Debug.Log("1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}


   
    private void OnCollisionEnter2D(Collision2D collision) {
       // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name=="1") {
           // Debug.Log("1");
        }
        if (collision.gameObject.name == "2") {
            //
          //  Debug.Log("2");
        }

    }
}
