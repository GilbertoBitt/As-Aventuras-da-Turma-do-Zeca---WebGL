using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Serialization;

public class ManagerSongQuiz : OverridableMonoBehaviour {

    public SongFileQuiz currentSong;

    public SongFileQuiz[] songFiles;
    public int currentSongIndex;

    public AudioSource mainAudioSource;

    //the current position of the song (in seconds)
    public float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;  

    //how much time (in seconds) has passed since the song started
    public float dsptimesong;    

    //the index of the next note to be spawned
    public int nextIndex = 0;

    public float beatsShownInAdvance = 3;

    public float delayToStartSong;
    public bool canSpawnNotes;
    public TextMeshProUGUI scoreText;
    public int scoreAmount;
    public int scoreComboPeriod;
    public int scoreByRight;
    public int timesCombo;
    public int rightAmountTotal;

    [AssetsOnly]
    public GameObject prefabNotes;
    public Transform spawnPos;
    public Transform toPosTransform;
    [FoldoutGroup("Mark Config")]
    public MarkSongQuiz mark;

    public bool isRecording;

    [FoldoutGroup("Pool Notes")]
    public Queue<NoteSongQuiz> notePool = new Queue<NoteSongQuiz>();
    [FoldoutGroup("Pool Notes")]
    public int sizeOfPoolNotes;

    [FoldoutGroup("Mark Config")]
    public float perfectRangeByBeats;
    [FoldoutGroup("Mark Config")]
    public float goodRangeByBeats;

    public PauseManager PauseController;
    public float songPositionOnPause;
    public float SondPosDelta;
    
    float[] _samples;
    private float latestsongBeat;

    public RadialSlideTool rslider;
    [TitleGroup("Question Window Configuration")]
    public float SafeArea;
    public int MaxTimesToCall = 3;
    public int CurrentTimes = 0;
    [PropertyRange(0,100)]
    public int PercentageTimes;

    public bool isSongPaused = false;
    public bool isGameEnded = true;
    public FinalScore PanelScore;

    public GameObject CanvasPanel;

    public GameObject MainPanel;
    // Use this for initialization
    public void Start() {
        PauseController = FindObjectOfType(typeof(PauseManager)) as PauseManager;
        if (PauseController != null){
            PauseController.OnPauseMenuOpen.AddListener(PauseSongPosition);
            PauseController.OnPauseMenuClose.AddListener(ResumeSong);
        }
        PoolSetup();
        CurrentTimes = 0;
    }

    public void PauseSongPosition(){
        isSongPaused = true;
        mainAudioSource.Pause();
        songPositionOnPause = songPosition;
    }

    [Button("Start Game")]
    public void StartGame () {
        //currentSong = songFiles[currentSongIndex];
        //calculate how many seconds is one beat
        //bmp = UniBpmAnalyzer.AnalyzeBpm(currentSong);
        //we will see the declaration of bpm later
        //secPerBeat = 60f / bmp;

        Invoke("StartSong", delayToStartSong);
    }

    [Button("Clear Song Notes")]
    public void ClearNotes() {
        currentSong.notes.Clear();
    }

    /*public void AddScore() {
        Sequence addScore = DOTween.Sequence();
        addScore.SetId(456);
        addScore.Append()
    }*/

    public void PoolSetup() {
        notePool = new Queue<NoteSongQuiz>();
        for (int i = 0; i < sizeOfPoolNotes; i++) {           
            notePool.Enqueue(InstanceNote());
        }
    }

    

    public NoteSongQuiz InstanceNote() {
        var go = Instantiate(prefabNotes, spawnPos.position*1000f, Quaternion.identity, spawnPos.parent);
        var note = go.GetComponent<NoteSongQuiz>();
        //initialize the fields of the music note
        note.mngr = this;
        return note;
    }

    public NoteSongQuiz GetNoteSong() {
        if(notePool.Count <= 0) {
            notePool.Enqueue(InstanceNote());
        }
        return notePool.Dequeue();
    }

    public void StartSong() {
        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;
        rslider.slider = 0f;
        //start the song
        mainAudioSource.clip = currentSong.audioClip;
        _samples = new float[mainAudioSource.clip.samples*mainAudioSource.clip.channels];
 
        //store all the sample data of the clip in this array.
        mainAudioSource.clip.GetData(_samples, 0);
        mainAudioSource.Play();
        isGameEnded = false;
    }

    public void ResumeSong()
    {
        
        var resumeSong = DOTween.Sequence();
        resumeSong.AppendInterval(1f);
        resumeSong.AppendCallback(() => mainAudioSource.Play());
        resumeSong.AppendCallback(() => isSongPaused = false);
        resumeSong.AppendCallback(() => SondPosDelta = (((float)(AudioSettings.dspTime) - dsptimesong) - songPositionOnPause));
    }


    public override void UpdateMe() {
        

        if (mainAudioSource.isPlaying && !isRecording) {
            //calculate the position in seconds
            songPosition = ((float)(AudioSettings.dspTime) - dsptimesong);
            songPosition -= SondPosDelta;

            //calculate the position in beats
            songPosInBeats = songPosition / currentSong.secPerBeat;

            if (nextIndex < currentSong.notes.Count && currentSong.notes[nextIndex].positionInBeats < songPosInBeats + beatsShownInAdvance) {
                if (canSpawnNotes) {
                    var note = GetNoteSong();
                    //initialize the fields of the music note
                    //note.mngr = this;
                    note.SpawnPos = new Vector3(spawnPos.position.x, spawnPos.position.y, 0f);
                    note.toPos = toPosTransform.position;
                    var distance = Vector2.Distance(toPosTransform.position, spawnPos.position);
                    note.toPos2 = new Vector2(note.toPos.x + (distance * -1f), note.toPos.y);
                    note.beatOfThisNote = currentSong.notes[nextIndex].positionInBeats;
                    note.UpdateType(currentSong.notes[nextIndex].Type);
                    note.canLerp = true;
                }
                nextIndex++;
            } 
        } else if (mainAudioSource.isPlaying && isRecording) {
            //calculate the position in seconds
            songPosition = (float)(AudioSettings.dspTime - dsptimesong);

            //calculate the position in beats
            songPosInBeats = songPosition / currentSong.secPerBeat;
            
            float currentSample = _samples[mainAudioSource.timeSamples];
            
            if (currentSample > currentSong.CurrentSampleDelta && (songPosInBeats - latestsongBeat >= currentSong.CurrentSongBeatDelta))
            {
                //SetNotes();
                var randomValue = Random.Range(0, 100);
                if (randomValue > PercentageTimes || songPosInBeats > songPosInBeats + beatsShownInAdvance)
                {
                    SetNotes();
                }
                else
                {
                    SetCallQuestionNotes();
                }
                
                
            }

            /*if (Input.GetKeyDown(KeyCode.RightArrow)) {
                currentSong.notes.Add(new NoteClassQ(songPosInBeats, true));
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                currentSong.notes.Add(new NoteClassQ(songPosInBeats, false));
            }*/
        }

        if (!mainAudioSource.isPlaying && !isRecording && !isSongPaused)
        {
            if (!isGameEnded)
            {
                isGameEnded = true;
                PanelScore.setScore(scoreAmount);
                CanvasPanel.SetActive(true);
                MainPanel.SetActive(false);
            }   
        }

        if (!mainAudioSource.isPlaying) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            mark.TryNote(NoteClassQ.NoteType.Left);
        } else if(Input.GetKeyDown(KeyCode.RightArrow)){
            mark.TryNote(NoteClassQ.NoteType.Right);
        }
    }

    public void SetNotes()
    {
        var randomValue = Random.Range(0, 2);
        currentSong.notes.Add(randomValue == 0
            ? new NoteClassQ(songPosInBeats, NoteClassQ.NoteType.Right)
            : new NoteClassQ(songPosInBeats, NoteClassQ.NoteType.Left));
        Debug.Log("Delta SonBeatPos: " + (songPosInBeats - latestsongBeat) + " \n SongPosInBeats: " + songPosInBeats);
        latestsongBeat = songPosInBeats;
    }

    public void SetCallQuestionNotes()
    {
        if (CurrentTimes >= MaxTimesToCall) return;
        CurrentTimes++;
        currentSong.notes.Add(new NoteClassQ(songPosInBeats, NoteClassQ.NoteType.Question));

    }
}
