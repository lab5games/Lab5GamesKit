using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Lab5Games
{
    public partial class AudioSystem : Singleton<AudioSystem>
    {
        public bool ShowLog = true;

        [SerializeField] AudioSource backgroundMusicSrc;
        [SerializeField] AudioSource[] soundEffectSrc;
        [SerializeField] AudioSource[] uiEffectSrc;

        [SerializeField] AudioMixer audioMixer;
        [SerializeField] string masterVolumeParameter = "MasterVolume";
        [SerializeField] string musicVolumeParameter = "MusicVolume";
        [SerializeField] string effectVolumeParameter = "EffectVolume";
        [SerializeField] string uiVolumeParameter = "UIVolume";

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
            AudioMixerGroup musicGroup = audioMixer.FindMatchingGroups("Music").First();
            backgroundMusicSrc.playOnAwake = false;
            backgroundMusicSrc.outputAudioMixerGroup = musicGroup;
            BackgroundMusic = new Sound(backgroundMusicSrc);

            // Sound Effect
            AudioMixerGroup effectGroup = audioMixer.FindMatchingGroups("SoundEffect").First();

            for(int i=0; i<soundEffectSrc.Length; i++)
            {
                soundEffectSrc[i].playOnAwake = false;
                soundEffectSrc[i].outputAudioMixerGroup = effectGroup;

                Sound effectSound = new Sound(soundEffectSrc[i]);
                effectSound.onStop += EffectSoundStopped;

                _availableEffectSounds.Push(effectSound);
            }

            // UI Effect
            AudioMixerGroup uiGroup = audioMixer.FindMatchingGroups("UI").First();

            for(int i=0; i<uiEffectSrc.Length; i++)
            {
                uiEffectSrc[i].playOnAwake = false;
                uiEffectSrc[i].outputAudioMixerGroup = uiGroup;

                Sound uiSound = new Sound(uiEffectSrc[i]);
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
