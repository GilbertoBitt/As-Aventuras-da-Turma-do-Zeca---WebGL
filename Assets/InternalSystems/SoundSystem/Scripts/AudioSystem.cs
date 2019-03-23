
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace InternalSystems.SoundSystem.Scripts{
    public class AudioSystem : SingletonScriptableObject<AudioSystem>{

        public AudioMixer MainAudioMixer;
        [HideInInspector]
        public bool HasMainAudioSource;

        [ShowIf("HasMainAudioSource")] [BoxGroup("Mixer Groups")]
        public AudioMixerGroup MasterAudioMixerGroup;
        [ShowIf("HasMainAudioSource")] [BoxGroup("Mixer Groups")]
        public AudioMixerGroup BackgroundAudioMixerGroup;
        [ShowIf("HasMainAudioSource", null)] [BoxGroup("Mixer Groups")]
        public AudioMixerGroup VoiceAudioMixerGroup;
        [ShowIf("HasMainAudioSource", null)] [BoxGroup("Mixer Groups")]
        public AudioMixerGroup EffectsAudioMixerGroup;

        [InlineEditor(InlineEditorModes.FullEditor)]
        public List<AudioContainer> AudioContainers = new List<AudioContainer>();
        
        private void OnValidate(){
            HasMainAudioSource = MainAudioMixer != null;
        }

        public AudioMixerGroup AudioMixerInstance(AudioContainer audioContainer){
            return AudioMixerInstance(audioContainer.TypeAudio);
        }

        public AudioMixerGroup AudioMixerInstance(AudioType audioType){
            switch (audioType){
                case AudioType.Background:
                    return BackgroundAudioMixerGroup;
                case AudioType.Effects:
                    return EffectsAudioMixerGroup;
                case AudioType.Voice:
                    return VoiceAudioMixerGroup;
                default:
                    return MasterAudioMixerGroup;
            }
        }
        
    }

    public enum AudioType{
        None = 0,
        Background = 1,
        Voice = 2,
        Effects = 3
    }
}
