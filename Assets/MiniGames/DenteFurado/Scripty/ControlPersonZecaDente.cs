using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPersonZecaDente : MonoBehaviour {

    public bool checkTokInim;
    public ManageQuizDenteFurado manageQuizDenteFurado;
    public Animator animPerson;
    Rigidbody2D rigPerson;
    public GameObject partataque1;
    public GameObject[] partataque2;
    public GameObject[] partataque3;

    public GameObject partataque1TatiBia;
    public GameObject[] partataque2TatiBia;
    public GameObject[] partataque3TatiBia;

    public GameObject partataque1Paulo;
    public GameObject[] partataque2Paulo;
    public GameObject[] partataque3Paulo;

    public GameObject partataque1JoaoManu;
    public GameObject[] partataque2JoaoManu;
    public GameObject[] partataque3JoaoManu;
    public int numb;
    public string[] namePerson;


    private void Awake()
    {
        manageQuizDenteFurado = Camera.main.GetComponent<ManageQuizDenteFurado>();
        animPerson = GetComponent<Animator>();
    }

    void Start () {
       int person =  PlayerPrefs.GetInt("characterSelected", 0);
        //  numbPerson = PlayerPrefs.GetInt("characterSelected", 0);
        manageQuizDenteFurado = Camera.main.GetComponent<ManageQuizDenteFurado>();
        manageQuizDenteFurado.namePersonText.text = "" + namePerson[person];
        if (manageQuizDenteFurado.numbPerson == 1 || manageQuizDenteFurado.numbPerson == 5) {
            partataque1 = partataque1TatiBia;
            for (int i = 0; i < partataque2TatiBia.Length; i++) {
                partataque2[i] = partataque2TatiBia[i];
            }
            for (int i = 0; i < partataque3TatiBia.Length; i++) {
                partataque3[i] = partataque3TatiBia[i];
            }
        } else if (manageQuizDenteFurado.numbPerson == 2) {
            partataque1 = partataque1Paulo;
            for (int i = 0; i < partataque2Paulo.Length; i++) {
                partataque2[i] = partataque2Paulo[i];
            }
            for (int i = 0; i < partataque3Paulo.Length; i++) {
                partataque3[i] = partataque3Paulo[i];
            }
        } else if (manageQuizDenteFurado.numbPerson == 3 || manageQuizDenteFurado.numbPerson == 4) {
            partataque1 = partataque1JoaoManu;
            for (int i = 0; i < partataque2JoaoManu.Length; i++) {
                partataque2[i] = partataque2JoaoManu[i];
            }
            for (int i = 0; i < partataque3JoaoManu.Length; i++) {
                partataque3[i] = partataque3JoaoManu[i];
            }

        }
        animPerson = GetComponent<Animator>();
        rigPerson = GetComponent<Rigidbody2D>();
      
        
    }
	


    public void AtaquePerson() {
        manageQuizDenteFurado = Camera.main.GetComponent<ManageQuizDenteFurado>();
        animPerson = GetComponent<Animator>();
        manageQuizDenteFurado.personAtaq = true;
        numb = numb + 1;
       
        if (numb == 3 || numb == 0) {
       
            numb = 0;
            // manageQuizDenteFurado.colParede3.enabled = false;
            AndarPerson();
            animPerson.SetInteger("tipoAtaque", 0);
        }
       
        else if(numb == 1) {
            // animPerson.SetInteger("tipoAtaque", numb);
      
            manageQuizDenteFurado.colParede3.SetActive(true);

            animPerson.SetInteger("tipoAtaque", 0);
            AndarPerson();
          //  animPerson.SetInteger("tipoAtaque", numb);
            // manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", true);
            // PlayerAtaqq2();
        } else if (numb == 2) {
         
            // animPerson.SetInteger("tipoAtaque", numb);
            AndarPerson();
            //   animPerson.SetInteger("tipoAtaque", numb);
            manageQuizDenteFurado.colParede3.SetActive(true);
            animPerson.SetInteger("tipoAtaque", 0);
            //  manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", true);
            // PlayerAtaqq3();
        }
       
      
    }

    public void AtivaPartOn() {
        for (int i = 0; i < partataque2.Length; i++) {
            partataque2[i].SetActive(true);
        }
    }
    public void AtivaPartOn3() {
        for (int i = 0; i < partataque3.Length; i++) {
            partataque3[i].SetActive(true);
        }
       
    }
    public void AtivaPartOff() {
        for (int i = 0; i < partataque2.Length; i++) {
            partataque2[i].SetActive(false);
        }
        for (int i = 0; i < partataque3.Length; i++) {
            partataque3[i].SetActive(false);
        }
        animPerson.SetBool("AtaquePerson", false);
        animPerson.SetBool("DefPerson", false);
       

        manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", false);
      

    }
    void PlayerAtaqq3() {
        //  manageQuizDenteFurado.enemyAndar = false;
        rigPerson.velocity = new Vector2(0, 0);
        animPerson.SetFloat("Speed", 0);
        //manageQuizDenteFurado.enemyAndar = false;

        animPerson.SetInteger("tipoAtaque", numb);
        animPerson.SetBool("AtaquePerson", true);
 

        // Invoke("EnemyVoltar", 3f);
    }

    void PlayerAtaqq2() {
        //  manageQuizDenteFurado.enemyAndar = false;
        rigPerson.velocity = new Vector2(0, 0);
        animPerson.SetFloat("Speed", 0);
        //manageQuizDenteFurado.enemyAndar = false;

        animPerson.SetInteger("tipoAtaque", numb);
        animPerson.SetBool("AtaquePerson", true);
      

        // Invoke("EnemyVoltar", 3f);
    }

    void AndarPerson() {
        rigPerson = GetComponent<Rigidbody2D>();
        rigPerson.velocity = new Vector2(3,0);
        animPerson.SetFloat("Speed", Mathf.Abs(rigPerson.velocity.x));
        if (checkTokInim) {
            rigPerson.velocity = new Vector2(0, 0);
            animPerson.SetFloat("Speed",0);
            manageQuizDenteFurado.personAndar = false;
            animPerson.SetBool("AtaquePerson", true);
            Invoke("PersonVoltar",3);
           
        }        
    }

    void PersonVoltar() {
        manageQuizDenteFurado.colParede3.SetActive(false);
        AtivaPartOff();
        partataque1.SetActive(false);
        manageQuizDenteFurado.personVoltar = true;
        rigPerson.velocity = new Vector2(-3, 0);
        animPerson.SetBool("AtaquePerson", false);
        animPerson.SetBool("DefPerson", false);
        animPerson.SetBool("personVoltar", true);
        animPerson.SetFloat("Speed", -Mathf.Abs(rigPerson.velocity.x *2));
        manageQuizDenteFurado.DiminuirBarra();
        manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", false);
       

    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Dente") && manageQuizDenteFurado.checkAcerto) {
         
          
            manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", true);
            animPerson.SetInteger("tipoAtaque", 0);
            // checkTokInim = true;
            // animPerson.SetBool("AtaquePerson", true);
            // animPerson.SetBool("Defes", false);
            // Debug.Log("parou");
            partataque1.SetActive(true);
            rigPerson.velocity = new Vector2(0, 0);
            animPerson.SetFloat("Speed", Mathf.Abs(rigPerson.velocity.x));
            manageQuizDenteFurado.personAndar = false;
          
            animPerson.SetBool("AtaquePerson", true);
          
            Invoke("PersonVoltar", 3);
        } 
        else if (collision.gameObject.name == "coolParede3" && manageQuizDenteFurado.checkAcerto) {
          
       
            animPerson.SetInteger("tipoAtaque", numb);
            manageQuizDenteFurado.ControlEnemyDente2.animEnemy.SetBool("DefEnemy", true);

            rigPerson.velocity = new Vector2(0, 0);
            animPerson.SetFloat("Speed", Mathf.Abs(rigPerson.velocity.x));
            manageQuizDenteFurado.personAndar = false;

            animPerson.SetBool("AtaquePerson", true);
           // animPerson.SetInteger("tipoAtaque", numb);
            Invoke("PersonVoltar", 5);

        } else if (collision.CompareTag("Dente") && !manageQuizDenteFurado.checkAcerto) {
            animPerson.SetBool("AtaquePerson", false);
            animPerson.SetBool("DefPerson", true);

        }
        else if (collision.gameObject.name == "coolParede" && manageQuizDenteFurado.personVoltar) {           
            manageQuizDenteFurado.personVoltar = false;
            animPerson.SetBool("personVoltar", false);
            rigPerson.velocity = new Vector2(0, 0);
            animPerson.SetFloat("Speed", 0);
            manageQuizDenteFurado.personVoltar = false;
            animPerson.SetBool("AtaquePerson", false);
           // animPerson.SetBool("DefPerson", true);
            checkTokInim = false;
           // Debug.Log("Painel");
            manageQuizDenteFurado.PainelActive(true);
            if (!manageQuizDenteFurado.chekPanel && !manageQuizDenteFurado.proxEnemey) { manageQuizDenteFurado.PainelActive(true); }
            
            if (!manageQuizDenteFurado.proxEnemey) {
              //  manageQuizDenteFurado.PainelActiveExt(0.5f);
            }
        }
    }
}
