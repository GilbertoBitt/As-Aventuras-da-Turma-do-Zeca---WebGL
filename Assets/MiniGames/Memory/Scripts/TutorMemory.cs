using UnityEngine;

public class TutorMemory : MonoBehaviour {

	public Color fundCor;
    public MemoryGameManager MemoryGameManager2;

    public void SomTutor() {
        if (MemoryGameManager2.checkAudio == true) {
            MemoryGameManager2.sound.startVoiceFX(MemoryGameManager2.audiosTutorial[MemoryGameManager2.numTutor]);
        }
    }
    public void checkAudioTrue() {
        MemoryGameManager2.checkAudio = true;
        MemoryGameManager2.sound.startVoiceFX(MemoryGameManager2.audiosTutorial[MemoryGameManager2.numTutor]);

    }


}
