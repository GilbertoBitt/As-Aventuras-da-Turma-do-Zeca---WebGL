using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MEC;

public class ControlBlock : MonoBehaviour {

    Vector3 offsetFollow;
    Vector3 defaultOffset;
    public float transitionDuration;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    Transform cuboAzul;
    public Transform cuboAzu2;
    bool checkpass;
    public float time;
    public bool ultimo;
    public GameObject letra;
    Rigidbody2D blocofisica;
    Vector3 endValue;
    public float jumpPower;
    public int numJumps;
    public float duration;
    public bool snapping;
    //public float toAngle;
    public float durationr;



    void Start () {
        cuboAzul = GetComponent<Transform>();
        blocofisica = GetComponent<Rigidbody2D>();
        endValue = cuboAzu2.localPosition;
        blocofisica.DOJump(endValue, jumpPower, numJumps, duration, snapping);
        blocofisica.DORotate(cuboAzu2.transform.eulerAngles.z, durationr);

        // cuboAzul.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        cuboAzul.GetComponent<Collider2D>().enabled = false;
        if (ultimo) {
            Invoke("actiobj", 5f);
        }

    }

    void actiobj() {
        letra.SetActive(true);
    }
    public IEnumerator<float> TamanhoTime(float time) {


        // Vector3 startOffsetB = offsetFollow;
        // Vector3 endOffsetB = defaultOffset;

        yield return Timing.WaitForSeconds(time);


        // cuboAzul[numbloco].GetComponent<Rigidbody2D>().isKinematic = true;
        cuboAzul.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        cuboAzul.GetComponent<Collider2D>().enabled = false;
        Vector3 startOffsetB = new Vector3(cuboAzul.transform.position.x, cuboAzul.transform.position.y, cuboAzul.transform.position.z);
        Vector3 endOffsetB = new Vector3(cuboAzu2.transform.position.x, cuboAzu2.transform.position.y, cuboAzu2.transform.position.z);
        Vector3 endOffsetrot = new Vector3(cuboAzu2.transform.eulerAngles.x, cuboAzu2.transform.eulerAngles.y, cuboAzu2.transform.eulerAngles.z);
        float times = 0.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;

            cuboAzul.transform.position = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            cuboAzul.transform.eulerAngles = Vector3.Lerp(startOffsetB, endOffsetrot, transitionCurve.Evaluate(s));

            yield return Timing.WaitForOneFrame;
        }
        cuboAzul.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (ultimo) {
            letra.SetActive(true);
        }

        //yield return Timing.WaitForSeconds(.1f);

        //  Vector3 startOffsetB = new Vector3(cuboAzul.transform.position.x, cuboAzul.transform.position.y, cuboAzul.transform.position.z);
        //  Vector3 endOffsetB = new Vector3(cuboAzu2.transform.position.x, cuboAzu2.transform.position.y, cuboAzu2.transform.position.z);


    }
    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision) {
         if (collision.gameObject.name == "coolPe2" && !checkpass) {
            checkpass = true;
           // Debug.Log("sss"); 
          //  Timing.RunCoroutine(TamanhoTime(time));
        }
        
    }
}
