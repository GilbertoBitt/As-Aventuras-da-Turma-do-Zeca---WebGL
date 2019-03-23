using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using MEC;

public class ControlLeiteMaterno : MonoBehaviour {

    public Manager1_3A Manager1_3AR;
    Collider2D colliderBox;
    public Vector2 posLeite;
    public Vector2 posPlayer;
    public AreaEffector2D areaEffector;
    public NurseFollow1_3A NurseFollow1_3A2;



    void Start () {
      
        this.Manager1_3AR = Camera.main.GetComponent<Manager1_3A>();
        this.NurseFollow1_3A2 = Manager1_3AR.enfermeiraG.GetComponent<NurseFollow1_3A>();
        colliderBox = this.GetComponent<Collider2D>();
        areaEffector = this.GetComponent<AreaEffector2D>();
        areaEffector.enabled = false;

    }


    private void Update() {
        if (Manager1_3AR.nurseManager.hasCollide==true || NurseFollow1_3A2.endGameP == true) {
            this.colliderBox.enabled = false;
           // Manager1_3AR.checkPiscar = true;
            StopCoroutine(TimerVolta());
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("PlayerPe") == true && Manager1_3AR.playerRigidbody.velocity.y <= 0 && !Manager1_3AR.plataformController2d.isFlying) {
            Timing.KillCoroutines();

            areaEffector.enabled = true;
            Manager1_3AR.plataformController2d.checkDeslizando = true;
            Manager1_3AR.animPerson.SetInteger("NumCaindo", 1);
            Manager1_3AR.animPerson.SetBool("DelizandoLeite", true);
            Manager1_3AR.ButtonsEnable(false);
            Manager1_3AR.deslizandoLeite = true;



        }
            /*
            if (this.transform.position.y > other.transform.position.y) {
                areaEffector.enabled = false;
                Debug.Log("em baixo do leite");
            } else {
                Manager1_3AR.plataformController2d.checkDeslizando = true;
                Manager1_3AR.animPerson.SetInteger("NumCaindo", 1);
                Manager1_3AR.animPerson.SetBool("DelizandoLeite", true);
                // Debug.Log("Ecorrengando");
                Manager1_3AR.ButtonsEnable(false);
                areaEffector.enabled = true;
            }

       
        }
        */

        /*
        if (this.transform.position.y > other.transform.position.y) {
                areaEffector.enabled = false;
                Debug.Log("em baixo do leite");
            } else {
                Debug.Log("em cima do leite");
                Manager1_3AR.animPerson.SetInteger("NumCaindo", 1);
                // Debug.Log("Ecorrengando");
                Manager1_3AR.ButtonsEnable(false);
                areaEffector.enabled = true;
            }

             ContactPoint2D[] contatscs = new ContactPoint2D[10];

             other.GetContacts(contatscs);


             Vector3 hit = contatscs[0].normal;
             Debug.Log(hit);
             float angle = Vector3.Angle(hit, Vector3.up);

             if (Mathf.Approximately(angle, 0) || Mathf.Approximately(angle, 5) || Mathf.Approximately(angle, -5)) {
                 //Down
                 Manager1_3AR.animPerson.SetInteger("NumCaindo", 1);
                 Manager1_3AR.ButtonsEnable(false);
                 areaEffector.enabled = true;
                 Debug.Log("baixo");
             }
             if (Mathf.Approximately(angle, 180)) {
                 //Up               
                 // Debug.Log("Ecorrengando");

                 areaEffector.enabled = false;
                 Debug.Log("cima");
             }
             /*if (Mathf.Approximately(angle, 90)) {
                 // Sides
                 Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                 if (cross.y > 0) { // left side of the player
                     areaEffector.enabled = false;
                     Debug.Log("Esquerda");
                 } else { // right side of the player
                     Debug.Log("Direita");
                     areaEffector.enabled = false;
                 }
             }*/


    }

    void OnTriggerExit2D(Collider2D other)
    {
        Timing.KillCoroutines("seringaHIT");
        if (other.CompareTag("PlayerPe") == true && Manager1_3AR.jumpButton.interactable == false) {
          
          //  Debug.Log("Caindo");
           StartCoroutine(TimerVolta());
         
        }
      
    }
    IEnumerator TimerVolta()
    {
        colliderBox.enabled = false;
        Manager1_3AR.checkPiscar = true;
        Manager1_3AR.imgPersonAnimP.enabled = true;
        yield return new WaitForSeconds(0.5f);
       // colliderBox.enabled = false;
        Manager1_3AR.numCaindo = 2;
        Manager1_3AR.StopRunning();      
        Manager1_3AR.animPerson.SetBool("DelizandoLeite", false);
        Manager1_3AR.animPerson.SetInteger("NumCaindo", 1);
        yield return new WaitForSeconds(1f);
        Manager1_3AR.animPerson.SetBool("DelizandoLeite", false);
        colliderBox.enabled = false;
        CrossPlatformInputManager.SetAxisZero("Vertical");
        yield return new WaitForSeconds(1f);
        Manager1_3AR.animPerson.SetBool("DelizandoLeite", false);
        //  Manager1_3AR.BackRunning();
        //Manager1_3AR.ButtonsEnable(true);

        Manager1_3AR.checkPiscar = false;
    }



}
