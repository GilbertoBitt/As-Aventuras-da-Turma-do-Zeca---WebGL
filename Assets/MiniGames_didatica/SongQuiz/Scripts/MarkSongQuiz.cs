using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using QuestionSystem.Scripts;
using TMPro;
using Sirenix.OdinInspector;

public class MarkSongQuiz : QuestionResult {

    public bool hasNoteAbove = false;
    public NoteSongQuiz noteAbove;
    //public NoteSongQuiz noteAboveEnter;
    public ManagerSongQuiz mgr;
    public List<NoteSongQuiz> noteSongs = new List<NoteSongQuiz>();
    public ParticleSystem waveParticle;
    public ParticleSystem startBurstParticle;
    public ParticleSystem startPuffParticleSystem;
    public TextMeshProUGUI textFeedBack;
    public TMP_ColorGradient gradientPerfect;
    public TMP_ColorGradient gradientGood;
    public TMP_ColorGradient gradientMiss;
    public TextMeshProUGUI comboText;
    public int countCombo = 0;
    public RadialSlideTool rslider;
    public bool isFeverTime;
    public float timerFever;
    public float rightValueFeverBarIncrease;
    public int correctWhileFeverTime;
    public Animator[] animPersonM;
    Animator animPerson;
    public Transform posReferenceParent;
    public GameObject prefabGameObject;
    public Queue<SQParticle> startParticles = new Queue<SQParticle>();
    public WsqController CtrlQuestions;
    int selp;

    public NoteSongQuiz CurrentNote;
    public float OnRightQuestionIncreaseValue = 20f;
    // Use this for initialization
    void Start () {
        if(noteSongs == null) {
            noteSongs = new List<NoteSongQuiz>();
        }
        isFeverTime = false;
        startParticles = new Queue<SQParticle>();

        for (int i = 0; i < 5; i++) {
            GameObject go = Instantiate(prefabGameObject, posReferenceParent.position, Quaternion.identity, posReferenceParent);
            SQParticle ps = go.GetComponent<SQParticle>();
            ps.msmgr = this;
            startParticles.Enqueue(ps);
        }
        selp = PlayerPrefs.GetInt("characterSelected", 0);
        animPerson = selp == 0 ? animPersonM[0] : animPersonM[1];

    }

    public SQParticle GetOneStarParticle()
    {
        return startParticles.Count >= 1 ? startParticles.Dequeue() : null;
    }

    public void VoltarAnim() {
        animPerson.SetInteger("danceAnim", 0);

    }
    public void OnTriggerEnter2D(Collider2D col) {
        noteAbove = col.GetComponent<NoteSongQuiz>();
        if (noteAbove.NoteType == NoteClassQ.NoteType.Question)
        {
            CurrentNote = noteAbove;
            mgr.PauseSongPosition();
            CtrlQuestions.OpenQuestionScreen();
        }
        else {
            noteSongs.Add(noteAbove);
        }
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        hasNoteAbove = noteSongs.Count >= 1;
    }

    public void OnTriggerExit2D(Collider2D col) {
        NoteSongQuiz exitSong = col.GetComponent<NoteSongQuiz>();
        if (!noteSongs.Contains(exitSong)) return;
        countCombo = 0;
        noteSongs.Remove(exitSong);
        //ShowTextAnimationCombo(countCombo.ToString());
        comboText.DOFade(0f, .8f);
    }

    public void TryNote(NoteClassQ.NoteType type) {
        if (!hasNoteAbove || noteSongs.Count < 1) return;
        NoteSongQuiz noteH = noteSongs[0];
        //waveParticle.Emit(1);
        noteH.canLerp = false;
        noteH.componentImage.DOFade(0f, .1f);
        noteH.transform.DOScale(Vector3.zero, .1f);
        noteSongs.Remove(noteH);            
        float offsetabs = Mathf.Abs(noteH.beatOfThisNote - mgr.songPosInBeats);
        if ((noteH.NoteType == type) && offsetabs < mgr.perfectRangeByBeats) {               
            ShowTextAnimation("Perfeito!", gradientPerfect);                
            Debug.Log("Perfeito!");
            animPerson.SetInteger("danceAnim", 2);
            if (!isFeverTime) {
                countCombo++;
                if (countCombo >= 1) {
                    mgr.scoreAmount += countCombo * mgr.scoreByRight;
                } else {
                    mgr.scoreAmount += mgr.scoreByRight;
                }
                AddScoreAnimation();
            } else {
                countCombo += 2;
                correctWhileFeverTime++;
            }
            GetOneStarParticle().ExecuteParticle();
            if (!isFeverTime) {
                IncreaseSliderSmoothly(rightValueFeverBarIncrease);
            }
            mgr.rightAmountTotal++;
        } else if ((noteH.NoteType == type) && offsetabs > mgr.perfectRangeByBeats && offsetabs < mgr.goodRangeByBeats) {
            ShowTextAnimation("Bom!", gradientGood);
            animPerson.SetInteger("danceAnim", 1);
            if (!isFeverTime) {
                countCombo++;
                if (countCombo >= 1) {
                    mgr.scoreAmount += countCombo * mgr.scoreByRight;
                } else {
                    mgr.scoreAmount += mgr.scoreByRight;
                }
                AddScoreAnimation();
            } else {
                countCombo += 2;
                correctWhileFeverTime++;
            }
            Debug.Log("Bom!");
            if (!isFeverTime) {
                IncreaseSliderSmoothly(rightValueFeverBarIncrease);
            }
            mgr.rightAmountTotal++;
        } else if ((noteH.NoteType != type) || offsetabs > mgr.goodRangeByBeats) {
            ShowTextAnimation("Errou!", gradientMiss);
            if (!isFeverTime) {
                countCombo = 0;
            }
            // animPerson.SetInteger("danceAnim", 0);
            // Debug.Log("Errou!");
            comboText.DOFade(0f, .8f);
        }
        //  Invoke("VoltarAnim", 2f);
        if (countCombo < 1) return;
        if (!isFeverTime) {
            ShowTextAnimationCombo(countCombo.ToString() + " \n Combo");
        } else {
            ShowTextAnimationCombo(countCombo.ToString() + " \n Combo 2x");
        }
    }

    public void ShowTextAnimation(string _string, TMP_ColorGradient _gradient) {
        textFeedBack.colorGradientPreset = _gradient;
        textFeedBack.SetText(_string);
        textFeedBack.DOFade(0f, 0.0001f);
        DOTween.Kill(005);
        Sequence animationText = DOTween.Sequence();
        animationText.SetId(005);
        animationText.Append(textFeedBack.transform.DOLocalJump(new Vector3(0f, -120f, 0f), 3f, 1, .5f, false));
        animationText.Join(textFeedBack.DOFade(1f,.8f));
        animationText.Append(textFeedBack.DOFade(0f, .8f));
    }

    public void ShowTextAnimationCombo(string _string) {
        if(countCombo >= 1) {
            startPuffParticleSystem.Emit(25);
        }
        comboText.SetText(_string);
        comboText.DOFade(0f, 0.0001f);
        DOTween.Kill(002);
        Sequence animationText = DOTween.Sequence();
        animationText.SetId(002);
        animationText.Append(comboText.transform.DOLocalJump(new Vector3(0f, 120f, 0f), 3f, 1, .5f, false));
        animationText.Join(comboText.DOFade(1f, .8f));     
        if(countCombo <= 0) {
            animationText.Append(comboText.DOFade(0f, .8f));
        }
    }

    [ButtonGroup("Buttons")]
    [Button("Slider Increase")]
    public void IncreaseByOne() {
        IncreaseSliderSmoothly(1f);
    }

    public void IncreaseSliderSmoothly(float _value) {        
        DOTween.Kill(008);
        float initValue = rslider.slider;
        Sequence increasSlider = DOTween.Sequence();
        increasSlider.SetId(008);
        increasSlider.Append(DOTween.To(() => rslider.slider, x => rslider.slider = x, initValue + _value, .5f));
        increasSlider.AppendCallback(ActivateFeverTime);
    }

    public void ActivateFeverTime() {
        if(rslider.slider >= 100f) {
            DecreaseSliderBar();
        }
    }

    [ButtonGroup("Buttons")]
    [Button("Slider Decrease")]
    public void DecreaseSliderBar() {
        DOTween.Kill(008);
        Sequence FeverTime = DOTween.Sequence();
        FeverTime.SetId(008);
        isFeverTime = true;
        FeverTime.Append(DOTween.To(() => rslider.slider, x => rslider.slider = x, 0, timerFever));
        FeverTime.AppendCallback(() => isFeverTime = false);
        FeverTime.AppendCallback(() => AddFeverTimeScore());
        FeverTime.AppendCallback(() => correctWhileFeverTime = 0);
    }

    public void AddFeverTimeScore() {
        mgr.scoreAmount += correctWhileFeverTime * countCombo;
        AddScoreAnimation();
    }

    public void AddScoreAnimation() {
        DOTween.Kill(48484, false);
        int currentScoreOnText = int.Parse(mgr.scoreText.text);
        Sequence scoreIncresing = DOTween.Sequence();
        scoreIncresing.SetId(48484);
        scoreIncresing.Append(mgr.scoreText.DOTextInt(currentScoreOnText, mgr.scoreAmount, .5f));
    }

    public override void IsCorrect(bool isRight)
    {
        if (isRight) {
            IncreaseSliderSmoothly(OnRightQuestionIncreaseValue);
            CurrentNote.canLerp = false;
            CurrentNote.componentImage.DOFade(0f, .1f);
            CurrentNote.transform.DOScale(Vector3.zero, .1f);
        }
        mgr.ResumeSong();
    }
}
