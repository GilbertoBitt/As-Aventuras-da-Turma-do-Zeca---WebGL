using DG.Tweening;
using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager1_4B : OverridableMonoBehaviour {

    public Manager1_4A previousManager;
    public GameObject panelInteragindo;
    public GameObject oldPanelDisable;
    public GameObject activateThisPanel;
    //public VerticalLayoutGroup verticalLayoutGroup;
    public GameObject panelCountSyllable;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Controles")]
#endif
    public bool isPlaying = false;
    public bool isDragging = false;
    public int wordsDone = 0;
    public int wordsNeed = 3;
    public Word1_4B currentWord;
    public bool hasWordComplete = false;
    public bool isCountSyllables = false;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Palavras")]
#endif
    public Text entireWordTextcomp;
    public Transform syllablesParent;
    public GameObject syllablePrefab;
    public GridLayoutGroup syllableGrid;
    public List<Word1_4B> words = new List<Word1_4B>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Espaços Brancos")]
#endif
    public Transform blankSpaceParent;
    public GameObject blankSpacePrefab;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Arraste")]
#endif
    public Transform dragParent;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Pontuação")]
#endif
    public Text textScoreComp;
    public int scoreAmount;
    public int amountScorePerCorrect = 0;
    public int amountScorePerCorrectNumber = 0;
    public int amountScorePerCorrectSyllabes = 0;
    public float scoreIncreaseDuration = 1.0f;
    public AnimationCurve ScoreIncreaseCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Estrelas")]
#endif
    public int starsAmount;
    public Transform starsParent;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Scroll(Numero de Letras)")]
#endif
    public Text scrollsnap;
    public int scrollNumber;
    public int playerNumberSelect = 0;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Animação FadeIn")]
#endif
    public float fadeInTextDuration = 1.0f;
    public AnimationCurve fadeInTextCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Instancias")]
#endif
    public List<SyllableHandler1_4B> syllablesInstance = new List<SyllableHandler1_4B>();
    public List<BlankSpace1_4B> blankSpaceInstance = new List<BlankSpace1_4B>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Audio")]
#endif
    public SoundManager _soundManager;
    public AudioClip[] audios;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Painel de Vitoria")]
#endif
    public changeLevel highlight;
    public PanelDesafioControl painelDesafio;
    public int tutorIniMD_0;
    public GameObject tutorial;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("LOG")]
#endif
    public LogSystem log;
    public int idDificuldade;
    public int idHabilidade;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Buttons")]
#endif
    public AudioClip buttomConfirmClipAudio;
    public Button buttonIncrease;
    public Button buttonDecrease;
    bool checkpassAudio;
    public TutorMontanha TutorMontanha2;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("-- new")]
#endif
    public Text comandTextComp;
    public Text completeWordTextComp;
    public Text TextEnumComp;
    public Word1_4B[] monosyllable = new Word1_4B[10];
    public Word1_4B[] Dissyllable = new Word1_4B[10];
    public Word1_4B[] Trisyllable = new Word1_4B[10];
    public Word1_4B[] routineOneList = new Word1_4B[10];
    public Word1_4B[] routineTwoList = new Word1_4B[10];
    public int TotalRounds = 10;
    public int currentRound = 0;
    [HideInInspector]
    public Queue<SyllableHandler1_4B> blueBoxPool = new Queue<SyllableHandler1_4B>();
    public SyllableHandler1_4B[] blueBoxInstances = new SyllableHandler1_4B[0];
    [HideInInspector]
    public Queue<BlankSpace1_4B> emptyBoxPool = new Queue<BlankSpace1_4B>();
    public BlankSpace1_4B[] emptyBoxInstances = new BlankSpace1_4B[0];
    public int poolSize = 12;
    public GameObject prefabBlueBox;
    public GameObject prefabEmptyBox;
    public Transform poolParent;
    public Transform blueBoxParent;
    public Transform emptyBoxParent;
    public Button confirmButton;
    public Button confirmButtonRoutine2;
    public CanvasGroup nexPanelGroupComp;

    void Start() {
        TutorMontanha2 = previousManager.TutorMontanha2;
        blueBoxPool = new Queue<SyllableHandler1_4B>();
        emptyBoxPool = new Queue<BlankSpace1_4B>();
    }

    public override void UpdateMe() {

        if (isPlaying) {
            if (emptyBoxInstances.All(x => x.hasDrop == true) && !hasWordComplete) {
                hasWordComplete = true;
                confirmButton.interactable = true;
            } else if (!emptyBoxInstances.All(x => x.hasDrop == true) && hasWordComplete) {
                hasWordComplete = false;
                confirmButton.interactable = false;
            }
        }

    }

#pragma warning disable IDE1006 // Estilos de Nomenclatura
    public IEnumerator startIt() {
#pragma warning restore IDE1006 // Estilos de Nomenclatura

        yield return new WaitUntil(() => previousManager.isGameEnded == true);
        //comandTextComp.text = "Forme as Palavras.";

        if (PlayerPrefs.HasKey("TutorMD_0") == false) {
            oldPanelDisable.GetComponent<GraphicRaycaster>().enabled = false;
            activateThisPanel.GetComponent<GraphicRaycaster>().enabled = false;
            PlayerPrefs.SetInt("TutorMD_0", 0);
        } else {
            PlayerPrefs.SetInt("TutorMD_0", 1);
            //tutorIniMD_0 = PlayerPrefs.GetInt("TutorMD_0",1);
        }
        log.StartTimerDidatica(true);
        previousManager.enabled = false;
        //verticalLayoutGroup.enabled = false;
        previousManager.enabled = false;
        oldPanelDisable.SetActive(false);
        activateThisPanel.SetActive(true);
        if (PlayerPrefs.HasKey("TutorM_Didatica") == false) {
            oldPanelDisable.GetComponent<GraphicRaycaster>().enabled = false;
            activateThisPanel.GetComponent<GraphicRaycaster>().enabled = false;
            PlayerPrefs.SetInt("TutorM_Didatica", 0);

            yield return Yielders.Get(.3f);
            this.tutorial.GetComponent<TutorMontanha>().animTutor.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().animTutor.SetInteger("numbTutor", 6);
            this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor[7];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[7]);
            TutorMontanha2.profBalao.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Iniciar";
            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 7;
            foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
                item.SetActive(true);
            }
            this.tutorial.GetComponent<TutorMontanha>().gTutor[6].SetActive(false);
            this.tutorial.GetComponent<GraphicRaycaster>().enabled = true;
            this.tutorial.SetActive(true);
            Time.timeScale = 0f;
        } else {
            PlayerPrefs.SetInt("TutorM_Didatica", 1);
            //this.tutorObjLock = PlayerPrefs.GetInt ("TutorM_Didatica", 1);
        }
        //panelCountSyllable.SetActive(false);
        nexPanelGroupComp.blocksRaycasts = false;
        nexPanelGroupComp.interactable = false;
        nexPanelGroupComp.alpha = 0f;
        //words.Suffle();
        // TutorMontanha2.profBalao.enabled = false;
        //StartCoroutine(StartThis());
        //yield return new WaitForSeconds(1f);
        //verticalLayoutGroup.enabled = true;
        scoreAmount = previousManager.scoreAmount;
        textScoreComp.text = scoreAmount.ToString();
        starsAmount = previousManager.starAmount;
        //Debug.Log("estrelas " + starsAmount);

        updateStarAmount();

        SuffleAndGenerateFirstRoutine();
        SetupPools();

        Timing.RunCoroutine(StartDidatic());



    }

    public void ChangeTextTitle(string _text) {
        comandTextComp.text = _text;
    }

    public void SuffleAndGenerateFirstRoutine() {
        int amountWords = monosyllable.Length + Dissyllable.Length;
        routineOneList = new Word1_4B[amountWords];
        System.Array.Copy(monosyllable, routineOneList, monosyllable.Length);
        System.Array.Copy(Dissyllable, 0, routineOneList, monosyllable.Length, Dissyllable.Length);
        routineOneList.Suffle();

        amountWords = Trisyllable.Length + Dissyllable.Length;
        routineTwoList = new Word1_4B[amountWords];
        System.Array.Copy(Trisyllable, routineTwoList, Trisyllable.Length);
        System.Array.Copy(Dissyllable, 0, routineTwoList, Trisyllable.Length, Dissyllable.Length);
        routineTwoList.Suffle();
    }

    public void SetupPools() {
        //Blue box pool Setup;
        for (int i = 0; i < poolSize; i++) {
            GameObject temp = Instantiate(prefabBlueBox, new Vector3(1000, 1000, poolParent.position.z), Quaternion.identity, poolParent);
            SyllableHandler1_4B tempHandler = temp.GetComponent<SyllableHandler1_4B>();
            tempHandler.thisCanvasGroup.alpha = 0f;
            tempHandler.manager = this;
            blueBoxPool.Enqueue(tempHandler);
        }

        for (int i = 0; i < poolSize; i++) {
            GameObject temp = Instantiate(prefabEmptyBox, new Vector3(1000, 1000, poolParent.position.z), Quaternion.identity, poolParent);
            BlankSpace1_4B tempHandler = temp.GetComponent<BlankSpace1_4B>();
            tempHandler.manager = this;
            tempHandler.thisImageComp.color = new Color(1f, 1f, 1f, 0f);
            emptyBoxPool.Enqueue(tempHandler);
        }

    }

    public IEnumerator<float> StartDidatic() {
        isPlaying = false;

        //Reconfigurar Pool.
        int temp = blueBoxInstances.Length;
        for (int i = 0; i < temp; i++) {
            blueBoxInstances[i].ResetToDefault();
            blueBoxPool.Enqueue(blueBoxInstances[i]);
            blueBoxInstances[i] = null;
            emptyBoxInstances[i].ResetToDefault();
            emptyBoxPool.Enqueue(emptyBoxInstances[i]);
            emptyBoxInstances[i] = null;
        }

        blueBoxInstances = new SyllableHandler1_4B[0];
        emptyBoxInstances = new BlankSpace1_4B[0];

        if (currentRound < 10) {
            if (currentRound < 5) {

                //Iniciar Rotina de Letras.               

                //Iniciando Sequencia de animação para mudança do texto do comando(Titulo).
                Sequence TextFadeChange = DOTween.Sequence();
                if (comandTextComp.color.a > 0f) {
                    TextFadeChange.Append(comandTextComp.DOFade(0f, .3f));
                }
                TextFadeChange.AppendCallback(() => ChangeTextTitle("Forme a Palavra"));
                TextFadeChange.Append(comandTextComp.DOFade(1f, .3f));

                //Configurando Didatica com a palavra escolhida.
                currentWord = routineOneList[currentRound];
                blueBoxInstances = new SyllableHandler1_4B[currentWord.numberOfChars];
                emptyBoxInstances = new BlankSpace1_4B[currentWord.numberOfChars];
                currentWord.characters.Suffle();
                for (int i = 0; i < currentWord.numberOfChars; i++) {
                    blueBoxInstances[i] = blueBoxPool.Dequeue();
                    blueBoxInstances[i].syllable = currentWord.characters[i].ToUpper();
                    blueBoxInstances[i].UpdateTextContent();
                    blueBoxInstances[i].transform.SetParent(blueBoxParent);
                    blueBoxInstances[i].DoFade(1f);
                    emptyBoxInstances[i] = emptyBoxPool.Dequeue();
                    emptyBoxInstances[i].transform.SetParent(emptyBoxParent);
                    emptyBoxInstances[i].DoFade(1f);
                }

                yield return Timing.WaitForOneFrame;

                //Iniciando Rotina 1.
                isPlaying = true;
                confirmButton.interactable = false;

            } else {
                //Iniciar Rotina de Silabas.
                hasWordComplete = false;
                Sequence TextFadeChange = DOTween.Sequence();
                if (comandTextComp.color.a > 0f) {
                    TextFadeChange.Append(comandTextComp.DOFade(0f, .3f));
                }
                TextFadeChange.AppendCallback(() => ChangeTextTitle("Organize as Sílabas"));
                TextFadeChange.Append(comandTextComp.DOFade(1f, .3f));

                currentWord = routineTwoList[currentRound];
                blueBoxInstances = new SyllableHandler1_4B[currentWord.syllablesCount];
                emptyBoxInstances = new BlankSpace1_4B[currentWord.syllablesCount];
                currentWord.syllables.Suffle();
                for (int i = 0; i < currentWord.syllablesCount; i++) {
                    blueBoxInstances[i] = blueBoxPool.Dequeue();
                    blueBoxInstances[i].syllable = currentWord.syllables[i].ToUpper();
                    blueBoxInstances[i].UpdateTextContent();
                    blueBoxInstances[i].transform.SetParent(blueBoxParent);
                    blueBoxInstances[i].DoFade(1f);
                    emptyBoxInstances[i] = emptyBoxPool.Dequeue();
                    emptyBoxInstances[i].transform.SetParent(emptyBoxParent);
                    emptyBoxInstances[i].DoFade(1f);
                }

                yield return Timing.WaitForOneFrame;
                //Iniciando Rotina 1.
                isPlaying = true;
                confirmButton.interactable = false;
            }
        } else {
            //Iniciar Interagindo.
            endDitatica();
        }

    }

    public void ConfirmButtonRoutine() {
        _soundManager.startSoundFX(buttomConfirmClipAudio);
        confirmButton.interactable = false;
        isPlaying = false;
        if (currentRound < 5) {
            //correção de palavras.
            string fullPlayerWordGuess = string.Empty;
            for (int i = 0; i < currentWord.numberOfChars; i++) {
                fullPlayerWordGuess += emptyBoxInstances[i].thisSyllable.textComp.text;
            }

            fullPlayerWordGuess = ToTitleCase(fullPlayerWordGuess);
            currentWord.completeWord = ToTitleCase(currentWord.completeWord);

            Debug.Log("Respostas do Jogador: [" + fullPlayerWordGuess + "] \nResposta correta: [" + currentWord.completeWord + "]");

            if (fullPlayerWordGuess == currentWord.completeWord) {
                //Acertou a palavra.
                log.SaveEstatistica(86, true);
                _soundManager.startSoundFX(audios[0]);
                highlight.startCerto("Parabéns! Você acertou!");
                textScoreComp.DOTextInt(scoreAmount, (scoreAmount + amountScorePerCorrectSyllabes), 0.5f);
                scoreAmount += amountScorePerCorrectSyllabes;
                //Iniciar Contagem de Letras.
                TextEnumComp.text = "Quantas letras tem essa palavra?";
                StartRoutineTwo();
            } else {
                //Errou a palavra.
                log.SaveEstatistica(86, false);
                _soundManager.startSoundFX(audios[1]);
                currentRound++;
                highlight.startErrado("Você errou! O correto era: " + ToTitleCase(currentWord.completeWord) + string.Empty);
                //Iniciar novo Round.
                Timing.RunCoroutine(StartDidatic());
            }
        } else {
            //correção de palavras.
            string fullPlayerWordGuess = string.Empty;
            for (int i = 0; i < currentWord.syllablesCount; i++) {
                if (emptyBoxInstances.Length >= i && emptyBoxInstances[i] != null){
                    fullPlayerWordGuess += emptyBoxInstances[i].thisSyllable.textComp.text;
                }
            }

            fullPlayerWordGuess = ToTitleCase(fullPlayerWordGuess);
            currentWord.completeWord = ToTitleCase(currentWord.completeWord);

            Debug.Log("Respostas do Jogador: [" + fullPlayerWordGuess + "] \nResposta correta: [" + currentWord.completeWord + "]");

            if (fullPlayerWordGuess == currentWord.completeWord) {
                //Acertou a palavra.
                log.SaveEstatistica(86, true);
                _soundManager.startSoundFX(audios[0]);
                highlight.startCerto("Parabéns! Você acertou!");
                textScoreComp.DOTextInt(scoreAmount, (scoreAmount + amountScorePerCorrectSyllabes), 0.5f);
                scoreAmount += amountScorePerCorrectSyllabes;
                //Iniciar Contagem de Letras.
                TextEnumComp.text = "Quantas sílabas tem essa palavra?";
                StartRoutineTwo();
            } else {
                //Errou a palavra.
                log.SaveEstatistica(86, false);
                _soundManager.startSoundFX(audios[1]);
                currentRound++;
                highlight.startErrado("Você errou! O correto era: " + ToTitleCase(currentWord.completeWord) + string.Empty);
                //Iniciar novo Round.
                Timing.RunCoroutine(StartDidatic());
            }
        }
    }

    public void ConfirmButtonRoutineTwo() {
        _soundManager.startSoundFX(buttomConfirmClipAudio);
        //scrollNumber
        if (currentRound < 5) {
            //correção numero de letras.
            if (scrollNumber == currentWord.numberOfChars) {
                //acertou o numero de letras.
                _soundManager.startSoundFX(audios[0]);
                textScoreComp.DOTextInt(scoreAmount, (scoreAmount + amountScorePerCorrectSyllabes), 0.5f);
                scoreAmount += amountScorePerCorrectSyllabes;
                log.SaveEstatistica(86, true);
                highlight.startCerto("Parabéns! Você acertou!");
                EndRoutineTwo();
            } else {
                //errou o numero de letras.
                _soundManager.startSoundFX(audios[1]);
                log.SaveEstatistica(86, true);
                highlight.startErrado("Você errou o certo era: " + currentWord.numberOfChars);
                EndRoutineTwo();
            }
        } else {
            //correção numero de silabas.
            if (scrollNumber == currentWord.syllablesCount) {
                //acertou o numero de letras.
                _soundManager.startSoundFX(audios[0]);
                textScoreComp.DOTextInt(scoreAmount, (scoreAmount + amountScorePerCorrectSyllabes), 0.5f);
                scoreAmount += amountScorePerCorrectSyllabes;
                log.SaveEstatistica(86, true);
                highlight.startCerto("Parabéns! Você acertou!");
                EndRoutineTwo();
            } else {
                //errou o numero de letras.
                _soundManager.startSoundFX(audios[1]);
                log.SaveEstatistica(86, true);
                highlight.startErrado("Você errou o certo era: " + currentWord.syllablesCount);
                EndRoutineTwo();
            }
        }
    }

    public void EndRoutineTwo() {
        //completeWordTextComp.text = currentWord.completeWord.ToUpper();        
        confirmButton.interactable = true;
        confirmButton.gameObject.SetActive(true);
        completeWordTextComp.DOFade(0f, 0.3f);
        nexPanelGroupComp.blocksRaycasts = false;
        nexPanelGroupComp.interactable = false;
        nexPanelGroupComp.DOFade(0f, 0.3f);
        currentRound++;
        Timing.RunCoroutine(StartDidatic());
    }

    public void StartRoutineTwo() {
        completeWordTextComp.text = currentWord.completeWord.ToUpper();
        scrollNumber = 0;
        scrollsnap.text = "0";
        confirmButton.interactable = false;
        confirmButton.gameObject.SetActive(false);
        Sequence routine2Start = DOTween.Sequence();
        routine2Start.AppendCallback(() => FadesSecondRoutine());
        routine2Start.AppendInterval(0.5f);
        routine2Start.Append(completeWordTextComp.DOFade(1f, 1f));
        routine2Start.Play();
        nexPanelGroupComp.blocksRaycasts = true;
        nexPanelGroupComp.interactable = true;
    }

    public void FadesSecondRoutine() {
        nexPanelGroupComp.DOFade(1f, 0.3f);
        int temp = blueBoxInstances.Length;
        for (int i = 0; i < temp; i++) {
            blueBoxInstances[i].DoFade(0f);
            emptyBoxInstances[i].DoFade(0f);
        }
    }

    public IEnumerator StartThis() {
        isPlaying = false;

        if (wordsDone >= wordsNeed) {
            endDitatica();
        }

        CleanWords();

        currentWord = words[wordsDone];

        List<string> syllablesTemp = currentWord.syllables.ToList();
        syllablesTemp.Suffle();

        entireWordTextcomp.text = currentWord.completeWord.ToUpper();

        Color colorTemp = entireWordTextcomp.color;
        colorTemp.a = 0f;
        entireWordTextcomp.color = colorTemp;

        for (int i = 0; i < currentWord.syllablesCount; i++) {
            GameObject syllable = Instantiate(syllablePrefab, Vector3.zero, Quaternion.identity, syllablesParent);
            syllable.transform.localScale = Vector3.one;
            SyllableHandler1_4B handler = syllable.GetComponent<SyllableHandler1_4B>();
            handler.manager = this;
            handler.syllable = syllablesTemp[i].ToUpper();
            handler.UpdateTextContent();
            syllablesInstance.Add(handler);
        }

        for (int i = 0; i < currentWord.syllablesCount; i++) {
            GameObject blankSpace = Instantiate(blankSpacePrefab, Vector3.zero, Quaternion.identity, blankSpaceParent);
            blankSpace.transform.localScale = Vector3.one;
            BlankSpace1_4B handler = blankSpace.GetComponent<BlankSpace1_4B>();
            handler.manager = this;
            blankSpaceInstance.Add(handler);
        }

        yield return new WaitForSeconds(.5f);
        isPlaying = true;
        hasWordComplete = false;
        isCountSyllables = false;

    }

    public void onValueScrollChange() {
        playerNumberSelect = scrollNumber;
    }

    public IEnumerator<float> scoreIncrease(int increase) {

        //Debug.Log ("Start Increase Points");
        float times = 0.0f;
        int startPoints = scoreAmount;
        int scoreT = scoreAmount + increase;
        scoreAmount += increase;

        log.AddPontosPedagogica(increase);
        while (times < scoreIncreaseDuration) {
            times += Time.deltaTime;
            float s = times / scoreIncreaseDuration;

            int scory = (int)Mathf.Lerp(startPoints, scoreT, ScoreIncreaseCurve.Evaluate(s));
            textScoreComp.text = scory.ToString();
            yield return Timing.WaitForOneFrame;
        }

    }

    void updateStarAmount() {

        if (starsAmount < 3) {
            for (int i = 0; i < 3; i++) {
                starsParent.GetChild(i).GetComponent<Image>().sprite = previousManager.spriteEmptyStar;
            }

            for (int i = 0; i < starsAmount; i++) {
                starsParent.GetChild(i).GetComponent<Image>().sprite = previousManager.spriteFullStar;
            }
        }

    }

    public void confirmButton02() {
        if (scrollNumber == currentWord.syllablesCount) {
            //Debug.Log("Correto o numero de silabas!");
            //scoreAmount += amountScorePerCorrectSyllabes;
            _soundManager.startSoundFX(audios[0]);
            StartCoroutine(scoreIncrease(amountScorePerCorrectSyllabes));
            log.SaveEstatistica(86, true);
            highlight.startCerto("Parabéns! Você acertou!");
        } else {
            //Debug.Log("Errado o numero de silabas!");
            _soundManager.startSoundFX(audios[1]);
            log.SaveEstatistica(86, true);
            highlight.startErrado("Você errou o certo era: " + currentWord.syllablesCount.ToString());
        }
        StartCoroutine(GameCorrectionSyllable());
    }

    IEnumerator GameCorrectionSyllable() {
        yield return new WaitForSeconds(.5f);
        scrollNumber = 0;
        scrollsnap.text = "0";

        panelCountSyllable.SetActive(false);
        wordsDone++;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(StartThis());
    }

    public void confirmButton01() {
        if ((!blankSpaceInstance.All(x => x.hasDrop == true))) {
            //Debug.Log("Defina o numero de silabas!");
        } else {
            //Debug.Log("Corrigir!");

            StartCoroutine(GameCorrectionWord());
        }
    }

    IEnumerator GameCorrectionWord() {
        List<SyllableHandler1_4B> syllabeTemp = new List<SyllableHandler1_4B>();
        int inscreaseamount = 0;
        bool isWordWrong = false;
        int blankSpaceInstanceCount = blankSpaceInstance.Count;

        string correctWords = string.Empty;
        string currentWords = currentWord.completeWord.ToUpper();

        for (int i = 0; i < blankSpaceInstanceCount; i++) {
            correctWords += blankSpaceInstance[i].thisSyllable.textComp.text;
        }

        correctWords = correctWords.ToUpper();
        if (correctWords == currentWords) {
            isWordWrong = false;
            //log.SaveEstatistica(1, true);
        } else {
            isWordWrong = true;
            //log.SaveEstatistica(1, false);
        }


        for (int i = 0; i < blankSpaceInstanceCount; i++) {
            syllabeTemp.Add(blankSpaceInstance[i].thisSyllable);
        }

        if (syllabeTemp.Count >= currentWord.syllables.Length) {
            for (int i = 0; i < syllabeTemp.Count; i++) {
                if (syllabeTemp[i].syllable != currentWord.syllables[i].ToUpper()) {
                    break;
                }
            }
        }
        Debug.Log("THIs");
        if (isWordWrong) {
            //Debug.Log("Palavra errada!");
            _soundManager.startSoundFX(audios[1]);
            wordsDone++;
            isPlaying = false;
            StartCoroutine(StartThis());
            highlight.startErrado("Você errou! O correto era: " + currentWords + string.Empty);
            //Debug.Log ("THISE");
        }

        if (isWordWrong == false) {
            panelCountSyllable.SetActive(true);
            //Debug.Log("Palavra Certa!");			
            //StartCoroutine(scoreIncrease(amountScorePerCorrect));
            inscreaseamount += amountScorePerCorrect;
            _soundManager.startSoundFX(audios[0]);
            isCountSyllables = true;
            isPlaying = true;
            Debug.Log("THISE");
            StartCoroutine(AskAmountSyllables());
            highlight.startCerto("Parabéns! Você acertou!");
        }

        if (inscreaseamount > 0) {
            StartCoroutine(scoreIncrease(inscreaseamount));
        }

        yield return new WaitForSeconds(.5f);
    }

    IEnumerator AskAmountSyllables() {
        isPlaying = true;
        //buttonIncrease.interactable = true;
        // buttonDecrease.interactable = true;
        //entireWordTextcomp.color = Color.black;

        Color _color = Color.black;
        _color.a = 0f;
        Color blankStartColor = blankSpaceInstance[0].thisImageComp.color;
        float times = 0.0f;

        Color colorTemp = entireWordTextcomp.color;
        colorTemp.a = 1f;
        Color startColor = entireWordTextcomp.color;

        panelCountSyllable.SetActive(true);

        while (times < fadeInTextDuration) {
            times += Time.deltaTime;
            float s = times / fadeInTextDuration;

            //int scory = (int)Mathf.Lerp (startPoints, scoreT, fadeInTextCurve.Evaluate (s));
            int syllablesInstanceCount = syllablesInstance.Count;
            for (int i = 0; i < syllablesInstanceCount; i++) {
                syllablesInstance[i].imageComp.color = Color.Lerp(Color.black, _color, fadeInTextCurve.Evaluate(s));
                syllablesInstance[i].textComp.color = Color.Lerp(Color.black, _color, fadeInTextCurve.Evaluate(s));
                blankSpaceInstance[i].thisImageComp.color = Color.Lerp(blankStartColor, Color.clear, fadeInTextCurve.Evaluate(s));
                blankSpaceInstance[i].traceImageComp.color = Color.Lerp(blankStartColor, Color.clear, fadeInTextCurve.Evaluate(s));
            }

            entireWordTextcomp.color = Color.Lerp(startColor, colorTemp, fadeInTextCurve.Evaluate(s));

            yield return new WaitForEndOfFrame();
        }

        panelCountSyllable.SetActive(true);


    }

    public void CleanWords() {
        int syllablesInstanceCount = syllablesInstance.Count;
        for (int i = 0; i < syllablesInstanceCount; i++) {
            Destroy(syllablesInstance[i].gameObject);
        }
        int blankSpaceInstanceCount = blankSpaceInstance.Count;
        for (int i = 0; i < blankSpaceInstanceCount; i++) {
            Destroy(blankSpaceInstance[i].gameObject);
        }

        syllablesInstance.Clear();
        blankSpaceInstance.Clear();
    }

    public void endDitatica() {
        StopAllCoroutines();
        //TODO delay aqui
        painelDesafio.scoreAmount = scoreAmount;
        painelDesafio.starAmount = starsAmount;
        panelInteragindo.SetActive(true);
        //		panelInteragindo.GetComponent<Animator>().SetInteger("panelDesafioNumber",1);
    }

    public void ButtonIncrease() {
        _soundManager.startSoundFX(buttomConfirmClipAudio);
        confirmButtonRoutine2.interactable = true;
        if (scrollNumber >= 9) {
            scrollNumber = 0;
        } else {
            scrollNumber++;
        }

        scrollsnap.text = scrollNumber.ToString();
    }

    public void ButtonDecrease() {
        _soundManager.startSoundFX(buttomConfirmClipAudio);
        confirmButtonRoutine2.interactable = true;
        if (scrollNumber <= 0) {
            scrollNumber = 9;
        } else {
            scrollNumber--;
        }

        scrollsnap.text = scrollNumber.ToString();
    }

    public string ToTitleCase(string str) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }

    public void DropCardSound() {
        _soundManager.startSoundFX(audios[2]);
    }

}
