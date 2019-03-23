using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MEC;
using TMPro;
using DG.Tweening;

public class Manager_EF02LP33 : OverridableMonoBehaviour {
    [BoxGroup("Referencias Nescessarias", true, true)]
    [ListDrawerSettings(ShowPaging = true)]
    [BoxGroup("Referencias Nescessarias", true, true)]
    public List<Item_EF02LP33> wordsDatabase = new List<Item_EF02LP33>();
    [BoxGroup("Referencias Nescessarias")]
    public List<Item_EF02LP33> wordsDatabase03 = new List<Item_EF02LP33>();
    public List<Item_EF02LP33> WordsList {
        get{ return 1 == 3 ? wordsDatabase03 : wordsDatabase; }
    }
    [BoxGroup("ETC")]
    public bool isPlaying = false;
    [BoxGroup("Referencias Nescessarias", true, true)]
    public List<DropArea_EF02LP33> dropAreaDB = new List<DropArea_EF02LP33>();
    [BoxGroup("Referencias Nescessarias", true, true)]
    public List<ItemDraggable_EF02LP33> draggableDB = new List<ItemDraggable_EF02LP33>();

    [BoxGroup("Configuração de Tempo", true, true)]
    public Slider timerSliderBar;
    [BoxGroup("Configuração de Tempo")]
    public int[] maxTime;
    [BoxGroup("Configuração de Tempo")]
    public bool hasTimerRunning;

    [BoxGroup("Configuração de Pontuação", true, true)]
    public TextMeshProUGUI scoreTextComponent;
    [BoxGroup("Configuração de Pontuação")]
    public int scoreAmount;
    [BoxGroup("Configuração de Pontuação")]
    public int correctPerRight;
    [BoxGroup("Configuração de Pontuação")]
    public int correctPerRound;
    
    [BoxGroup("Referencias Nescessarias")]
    public Button confirmButton;
    [BoxGroup("Referencias Nescessarias")]
    public TextMeshProUGUI confirmButtonTextComponent;
    [BoxGroup("Referencias Nescessarias")]
    [HorizontalGroup("Group 1"), LabelWidth(100)]
    public Sprite CorrectTick;
    [BoxGroup("Referencias Nescessarias")]
    [HorizontalGroup("Group 1"), LabelWidth(100)]
    public Sprite WrongTick;

    [BoxGroup("ETC")]
    public bool waitingToProceed;
    [BoxGroup("Textos")]
    public string textToConfirmBtn;
    [BoxGroup("Textos")]
    public string textToNextRoundBtn;
    [BoxGroup("Textos")]
    public string textToFinishGameBtn;
    [BoxGroup("ETC")]
    public int maxRounds = 6;
    [BoxGroup("ETC")]
    public int currentRounds = 0;

    #region AudioSection

    [TabGroup("Audio System")] public SoundManager ManagerSound;
    [TabGroup("Audio System")] public AudioClip[] SoundClips;

    #endregion

    public void OnValidate() {
        var oldMaxTime = new int[maxRounds];
        maxTime.CopyTo(oldMaxTime, 0);
        maxTime = new int[maxRounds];
        oldMaxTime.CopyTo(maxTime, 0);
    }

    public void Start(){
        currentRounds = 0;
//        gdl.StartTimer();
    }

    [ButtonGroup("principal")]
    [Button("Start Game")]
    public void StartGame() {
        confirmButtonTextComponent.SetText(textToConfirmBtn);
        waitingToProceed = false;
        WordsList.Suffle();
        draggableDB.Suffle();
        int tempCount = dropAreaDB.Count;
        for (int i = 0; i < tempCount; i++) {
            draggableDB[i].item = WordsList[i];
            draggableDB[i].UpdateText();
            //sequencesToPlay.Add(draggableDB[i].UpdateTextSequence());
            draggableDB[i].droppedArea = null;
            draggableDB[i].hasValidDrop = false;
            draggableDB[i].hasBeenDrop = false;
            draggableDB[i].isBeenDrag = false;
            draggableDB[i].EndDrag();
        }
        //dropAreaDB.Suffle();
        tempCount = dropAreaDB.Count;
        for (int i = 0; i < tempCount; i++) {
            dropAreaDB[i].item = WordsList[i];
            dropAreaDB[i].HideTicker();
            dropAreaDB[i].UpdateText();
            //sequencesToPlay.Add(dropAreaDB[i].UpdateTextSequence());
            dropAreaDB[i].droppedItem = null;
            dropAreaDB[i].tempDrag = null;
            dropAreaDB[i].currentItem = null;
            if (dropAreaDB[i].manager == null) {
                dropAreaDB[i].manager = this;
            }
        }
        ResetTimer();
        StartTimer();
        SetDrag(true);
        isPlaying = true;
    }

    [ButtonGroup("principal")]
    public void CorrectValues() {
        SetDrag(false);
        int amountCorrect = 0;
        int tempCount = dropAreaDB.Count;
        Sequence correctingValues = DOTween.Sequence();
        correctingValues.SetId(001);
        for (int i = 0; i < tempCount; i++) {
            string completeWordFormed = "";

            if (dropAreaDB[i].currentItem != null) {
                completeWordFormed = dropAreaDB[i].currentItem.item.firstSyllable + dropAreaDB[i].item.restOfWord;
                completeWordFormed = completeWordFormed.ToLower();
            }

            if (dropAreaDB[i].item != null && completeWordFormed == dropAreaDB[i].item.entireWorld.ToLower()) {
                //Correta.                
                correctingValues.AppendCallback(() => ManagerSound.startSoundFX(SoundClips[0]));
                correctingValues.Append(dropAreaDB[i].ShowTicker(true));
                amountCorrect++;
                //logHandler.LogShow("Palavra Correta [" + completeWordFormed + "]", "#3be254");
//                sdl.SaveEstatistica(true);
            } else {
                correctingValues.AppendCallback(() => ManagerSound.startSoundFX(SoundClips[1]));
                correctingValues.Append(dropAreaDB[i].ShowTicker(false));
                //dropAreaDB[i].ShowTicker(false);
                if (dropAreaDB[i].currentItem != null) {
                    //logHandler.LogShow("Palavra Errada, [" + dropAreaDB[i].currentItem.item.firstSyllable + "] + [" + dropAreaDB[i].item.restOfWord + "] =/= [" + completeWordFormed + "]", "#b70923");
//                    sdl.SaveEstatistica(false);
                } else {
                    //logHandler.LogShow("Sem Seleção", "#b70923");
                }
            }

            correctingValues.AppendInterval(.3f);            
           
        }

        scoreAmount += (correctPerRight * amountCorrect) + correctPerRound;
//        gdl.AddScore((correctPerRight * amountCorrect) + correctPerRound);
        UpdateScoreTextValue();
        correctingValues.AppendCallback(ReadyToReplayGame);
        correctingValues.Play();
        
    }

    public void ReadyToReplayGame() {
        waitingToProceed = true;
        isPlaying = false;
        if (currentRounds >= maxRounds-1) {
            confirmButtonTextComponent.SetText(textToFinishGameBtn);
            confirmButton.interactable = true;
            confirmButtonTextComponent.alpha = 1f;
        } else {
            confirmButtonTextComponent.SetText(textToNextRoundBtn);
            confirmButton.interactable = true;
            confirmButtonTextComponent.alpha = 1f;
        }
    }

    public void MaxRoundsChecker() {
        if(currentRounds >= maxRounds-1) {
            //Terminar Jogo Chamar tela final
//            gdl.StopTimer();
//            gdl.SaveLog();
//            sdl.Send();
            Debug.Log("Game Finalizado!");
        } else {
            currentRounds++;
            StartGame();
        }
    }

    public void ButtonConfirmAction() {
        if (waitingToProceed) {
            MaxRoundsChecker();
        } else {
            InteractableFalse();
            StopTimer();
            CorrectValues();
        }
    }

    public void UpdateScoreTextValue() {
        int pValue = int.Parse(scoreTextComponent.text);
        scoreTextComponent.DOTextInt(pValue, scoreAmount, 1f);
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
        Timing.KillCoroutines("TimerSystem");
    }

    [ButtonGroup("Timer")]
    [Button("Reset Timer")]
    public void ResetTimer() {
        timerSliderBar.maxValue = maxTime[currentRounds];
        timerSliderBar.value = timerSliderBar.maxValue;
    }

    public IEnumerator<float> TimerSystem() {
        while (hasTimerRunning) { 
            yield return Timing.WaitForSeconds(1f);
            timerSliderBar.value -= 1f;
            HasTimeEnded();
        }
    }

    public void HasTimeEnded() {
        if (!(timerSliderBar.value <= timerSliderBar.minValue)) return;
        //Tempo Acabou.
        //Chamar fim!
        hasTimerRunning = false;
        CorrectValues();
    }

    public void VerificationToRelease() {

        if(ReleaseIt() == false) {
            confirmButton.interactable = false;
            confirmButtonTextComponent.alpha = .5f;
        } else {
            confirmButton.interactable = true;
            confirmButtonTextComponent.alpha = 1f;
        }      
       
    }

    public bool ReleaseIt() {
        int tempcount = dropAreaDB.Count;
        for (int i = 0; i < tempcount; i++) {
            if (dropAreaDB[i].currentItem == null) { 
                return false;
            }
        }
        return true;
    }

    public void InteractableFalse() {
        confirmButton.interactable = true;
        confirmButtonTextComponent.alpha = .5f;
    }

    public void SetDrag(bool _enable) {
        int tempCount = dropAreaDB.Count;
        for (int i = 0; i < tempCount; i++) {
            draggableDB[i].dragAvaible = _enable;
        }
    }
    
    //TODO Lista de musicas.
    //TODO Adicionar musica ao terminar tempo.
    //TODO Adicionar efeito sonoro no botão de confirmação.
    //TODO Adicionar efeito sonoro no botão de avançar.
    //TODO adicionar efeito ao drop dos itens nas palavras.

}
