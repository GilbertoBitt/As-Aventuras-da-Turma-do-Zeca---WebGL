using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlTecladoTutor :MonoBehaviour {

    public Vector3 offsetFollowB;
    public Vector3 defaultOffsetB;

    public Vector4 offsetFollowColor;
    public Vector4 defaultOffsetColor;

    public Vector3 offsetFollowImage;
    public Vector3 defaultOffsetImage;

    public float transitionDurationB;
    public GameObject textTutor;
    public Image[] tecla;
    public Color corTe;
    public Text textPal2s;
    // public Text textPal2asfs;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public bool fim;

    public Manager1_3A Manager1_3A;
    public Manager1_3B Manager1_3B1;
    public bool didatica;
    int contTutor;

    public GameObject canudo;
    void Start() {
        defaultOffsetColor = tecla[0].color;
        offsetFollowColor = corTe;
        if (Manager1_3B1.didadMovel != true) {
            StartCoroutine(TimeText1()); }
     
        //  StartCoroutine(corTeclaTime());

    }



    public IEnumerator TimeText1() {

        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;
        if (!didatica) {
            textPal2s.text = "Pular";
        } else {
            fim = false;
            textPal2s.text = "Atirar";
        }

        yield return new WaitForSeconds(0.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            tecla[3].color = Vector4.Lerp(endOffsetBColor, startOffsetBColor, transitionCurve.Evaluate(s));
            yield return 0f;
        }



        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        yield return new WaitForSeconds(3f);
        if (!didatica) {
            StartCoroutine(TimeText2());
        } else {

            timesB = 0.0f;
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                textTutor.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
                yield return 0f;
            }

            yield return new WaitForSeconds(.2f);
            startOffsetBColor = offsetFollowColor;
            endOffsetBColor = defaultOffsetColor;
            timesB = 0.0f;
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                tecla[3].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
                yield return 0f;
            }

            StartCoroutine(TimeTextDir());
        }
      
        //  jucaAnim.SetInteger("animJuca",1);

    }


    public IEnumerator TimeText2() {
        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        if (!fim) {
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                tecla[3].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
                yield return 0f;
            }

        } else {
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                tecla[2].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
                yield return 0f;
            }

        }

        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;

        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        if (!fim) {
            //  yield return new WaitForSeconds(3f);
            StartCoroutine(TimeText3());
        }



    }
    // Update is called once per frame

    public IEnumerator TimeText3() {

        fim = true;
        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;
        textPal2s.text = "Abaixar";
        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            tecla[2].color = Vector4.Lerp(endOffsetBColor, startOffsetBColor, transitionCurve.Evaluate(s));
            yield return 0f;
        }



        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(TimeText2());
        //  jucaAnim.SetInteger("animJuca",1);
        Manager1_3A.IniCont();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public IEnumerator TimeTextDir() {

        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;

        textPal2s.text = "Direita";


        yield return new WaitForSeconds(0.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            tecla[0].color = Vector4.Lerp(endOffsetBColor, startOffsetBColor, transitionCurve.Evaluate(s));
            yield return 0f;
        }



        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(TimeTextDirF());
        //  jucaAnim.SetInteger("animJuca",1);

    }


    public IEnumerator TimeTextDirF() {
        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        if (!fim) {
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                tecla[0].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
                yield return 0f;
            }

        } else {
            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                tecla[2].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
                yield return 0f;
            }

        }

        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;

        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        if (!fim) {
            //  yield return new WaitForSeconds(3f);
            StartCoroutine(TimeTextEsq());
        }


    }
    public IEnumerator TimeTextEsq() {

        Vector4 startOffsetBColor = offsetFollowColor;
        Vector4 endOffsetBColor = defaultOffsetColor;
        float timesB = 0.0f;

        textPal2s.text = "Esquerda";

        yield return new WaitForSeconds(0.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            tecla[1].color = Vector4.Lerp(endOffsetBColor, startOffsetBColor, transitionCurve.Evaluate(s));
            yield return 0f;
        }



        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        timesB = 0.0f;

        yield return new WaitForSeconds(.2f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        yield return new WaitForSeconds(3f);

        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            tecla[0].color = Vector4.Lerp(startOffsetBColor, endOffsetBColor, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        yield return new WaitForSeconds(3f);
        //  Manager1_3B.startDidaticaS
        for (int i = 0; i < Manager1_3B1.bolhasDid.Length; i++) {
            Manager1_3B1.bolhasDid[i].enabled = true;
        }
        yield return new WaitForSeconds(0.1f);
        Manager1_3B1.checkTutorteclado = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
       // StartCoroutine(TimeTextDirF());
        //  jucaAnim.SetInteger("animJuca",1);

    }

    public IEnumerator TextOn() {
        textTutor.SetActive(true);
        float timesB = 0.0f;
        offsetFollowImage = new Vector3(2, 1, 2);
        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        yield return new WaitForSeconds(.1f);
        canudo.GetComponent<Animator>().enabled = true;
        canudo.GetComponent<Animator>().SetBool("Tutor", true);
        textTutor.transform.localScale = new Vector3();
        textPal2s.transform.localScale = new Vector3(1,2,1);
        textPal2s.fontSize = 25;
        if (contTutor == 0) {
           
            textPal2s.text = "Arraste para Esquerda";
            contTutor = 1;
        }
        else if (contTutor == 1) {
            textPal2s.text = "Arraste para Direita";
            contTutor = 2;
        }
        else if (contTutor == 2) {
            textPal2s.text = "Atirar ao Soltar";
            contTutor = 3;
        }
        canudo.GetComponent<Animator>().SetInteger("PosTutor", contTutor);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        //yield return new WaitForSeconds(3f);
        // StartCoroutine(TextOff());
    }

    public IEnumerator TextOff() {
        float timesB = 0.0f;
        Vector3 startOffsetB = offsetFollowImage;
        Vector3 endOffsetB = defaultOffsetImage;
        yield return new WaitForSeconds(.1f);
        //jucaAnim.SetInteger("animJuca", 1);
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            textTutor.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
       
    
        if (contTutor == 3) {
            yield return new WaitForSeconds(0.5f);
           
            //canudo.GetComponent<Animator>().SetBool("Tutor", false);
            Manager1_3B1.canShoot = true;
            Manager1_3B1.didadMovel = false;
            gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            canudo.GetComponent<Animator>().SetBool("Tutor", false);
            canudo.GetComponent<Animator>().SetInteger("posTiro", 1);
            yield return new WaitForSeconds(0.25f);
            canudo.GetComponent<Cannon1_3B>().OffAnimCanudo();
            //canudo.GetComponent<Animator>().enabled = false;
            // gameObject.GetComponent<Animator>().SetBool("Tutor", false);

        }


        // StartCoroutine(TimeTextDir());
    }

}
