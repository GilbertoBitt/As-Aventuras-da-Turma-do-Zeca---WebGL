using UnityEngine;

public class ControlSomTutor : MonoBehaviour {

    public SoundManager soundManager;
    public AudioClip[] audiosTutorial;
    public int numTutor;
    public bool checkSom;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    bool checkPassou;


    public void Start() {
        
    }

    public void SomTutor() {

        if (checkSom && !checkPassou) {
          
            checkPassou = true;          
            
            audio = soundManager.startVoiceFXReturn(audiosTutorial[numTutor]);
            if (audio != null) {
                audio.ignoreListenerPause = false;
            }
            //audio.Play();
        } 
        else if(checkSom && checkPassou) {
            checkPassou = false;
        }
        else if (!checkSom) {
           
            audio = soundManager.startVoiceFXReturn(audiosTutorial[numTutor]);
          if(audio!=null)
            audio.ignoreListenerPause = true;
           // audio.Play();
        }


    }
    public void RepetirAudio() {

        soundManager.startVoiceFXReturn(audiosTutorial[numTutor]);


    }
}
