using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour {

	public bool isOnPause;
	public Toggle[] Toggles;
	public GameConfig config;
	public SoundManager sound;
	public LoadManager load;
	public GameObject pausePanel;

	public GameObject panelLoad;
	public GameObject btPause;

	public UnityEvent OnPauseMenuOpen;
	public UnityEvent OnPauseMenuClose;
    public SoundManager soundManager;
    public AudioClip audio1;

    public void SomBT() {
        if (soundManager == null) {
            soundManager = FindObjectOfType<SoundManager>();
        }
        soundManager.startVoiceFX(audio1);
    }

    public void PauseGame(){
       SomBT();

        isOnPause = true;
		OnPauseMenuOpen.Invoke();
		Time.timeScale = 0f;
		OpenConfig ();
		pausePanel.SetActive (true);
	}

	public void UnpauseGame(){
        SomBT();
        btPause.SetActive (true);
		isOnPause = false;
		Time.timeScale = 1f;
		OnPauseMenuClose.Invoke();
		pausePanel.SetActive (false);
		config.SavePrefs ();
	}

	public void OpenConfig(){

        SomBT();
        if (config.isAudioOn) {
			Toggles [0].isOn = false;
		} else {
			Toggles [0].isOn = true;
		}

		if (config.isAudioFXOn) {
			Toggles [1].isOn = false;
		} else {
			Toggles [1].isOn = true;
		}

		if (config.isAudioVoiceOn) {
			Toggles [2].isOn = false;
		} else {
			Toggles [2].isOn = true;
		}

	}

	public void backgroundMusicValidate(){
        SomBT();
        if (Toggles [0].isOn) {
			config.isAudioOn = false;
		} else {
			config.isAudioOn = true;
		}

	}

	public void soundFXValidate(){
        SomBT();
        if (Toggles [1].isOn) {
			config.isAudioFXOn = false;
		} else {
			config.isAudioFXOn = true;
		}

	}

	public void VoicesValidate(){
        SomBT();
        if (Toggles [2].isOn) {
			config.isAudioVoiceOn = false;
		} else {
			config.isAudioVoiceOn = true;
		}

	}

	public void backToMenu(){
        SomBT();
        panelLoad.SetActive(true);
		Time.timeScale = 1;
		load.LoadAsync ("selectionMinigames");
	}
}
