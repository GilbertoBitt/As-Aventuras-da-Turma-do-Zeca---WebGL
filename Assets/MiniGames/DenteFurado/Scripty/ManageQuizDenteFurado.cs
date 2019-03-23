using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MEC;
using QuestionSystem.Scripts;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class ManageQuizDenteFurado : QuestionResult {


    public GameObject painelQuiz;
    RectTransform painelQuizRT;
    public Text questtext;
    public Text[] textalternativa;
    public Sprite[] sprAlter;
    public Image[] imgAlter;
    public float timeTextAlter;
    public bool richTextEnabled;
    

    public List<QuestDenteFurado> QuestDenteFurado2 = new List<QuestDenteFurado>();
    public int numPerg;
   
    public GameObject personGB;
    public GameObject personsGB;
    public GameObject[] DentesGB2;
    public ControlPersonZecaDente controlPersonZecaDente;
    public ControlEnemyDente ControlEnemyDente2;

    public bool checkAcerto;
    public bool personAndar;
    public bool personVoltar;
    public bool personAtaq;


    public bool enemyAndar;
    public bool enemyAtaq;
    public bool enemyVoltar;

    public SpriteRenderer[] sprCenario;

    public Sprite[] limpo;
    public Sprite[] sujoLeve;
    public Sprite[] sujo1;
    public Sprite[] sujo2Leve;
    public Sprite[] sujo2;
    public int numbAcerto;
    public int numbErro;
    public int numbAcetErr;
    public bool chamarQuiz;
    public int numbPerson;
    public float dimiBarraPerson;
    public float valorAtualBarraPerson;
    public float dimiBarra;
    public float valorAtualBarra;
    public GameObject barraGBPerson;
    public Slider barraPerson;
    public GameObject barraGB;
    public Slider barraEnemy;
    public Vector3 barraV3;

    public bool proxEnemey;
    public Vector2 posQuestON;
    public Vector2 posQuestOff;

    public int numberEnemy;
    public bool chekPanel;
    public GraphicRaycaster GraphicRaycasterPanel;
    bool chekPass;
    public WsqManager QuestionManager;
    [ListDrawerSettings(NumberOfItemsPerPage = 5, ShowPaging = true)]
    public List<QuestionBase<object>> QuestionsList;
    public GameObject levelAnim;
    changeLevel changeLevel2;
    public Text namePersonText;
    public Text nameEnemeyText;
    public GameObject colParede3;
    //Controle do Sistema de Perguntas.
    public WsqController Controller;
    void Start() {
      
        QuestionsList = QuestionManager.GetListQuestionLayoutBase(layout:1, amount:1);
        QuestDenteFurado2.Clear();
        QuestionBaseToQuestDenteFurado();
        QuestDenteFurado2.Suffle();
        painelQuizRT = painelQuiz.GetComponent<RectTransform>();
        changeLevel2 = levelAnim.GetComponent<changeLevel>();
        valorAtualBarra = barraEnemy.value;
        barraGB.transform.DOScale(barraV3, 1.5f);
        barraGBPerson.transform.DOScale(barraV3, 1.5f);
        numbPerson = PlayerPrefs.GetInt("characterSelected", 0);
        InstPersons();
        Invoke("chamarPanelDelay", 2f);
        nameEnemeyText.text = "" + ControlEnemyDente2.nameDente;
    }

    public void QuestionBaseToQuestDenteFurado(){
        foreach (var questionBase in QuestionsList){
            QuestDenteFurado2.Add(new QuestDenteFurado(questionBase.Value as string, GetIndexCorrectAlternative(questionBase), GetAlternativas(questionBase)));
        }
    }

    public int GetIndexCorrectAlternative(QuestionBase<object> q){
        for (int i = 0; i < q.OptionAnswers.Count; i++){
            if (q.OptionAnswers[i].IsCorrect){
                return i;
            }
        }
        return -1;
    }
    
    public string[] GetAlternativas(QuestionBase<object> q){
        string[] alternativas = new string[4];
        for (int i = 0; i < q.OptionAnswers.Count; i++){
            alternativas[i] = q.OptionAnswers[i].Value as string;
        }

        return alternativas;
    }
        
    //TODO Chamar painel de Perguntas.
    public void PainelActive(bool checkP) {
        chekPanel = checkP;
        if (checkP) {
            chekPass = true;
            //ChamarQuestao();
            //painelQuizRT.DOAnchorPosY(0, 1, false);
            Controller.OpenQuestionScreen();
        } 
        else {           
            //painelQuizRT.DOAnchorPosY(450, 1, false);
        }

      //  Debug.Log("Painel");
        Controller.OpenQuestionScreen();
       // GraphicRaycasterPanel.enabled = checkP;
        //painelQuiz.SetActive(checkP);

    }

    void chamarLimpo() {
        for (int i = 0; i < sprCenario.Length; i++) {
            if (sprCenario[i] != null && limpo[i] != null)
                sprCenario[i].sprite = limpo[i];
        }
    }
    void chamarsujoLeve() {
        for (int i = 0; i < sprCenario.Length; i++) {
            if (sprCenario[i] != null && sujoLeve[i] != null)
                sprCenario[i].sprite = sujoLeve[i];
        }
    }
    void chamarsujo1() {
        for (int i = 0; i < sprCenario.Length; i++) {
            if (sprCenario[i] != null && sujo1[i] != null)
                sprCenario[i].sprite = sujo1[i];
        }
    }
    void chamarsujoLeve2() {
        for (int i = 0; i < sprCenario.Length; i++) {
            if (sprCenario[i] != null && sujo2Leve[i] != null)
                sprCenario[i].sprite = sujo2Leve[i];
        }
    }
    void chamarsujo2() {
        for (int i = 0; i < sprCenario.Length; i++) {
            if (sprCenario[i] != null)
                if (sprCenario[i] != null && sujo2[i] != null)
                    sprCenario[i].sprite = sujo2[i];
        }
    }
    public void AtaquePersonagens() {
        controlPersonZecaDente.AtaquePerson();
    }

    public void AtaqueInimigo() {
        ControlEnemyDente2.AtaqueEnemey();
    }

    void InstPersons() {
        if (numbPerson != 0) {
            controlPersonZecaDente = personsGB.GetComponent<ControlPersonZecaDente>();
           // Debug.log(numbPerson);

        } else {
            controlPersonZecaDente = personGB.GetComponent<ControlPersonZecaDente>();
          //  Debug.log(numbPerson);
        }
        
      //  Debug.Log("numbPerson " + numbPerson);
    }
    
    void ChamarQuestao(){
    
        if (numPerg == QuestDenteFurado2.Count) {
            numPerg = 0;
        }
        questtext.text =  QuestDenteFurado2[numPerg].questionString;
       
       // Debug.Log(QuestDenteFurado2[numPerg].Alternative[numPerg]);
        for (int i = 0; i < QuestDenteFurado2[numPerg].Alternative.Length; i++) {
            textalternativa[i].text = QuestDenteFurado2[numPerg].Alternative[i];
        }
      

    }
   

    // Update is called once per frame
    

    public void BtAlternative(int userchoise) {
      
        chekPanel = false;
        PainelActive(false);
      
        if (userchoise == 1) 
        {
           
            for (int i = 0; i < imgAlter.Length; i++) {
                imgAlter[i].sprite = sprAlter[0];
            }
            imgAlter[0].sprite = sprAlter[1];
        } 
        else if (userchoise == 2)
        {
           
            for (int i = 0; i < imgAlter.Length; i++) {
                imgAlter[i].sprite = sprAlter[0];
            }
            imgAlter[1].sprite = sprAlter[1];

        }
        else if (userchoise == 3)
        {
          
            for (int i = 0; i < imgAlter.Length; i++) {
                imgAlter[i].sprite = sprAlter[0];
            }
            imgAlter[2].sprite = sprAlter[1];
        } 
        else if (userchoise == 4) {
           
            for (int i = 0; i < imgAlter.Length; i++) {
                imgAlter[i].sprite = sprAlter[0];
            }
            imgAlter[3].sprite = sprAlter[1];
        }

            checkAlter(userchoise, QuestDenteFurado2[numPerg].AlternativeCorreta);
        
      
     

    }
    
    public override void IsCorrect(bool isRight){
      //  Debug.Log(isRight ? "Correto" : "Errado");
        chekPass = false;
        numbAcetErr = numbAcerto - numbErro;
        if (isRight)
        {
            numbAcerto = numbAcerto + 1;
            checkAcerto = true;
            Timing.RunCoroutine(Corretresp());
        }
        else
        {
            if (numbAcetErr == 0) {
                numbAcetErr =- 1;
            }
            CenarioSujo();
            numbErro = numbErro + 1;
            checkAcerto = false;
            //  Debug.Log("errado");
           
            Timing.RunCoroutine(Erresp());
        }
        
    }

    void checkAlter(int alterSel, int alterCorret) {
        numbAcetErr = numbAcerto - numbErro;
      

        if (alterSel == alterCorret) {
          //  Debug.Log("Resposta numbPerg: " + numbPerg);
            numbAcerto = numbAcerto + 1;
            checkAcerto = true;
          //  Debug.Log("certo");
            Timing.RunCoroutine(Corretresp());
            
        }         
        else {
            if (numbAcetErr == 0) {
                numbAcetErr =- 1;
            }
            CenarioSujo();
            numbErro = numbErro + 1;
            checkAcerto = false;
          //  Debug.Log("errado");
           
            Timing.RunCoroutine(Erresp());
          
        }
        
  
    }

    void CenarioSujo() {
        if (numbAcetErr == 0) {
            chamarLimpo();
        } else if (numbAcetErr == -1) {
            chamarsujoLeve();
        } else if (numbAcetErr == -2) {
            chamarsujo1();
        } else if (numbAcetErr == -3) {
            chamarsujoLeve2();
        } else if (numbAcetErr == -4) {
            chamarsujo2();
        }
    }

    IEnumerator<float> Corretresp() {
     
        yield return Timing.WaitForSeconds(0.5f);
        AtaquePersonagens();
        yield return Timing.WaitForSeconds(0.5f);
        numPerg = numPerg + 1;
       
        

        for (int i = 0; i < imgAlter.Length; i++) {
        imgAlter[i].sprite = sprAlter[0];
        }

        chekPass = false;


    }
    IEnumerator<float> Erresp() {

        yield return Timing.WaitForSeconds(0.5f);
        AtaqueInimigo();

      //  controlPersonZecaDente.animPerson.SetBool("DefPerson", true);
        yield return Timing.WaitForSeconds(0.5f);
        numPerg = numPerg + 1;
       

        for (int i = 0; i < imgAlter.Length; i++) {
            imgAlter[i].sprite = sprAlter[0];
        }

        chekPass = false;


    }

    public void DiminuirBarra() {
        valorAtualBarra = barraEnemy.value;
        barraEnemy.DOValue(valorAtualBarra-dimiBarra, 1, false);
        valorAtualBarra = barraEnemy.value;
   
        if (valorAtualBarra<=1 && !proxEnemey) {
            proxEnemey = true;
            Invoke("ProxEnemy",1f);
        }
    }

    public void DiminuirBarraPerson() {
        valorAtualBarraPerson = barraPerson.value;
        barraPerson.DOValue(valorAtualBarraPerson - dimiBarraPerson, 1, false);
        valorAtualBarraPerson = barraPerson.value;

       
    }

    void ProxEnemy() {
        //  DentesGB2[numberEnemy].SetActive(false);
       
        ControlEnemyDente2.TammanhoDenteP();
        numberEnemy = numberEnemy + 1;
        DentesGB2[numberEnemy].SetActive(true);
        Invoke("EnemetCresc", 1f);
    }
    void EnemetCresc() {
        personsGB = DentesGB2[numberEnemy].GetComponent<GameObject>();
        ControlEnemyDente2 = DentesGB2[numberEnemy].GetComponent<ControlEnemyDente>();
        changeLevel2.startChangeLevelAnimation(numberEnemy);
        nameEnemeyText.text = "" + ControlEnemyDente2.nameDente;
       // ControlEnemyDente2.TammanhoDenteG();
        barraEnemy.DOValue(10, 1, false);
        proxEnemey = false;
        DentesGB2[numberEnemy-1].GetComponent<GameObject>().SetActive(false);
        Invoke("chamarPanelDelay",2f);
    }
    void chamarPanelDelay() {
        chekPass = true;
        Controller.OpenQuestionScreen();
        PainelActive(true);
    }
}
