using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPostInst : MonoBehaviour {

    Rigidbody2D rigPos;
    public float veloInst;
   
    void Start () {
        rigPos = GetComponent<Rigidbody2D>();
        rigPos.velocity = new Vector2(veloInst, 0f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            veloInst = veloInst * -1;
            rigPos.velocity = new Vector2(veloInst, 0f);
        }
        
    }
}
