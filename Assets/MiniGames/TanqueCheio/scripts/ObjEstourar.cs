using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using MEC;
using DG.Tweening;
using UnityEngine.Rendering;

public class ObjEstourar :MonoBehaviour {

    public SpriteRenderer objSprBolha;
    public GameObject particulaExplo;
    public ParticleSystem[] partSis;
    public Color partCorObj;
    public Sprite[] corBola;
    public Light luzBola;
  
    
    public int numberBolaLocal;
    
    Rigidbody2D rigBola;
    Collider2D coll;

    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 scanPos;
    public ControlTanqueCheio ControlTanqueCheio2;
    Vector3 tamanhoMin;
    public bool chekCarregado;
    bool dentroRecp;
    public Transform recpTocado;
    public SpriteRenderer[] recpCor;
    public int numberRecpLocal;
    public bool desativarMove;
    public int destroyPass;
    public float timeMy;
    bool dentroDestroyLocal;
    public int quantLetra;
    public string recebetext;
    public TextMesh letraBolha;
    public bool textVogal;
    string texTemp;
    int numBolaVogal;
    public GameObject Sublinhado;
    public int corNumLocal;
 




    void Start() {
        //  numberBola = Random.Range(0, corBola.Length);
        // GetComponent<SpriteRenderer>().sprite = corBola[numberBola];
 
       // Invoke("DesativarTriggue", 2);
        coll = GetComponent<Collider2D>();
        rigBola = GetComponent<Rigidbody2D>();
        
        //  rigBola.AddForce(transform.up * 700);
        texTemp = recebetext.ToUpper();
        if (texTemp == "A" || texTemp == "E" || texTemp == "I" || texTemp == "O" || texTemp == "U") {
            textVogal = true;
            numBolaVogal = 0;
        } else {
            numBolaVogal = 1;
        }
        if (texTemp == "W" || texTemp == "N" || texTemp == "Z") {
            Sublinhado.SetActive(true);
        }

        //return array;
    }

    // Update is called once per frame
    void Update() {
        if (!desativarMove) {
            scanPos = transform.position;
        }


    }

    void DesativarTriggue() {
        coll.isTrigger = false;
    }
    void OnMouseDown() {
        if (!desativarMove) {
            screenPoint = Camera.main.WorldToScreenPoint(scanPos);
            offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            this.chekCarregado = true;
        }

    }
    void OnMouseDrag() {
        if (!desativarMove) {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            this.chekCarregado = true;
        }

    }
    void OnMouseUp() {

        chekCarregado = false;


        if (textVogal && dentroRecp && numberRecpLocal == numBolaVogal) {           
            Timing.RunCoroutine(AcertouTime());
        } else if (!textVogal && dentroRecp && numberRecpLocal == numBolaVogal) {         
            Timing.RunCoroutine(AcertouTime());
        } else if ((!textVogal || textVogal) && dentroRecp && numberRecpLocal != numBolaVogal) {
            recpTocado.DOPunchRotation(new Vector3(0, 0, 40f), 1, 5, 0.2f);
            ControlTanqueCheio2.ContagemErroAcerto(false);
            Timing.RunCoroutine(DestroyOBJTime());
           

        } 

        /*
        if (dentroRecp && numberRecpLocal == numberBolaLocal) {
            //   gameObject.transform.DOLocalMove(new Vector3(0, -9, 0), 0.5f, false);
            Timing.RunCoroutine(BolanoRecp());
        } else {
            recpTocado.DOPunchRotation(new Vector3(0, 0, 40f), 1, 5, 0.2f);

        }*/

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Dente") && chekCarregado && !dentroRecp) {
            dentroRecp = true;
       
            recpTocado = collision.transform;
           
            recpCor[0] = recpTocado.gameObject.GetComponent<RecipineteControl>().corCano[0];
            recpCor[1] = recpTocado.gameObject.GetComponent<RecipineteControl>().corCano[1];
          
            numberRecpLocal = collision.gameObject.GetComponent<RecipineteControl>().numbRecp;
           // gameObject.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.25f);
        }
        if (collision.gameObject.name == "PontoDestroy") {
            dentroDestroyLocal = true;
            // Debug.Log(timeDentro);
            Invoke("ChamarDestroyTotal", 2f);
        }


    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Dente") && dentroRecp) {
          //  gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
            dentroRecp = false;
        }

        if (collision.gameObject.name == "PontoDestroy") {
            DesativarTriggue();
            dentroDestroyLocal = false;
        }

    }

    IEnumerator<float> AcertouTime() {
        ControlTanqueCheio2.moveRecp = false;
        recpTocado.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        // recpTocado.gameObject.GetComponent<RecipineteControl>().Move = false;
        dentroDestroyLocal = false;
        Destroy(rigBola);
        coll.enabled = false;
        transform.SetParent(recpTocado);
        if (ControlTanqueCheio2.numbLevel > 1) {
           // recpTocado.gameObject.GetComponent<RecipineteControl>().Move = false;
            
        }

        yield return Timing.WaitForSeconds(0.3f);
        gameObject.transform.DOLocalMove(new Vector3(0, -6, 0), 0.25f, false);
        yield return Timing.WaitForSeconds(0.6f);
        if (ControlTanqueCheio2.numbLevel > 1) {
            
            recpTocado.DOLocalMove(new Vector3(recpTocado.localPosition.x, 4.5f, recpTocado.localPosition.z), 0.25f, false);
            //recpTocado.gameObject.GetComponent<RecipineteControl>().MoverY();
        }
        objSprBolha.sortingOrder = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, -4);
        for (int i = 0; i < recpCor.Length; i++) {
            recpCor[i].color = objSprBolha.color;
        }
        ControlTanqueCheio2.ContagemErroAcerto(true);
        for (int i = 0; i < ControlTanqueCheio2.corCenario.Length; i++) {
            ControlTanqueCheio2.corCenario[i].color = objSprBolha.color;
        }
        ControlTanqueCheio2.corCano[0].color= objSprBolha.color;
        ControlTanqueCheio2.corCano[1].color = objSprBolha.color;
        ControlTanqueCheio2.panelPont.GetComponent<Image>().color = objSprBolha.color;
        ControlTanqueCheio2.panelPont.GetComponent<Image>().DOFade(0.45f, 0.2f);
        gameObject.transform.DOLocalMove(new Vector3(0, 10, 0), 2f, false);
        recpTocado.DOPunchScale(new Vector3(.5f, .5f, .5f), 0.5f, 1, 0.2f);
        yield return Timing.WaitForSeconds(0.3f);
        recpTocado.localPosition = new Vector3(recpTocado.localPosition.x, 4.5f, recpTocado.localPosition.z);
        recpTocado.DOLocalMove(new Vector3(recpTocado.localPosition.x, 4.5f, recpTocado.localPosition.z), 0.25f, false);
        ControlTanqueCheio2.moveRecp = true;
        //  recpTocado.GetComponent<RecipineteControl>().MoverY();
        if (ControlTanqueCheio2.numbLevel > 0) {
            //recpTocado.GetComponent<RecipineteControl>().MudaDir();
        }
        //recpTocado.gameObject.GetComponent<RecipineteControl>().Move = true;
        //recpTocado.transform.DOLocalMove(new Vector3(recpTocado.position.x, 0.0002f, 0), 0.5f, false);

    }

    public void DestroyOBJ() {
        Timing.RunCoroutine(DestroyOBJTime());
    }
    IEnumerator<float> DestroyOBJTime() {
        ControlTanqueCheio2.moveRecp = false;
        recpTocado.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        particulaExplo.SetActive(true);
        luzBola.enabled = false;
        Sublinhado.GetComponent<TextMesh>().color = objSprBolha.color;
       // ControlTanqueCheio2.panelPont.GetComponent<Image>().color = objSprBolha.color;
       // ControlTanqueCheio2.panelPont.GetComponent<Image>().DOFade(0.45f, 0.2f);
        letraBolha.color = objSprBolha.color;
        for (int i = 0; i < partSis.Length; i++) {
            partSis[i].startColor = partCorObj;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        if (!ControlTanqueCheio2.destroyCor) {
            ControlTanqueCheio2.gameObject.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0.2f), 10, 4, false);

        } else { 
            ControlTanqueCheio2.gameObject.transform.DOShakePosition(1f, new Vector3(0.05f, 0.05f, 0.05f), 10, 4, false);
           
        }
        gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 0.25f);
        yield return Timing.WaitForSeconds(0.05f);
        if (objSprBolha != null) {
            objSprBolha.enabled = false;
        }     
        if (ControlTanqueCheio2.destroyTudo) {
            for (int i = 0; i < ControlTanqueCheio2.corCenario.Length; i++) {
                ControlTanqueCheio2.corCenario[i].color = objSprBolha.color;
            }

        }

        //gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.25f);
        // yield return Timing.WaitForSeconds(0.3f);
        //ControlTanqueCheio2.gameObject.transform.localPosition = ControlTanqueCheio2.posCamIni;
        ControlTanqueCheio2.objSceneTotal.Remove(gameObject);
        if (corNumLocal == 0) {
            ControlTanqueCheio2.listVermelho.Remove(gameObject);
        } else if (corNumLocal == 1) {
            ControlTanqueCheio2.listVerde.Remove(gameObject);
        } else if (corNumLocal == 2) {
            ControlTanqueCheio2.listAmarelo.Remove(gameObject);
        } else if (corNumLocal == 3) {
            ControlTanqueCheio2.listAzul.Remove(gameObject);
        }

        if (ControlTanqueCheio2.objSceneTotal.Count == 0) {
            ControlTanqueCheio2.destroyCor = false;
        }
        recpTocado.localPosition = new Vector3(recpTocado.localPosition.x, 4.5f, recpTocado.localPosition.z);
        recpTocado.DOLocalMove(new Vector3(recpTocado.localPosition.x, 4.5f, recpTocado.localPosition.z), 0.25f, false);
        ControlTanqueCheio2.moveRecp = true;
        //  recpTocado.GetComponent<RecipineteControl>().MoverY();
        if (ControlTanqueCheio2.numbLevel > 0) {
        //    recpTocado.GetComponent<RecipineteControl>().MudaDir();
        }
        Destroy(this.gameObject, 0.5f);

        //recpTocado.transform.DOLocalMove(new Vector3(recpTocado.position.x, 0.0002f, 0), 0.5f, false);

    }
    void ChamarDestroyTotal() {
        if (dentroDestroyLocal && !chekCarregado) {
            ControlTanqueCheio2.DestrotBolasTotal();
        }
    }
}
