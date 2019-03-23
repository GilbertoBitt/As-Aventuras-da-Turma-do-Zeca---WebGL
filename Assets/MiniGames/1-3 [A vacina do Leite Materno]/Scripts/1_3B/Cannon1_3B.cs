using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Cannon1_3B :MonoBehaviour {

    public Manager1_3B manager;
    public SpriteRenderer rightArrow;
    public SpriteRenderer leftArrow;
    public float durationEffect;
    public AnimationCurve Curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public Sprite[] canudosSprM;
    SpriteRenderer canudoSpr;

    public Animator animCanudo;




    private void Start() {
        canudoSpr = GetComponent<SpriteRenderer>();
        animCanudo = GetComponent<Animator>();
      //  animCanudo.enabled = false;


    }

    void OnMouseDrag() {
        manager.OnCannonPressed();
        /*if (manager.canShoot) {
			rightArrow.color = Color.white;
			leftArrow.color = Color.white;
		}*/
    }

    public void OnMouseDown() {
        manager.OnCannonPressed();
        if (manager.canShoot) {
            // rightArrow.color = Color.white;
            // leftArrow.color = Color.white;
            canudoSpr.sprite = canudosSprM[1];
            Timing.RunCoroutine(FadeIn());
        }
    }

    public void DownKey() {
        if (manager.canShoot) {
            // rightArrow.color = Color.white;
            // leftArrow.color = Color.white;
            canudoSpr.sprite = canudosSprM[1];
            Timing.RunCoroutine(FadeIn());
        }
    }


    public void OnMouseUp() {
      //  animCanudo.SetBool("Tutor", false);
      //  animCanudo.SetInteger("PosTutor", -1);
        if (manager.canShoot) {
            //manager.OnCannonReleased ();
            animCanudo.enabled = true;
            //animCanudo.SetBool("Tutor", false);
            animCanudo.SetInteger("posTiro", 1);
            //rightArrow.color = Color.clear;
            //leftArrow.color = Color.clear;
            Timing.RunCoroutine(FadeOut());
        }
    }

    public void UpKey() {
      //  animCanudo.SetBool("Tutor", false);
     //   animCanudo.SetInteger("PosTutor", -1);
        if (manager.canShoot) {
            //manager.OnCannonReleased ();
            animCanudo.enabled = true;
           // animCanudo.SetBool("Tutor", false);
            animCanudo.SetInteger("posTiro", 1);
            //rightArrow.color = Color.clear;
            //leftArrow.color = Color.clear;
            Timing.RunCoroutine(FadeOut());
        }
    }

   public void OffAnimCanudo() {
      //  animCanudo.SetBool("Tutor", false);
        animCanudo.SetInteger("posTiro", 0);
        if (!manager.didadMovel) {
            animCanudo.enabled = false;
        }

       // animCanudo.enabled = false;
        canudoSpr.sprite = canudosSprM[0];

    }

    void ShootCanon() {
        manager.OnCannonReleased();
    }

    public IEnumerator<float> FadeIn() {

        Color startColor = Color.white;
        Color endColor = startColor;
        endColor.a = 0f;

        float times = 0.0f;
        while (times < durationEffect) {
            times += Time.deltaTime;
            float s = times / durationEffect;

            rightArrow.color = Color.Lerp(endColor, startColor, Curve.Evaluate(s));
            leftArrow.color = Color.Lerp(endColor, startColor, Curve.Evaluate(s));

            yield return 0f;
        }
    }

    public IEnumerator<float> FadeOut() {

        Color startColor = Color.white;
        startColor.a = 0f;
        Color endColor = Color.white;


        float times = 0.0f;
        while (times < durationEffect) {
            times += Time.deltaTime;
            float s = times / durationEffect;

            rightArrow.color = Color.Lerp(endColor, startColor, Curve.Evaluate(s));
            leftArrow.color = Color.Lerp(endColor, startColor, Curve.Evaluate(s));

            yield return 0f;
        }
    }



    public void AnimTutorOn() {
        StartCoroutine(manager.ControlTecladoTutor2.TextOn());
    }
    public void AnimTutorOff() {
        StartCoroutine(manager.ControlTecladoTutor2.TextOff());
    }





}
