using System.Collections.Generic;
using UnityEngine;
using MEC;

public class COntrolLogoVideo : MonoBehaviour {

    Vector3 offsetFollow;
    Vector3 defaultOffset;
    public float transitionDuration;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public Transform[] cuboAzul;
    public Transform[] cuboAzu2;

   // Vector3 endOffsetB;
    void Start () {
      
            Timing.RunCoroutine(TamanhoTime());

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator<float> TamanhoTime() {

        // Debug.Log("dfds");

        // Vector3 startOffsetB = offsetFollow;
        // Vector3 endOffsetB = defaultOffset;
        yield return Timing.WaitForSeconds(2.5f);
       
        for (int i = 0; i < cuboAzul.Length; i++) {
            cuboAzul[i].GetComponent<Rigidbody>().isKinematic = true;
            cuboAzul[i].GetComponent<BoxCollider>().enabled = false;
            Vector3 startOffsetB = new Vector3(cuboAzul[i].transform.position.x, cuboAzul[i].transform.position.y, cuboAzul[i].transform.position.z);
            Vector3 endOffsetB = new Vector3(cuboAzu2[i].transform.position.x, cuboAzu2[i].transform.position.y, cuboAzu2[i].transform.position.z);
            Vector3 endOffsetrot = new Vector3(cuboAzu2[i].transform.eulerAngles.x, cuboAzu2[i].transform.eulerAngles.y, cuboAzu2[i].transform.eulerAngles.z);
            float times = 0.0f;
            while (times < transitionDuration) {
                times += Time.deltaTime;
                float s = times / transitionDuration;

                cuboAzul[i].transform.position = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
                cuboAzul[i].transform.eulerAngles = Vector3.Lerp(startOffsetB, endOffsetrot, transitionCurve.Evaluate(s));

                yield return Timing.WaitForOneFrame;
            }
            //yield return Timing.WaitForSeconds(.1f);
        }
      //  Vector3 startOffsetB = new Vector3(cuboAzul.transform.position.x, cuboAzul.transform.position.y, cuboAzul.transform.position.z);
      //  Vector3 endOffsetB = new Vector3(cuboAzu2.transform.position.x, cuboAzu2.transform.position.y, cuboAzu2.transform.position.z);
      
        
    }

    }
