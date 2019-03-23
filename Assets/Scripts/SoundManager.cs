using UnityEngine;
using System.Collections.Generic;
using MEC;

public class SoundManager : OverridableMonoBehaviour {

	public AudioClip backgroundMusic;
    public AudioClip endGameTheme;
	public GameConfig gameConfig;
    public AudioSource musicBack;
	public AudioClip AmbientSFX;
	public bool hasAmbientFX = false;
	public AudioSource AmbientSFXComp;
	public List<AudioSource> fxsList = new List<AudioSource>();
    public List<AudioSource> voiceFxs = new List<AudioSource>();
	public int amountOfSoundFxs;
	Dictionary<AudioClip, AudioSource> openWith = new Dictionary<AudioClip, AudioSource>();
	public bool isStarted = false;
	private bool hasReferences = false;
    private bool _pause = false;
    public bool hasBackaudio = true;
    public float OnVoiceEffectVolumeBackground = 0.4f;
    public float OnVoiceEffectVolumeAmbient = 0.4f;
    public bool isVoicePlaying;
    public float hasAmbientFXVolume = 0.4f;
    public float backgroundMusicVolume = 0.8f;
    public PauseManager PauseManager2;
    public AudioClip audio1;

    void SomBT2() {
        if (PauseManager2 == null) {
            PauseManager2 = FindObjectOfType<PauseManager>();
            //audio1 = PauseManager2.audio1;
        }
        if (audio1 != null && PauseManager2 != null) {
            audio1 = PauseManager2.audio1;
        }
      
        //startVoiceFX(audio1);
    }

    IEnumerator<float> delayStart()
    {   
        musicBack = this.GetComponent<AudioSource> ();
        if (hasBackaudio && musicBack == null) {
            musicBack = this.gameObject.GetComponent<AudioSource>();
        }

        yield return Timing.WaitForSeconds(1.5f);       

        if (hasAmbientFX && gameConfig.isAudioFXOn && AmbientSFXComp != null)
        {
                AmbientSFXComp.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
                AmbientSFXComp.clip = AmbientSFX;
                AmbientSFXComp.loop = true;
                if (!isVoicePlaying) {
                    AmbientSFXComp.volume = hasAmbientFXVolume;
                } else {
                    AmbientSFXComp.volume = OnVoiceEffectVolumeAmbient;
                }
                AmbientSFXComp.Play();
        } 

        if(!hasAmbientFX && AmbientSFXComp != null && AmbientSFXComp.isPlaying) {
            AmbientSFXComp.Stop();
        }

        if (hasBackaudio && backgroundMusic != null && gameConfig.isAudioOn && musicBack != null)
        {
            musicBack.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
            musicBack.clip = backgroundMusic;
            musicBack.loop = true;
            if (!isVoicePlaying) {
                musicBack.volume = backgroundMusicVolume;
            } else {
                musicBack.volume = OnVoiceEffectVolumeBackground;
            }
            musicBack.Play();
        }

        isStarted = true;
    }

	// Use this for initialization
	public void StartLate () {
        Timing.RunCoroutine(delayStart());

	}


	public void Start(){
        StartLate();       
    }

	public AudioSource returnRightSource(AudioClip _clip){

		if (openWith.ContainsKey(_clip)) {
			AudioSource d = openWith[_clip];
			return d;
		} else {
			GameObject effectSound = new GameObject ("EffectSound");
			effectSound.transform.SetParent (this.transform.GetChild (0));
			effectSound.AddComponent<AudioSource> ();
			AudioSource d = effectSound.GetComponent<AudioSource>();
			d.playOnAwake = false;
			d.clip = _clip;
			openWith.Add(_clip, d);
			return d;
		}

	}

    public AudioSource VoiceEffectSource(AudioClip _clip) {

        if (openWith.ContainsKey(_clip)) {
            AudioSource d = openWith[_clip];
            return d;
        } else {
            GameObject voiceEffect = new GameObject("VoiceEffect");
            voiceEffect.transform.SetParent(this.transform.GetChild(0));
            voiceEffect.AddComponent<AudioSource>();
            AudioSource d = voiceEffect.GetComponent<AudioSource>();
            d.playOnAwake = false;
            d.clip = _clip;
            openWith.Add(_clip, d);
            return d;
        }

    }

    public AudioSource VoiceEffectSource(AudioClip _clip, float _volume) {

        if (openWith.ContainsKey(_clip)) {
            AudioSource d = openWith[_clip];
            d.volume = _volume;
            return d;
        } else {
            GameObject voiceEffect = new GameObject("VoiceEffect");
            voiceEffect.transform.SetParent(this.transform.GetChild(0));
            voiceEffect.AddComponent<AudioSource>();
            AudioSource d = voiceEffect.GetComponent<AudioSource>();
            d.playOnAwake = false;
            d.clip = _clip;
            d.volume = _volume;
            d.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
            openWith.Add(_clip, d);
            return d;
        }

    }

    public AudioSource returnRightSource(AudioClip _clip, float _volume){

		if (openWith.ContainsKey(_clip)) {
			AudioSource d = openWith[_clip];
			d.volume = _volume;
			return d;
		} else {
			GameObject effectSound = new GameObject ("EffectSound");
            effectSound.transform.SetParent(this.transform.GetChild(0));
            effectSound.AddComponent<AudioSource> ();
			AudioSource d = effectSound.GetComponent<AudioSource>();
			d.playOnAwake = false;
			d.clip = _clip;
			d.volume = _volume;
			d.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
			openWith.Add(_clip, d);
			return d;
		}

	}
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// 

    public override void UpdateMe(){
		if (isStarted) {
			fixSound();
		}
	}

	void fixSound(){

        if(musicBack == null){
             musicBack = this.gameObject.GetComponent<AudioSource>();
             musicBack.clip = backgroundMusic;
        }

        if (!hasReferences && !_pause) {
			hasReferences = true;
			
			if(hasAmbientFX){
				if (gameConfig.isAudioFXOn) {
					AmbientSFXComp.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
					AmbientSFXComp.clip = AmbientSFX;
					AmbientSFXComp.loop = true;
                    if (!isVoicePlaying) {
                        AmbientSFXComp.volume = hasAmbientFXVolume;
                    } else {
                        AmbientSFXComp.volume = OnVoiceEffectVolumeAmbient;
                    }
					AmbientSFXComp.Play();

				}
			}

            if (!hasAmbientFX && AmbientSFXComp != null && AmbientSFXComp.isPlaying) {
                AmbientSFXComp.Stop();
            }

            if (hasBackaudio && backgroundMusic != null && gameConfig.isAudioOn) {
				musicBack.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
				musicBack.clip = backgroundMusic;	
				musicBack.loop = true;
                if (!isVoicePlaying) {
                    musicBack.volume = backgroundMusicVolume;
                } else {
                    musicBack.volume = OnVoiceEffectVolumeBackground;
                }
                musicBack.Play ();
			}

			isStarted = true;
		}

        if (!_pause) {
            if (hasBackaudio && backgroundMusic != null && musicBack.isPlaying == true && gameConfig.isAudioOn == false)  {
                musicBack.clip = backgroundMusic;
                musicBack.Stop();
            } else if (hasBackaudio && backgroundMusic != null && musicBack.isPlaying == false && gameConfig.isAudioOn == true) {
                musicBack.clip = backgroundMusic;
                musicBack.Play();
            }

            if (!hasAmbientFX && AmbientSFXComp != null && AmbientSFXComp.isPlaying) {
                AmbientSFXComp.Stop();
            }

            if (hasAmbientFX) {
                if (AmbientSFXComp.isPlaying == true && gameConfig.isAudioFXOn == false) {
                    AmbientSFXComp.Stop();
                } else if (AmbientSFXComp.isPlaying == false && gameConfig.isAudioFXOn == true) {
                    AmbientSFXComp.Play();
                }
            }
        }
	}


	public void stopCurrentFxs(){
		foreach (var item in fxsList) {
			item.Stop ();
		}
	}

	public void playCurrentsFxs(){
		foreach (var item in fxsList) {
			item.Play ();
		}
	}

	public void changeBackgroundMusic(AudioClip song, bool isLoop){
		if (gameConfig.isAudioOn) {
			musicBack.Stop ();
			musicBack.clip = song;
			musicBack.loop = isLoop;
			musicBack.Play ();
		}
		fixSound();
	}


	public void changeBackgroundMusic(AudioClip song){
		if (gameConfig.isAudioOn) {
			musicBack.Stop ();
			musicBack.clip = song;
			musicBack.loop = true;
			musicBack.Play ();
		}
		fixSound();
	}

    public void VoiceOverHandler() {
        int tempCount = voiceFxs.Count;

        for (int i = 0; i < tempCount; i++) {
            if (voiceFxs[i] != null) {
                voiceFxs[i].Stop();
            }
        }
    }

    public void StopVoiceEffects() {
        int tempCount = voiceFxs.Count;

        for (int i = 0; i < tempCount; i++) {
            if(voiceFxs[i] != null) {
                voiceFxs[i].Pause();
            }            
        }
    }

    public void PlayVoiceEffects() {
        int tempCount = voiceFxs.Count;

        for (int i = 0; i < tempCount; i++) {
            if (voiceFxs[i] != null) {
                voiceFxs[i].UnPause ();
            }
        }
    }


    public void startSoundFX(AudioClip effectSong){
		if (gameConfig.isAudioFXOn) {
			//AudioSource voic = 
            returnRightSource(effectSong).PlayOneShot(effectSong);
            StopVoiceEffects();
            //voiceFxs.Add(voic);
            //oic.PlayOneShot(effectSong);
            //StartCoroutine (playAndDelete (effectSound));
        }
		fixSound();
	}

	public AudioSource startSoundFXWithReturn(AudioClip effectSong){
		if (gameConfig.isAudioFXOn) {
			return returnRightSource(effectSong);
		}
		return null;
	}

	public void startSoundFX(AudioClip effectSong, float volume){
		if (gameConfig.isAudioFXOn) {
			returnRightSource(effectSong, volume).PlayOneShot(effectSong);
		}
	}

	public void startSoundFX(AudioClip effectSong, float volume, float delay){
		Timing.RunCoroutine (startSoundFXdelayed (effectSong, volume, delay));
		fixSound();
	}

	IEnumerator<float> startSoundFXdelayed(AudioClip effectSong, float volume, float delay){
		yield return Timing.WaitForSeconds (delay);
		if (gameConfig.isAudioFXOn) {
			returnRightSource(effectSong, volume).PlayOneShot(effectSong);
		}
	}

	public void startVoiceFX(AudioClip VoiceSound){
        SomBT2();
        if (gameConfig.isAudioVoiceOn) {
            AudioSource voic = VoiceEffectSource(VoiceSound);
            VoiceOverHandler();
            voiceFxs.Add(voic);
            voic.volume = 1f;
            voic.clip = VoiceSound;
            voic.Play();
            isVoicePlaying = true;
            Timing.KillCoroutines("soundVoice");
            Timing.RunCoroutine(BackVolumeOverTime(VoiceSound.length), "soundVoice");
        }
    }

    IEnumerator<float> BackVolumeOverTime(float time) {
        if (hasBackaudio && musicBack != null) {
            musicBack.volume = OnVoiceEffectVolumeBackground;
        }
        if (hasAmbientFX) {
            AmbientSFXComp.volume = OnVoiceEffectVolumeAmbient;
        }
        yield return Timing.WaitForSeconds(time);
        if (hasBackaudio) {
            musicBack.volume = backgroundMusicVolume;
        }
        if (hasAmbientFX) {
            AmbientSFXComp.volume = hasAmbientFXVolume;
        }
        isVoicePlaying = false;
    }

    public AudioSource startVoiceFXReturn(AudioClip VoiceSound) {
        if (gameConfig.isAudioVoiceOn) {
            AudioSource voic = VoiceEffectSource(VoiceSound);
            VoiceOverHandler();
            voic.volume = 1f;
            VoiceOverHandler();
            voiceFxs.Add(voic);
            voic.clip = VoiceSound;
            voic.Play();
            isVoicePlaying = true;
            Timing.KillCoroutines("soundVoice");
            Timing.RunCoroutine(BackVolumeOverTime(VoiceSound.length), "soundVoice");
            return voic;            
        }
        return null;
    }

    public void startVoiceFX(AudioClip VoiceSound, float volume){
		if (gameConfig.isAudioVoiceOn) {
            VoiceEffectSource(VoiceSound, volume).PlayOneShot(VoiceSound);
            VoiceOverHandler();
            Timing.KillCoroutines("soundVoice");
            Timing.RunCoroutine(BackVolumeOverTime(VoiceSound.length), "soundVoice");
        }
	}

	public void startVoiceFX(AudioClip VoiceSound, float volume, float delay){
		
		Timing.RunCoroutine (startVoiceFXDekat (VoiceSound, volume, delay));
	}


	IEnumerator<float> startVoiceFXDekat(AudioClip VoiceSound, float volume, float delay){
		yield return Timing.WaitForSeconds (delay);
		if (gameConfig.isAudioVoiceOn) {
            VoiceEffectSource(VoiceSound, volume).PlayOneShot(VoiceSound);
		}
	}

	IEnumerator<float> playAndDelete(GameObject soundFX){
		soundFX.GetComponent<AudioSource> ().Play();
		yield return Timing.WaitForSeconds (soundFX.GetComponent<AudioSource> ().clip.length);

		if (fxsList.Contains (soundFX.GetComponent<AudioSource> ())) {
			fxsList.Remove (soundFX.GetComponent<AudioSource> ());
		}

		Destroy (soundFX);
		fixSound();
	}

	public void OnPause(){
        _pause = true;
        stopCurrentFxs();
        StopVoiceEffects();
        if (musicBack == null) {
            musicBack = GetComponent<AudioSource>();
        }
        musicBack.Pause();
		AmbientSFXComp.Pause();
        

	}

	public void OnUnPause(){
        _pause = false;

        fixSound();

        if (gameConfig.isAudioFXOn){
			playCurrentsFxs();
		}

		if(gameConfig.isAudioOn){
            if(musicBack.clip == null) {
                musicBack.clip = backgroundMusic;
                if (!isVoicePlaying) {
                    musicBack.volume = backgroundMusicVolume;
                } else {
                    musicBack.volume = OnVoiceEffectVolumeBackground;
                }
                musicBack.Play();
            } else if(!musicBack.isPlaying && musicBack.clip != null) {
                musicBack.UnPause();
            }			
		}

		if(gameConfig.isAudioFXOn && hasAmbientFX){
            if (AmbientSFXComp.clip == null) {
                AmbientSFXComp.clip = AmbientSFX;
                if (!isVoicePlaying) {
                    AmbientSFXComp.volume = hasAmbientFXVolume;
                } else {
                    AmbientSFXComp.volume = OnVoiceEffectVolumeAmbient;
                }
                AmbientSFXComp.Play();
            } else if (!AmbientSFXComp.isPlaying && AmbientSFXComp.clip != null) {
                AmbientSFXComp.UnPause();
            }
        }

        if (!hasAmbientFX && AmbientSFXComp != null && AmbientSFXComp.isPlaying) {
            AmbientSFXComp.Stop();
        }

        if (gameConfig.isAudioVoiceOn) {
            //PlayVoiceEffects();
        }

        isStarted = true;

        
        

    }

    public void PlayEndGameTheme() {
        musicBack.Stop();
        musicBack.clip = endGameTheme;
        musicBack.Play();
    }

	public void EndAllCoroutines(){
        Timing.KillCoroutines();
        StopAllCoroutines();
	}

    

}
