using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using MEC;
using DG.Tweening;
using UnityEngine.Rendering;

public class RecipineteControl :MonoBehaviour {

    public int numbRecp;
    public SpriteRenderer[] corCano;
    Rigidbody2D rigRep;
    public float velX;
    public float velY;
  
    public ControlTanqueCheio ControlTanqueCheio2;
    bool pass1;
    void Start() {
        rigRep = GetComponent<Rigidbody2D>();
        if (numbRecp==0) {
            velX = velX * -1;
            //velY = velY * -1;
        }

    }

    // Update is called once per frame
    void Update() {

       
       if (this.transform.localPosition.x > 7f) {
          //  transform.localPosition = new Vector2(transform.localPosition.x, 4.5f);
            velY = velY * -1;
            this.rigRep.velocity = new Vector2(this.velX, this.rigRep.velocity.y);    
           
        }
        else if (this.transform.localPosition.x < -7f) {
            //  transform.localPosition = new Vector2(transform.localPosition.x, 4.5f);
            velY = velY * -1;
            this.rigRep.velocity = new Vector2(this.velX, this.rigRep.velocity.y);
           
        }
     

    }
    public void animRecp1() {
        gameObject.transform.DOPunchPosition(new Vector3(transform.localPosition.x, 8.5f, transform.localPosition.z), 1f, 3, 2, false);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
     
        if (collision.gameObject.CompareTag("Dente") || collision.gameObject.CompareTag("Ground")) {
          //  transform.localPosition = new Vector2(transform.localPosition.x, 4.5f);
            MudaDir();
        }
        

    }

    public void MudaDir(){
     
            velX = velX * -1;
            rigRep.velocity = new Vector2(velX, rigRep.velocity.y);
       
    }
    

}
