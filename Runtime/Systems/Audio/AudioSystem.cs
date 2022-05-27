using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

namespace Lab5Games
{
    public partial class AudioSystem : Singleton<AudioSystem>
    {
        public bool ShowLog = true;

        [SerializeField] int soundEffectCapacity = 8;
        [SerializeField] int uiEffectCapacity = 6;

        [SerializeField, Required] AudioMixer audioMixer;
        [SerializeField] string masterVolumeParameter = "MasterVolume";
        [SerializeField] string musicVolumeParameter = "MusicVolume";
        [SerializeField] string effectVolumeParameter = "EffectVolume";
        [SerializeField] string uiVolumeParameter = "UIVolume";
        [SerializeField] string musicGroup = "Music";
        [SerializeField] string effectGroup = "SoundEffect";
        [SerializeField] string uiGroup = "UI";

        Stack<Sound> _availableEffectSounds = new Stack<Sound>();
        Stack<Sound> _availableUISounds = new Stack<Sound>();
        List<Sound> _playingEffectSounds = new List<Sound>();
        List<Sound> _playingUISounds = new List<Sound>();

        public Sound BackgroundMusic { get; private set; }

        Sound NextAvailableSound(SoundType soundType)
        {
            if(soundType == SoundType.Effect)
            {
                if (_availableEffectSounds.Count > 0)
                    return _availableEffectSounds.Pop();
            }

            if(soundType == SoundType.UI)
            {
                if (_availableUISounds.Count > 0)
                    return _availableUISounds.Pop();
            }

            return null;
        }

        void EffectSoundStopped(Sound sound)
        {
            if (_playingEffectSounds.Contains(sound))
            {
                int indx = _playingEffectSounds.IndexOf(sound);

                _playingEffectSounds.RemoveAt(indx);
                _availableEffectSounds.Push(sound);
            }
        }

        void UISoundStopped(Sound sound)
        {
            if(_playingUISounds.Contains(sound))
            {
                int indx = _playingUISounds.IndexOf(sound);

                _playingUISounds.RemoveAt(indx);
                _availableUISounds.Push(sound);
            }
        }

        private void Start()
        {
            // Background Music
            AudioMixerGroup musicGroup = audioMixer.FindMatchingGroups(this.musicGroup).First();

            GameObject goMusic = new GameObject(this.musicGroup);
            goMusic.transform.SetParent(transform);

            var bgmSrc = goMusic.AddComponent<AudioSource>();
            bgmSrc.playOnAwake = false;
            bgmSrc.outputAudioMixerGroup = musicGroup;
            BackgroundMusic = new Sound(bgmSrc);

            // Sound Effect
            AudioMixerGroup effectGroup = audioMixer.FindMatchingGroups(this.effectGroup).First();

            GameObject goSFx = new GameObject(this.effectGroup);
            goSFx.transform.SetParent(transform);

            for (int i=0; i<soundEffectCapacity; i++)
            {
                var audioSrc = goSFx.AddComponent<AudioSource>();
                audioSrc.playOnAwake = false;
                audioSrc.outputAudioMixerGroup = effectGroup;

                Sound effectSound = new Sound(audioSrc);
                effectSound.onStop += EffectSoundStopped;

                _availableEffectSounds.Push(effectSound);
            }

            // UI Effect
            AudioMixerGroup uiGroup = audioMixer.FindMatchingGroups(this.uiGroup).First();

            GameObject goUIFx = new GameObject(this.uiGroup);
            goUIFx.transform.SetParent(transform);

            for (int i=0; i<uiEffectCapacity; i++)
            {
                var audioSrc = goUIFx.AddComponent<AudioSource>();
                audioSrc.playOnAwake = false;
                audioSrc.outputAudioMixerGroup = effectGroup;

                Sound uiSound = new Sound(audioSrc);
                uiSound.onStop += UISoundStopped;

                _availableUISounds.Push(uiSound);
            }

            LoadSettings();
        }

        void Update()
        {
            for (int i = _playingEffectSounds.Count - 1; i >= 0; i--)
            {
                if (!_playingEffectSounds[i].IsPlaying)
                {
                    _playingEffectSounds[i].Stop();
                }
            }

            for (int i = _playingUISounds.Count - 1; i >= 0; i--)
            {
                if (!_playingUISounds[i].IsPlaying)
                {
                    _playingUISounds[i].Stop();
                }
            }
        }
    }
}
