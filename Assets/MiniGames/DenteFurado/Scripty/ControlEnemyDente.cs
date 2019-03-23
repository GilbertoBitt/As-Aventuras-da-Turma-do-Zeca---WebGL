using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using MEC;
using DG.Tweening;
using UnityEngine.Rendering;

public class ControlEnemyDente : MonoBehaviour {

    public bool checkTokPlayer;
    public ManageQuizDenteFurado manageQuizDenteFurado;
    public Animator animEnemy;
    Rigidbody2D rigPEnemy;

    public GameObject[] partAtaque2;
    public GameObject[] partAtaque3;
    public GameObject partAtaque1;
    public GameObject partGermes;
    public ExpressFacialZeca ExpressFacialZeca2;
    public Vector3 tamannhoG;
    public Vector3 tamannhoP;
    public Vector3 PosF;
    public SortingGroup imgsPersons;
    public Transform dente;
    bool checkPass;
    public int numb;
    public int p;
    public string nameDente;
    bool pass;
   

    public void Start () {
        dente = GetComponent<Transform>();
        TammanhoDenteG();

        tamannhoG = new Vector3(tamannhoG.x + 1, tamannhoG.y + 1, tamannhoG.z + 1);

         animEnemy = GetComponent<Animator>();
        rigPEnemy = GetComponent<Rigidbody2D>();
        manageQuizDenteFurado = Camera.main.GetComponent<ManageQuizDenteFurado>();
       // manageQuizDenteFurado.proxEnemey = false;
        ExpressFacialZeca2 = GetComponent<ExpressFacialZeca>();
        numb = Random.Range(0, 2);

    }
    public void MudalerLayer() {
        imgsPersons.enabled = true;
        dente.DOScale(tamannhoG, 1f);


    }
    public void DesativarOBJ() {      
        gameObject.SetActive(false);


    }

    public void TammanhoDenteG() {
        dente.DOScale(tamannhoG, 1f);

    }
    public void TammanhoDenteP() {
        // gameObject.transform.DOScale(tamannhoP, 2f);
        animEnemy.SetBool("Perdeu", true);

    }
    public void PartAtaque2() {
        for (int i = 0; i < partAtaque2.Length; i++) {
            partAtaque2[i].SetActive(true);
        }
    }
    public void PartAtaque1() {
        partAtaque1.SetActive(true);
    }
    public void PartAtaque3() {
        if (!pass) {
            pass = true;
            partAtaque3[0].SetActive(true);
           // StopAtaque();
        }
    }
    void PartAtaqueDesativar() {
        partAtaque1.SetActive(false);
        for (int i = 0; i < partAtaque2.Length; i++) {
            partAtaque2[i].SetActive(false);
            checkPass = false;
            animEnemy.SetBool("AtaqueEnemy", false);
            manageQuizDenteFurado.enemyAtaq = false;
          
        }
        for (int i = 0; i < partAtaque3.Length; i++) {
                          partAtaque3[i].SetActive(false);       
               
        }
      
       
      
        manageQuizDenteFurado.controlPersonZecaDente.animPerson.SetBool("DefPerson", false);
        //manageQuizDenteFurado.PainelActive(true);
     
        partGermes.SetActive(false);
        pass = false;
      
    }
    // Update is called once per frame
  
    public void AtaqueEnemey() {
       // manageQuizDenteFurado.checkAcerto = false;
        manageQuizDenteFurado.enemyAtaq = true;
        numb = numb + 1;
        if (numb == 3 || numb==0) {
            numb = 0;
            manageQuizDenteFurado.colParede3.SetActive(false);
            // manageQuizDenteFurado.colParede3.SetActive(false);
        } else {
            manageQuizDenteFurado.colParede3.SetActive(true);
        }
      
        AndarEnemy();
    
        animEnemy.SetInteger("tipoAtaque", numb);
    }

  
    void EnemyAtaqq() {
        partGermes.SetActive(true);
        //  manageQuizDenteFurado.enemyAndar = false;
        rigPEnemy.velocity = new Vector2(0, 0);
        animEnemy.SetFloat("Speed", 0);
        //manageQuizDenteFurado.enemyAndar = false;
        manageQuizDenteFurado.controlPersonZecaDente.animPerson.SetBool("DefPerson", true);
     
        animEnemy.SetInteger("tipoAtaque", numb);
        animEnemy.SetBool("AtaqueEnemy", true);
     
        // Invoke("EnemyVoltar", 3f);
    }

    void AndarEnemy() {
        partGermes.SetActive(true);
        rigPEnemy.velocity = new Vector2(-3, 0);
            animEnemy.SetFloat("Speed", Mathf.Abs(rigPEnemy.velocity.x));       
       
            
        // animEnemy.SetFloat("Speed", Mathf.Abs(rigPEnemy.velocity.x * 2));

    }
    public void EnemyVoltar() {
        manageQuizDenteFurado.colParede3.SetActive(false);
        PartAtaqueDesativar();
        manageQuizDenteFurado.enemyVoltar = true;
        rigPEnemy.velocity = new Vector2(3, 0);
        animEnemy.SetBool("AtaqueEnemy", false);
        //  animEnemy.SetBool("Defes", false);
        manageQuizDenteFurado.DiminuirBarraPerson();
       
        animEnemy.SetBool("enemeyVoltar", true);
        animEnemy.SetFloat("Speed", -Mathf.Abs(rigPEnemy.velocity.x));

    }
   
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !manageQuizDenteFurado.checkAcerto) {
            checkTokPlayer = true;          
            manageQuizDenteFurado.enemyAndar = false;
            rigPEnemy.velocity = new Vector2(0, 0);
            animEnemy.SetFloat("Speed", 0);
            manageQuizDenteFurado.enemyAndar = false;
            animEnemy.SetInteger("tipoAtaque", numb);
            animEnemy.SetBool("AtaqueEnemy", true);
            Invoke("EnemyVoltar", 3f);


        } else if (collision.gameObject.name == "coolParede3" && !manageQuizDenteFurado.checkAcerto) {
            checkTokPlayer = true;
            manageQuizDenteFurado.enemyAndar = false;
            rigPEnemy.velocity = new Vector2(0, 0);
            animEnemy.SetFloat("Speed", 0);
            manageQuizDenteFurado.enemyAndar = false;
            animEnemy.SetInteger("tipoAtaque", numb);
            animEnemy.SetBool("AtaqueEnemy", true);
            Invoke("EnemyVoltar", 3f);

        } else if (collision.gameObject.name == "coolParede2" && manageQuizDenteFurado.enemyVoltar) {
            manageQuizDenteFurado.enemyVoltar = false;
            rigPEnemy.velocity = new Vector2(0, 0);
            animEnemy.SetBool("enemeyVoltar", false);
            checkTokPlayer = false;
            //Debug.Log("Painel");
            //   manageQuizDenteFurado.chekPanel=true;
            manageQuizDenteFurado.PainelActive(true);
            if (!manageQuizDenteFurado.chekPanel && !manageQuizDenteFurado.proxEnemey) { manageQuizDenteFurado.PainelActive(true);
              //  Debug.Log("Painel");
              
            }

        }


    }
    }
