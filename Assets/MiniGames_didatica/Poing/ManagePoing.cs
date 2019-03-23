

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagePoing : MonoBehaviour {

    public GameObject barraObj;
    float smooth = 5.0f;
    Rigidbody2D barraRig;
    public float range;
    public float vel;
    public GameObject bolaObj;
    public Transform localIns;
    public Transform localInsBola;
    float h;
    public bool chekcBt;
    void Start () {
        barraRig = barraObj.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKeyDown(KeyCode.Space)) {
            //InstBola();
        }
        if (!Input.GetButton("Horizontal")) {
            chekcBt = true;
            h = Input.GetAxis("Horizontal");
            vel = h * range;
            //barraRig.velocity = new Vector2(vel, 0);
            // barraRig.MoveRotation(barraRig.rotation + vel * Time.fixedDeltaTime);
            Quaternion target = Quaternion.Euler(0, 0, -vel);
            barraObj.transform.rotation = Quaternion.Slerp(barraObj.transform.rotation, target, Time.deltaTime * smooth);

        } 
        
        else {
            chekcBt = false;
            //Debug.Log(vel);
        }
    }

    public void DirecaoBarra() {
        //  barraRig.velocity = new Vector2(vel, 0); 
        //  barraRig.MoveRotation(barraRig.rotation + vel * Time.fixedDeltaTime);
      
       

    }


    public void InstBola() {       
        localIns.DOPunchPosition(new Vector3(-0.15f, 0.15f, 0), 0.5f, 2, 1f, false);
        GameObject clone = Instantiate(bolaObj, new Vector3(localInsBola.position.x, localInsBola.position.y+1, localInsBola.position.z), localInsBola.rotation) as GameObject;
       /// clone.GetComponent<Rigidbody2D>().AddForce(transform.forward * 800);
      
    }


    void VoltarPosInst() {
        localIns.DOMove(new Vector3(localIns.position.x + 0.5f, localIns.position.y - 0.5f, localIns.position.z), 0.25f, false);
    }

}
