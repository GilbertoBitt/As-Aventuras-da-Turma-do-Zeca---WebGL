using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MEC;

public class NurseFollow1_3A : OverridableMonoBehaviour {

	public Transform thisTransform;
#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
    public Rigidbody2D rigidbody;
#pragma warning restore CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
    public UnityStandardAssets._2D.PlatformerCharacter2D plataform2D;
	public float oldXValue = -1;
	public bool canWalk = false;
	public float speedNurse = 1f;
	private float oldSpeed;
    public float currentSpeed;
	public Animator enfermeira;
    public Transform playerTransform;
    public Manager1_3A manager;
    public Collider2D colliderThis;
    [HideInInspector]
    public float hspeed = -1;
	#region events
	public UnityEvent OnWalk;
    #endregion
    public float minDistance;
    public float maxDistance;
    
    

    [Header("Speeds")]
    public float VelocityMax;
    public float VelocityMiddle;
    public float VelocityMin;

    public float[] VelocityByLevel;

    bool firstTime = false;

    [Header("Camera")]
    public float CameraDistance;
    public Camera mainCamera;
    public float sizeCamera;
    public float defaulSizeCamera;
    public Vector3 offsetFollow;
    public Vector3 offsetFollowClose;

    public bool isPlayerClose = false;
    public bool isCameraFar = false;

    public float transitionDuration = 1.0f;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    private bool cameraCanActive;
    public bool hasCollide = false;
    bool checkEnd;
    public float oldCurrentSpeed;
    public LayerMask physicsLayer;
    private Transform thisTransformComponent;
    public Transform spriteTransform;
    public float smoothTime;
    public float offsetY;
    public int sqrLen;
    public Vector2 distance;
    public bool endGameP;
	private CoroutineHandle cameraAnimation;
    void Start(){
        checkEnd = false;
        enfermeira = GetComponent<Animator> ();
        defaulSizeCamera = mainCamera.orthographicSize;
        offsetFollowClose = manager.offsetFollow;
        playerTransform = plataform2D.transform;
        thisTransformComponent = this.transform;
    }

    public override void UpdateMe() {
        if (playerTransform != null) {
            float distance = Vector2.Distance(this.transform.position, playerTransform.position);
            //distance = thisTransformComponent.position - playerTransform.position;
            //sqrLen = Mathf.RoundToInt(distance.sqrMagnitude);           


			if (distance >= minDistance && distance < maxDistance) {
                currentSpeed = VelocityMiddle;
                if (oldCurrentSpeed != currentSpeed) {
                    oldCurrentSpeed = currentSpeed;
                    rigidbody.velocity = new Vector2(hspeed * currentSpeed, rigidbody.velocity.y);
                }
			} else if (distance >= maxDistance) {               
                currentSpeed = VelocityMax; 
                if (oldCurrentSpeed != currentSpeed) {
                    oldCurrentSpeed = currentSpeed;
                    rigidbody.velocity = new Vector2(hspeed * currentSpeed, rigidbody.velocity.y);
                }
            }

            if (rigidbody.velocity.x != (hspeed * currentSpeed)) {
                //oldCurrentSpeed = currentSpeed;
                rigidbody.velocity = new Vector2(hspeed * currentSpeed, rigidbody.velocity.y);
            }

			if (distance <= CameraDistance && isPlayerClose == false) {
                isPlayerClose = true;
			} else if(isPlayerClose == true && distance > CameraDistance) {
                isPlayerClose = false;
            }

            if (manager.playerisOnDefault) {
				if (isPlayerClose && !isCameraFar) {
					isCameraFar = true;
					Timing.KillCoroutines(cameraAnimation);
					cameraAnimation = Timing.RunCoroutine(CameraZoomOut());
				} else if (!isPlayerClose && isCameraFar) {
					if (!manager.checkCamZomm) {
						isCameraFar = false;
						Timing.KillCoroutines(cameraAnimation);
						cameraAnimation = Timing.RunCoroutine(CameraZoomIn());
					}
				}
            }

            //spriteTransform
            float interpolation2 = smoothTime * Time.deltaTime;
            Vector3 posS;
            posS.y = Mathf.Lerp(spriteTransform.position.y, playerTransform.transform.position.y + offsetY, interpolation2);
            posS.x = spriteTransform.position.x;
            posS.z = spriteTransform.position.z;
            spriteTransform.position = posS;
        }

       
    }

    public void RunCameraZoomOut() {
        Timing.KillCoroutines("ZoomCam");
        Timing.RunCoroutine(CameraZoomOut(),"ZoomCam");
    }
   

    public void StartWalk(){
        //Invoke("StartVelocity", 0f);
        StartVelocity();

    }

	public void StartVelocity(){
        playerTransform = manager.playerTransform;
        canWalk = true;
        //float hspeed = CrossPlatformInputManager.GetAxis("Horizontal");
        float result = speedNurse;
		float speed = result * 2;
        currentSpeed = speed;
        oldSpeed = currentSpeed;
		rigidbody.velocity = new Vector2(hspeed * speed, rigidbody.velocity.y);
		//enfermeira.SetInteger("NumbEnfer", 3);
	}


    IEnumerator<float> CameraZoomOut() {
        //if (!manager.checkCamZomm) {
            float startSize = mainCamera.orthographicSize;
            float endSize = sizeCamera;
            Vector3 endOffset;
            Vector3 startOffset = manager.offsetFollow;
            if (checkEnd == false) {
                endOffset = new Vector3(0f,0f,0f);
            } else {
                endOffset = new Vector3(-3f,-5f,0f);
            }

            float times = 0.0f;
            while (times < transitionDuration) {
                times += Time.deltaTime;
                float s = times / transitionDuration;
                mainCamera.orthographicSize = Mathf.Lerp(startSize,endSize,transitionCurve.Evaluate(s));
                manager.offsetFollow = Vector3.Lerp(startOffset,endOffset,transitionCurve.Evaluate(s));
                yield return 0f;
            }

            isCameraFar = true;
			//manager.playerisOnDefault = true;
       // }
    }

    public void CameraZoomOutIV() {
        checkEnd = true;
		Timing.KillCoroutines(cameraAnimation);
		cameraAnimation = Timing.RunCoroutine(CameraZoomOut());

    }

    IEnumerator<float> CameraZoomIn() {
        if (!manager.checkCamZomm) {
            float startSize = mainCamera.orthographicSize;
            float endSize = defaulSizeCamera;

            Vector3 startOffset = manager.offsetFollow;
            Vector3 endOffset = manager.defaultOffset;

            float times = 0.0f;
            while (times < transitionDuration) {
                times += Time.deltaTime;
                float s = times / transitionDuration;
                mainCamera.orthographicSize = Mathf.Lerp(startSize,endSize,transitionCurve.Evaluate(s));
                manager.offsetFollow = Vector3.Lerp(startOffset,endOffset,transitionCurve.Evaluate(s));
                yield return 0f;
            }

            isCameraFar = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && hasCollide == false) {
            hasCollide = true;
           // Debug.Log("Collider Name: " + collision.gameObject.name);
            manager.HitNurseSound();
            Timing.RunCoroutine(CatchPlayer(),"seringaHIT");

            
        }
    }

    void HasCollideBack() {
        hasCollide = false;
    }


    public IEnumerator<float> CatchPlayer() {
      
        hspeed = 0f;
        //manager.nurseHit = true;
        rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        // manager.batendoCaixa = true;
        // manager.animPerson.SetBool("Caindo", true);
        if (manager.batendoCaixa == false) {
            Timing.KillCoroutines("DeslizandoLeite");
            manager.animPerson.SetBool("DelizandoLeite", false);
            manager.batendoCaixa = true;
          //  manager.animPerson.SetBool("DelizandoLeite", false);
            manager.animPerson.SetBool("BatendoCaixa", true);
            manager.plataformController2d.collVoando.enabled = false;
            manager.StopRunning();
           // Timing.KillCoroutines("DeslizandoLeite");

        } else if (manager.batendoCaixa == false && manager.deslizandoLeite == true) {
            Timing.KillCoroutines("DeslizandoLeite");

            manager.animPerson.SetBool("DelizandoLeite", false);
            manager.animPerson.SetBool("BatendoCaixa", true);
            manager.plataformController2d.collVoando.enabled = false;
            manager.StopRunning();
            //Timing.KillCoroutines("DeslizandoLeite");
        } else {
            Timing.KillCoroutines("DeslizandoLeite");

            manager.animPerson.SetBool("DelizandoLeite", false);
            manager.animPerson.SetBool("BatendoCaixa", true);
            manager.plataformController2d.collVoando.enabled = false;
            manager.StopRunning();
         //   Timing.KillCoroutines("DeslizandoLeite");

        }
        //manager.StopRunning();
        manager.plataformControlerUser.m_slide = false;
        manager.CrounchEnd();
        manager.ButtonsEnable(false);
        enfermeira.SetInteger("NumbEnfer", 4);
        if (manager.playerLifes >= 1) {
            manager.playerLifes -= 1;
            manager.UpdateLifeText();
            yield return Timing.WaitForSeconds(4f);            
           // manager.ButtonsEnable(true);
           // manager.VoltarCorrer();
          //  manager.nurseHit = false;
            enfermeira.SetInteger("NumbEnfer", 3);
            yield return Timing.WaitForSeconds(1f);
            /*float result = speedNurse;
            float speed = result * 2;
            currentSpeed = speed;
            oldSpeed = currentSpeed;
            rigidbody.velocity = new Vector2(hspeed * speed, rigidbody.velocity.y);*/
            
            while(manager.ReturnHorizontalAxis() == 0) {
                yield return Timing.WaitForSeconds(.5f);
            }

            HasCollideBack();
            yield return Timing.WaitForSeconds(3f);
            float result = speedNurse;
            float speed = result * 2;
            currentSpeed = speed;
            oldSpeed = currentSpeed;
            hspeed = -1f;
            rigidbody.velocity = new Vector2(hspeed * speed, rigidbody.velocity.y);
            
        } else {
            //GO TO DIDATICA!
            // Debug.Log("endGame Here");
            //manager.GoToDidatica();
            endGameP = true;
            manager.plataformController2d.EndedByLife = true;
            //Debug.Log("3");
            manager.LostSoundError();
            manager.TransitionDidaticaByLife();

        }

    }

    public void KillCoroutines() {
        Timing.KillCoroutines();
        StopAllCoroutines();
    }

    public void UpdateSpeed() {
        rigidbody.velocity = new Vector2(hspeed * currentSpeed, rigidbody.velocity.y);
    }

}
