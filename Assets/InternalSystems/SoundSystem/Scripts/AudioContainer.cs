using InternalSystems.SoundSystem.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using AudioType = InternalSystems.SoundSystem.Scripts.AudioType;

namespace InternalSystems.SoundSystem{
	public class AudioContainer : SerializedScriptableObject{

		public AudioClip Audio;
		public AudioMixerGroup MixerGroup;
		private AudioSource _audioSource;
		public AudioType TypeAudio;
		[Range(0,1)]
		public float Volume;

		private void OnValidate(){
			if (AudioSystem.Instance.AudioContainers != null && AudioSystem.Instance.AudioContainers.Contains(this)){
				AudioSystem.Instance.AudioContainers.Add(this);
			}
		}

		public void AudioSource(float volume = 1f){
			var audioGameObject = new GameObject(this.Audio.name);
			_audioSource = audioGameObject.AddComponent(typeof(AudioSource)) as AudioSource;
			if(_audioSource == null) return;
			_audioSource.outputAudioMixerGroup = AudioSystem.Instance.AudioMixerInstance(this);
			_audioSource.volume = Volume;
		}

	}
}
