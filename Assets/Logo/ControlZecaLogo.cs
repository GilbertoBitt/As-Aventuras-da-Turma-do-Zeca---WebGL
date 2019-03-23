using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ControlZecaLogo : MonoBehaviour {

    Rigidbody2D personRig;
    UnityStandardAssets._2D.PlatformerCharacter2D m_Character;
    UnityStandardAssets._2D.Platformer2DUserControl plataformControlerUser;

    bool pausevel;

    public EdgeCollider2D[] line;

    bool pulo;
    int numb;
    public float forcaPulo;


   

    public bool checkCorr;
    bool checkpass;
    void Start () {
        personRig = GetComponent<Rigidbody2D>();
        m_Character = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        plataformControlerUser = GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();
        // personRig.DOJump(endValue, jumpPower, numJumps, duration, snapping);


    }
    void pauseoff() {
        pausevel = false;
       // plataformControlerUser.m_Jump = true;
    }

    void pulov() {
      
        //Invoke("pulov", .5f);
        if (line[7].enabled == false) {
            pausevel = false;
        } else {
            pausevel = false;
            plataformControlerUser.m_Jump = true;
           
        }
        checkpass = false;

    }
    // Update is called once per frame
    void Update () {
        if (!pausevel) {
           CrossPlatformInputManager.SetAxisNegative("Horizontal");
              m_Character.ThisUpdate();
          
        }
       
       
        //personRig.velocity = new Vector2(10, 0);
    }

  
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.name == "t0") {
            m_Character.m_JumpForce = 150f* forcaPulo;
            plataformControlerUser.m_Jump = true;
            
        }        
        else if (collision.gameObject.name == "t1" && !checkpass) {
            checkpass = true;
            line[1].enabled = false;         
            m_Character.m_JumpForce = 100f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
          
            Invoke("pulov",.5f);
        }
        else if (collision.gameObject.name == "t2" && !checkpass) {
            checkpass = true;
            line[2].enabled = false;
            m_Character.m_JumpForce = 100f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
           // Timing.RunCoroutine(TamanhoTime(0.5f, 1));
            Invoke("pulov", .5f);
        }
        else if (collision.gameObject.name == "t3") {
            line[3].enabled = false;
            m_Character.m_JumpForce = 200f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
           Invoke("pulov", .5f);
        }
        else if (collision.gameObject.name == "t4") {
            line[4].enabled = false;
            m_Character.m_JumpForce = 50f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
            Invoke("pulov", .8f);
        }
        else if (collision.gameObject.name == "t5") {
            line[5].enabled = false;
            m_Character.m_JumpForce = 100f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
            Invoke("pulov", .5f);
        }
        else if (collision.gameObject.name == "t6") {
            line[6].enabled = false;
            m_Character.m_JumpForce = 100f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
            Invoke("pulov", .5f);
        }
        else if (collision.gameObject.name == "t7") {
            line[7].enabled = false;
            m_Character.m_JumpForce = 100f * forcaPulo;
            pausevel = true;
            plataformControlerUser.m_Jump = false;
            CrossPlatformInputManager.SetAxisZero("Horizontal");
            m_Character.ThisUpdate();
             Invoke("pulov", .5f);
          
        }

    }

   


}
