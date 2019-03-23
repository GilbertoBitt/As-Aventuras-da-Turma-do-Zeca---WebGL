using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new SongFile", menuName = "others/songFileQuiz")]
[System.Serializable]
public class SongFileQuiz : SerializedScriptableObject {

    public string songText;
    public AudioClip audioClip;
    public float secPerBeat;
    public float inicialBeats;
    public float endingBeats;
    public float CurrentSampleDelta;
    public float CurrentSongBeatDelta;
    public int bmp;
    public List<NoteClassQ> notes;

    public void CalculateBMP() {
        bmp = UniBpmAnalyzer.AnalyzeBpm(audioClip);
        //we will see the declaration of bpm later
        secPerBeat = 60f / bmp;
    }

    public void OnValidate() {
        if(audioClip != null) {
            CalculateBMP();
        }
    }

}
