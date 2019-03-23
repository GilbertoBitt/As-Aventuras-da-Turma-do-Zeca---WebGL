using MEC;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class ManagerEF02MA09 : OverridableMonoBehaviour{
    public GDLHandler gdlHandler;
    public SDLHandler sdlHandler;
    private bool isMobile = false;
    private StringFast stringOpt;
    public TextMeshProUGUI textScoreBrops;
    [Header("Player Settings")] public KeyCode upKey;
    public KeyCode downKey;
    public Transform playerTransform;
    public BoxCollider2D playerCollider;
    public Rigidbody2D playerRigidBody2D;
    public float stepSize = 150;
    public float speed = 100;
    public float speedRigdbody2D = 2f;
    public float targetY;
    public float VerticalVirtualAxis = 0f;
    public bool checkMove = false;
    public float VerticalOld = 0f;
    public float speedVertical;
    public int[] playerFounded;
    public int playerCount;

    [Space(10)] [Header("Touch Settings")] public float initialPosition;
    public float currentPosition;
    public float deltaPositionY;
    public bool hasTouch;

    [Space(10)] [Header("Camera Settings")]
    public Camera mainCamera;

    public Rect rectCamera;
    public float maxCameraY;
    public float minCameraY;
    private float heightCamera;
    private float widthCamera;
    private Vector3 PositionCam;

    [Space(10)] [Header("Background Settings")]
    public List<Material> scrollingBackground = new List<Material>();

    public float[] scrollingSpeed;
    private float[] pos;
    public bool scrollingKeep = false;
    private int spritesCount;

    [Space(10)] [Header("Clouds Settings")]
    public float cloudsMovementSpeed;

    public float cloudsStepSize;

    [Space(10)] [Header("Instancer Settings")]
    public bool startInstancer = false;

    public float timeSpeedToInstanciate;
    public Transform[] instancerPos;
    [HideInInspector] public Queue<CloudsEF02MA09> cloudsPool = new Queue<CloudsEF02MA09>();
    public List<CloudsEF02MA09> cloudsInstanced = new List<CloudsEF02MA09>();
    public GameObject cloudsPrefab;
    public int cloudsPoolSize;
    public Transform parentPool;

    [Space(10)] [Header("Main Settings")] public bool isAscending;
    public int minRandomChar;
    public int maxRandomChar;
    public int[] randomChars;
    public int amountRandom;
    public int currentLevel = -1;
    public int maxLevel = 4;
    public bool isPlaying = false;
    public int scorePerCorrentSequence = 10;
    public int currentBrops;

    [Space(10)] [Header("Bubble Settings")]
    public BubbleEF02MA09[] bubblesInstance;

    [Space(10)] [Header("Timer Settings")] public bool startTimer = false;
    public TextMeshProUGUI timerText;
    public float inicialTimer;
    public float currentTimer;
    public int minutes = 0;
    public int seconds = 0;

    public Vector3[] screenCorners;
    public Vector3[] screenCornersWorldPoint;
    public float xMinWorld;
    public float xMaxWorld;
    public float yMinWorld;
    public float yMaxWorld;
    public float playerPosY;
    public float mousePosY;
    public float deltaPosY;

    public Transform[] transfNumb;
    public Vector3[] posNumber;
    public GameObject panelFinal;
    public GameObject panelInteragindo;
    public GameObject PanelConfirm;
    public Transform TransformReferenctPos;
    public Transform PanelMiddleTransform;
    public Vector3 initPosPanel;

    protected override void Awake(){
        base.Awake();
        heightCamera = 2f * mainCamera.orthographicSize;
        widthCamera = heightCamera * mainCamera.aspect;
        rectCamera = mainCamera.rect;
        initPosPanel = PanelMiddleTransform.position;
    }

    public void Start(){
        cloudsPool = new Queue<CloudsEF02MA09>();
        screenCorners = new Vector3[2];
        screenCorners[0] = new Vector2(rectCamera.xMin, rectCamera.yMin);
        screenCorners[1] = new Vector2(rectCamera.xMax, rectCamera.yMax);

        screenCornersWorldPoint = new Vector3[screenCorners.Length];
        for (int i = 0; i < screenCorners.Length; i++){
            screenCornersWorldPoint[i] = mainCamera.ViewportToWorldPoint(screenCorners[i]);
        }

        posNumber = new Vector3[6];

        for (int i = 0; i < posNumber.Length; i++){
            posNumber[i] = new Vector3(transfNumb[i].transform.position.x, transfNumb[i].transform.position.y,
                transfNumb[i].transform.position.z);
        }


        xMinWorld = screenCornersWorldPoint[0].x;
        xMaxWorld = screenCornersWorldPoint[1].x;
        yMinWorld = screenCornersWorldPoint[0].y;
        yMaxWorld = screenCornersWorldPoint[1].y;

        Input.multiTouchEnabled = false;
        stringOpt = new StringFast();
        heightCamera = 2f * mainCamera.orthographicSize;
        widthCamera = heightCamera * mainCamera.aspect;
        spritesCount = scrollingBackground.Count;
        startInstancer = false;
        InstantiatorPreparation();
        currentLevel = 0;
        pos = new float[spritesCount];
        for (int i = 0; i < spritesCount; i++){
            pos[i] = 0;
        }

        StartGame();
        gdlHandler.StartTimer();
    }

    public void SetRidbodyVelocityY(float _y){
        Vector2 newVelocity = playerRigidBody2D.velocity;
        newVelocity.y = _y;
        playerRigidBody2D.velocity = newVelocity;
    }

    public bool isPlayerInsideCameraBounds(float _posY){
        if (_posY > yMaxWorld){
            return false;
        } else if (_posY < yMinWorld){
            return false;
        } else{
            return true;
        }
    }

    public void StartPreLoad(){
        //Reseta lista de itens encontrados pelo player;
        playerFounded = new int[amountRandom];
        playerCount = 1;
        //randomizando nova ordem de numeros.
        RandomNumberOrder();
    }

    public void hasEndedGame(){
        if (currentLevel > maxLevel){
            gdlHandler.StopTimer();
            sdlHandler.Send();
            gdlHandler.currentLevel = this.currentLevel;
            gdlHandler.SaveLog();
            isPlaying = false;
            panelInteragindo.SetActive(true);
            panelFinal.GetComponent<Animator>().SetBool("dirDesadio", true);
            //Chamar interagindo | Fim do Game | Proxima Interação.
        } else{
            StartGame();
        }
    }

    public void StartGame(){
        //Inicia novamente o Game.
        isMobile = false;
        startInstancer = true;
        startTimer = true;
        scrollingKeep = true;
        RandomNumberOrder();
        Timing.RunCoroutine(GameTimer());
        Timing.RunCoroutine(InstancerTimer());
        isPlaying = true;
    }

    public override void UpdateMe(){
        if (isPlaying){
            PlayerMovimentationMobile();
            PlayerMovimentationStandalone();
            ParallaxController();
        }
    }

    public void RandomNumberOrder(){
        randomChars = new int[amountRandom];
        playerFounded = new int[amountRandom];
        playerCount = 1;
        isAscending = (currentLevel % 2) == 0 ? true : false;
        //isAscending = false;
        if (isAscending){
            randomChars[0] = Random.Range(minRandomChar, maxRandomChar - (amountRandom));
            for (int i = 1; i < amountRandom; i++){
                randomChars[i] = randomChars[0] + i;
            }

            HideAllTextBubble();
            RevealBubble(0);
        } else{
            randomChars[0] = Random.Range(minRandomChar + (amountRandom), maxRandomChar);
            for (int i = 1; i < amountRandom; i++){
                randomChars[i] = randomChars[0] - i;
            }

            HideAllTextBubble();
            RevealBubble(0);
        }
    }

    public void PlayerMovimentationStandalone(){
        Sequence virtualVertical = DOTween.Sequence();
        virtualVertical.SetId(001);

        if (Input.GetKeyDown(upKey)){
            if (virtualVertical.IsPlaying()){
                DOTween.Kill(001, false);
                virtualVertical = DOTween.Sequence();
                virtualVertical.SetId(001);
            }

            virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, -1f, speedVertical));
            checkMove = true;
        } else if (Input.GetKeyDown(downKey)){
            if (virtualVertical.IsPlaying()){
                DOTween.Kill(001, false);
                virtualVertical = DOTween.Sequence();
                virtualVertical.SetId(001);
            }

            virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, 1f, speedVertical));
            checkMove = true;
        } else if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey)){
            if (virtualVertical.IsPlaying()){
                DOTween.Kill(001, false);
                virtualVertical = DOTween.Sequence();
                virtualVertical.SetId(001);
            }

            virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, 0, speedVertical));
            checkMove = false;
        }

        if (Input.GetKey(upKey)){
            //Debug.Log("Going Up",this);
            targetY = playerTransform.position.y + stepSize;

            ApplyPlayerPos();
        } else if (Input.GetKey(downKey)){
            //Debug.Log("Going Down",this);
            targetY = playerTransform.position.y - stepSize;
            ApplyPlayerPos();
        }
    }

    public void PlayerMovimentationMobile(){
        //mousePosY = Input.mousePosition.y;
        //playerPosY = playerTransform.position.y;

        if (Input.GetMouseButton(0)){
            Sequence virtualVertical = DOTween.Sequence();
            virtualVertical.SetId(001);
            deltaPosY = mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
            float sensibility = deltaPosY - playerTransform.position.y;
            if (Mathf.Abs(sensibility) < 0.1f || !isPlayerInsideCameraBounds(deltaPosY)){
                SetRidbodyVelocityY(0f);

                if (virtualVertical.IsPlaying()){
                    DOTween.Kill(001, false);
                    virtualVertical = DOTween.Sequence();
                    virtualVertical.SetId(001);
                }

                virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, 0, speedVertical));
                checkMove = false;
            } else if (sensibility > 0.1f && playerTransform.position.y < deltaPosY &&
                       playerRigidBody2D.velocity.y != speedRigdbody2D && isPlayerInsideCameraBounds(deltaPosY)){
                SetRidbodyVelocityY(speedRigdbody2D);

                if (virtualVertical.IsPlaying()){
                    DOTween.Kill(001, false);
                    virtualVertical = DOTween.Sequence();
                    virtualVertical.SetId(001);
                }

                virtualVertical.Append(
                    DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, -1f, speedVertical));
                checkMove = true;
            } else if (sensibility < -0.1f && playerTransform.position.y > deltaPosY &&
                       playerRigidBody2D.velocity.y != -speedRigdbody2D && isPlayerInsideCameraBounds(deltaPosY)){
                SetRidbodyVelocityY(-speedRigdbody2D);


                if (virtualVertical.IsPlaying()){
                    DOTween.Kill(001, false);
                    virtualVertical = DOTween.Sequence();

                    virtualVertical.SetId(001);
                }

                virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, 1f,
                    speedVertical));
                checkMove = true;
            }
        }

        if (Input.GetMouseButtonUp(0)){
            Sequence virtualVertical = DOTween.Sequence();
            virtualVertical.SetId(001);
            SetRidbodyVelocityY(0f);
            if (virtualVertical.IsPlaying()){
                DOTween.Kill(001, false);
                virtualVertical = DOTween.Sequence();
                virtualVertical.SetId(001);
            }

            virtualVertical.Append(DOTween.To(x => VerticalVirtualAxis = x, VerticalVirtualAxis, 0, speedVertical));
            checkMove = false;
        }
    }

    public void ApplyPlayerPos(){
        Vector3 pos = playerTransform.position;
        float posY = pos.y;
        pos.y = targetY;
        Vector3 finalPos = CameraBoundsPosition(pos);
        pos.y = Mathf.Lerp(posY, finalPos.y, speed * Time.deltaTime);
        playerTransform.position = pos;
    }

    public Vector3 CameraBoundsPosition(Vector3 newPos){
        Vector3 camViewPoint = mainCamera.WorldToViewportPoint(newPos);
        camViewPoint.y = Mathf.Clamp(camViewPoint.y, minCameraY, maxCameraY);
        return mainCamera.ViewportToWorldPoint(camViewPoint);
    }

    public void ParallaxController(){
        if (scrollingKeep){
            for (int i = 0; i < spritesCount; i++){
                pos[i] += scrollingSpeed[i];
                if (pos[i] > 1.0){
                    pos[i] -= 1.0f;
                }

                scrollingBackground[i].mainTextureOffset = new Vector2(pos[i], 0);
            }
        }
    }

    public IEnumerator<float> InstancerTimer(){
        yield return Timing.WaitForSeconds(1f);
        while (startInstancer){
            yield return Timing.WaitForSeconds(timeSpeedToInstanciate);
            InstantiatorSystem();
        }
    }

    public IEnumerator<float> GameTimer(){
        while (startTimer){
            yield return Timing.WaitForSeconds(1f);
            currentTimer -= 1f;
            minutes = Mathf.FloorToInt(currentTimer / 60f);
            seconds = Mathf.FloorToInt(currentTimer % 60f);
            UpdateTimerText();
            if (currentTimer <= 0f){
                startTimer = false;
                Timing.KillCoroutines("EndGameCoroutine");

                Timing.RunCoroutine(EndTheGame(), "EndGameCoroutine");
            }
        }
    }

    public void UpdateTimerText(){
        if (currentTimer > 0f){
            stringOpt.Clear();
            stringOpt.Append(minutes).Append(":").Append(seconds.ToString("00"));
            timerText.SetText(stringOpt.ToString());
        } else{
            timerText.SetText("0:00");
        }
    }

    public void InstantiatorSystem(){
        int randomAmount = Random.Range(1, 4);
        //Debug.Log("Start Instantiator System");
        instancerPos.Suffle();
        for (int i = 0; i < randomAmount; i++){
            if (cloudsPool.Count >= 1){
                CloudsEF02MA09 cloudsInstance = cloudsPool.Dequeue();
                cloudsInstanced.Add(cloudsInstance);
                cloudsInstance.cloudTransform.position = instancerPos[i].position;
                cloudsInstance.StartCloudMovement();
            } else{
                GameObject clouds = Instantiate(cloudsPrefab, parentPool, false) as GameObject;
                CloudsEF02MA09 cloudsInstance = clouds.GetComponent<CloudsEF02MA09>();
                cloudsInstanced.Add(cloudsInstance);
                cloudsInstance.manager = this;
                clouds.transform.position = instancerPos[i].position;
                cloudsInstance.StartCloudMovement();
            }
        }
    }

    public void InstantiatorPreparation(){
        for (int i = 0; i < cloudsPoolSize; i++){
            GameObject clouds = Instantiate(cloudsPrefab, parentPool, false) as GameObject;
            CloudsEF02MA09 cloudsInstance = clouds.GetComponent<CloudsEF02MA09>();
            cloudsInstance.manager = this;
            clouds.transform.position = instancerPos[0].position;
            clouds.SetActive(false);
            cloudsPool.Enqueue(cloudsInstance);
        }
    }

    private CoroutineHandle wrongTextCoroutineHandler;

    public void PlayerGotCloud(int _found, Vector3 cloudPos){
        if (!isPlaying) return;
        int countTemp = randomChars.Length;
        playerFounded[playerCount] = randomChars[playerCount];
        int bubblePos = playerCount;
        if (!isAscending){
            bubblePos = amountRandom - playerCount;
            bubblePos--;
        }
        
        bubblesInstance[bubblePos].founded = true;
        playerCount++;

        if (playerCount >= amountRandom){
            VerticalVirtualAxis = 0f;
            playerRigidBody2D.velocity = Vector2.zero;
            
            isPlaying = false;
            startTimer = false;
            scrollingKeep = false;
            startInstancer = false;
            isPlaying = false;
            bubblesInstance[bubblePos].ShowTextStopTimer(_found, cloudPos, PanelConfirm, PanelMiddleTransform.DOMove(TransformReferenctPos.position, .5f));
        } else{
            bubblesInstance[bubblePos].ShowText(_found, cloudPos);
        }
        //dar Pontuação.
        //IncreaseScore();
        //abrir tela de correção.
        //Timing.KillCoroutines("EndGameCoroutine");
        //Timing.RunCoroutine(EndTheGame(), "EndGameCoroutine");
    }

    public void ResetAll(){
        playerRigidBody2D.velocity = Vector2.zero;
        VerticalVirtualAxis = 0f;
        PanelMiddleTransform.DOMove(initPosPanel, .5f);
        HideAllTextBubble();
    }

    public void IncreaseScore(){
        int initBrops = currentBrops;
        int endBrops = currentBrops + scorePerCorrentSequence;
        gdlHandler.AddScore(endBrops);
    }

    public void CorrectionGame(){
        
        VerticalVirtualAxis = 0f;
        PanelConfirm.SetActive(false);
        Sequence correction = DOTween.Sequence();
        correction.timeScale = 1f;
        if (!isAscending){
            for (int i = 0; i < amountRandom - 1; i++){
                //bubblesInstance[i].HideText();
                correction.AppendCallback(() => bubblesInstance[i].iconOk.enabled = true);
                correction.Append(bubblesInstance[i].IsCorrect(randomChars[i]));
                if (!bubblesInstance[i].isCorrected(randomChars[i])){
                    correction.Append(bubblesInstance[i].ShowCorrect(randomChars[i]));
                } else{
                    correction.AppendCallback(IncreaseScore);
                }
            }
        } else{
            for (int i = 1; i < amountRandom; i++){
                //bubblesInstance[i].HideText();
                correction.AppendCallback(() => bubblesInstance[i].iconOk.enabled = true);
                correction.Append(bubblesInstance[i].IsCorrect(randomChars[i]));
                bool isCorrect = bubblesInstance[i].isCorrected(randomChars[i]);
                if (!bubblesInstance[i].isCorrected(randomChars[i])){
                    correction.Append(bubblesInstance[i].ShowCorrect(randomChars[i]));
                } else{
                    correction.AppendCallback(IncreaseScore);
                }
            }
        }
        
        correction.AppendCallback(() => Timing.RunCoroutine(EndTheGame()));
        
        int tempCount = cloudsInstanced.Count;
        for (int i = 0; i < tempCount; i++){
            if (cloudsInstanced.Count >= 1){
                cloudsInstanced[0].ResetCloud();
            }
        }

        startTimer = false;
        scrollingKeep = false;
        startInstancer = false;
        isPlaying = false;
        playerCount = 1;
        Time.timeScale = 1f;
        correction.Play();
    }
    

    private void HideAllTextBubble(){
        PanelConfirm.SetActive(false);
        if (!isAscending){
            for (int i = 0; i < amountRandom - 1; i++){
                bubblesInstance[i].HideText();
                bubblesInstance[i].founded = false;
            }
        } else{
            for (int i = 1; i < amountRandom; i++){
                bubblesInstance[i].HideText();
                bubblesInstance[i].founded = false;
            }
        }
        
        for (int i = 0; i < amountRandom; i++){
            bubblesInstance[i].HideOk();
        }
        startTimer = true;
        scrollingKeep = true;
        startInstancer = true;
        Time.timeScale = 1f;
        playerCount = 1;
        Timing.RunCoroutine(InstancerTimer());
        Timing.RunCoroutine(GameTimer());
        isPlaying = true;
    }

    public IEnumerator<float> EndTheGame(){
        //Tela de Parabéns [IVO]
        SetRidbodyVelocityY(0f);
        yield return Timing.WaitForSeconds(2f);
        currentLevel++;

        //Remove nuvens instanciadas em tela.
        int tempCount = cloudsInstanced.Count;
        for (int i = 0; i < tempCount; i++){
            if (cloudsInstanced.Count >= 1){
                cloudsInstanced[0].ResetCloud();
            }
        }

        //Reseta o timer.
        currentTimer = inicialTimer;
        startTimer = false;
        scrollingKeep = false;
        startInstancer = false;
        UpdateTimerText();

        StartPreLoad();
        HideAllTextBubble();
        //Chamar animação de proximo level.
        PanelMiddleTransform.DOMove(initPosPanel, .5f);
        yield return Timing.WaitForSeconds(3f);

        hasEndedGame();
    }

    public void RevealBubble(int i){
        //Debug.Log (i);
        playerFounded[i] = randomChars[i];
        int bubblePos = i;
        if (!isAscending){
            bubblePos = (amountRandom - 1) - i;
        }

        bubblesInstance[bubblePos].ShowText(playerFounded[i]);
    }
}