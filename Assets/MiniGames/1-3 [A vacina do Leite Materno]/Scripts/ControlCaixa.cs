using UnityEngine;


public class ControlCaixa : OverridableMonoBehaviour {

    // Use this for initialization


    public Manager1_3A manager;
    BoxCollider2D colliderBox;
    bool checkExit;
    public int nunbOBJ;
    public bool caixa;
    int carrinhoNumb;
    public int numbCont;
    public PresetPlataforms1_3 PresetPlataforms1_3s;
    public NurseFollow1_3A NurseFollow1_3A2;
    void Start () {
        this.manager = Camera.main.GetComponent<Manager1_3A>();
        this.NurseFollow1_3A2 = manager.enfermeiraG.GetComponent<NurseFollow1_3A>();
        PresetPlataforms1_3s = this.transform.parent.GetComponentInParent<PresetPlataforms1_3>();
        colliderBox = this.GetComponent<BoxCollider2D>();
        numbCont = manager.numbCont;
        if (caixa==false) {
            if (PresetPlataforms1_3s.numbPlataform <= 18 && this.nunbOBJ == 1) {
               // Destroy(gameObject);
                gameObject.SetActive(true);
            }

            if (PresetPlataforms1_3s.numbPlataform <= 18 && this.nunbOBJ == 2) {
                // Destroy(gameObject);
                Destroy(this.GetComponent<Collider2D>());
                Destroy(this.GetComponent<SpriteRenderer>());
                this.gameObject.SetActive(false);
            }
            if (PresetPlataforms1_3s.numbPlataform > 18 && this.nunbOBJ == 2) {
              
                gameObject.SetActive(true);
            }
            if (PresetPlataforms1_3s.numbPlataform > 18 && nunbOBJ == 1) {
               // Destroy(gameObject);
                Destroy(this.GetComponent<Collider2D>());
                Destroy(this.GetComponent<SpriteRenderer>());
                this.gameObject.SetActive(false);
            }
               
        }
    }
	
	

    void StopPiscar() {
        manager.checkPiscar = false;
    }
    void EnableIMG() {
        manager.imgPersonAnimP.enabled = true;
        manager.imgPersonAnimP.SetBool("StopPiscar", false);
        Invoke("EnableIMGOff", 0.5f);
    }
    void EnableIMGOff() {
        manager.imgPersonAnimP.enabled = false;
    }


    public override void UpdateMe()
    {
        if (manager.nurseManager.hasCollide == true && NurseFollow1_3A2.endGameP == true)
        {
            if(this.colliderBox != null)
            this.colliderBox.enabled = false;
            // Manager1_3AR.checkPiscar = true;
            // StopCoroutine(TimerVolta());


        }
    }

    /*private void Update() {
        
    }*/

    private void OnCollisionEnter2D(Collision2D collision) {
  
    
        if (collision.gameObject.CompareTag("Player") == true) {
            Vector3 hit = new Vector3();

            if (collision.contacts.Length >= 1) {
                hit = collision.contacts[0].normal;
            }

            //Debug.Log(hit);
            
            float angle = Vector3.Angle(hit, Vector3.up);
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
          
            if (Mathf.Approximately(angle, 90) && cross.y > 0) {
                
                manager.batendoCaixa = true;
                Debug.Log("batendoCaixa [" + manager.batendoCaixa + "]");
                manager.HitSoundEffect();
                if (manager.plataformController2d.isFlying) {
                    manager.StopFlyCollision();
                }
                // colliderBox.isTrigger = true;
                colliderBox.isTrigger = true;                
                if (manager.batendoCaixa == true) {
                    manager.animPerson.SetBool("BatendoCaixa", true);
                    manager.plataformController2d.collVoando.enabled = false;
                    manager.batendoCaixa = true;
                    Debug.Log("batendoCaixa 2 [" + manager.batendoCaixa + "]");
                }                
                manager.StopRunning();
                manager.ButtonsEnable(false);
                manager.imgPersonAnimP.enabled = true;
                manager.checkPiscar = true;      
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") == true) {
           // Debug.Log("OnTriggerExit2D");
            //StopPiscar();
            //colliderBox.isTrigger = false;
            checkExit = true;
            manager.checkPiscar = false;
            Invoke("StopPiscar", 0.25f);
        }
      
    }
    void TimerVoltaBt()
    {
        manager.ButtonsEnable(true);
    }

    void TimerVolta() {
        //manager.batendoCaixa = false;
        manager.BackRunning();
    }


}
