using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour {

	public GameConfig gameConfig;

	public Text scoreText;
	public int scoreAmount;
	public Text bropsText;
	public int bropsAmount;
	public Text totalPoints;
	public int totalPointsAmount;
	public Text endMessage;
	public int starsAmount;
	public Transform starsParent;
	public Sprite fullStars;
	public Sprite EmptyStars;
	public float transferDurationTime;
	public int idMinigame;
	public AnimationCurve transferCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public UnityEvent OnFinalStart;
	public UnityEvent OnTransferStart;
	public UnityEvent OnPlayAgain;
	public LoadManager loadManager2;
    public SoundManager soundManager;
    public PanelDesafioControl PanelDesafioControl2;
    public AudioClip audio1;
   // public PanelDesafioControl PanelDesafioControl2;

    // Use this for initialization
    void Start () {
		bropsAmount = gameConfig.BropsAmount;
		totalPointsAmount = gameConfig.TotalPoints;
		totalPoints.text = totalPointsAmount.ToString ();
		scoreText.text = scoreAmount.ToString ();
		bropsText.text = bropsAmount.ToString ();
        soundManager = FindObjectOfType<SoundManager>();
        PanelDesafioControl2 = FindObjectOfType<PanelDesafioControl>();
        audio1 = PanelDesafioControl2.audio[1];





    }
	
	// Update is called once per frame


	public void startTransfer(){
		OnTransferStart.Invoke ();
		StartCoroutine (PointsStart ());
        
    }

	public IEnumerator PointsStart(){

        Debug.Log("rankSaved");
        gameConfig.Rank(idMinigame, scoreAmount, starsAmount);
        int bropsTarget = bropsAmount + scoreAmount;
        gameConfig.BropsAmount = bropsTarget;
        gameConfig.BropsDeviceAmount += scoreAmount;
        int scoreT = totalPointsAmount + scoreAmount;
        gameConfig.TotalPointsDevice += scoreAmount;
        gameConfig.TotalPoints = scoreT;
        gameConfig.UpdateScore(gameConfig.BropsAmount, gameConfig.TotalPoints);

        Debug.Log(" currentUser  --- " + gameConfig.currentUser.ToString());
        Debug.Log(" playerID  --- " + gameConfig.playerID);
        Debug.Log(" scoreAmount  --- " + scoreAmount);
        Debug.Log(" bropsTarget  --- " + scoreAmount);

        float times = 0.0f;
		int scoreTarget = scoreAmount;
		//int bropsTarget = bropsAmount + scoreAmount;
		//int scoreT = totalPointsAmount + scoreAmount;
		while (times < transferDurationTime)
		{
			times += Time.deltaTime;
			float s = times / transferDurationTime;

			int score = (int)Mathf.Lerp (0, scoreTarget, transferCurve.Evaluate (s));
			scoreText.text = score.ToString ();

			//bucketTextComponent.color = Color.Lerp (startColor, colors, transferCurve.Evaluate (s));
			yield return Yielders.EndOfFrame;
		}        
	}

	public IEnumerator StartBrops(){
		float times = 0.0f;
		int bropsTarget = bropsAmount + scoreAmount;
		//gameConfig.BropsAmount = bropsTarget;
		while (times < transferDurationTime)
		{
			times += Time.deltaTime;
			float s = times / transferDurationTime;

			int brops = (int)Mathf.Lerp (bropsAmount, bropsTarget, transferCurve.Evaluate (s));
			bropsText.text = brops.ToString ();

			//bucketTextComponent.color = Color.Lerp (startColor, colors, transferCurve.Evaluate (s));
			yield return Yielders.EndOfFrame;
		}

        
	}

	public void UpdateGameScore(){
		
	}

	public IEnumerator StartTotalPoints(){
		float times = 0.0f;
		int scoreT = totalPointsAmount + scoreAmount;
		gameConfig.TotalPoints = scoreT;
		while (times < transferDurationTime)
		{
			times += Time.deltaTime;
			float s = times / transferDurationTime;

			int scory = (int)Mathf.Lerp (totalPointsAmount, scoreT, transferCurve.Evaluate (s));
			totalPoints.text = scory.ToString ();
			yield return Yielders.EndOfFrame;
		}

        //gameConfig.UpdateScore(scoreAmount);
    }

	public void setScore(int score){
		scoreAmount = score;
	}

	public void stars(){

		for (int i = 0; i < 3; i++) {
			starsParent.GetChild (i).GetComponent<Image> ().sprite = EmptyStars;
		}

		for (int i = 0; i < starsAmount; i++) {
			starsParent.GetChild (i).GetComponent<Image> ().sprite = fullStars;
		}

		EndMessage ();

	}

	public void EndMessage(){
	
		switch (starsAmount) {
		case 0:
			endMessage.text = "Muito bem! Tente melhorar.";
			break;
		case 1:
			endMessage.text = "Parabéns! Bom trabalho.";
			break;
		case 2:
			endMessage.text = "UAU, você foi ótimo!";
			break;
		case 3:
			endMessage.text = "Sensacional! Parabéns!";
			break;
		default:
			endMessage.text = "Sensacional! Parabéns!";
			break;
		}	

	}

	public void PlayAgainGame(){
        if (soundManager == null) {
            soundManager = FindObjectOfType<SoundManager>();

        }
        if (PanelDesafioControl2 == null || audio1 == null) {
            PanelDesafioControl2 = FindObjectOfType<PanelDesafioControl>();
            audio1 = PanelDesafioControl2.audio[1];
        }
        soundManager.startVoiceFX(audio1);
        //  this.soundManager.startVoiceFXReturn(this.audio1);
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        

    }

	public void BackToMenu(){
        if (soundManager == null) {
            soundManager = FindObjectOfType<SoundManager>();

        }
        if (PanelDesafioControl2 == null || audio1==null) {
            PanelDesafioControl2 = FindObjectOfType<PanelDesafioControl>();
            audio1 = PanelDesafioControl2.audio[1];
        }
       soundManager.startVoiceFX(audio1);
        
        LoadManager load = GameObject.FindObjectOfType<LoadManager> ();
        if (load != null) {
			load.LoadAsync ("selectionMinigames");
		} else {
			//Debug.Log ("LoadManager Nulled");
		}
	}

	public void saveGame(){
		if (gameConfig.hasCredentials) {
			if (gameConfig.isOnline) {
				//offlineSave();
			} else {
				//offlineSave();
			}
		}
	}

	public void offlineSave(){
		int scoreT = totalPointsAmount + scoreAmount;
		gameConfig.TotalPoints = scoreT;
		int bropsTarget = bropsAmount + scoreAmount;
		gameConfig.BropsAmount = bropsTarget;
		
	}

    public void OnlineSave() {
        //gameConfig.netHelper.
    }
}
