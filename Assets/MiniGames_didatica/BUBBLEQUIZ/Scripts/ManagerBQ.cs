using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
using MEC;
using TMPro;

public class ManagerBQ : OverridableMonoBehaviour {

    [TabGroup("Database")]
    [LabelText("Database de Perguntas")]
    public List<QuestionBQ> questions = new List<QuestionBQ>();
    [TabGroup("Database")]
    [LabelText("Database das Bolhas em Cena")]
    public List<ContainerBubblesBQ> bubbles = new List<ContainerBubblesBQ>();
    [TabGroup("Realtime")]
    [LabelText("Pergunta Atual")]
    public QuestionBQ currentItem;
    [TabGroup("Configurações")]
    [LabelText("Numero Maximo de Rodadas")]
    public int maxRounds;
    [TabGroup("Realtime")]
    [LabelText("Rodada Atual")]
    public int currentRounds;
    [TabGroup("Realtime")]
    [LabelText("Bolha com a resposta certa")]
    public ContainerBubblesBQ currentCorrectContainer;
    [TabGroup("Configurações")]
    public TextMeshProUGUI questionTextComponent;
    [TabGroup("Realtime")]
    [LabelText("Player Está Jogando?")]
    public bool isPlaying;
    [TabGroup("Configurações")]
    public Slider timerBarSlider;
    [TabGroup("Configurações")]
    public int maxTimer;
    [TabGroup("Configurações")]
    public TextMeshProUGUI scoreTextComponent;
    [TabGroup("Realtime")]
    [LabelText("Pontuação Atual")]
    public int currentScore;
    [TabGroup("Configurações")]
    [LabelText("Pontos Por Acerto")]
    public int scorePerCorrect;
    [TabGroup("Configurações")]
    public float timeToWaitTillReleaseBtn;
    [TabGroup("Configurações")]
    public Button releaseButton;
    [TabGroup("Configurações")]
    public GameObject textGameObjectWrong;
    [TabGroup("Configurações")]
    public TextMeshProUGUI textWrongComponent;
    [TabGroup("Configurações")]
    public string defaulTextWrong;
    [TabGroup("Configurações")]
    public string timeEndTextWrong;
    [TabGroup("Configurações")]
    public TextMeshProUGUI textCorrectComponent;
    [TabGroup("Configurações")]
    public GameObject textGameObjectCorrect;
    [TabGroup("Realtime")]
    public bool hasTimerRunning = false;
    [TabGroup("LOG")]
    public SDLHandler sdlLog;
    [TabGroup("LOG")]
    public GDLHandler gdlLog;

    public void Start() {
        questions.Suffle();
        isPlaying = false;
    }

    [ButtonGroup("main3")]
    [Button("Start Game")]
    public void StartGame() {
        ResetBubblesPos();
        currentItem = questions[currentRounds];
        //Text textfile;
        //questionTextComponent.text = currentItem.question;
        questionTextComponent.DOText(currentItem.question, 3f);
        currentItem.ShuffleAnswers();
        UpdateBubbles();
        SetClickEnableBubble(true);
        BubbleSetKinematic(false);
        isPlaying = true;
        StartTimer();
    }


    public void RoundVerifier() {
        ResetTimer();
        textGameObjectCorrect.SetActive(false);
        textGameObjectWrong.SetActive(false);
        releaseButton.gameObject.SetActive(false);
        currentCorrectContainer.SetFloating(false);
        currentCorrectContainer.FallWW();
        if (currentRounds >= (maxRounds - 1)) {
            //Game Ended
            //Chamar Painel final
            Debug.Log("Jogo Finalizado");
            isPlaying = false;
            sdlLog.Send();
            gdlLog.StopTimer();
            gdlLog.SaveLog();
        } else {
            currentRounds++;
            Invoke("StartGame", 2.5f);
        }
    }

    
    public void Correction(ContainerBubblesBQ _container) {
        if (_container == null || _container.canBeClicked) {
            WrongsBubbleFallWW();
            BubbleSetKinematic(true);
            StopTimer();
            SetClickEnableBubble(false);
            currentCorrectContainer.GoToCenter();
            currentCorrectContainer.SetFloating(true);
            if (_container != null && _container.isCorrect) {
                //Player Acertou           
                StringFast textCorrect = new StringFast();
                textCorrect.Clear();
                textCorrect.Append("Você Acertou e ganhou +").Append(scorePerCorrect).Append(" Pontos!");
                textCorrectComponent.SetText(textCorrect.ToString());
                textGameObjectCorrect.SetActive(true);
                //sdlLog.SaveEstatistica(true);
                Debug.Log("Você Acertou!");
                int inicialScore = currentScore;
                currentScore += scorePerCorrect;
                if (scoreTextComponent != null) scoreTextComponent.DOTextInt(inicialScore, currentScore, .5f);
                gdlLog.scoreAmount = currentScore;
            } else {
                //Player Errou ou o tempo acabau!
                if (_container == null) {
                    textWrongComponent.SetText(timeEndTextWrong);

                } else {
                    textWrongComponent.SetText(defaulTextWrong);
                    //sdlLog.SaveEstatistica(true);
                }
                textGameObjectWrong.SetActive(true);
                Debug.Log("Você Errou!");
            }
            //Liberar Botão para Iniciar Proxima Rodada.
            HasButtonRelease();
        } 
    }
    

    public void HasButtonRelease() {
        if (currentRounds >= (maxRounds - 1)) {
            //Mudar Texto do Botão pra FInalizar o Jogo.
        } else {
            //Mudar Texto to Botão pra Iniciar proxima Rodada.
        }
        releaseButton.gameObject.SetActive(true);

    }


    public ContainerBubblesBQ GetCorrectOne() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            if (bubbles[i].isCorrect) {
                return bubbles[i];
            }
        }
        return null;
    }

    public void UpdateBubbles() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].UpdateSprite(currentItem.answers[i].spriteAnswer);
            bubbles[i].isCorrect = currentItem.answers[i].isCorrect;
            if (bubbles[i].isCorrect) {
                currentCorrectContainer = bubbles[i];
            }
        }
    }




    [ButtonGroup("main")]
    [Button("Reset Bubbles Position")]
    public void ResetBubblesPos() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].ResetPosToFall();
        }
    }

    [ButtonGroup("main")]
    [Button("Move Bubbles to Start Position")]
    public void BubbleFallingToStart() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].DownToStart();
        }
    }

    [ButtonGroup("main")]
    [Button("Fall Bubbles")]
    public void BubblesFallWW() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].FallWW();
        }
    }

    [ButtonGroup("Main2")]
    [Button("Fall Only Wrong Ones")]
    public void WrongsBubbleFallWW() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            if (bubbles[i].isCorrect) {
                currentCorrectContainer = bubbles[i];
            } else {
                bubbles[i].FallWW();
            }
        }
    }

    [ButtonGroup("Main2")]
    [Button("Fall Current Correct")]
    public void CorrectOneFallNow() {
        currentCorrectContainer.FallWW();
    }

    public void BubblesStopFloating() {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].SetFloating(false);
        }
    }

    public void SetClickEnableBubble(bool _enable) {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].canBeClicked = _enable;
        }
    }

    [ButtonGroup("Timer")]
    [Button("Start Timer")]
    public void StartTimer() {
        hasTimerRunning = true;
        Timing.RunCoroutine(TimerSystem(), "TimerSystem");
    }

    [ButtonGroup("Timer")]
    [Button("Stop Timer")]
    public void StopTimer() {
        hasTimerRunning = false;
        //Timing.KillCoroutines("TimerSystem");
    }

    [ButtonGroup("Timer")]
    [Button("Reset Timer")]
    public void ResetTimer() {
        timerBarSlider.maxValue = maxTimer;
        timerBarSlider.value = timerBarSlider.maxValue;
    }

    public IEnumerator<float> TimerSystem() {
        while (hasTimerRunning) {
            yield return Timing.WaitForSeconds(1f);
            //timerBarSlider.value -= 1f;
            timerBarSlider.DOValue(timerBarSlider.value - 1f, 0.8f);
            if (timerBarSlider.value <= timerBarSlider.minValue) {
                hasTimerRunning = false;
                Correction(null);
            }
        }
    }

    public void BubbleSetKinematic(bool _enable) {
        int tempCount = bubbles.Count;
        for (int i = 0; i < tempCount; i++) {
            bubbles[i].SetKinematic(_enable);
        }
    }
}
