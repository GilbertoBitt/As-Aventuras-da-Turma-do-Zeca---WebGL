using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PresetPlataforms1_3 : MonoBehaviour {

    public Manager1_3A manager;
    private GameObject nextPlataform;
    public Transform positionReference;
    public GameObject[] spriteRenderers;
    public GameObject[] spriteobjS;
    //  public SpriteRenderer[] spriteRend;
    public SpriteRenderer spriteBlokc;
    public GameObject spriteColuna;
    public BoxCollider2D tetoColl;
    public GameObject AnimLevel;
    public Animator anim_AnimLevel;
    public changeLevel changeLevelR;
    public int numb;
    public int numbPlataform;
    public int numbRandom;
    public BoxCollider2D endColl;
    public  int numbspr;
    bool checkPass;
    bool checkPass2;
    public float durationOfFade = 1.0f;
    public AnimationCurve fadeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    bool checkPass3;
    bool checkPass4;

   /// <summary>
   /// Awake is called when the script instance is being loaded.
   /// </summary>
   
   
    public void StartGo() {
       // chamarAnim();
        changeLevelR = AnimLevel.GetComponent<changeLevel>();
        if (numbPlataform == 9 || numbPlataform == 19) {
            Timing.RunCoroutine(FadeBackground());
            //spriteColuna.SetActive(true);
            //spriteColuna.GetComponent<SpriteRenderer>().enabled = false;
           // checkPass3 = true;
        }

        //  startPlataform();
        if (!checkPass2) {
            checkPass2 = true;
           // startPlataform();
            Invoke("startPlataform", 0.25f);
            spriteColuna.SetActive(false);
           // numbspr = spriteRenderers.Length;
            anim_AnimLevel = AnimLevel.GetComponent<Animator>();
            // Debug.Log("sfsa");
        }
    }

    void MudarFundo() {
        if (numbPlataform < 10) {
            numb = 0;
        } else if (numbPlataform > 9 && numbPlataform < 20) {
            numb = 1;
        } else if (numbPlataform > 19) {
            numb = 2;
        }
        MudarCenario();
    }

    void MudarCenario() {
        numbspr = spriteRenderers.Length;
        for (int i = 0; i < numbspr; i++) {
            if (i == 0 && spriteRenderers[i] != null) {

                spriteRenderers[i].GetComponent<SpriteRenderer>().sprite = manager.SpriteGround1[numb];

            } else if (i != 0 && spriteRenderers[i] != null) {

                spriteRenderers[i].GetComponent<SpriteRenderer>().sprite = manager.SpriteGround2[numb];

            }
        }
        if (spriteBlokc != null && checkPass3==false) {
            spriteBlokc.sprite = manager.SpriteBlock[numb];
        }

        if (numbPlataform == 10 || numbPlataform == 20) {
           // Timing.RunCoroutine(FadeBackground());
           // spriteColuna.SetActive(fa);
          //  spriteColuna.GetComponent<SpriteRenderer>().enabled = true;
          
        }
        //Debug.Log(numbPlataform + " :OnBecomeVisible");
        //  spriteColuna.SetActive(true);
       
    }


    public void startPlataform() {
        if (manager.plataforms.Count > 0) {
            manager.numbCont = manager.numbCont + 1; 
            nextPlataform = manager.plataforms[0].gameObject;
            
            manager.plataforms[0].MudarFundo();
            nextPlataform.SetActive(true);
            //manager.plataforms.Remove(manager.plataforms[0]);
            // MudarFundo();
           // manager.plataforms[0].spriteColuna.SetActive(false);
            Vector3 pos = positionReference.position;
            nextPlataform.transform.position = pos;
            if (numbPlataform >= 29) {
                manager.checkCamZomm = true;
                manager.nurseManager.RunCameraZoomOut();                
                manager.plataforms[0].endColl.enabled = true;
                manager.nurseManager.hspeed = 0f;
                manager.plataforms[0].EndFIm();
            }
            manager.plataforms.Remove(manager.plataforms[0]);
            //activateSprites();

        }
       
    }

    void EndFIm() {
        int temp = spriteRenderers.Length;
        int temp2 = spriteobjS.Length;
        for (int i = 0; i < temp; i++) {
            if (i == 0 && spriteRenderers[i] != null) {

               // spriteRenderers[i].SetActive(true);
              //  Destroy(spriteRenderers[i]);

            } else if (i != 0 && spriteRenderers[i] != null) {

                spriteRenderers[i].SetActive(false);
                Destroy(spriteRenderers[i]);


            }
        }
        for (int i = 0; i < temp2; i++) {
            if (spriteobjS[i] != null) {
                this.spriteobjS[i].SetActive(false);
                Destroy(spriteobjS[i]);
            }
        }

    

}

    public void activateSprites() {
        int temp = spriteRenderers.Length;
        int temp2 = spriteobjS.Length;
        if (tetoColl != null) {
            tetoColl.enabled = true;
        }
        for (int i = 0; i < temp; i++) {
            if (spriteRenderers[i] != null) {
                spriteRenderers[i].SetActive(true);
            }

        }
        for (int j = 0; j < temp2; j++) {
            if (spriteobjS[j] != null) {
                spriteobjS[j].SetActive(true);
            }
        }
    }

   /* void OnBecameVisible() {
        if (checkPass == false) {
            checkPass = true;
            //   Debug.Log(numbPlataform + " :OnBecomeVisible");
            //activateSprites();
            Invoke("startPlataform", 0.01f);
            Invoke("activateSprites", 0.3f);
        }
        //startPlataform();
    }
    
    void OnBecameInvisible() {
        //this.gameObject.SetActive(false);
        //Invoke("DeactiveSprites", 1f);
        Invoke("AnimOff", 2f);

    }
    */
    void  AnimOff(){
        anim_AnimLevel.enabled = false;
        AnimLevel.SetActive(false);
    }

    void DeactiveSprites() {
        int temp = spriteRenderers.Length;
        int temp2 = spriteobjS.Length;
        if (tetoColl != null) {
            tetoColl.enabled = false;
        }

        for (int i = 0; i < temp; i++) {
            if (spriteRenderers[i] != null)
                spriteRenderers[i].SetActive(false);

        }
        for (int j = 0; j < temp2; j++) {
            if (spriteobjS[j] != null)
                spriteobjS[j].SetActive(false);
        }
    }

    IEnumerator<float> FadeBackground() {

        checkPass3 = true;
        float times = 0.0f;
        while (times < durationOfFade) {
            times += Time.deltaTime;
            float s = times / durationOfFade;
            spriteBlokc.color = Color.Lerp(Color.white, Color.black, fadeCurve.Evaluate(s));
            yield return 0f;
        }
        if (!checkPass4) {
            anim_AnimLevel.enabled = true;
            AnimLevel.SetActive(true);
            manager.contadorLevel += 1;
            changeLevelR.startChangeLevelAnimation(manager.contadorLevel);
            Invoke("AnimOff", 1.5f);

           // Invoke("chamarAnim", .5f);
        }
        yield return Timing.WaitForSeconds(1.5f);
        if (spriteBlokc != null) {
            spriteBlokc.sprite = manager.SpriteBlock[numb+1];
        }

        times = 0.0f;
        while (times < durationOfFade) {
            times += Time.deltaTime;
            float s = times / durationOfFade;
            spriteBlokc.color = Color.Lerp(Color.black, Color.white, fadeCurve.Evaluate(s));
            yield return 0f;
        }
    }

    void chamarAnim() {
        anim_AnimLevel.enabled = true;
        AnimLevel.SetActive(true);
        manager.contadorLevel = manager.contadorLevel + 1;
        changeLevelR.startChangeLevelAnimation(manager.contadorLevel);
        Invoke("AnimOff", 1.5f);
    }

}