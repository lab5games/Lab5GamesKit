using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;

namespace Lab5Games
{
    public partial class AudioSystem : Singleton<AudioSystem>, IGameSystem
    {
        public GameSystemStatus Status { get; private set; }
        public string Message { get; private set; }

        public bool ShowLog = true;

        AudioMixer _audioMixer;

        Stack<Sound> _availableEffectSounds = new Stack<Sound>();
        Stack<Sound> _availableUISounds = new Stack<Sound>();
        List<Sound> _playingEffectSounds = new List<Sound>();
        List<Sound> _playingUISounds = new List<Sound>();

        const string KEY_AUDIO_MIXER_ADDRESS = "AudioMixer";

        const int MAX_EFFECT_SOUND_CAPACITY = 14;
        const int MAX_UI_SOUND_CAPACITY = 6;
       
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

                if (ShowLog)
                    Logger.LogToFilter($"[AudioSystem] Effect sound({indx}) stopped", LogFilter.System, this);
            }
        }

        void UISoundStopped(Sound sound)
        {
            if(_playingUISounds.Contains(sound))
            {
                int indx = _playingUISounds.IndexOf(sound);

                _playingUISounds.RemoveAt(indx);
                _availableUISounds.Push(sound);

                if (ShowLog)
                    Logger.LogToFilter($"[AudioSystem] UI sound({indx}) stopped", LogFilter.System, this);
            }
        }

        async void Start()
        {
            _audioMixer = await Addressables.LoadAssetAsync<AudioMixer>(KEY_AUDIO_MIXER_ADDRESS).Task;

            if(_audioMixer == null)
            {
                Status = GameSystemStatus.Failure;
                Message = "Failed to load AudioMixer";
                
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] " + Message, LogType.Error, this);
                
                return;
            }

            AudioMixerGroup musicMG = _audioMixer.FindMatchingGroups("Music").First(); 
            AudioMixerGroup effectMG = _audioMixer.FindMatchingGroups("SoundEffect").First();
            AudioMixerGroup uiMG = _audioMixer.FindMatchingGroups("UI").First();

            // Background Music
            GameObject goMusic = new GameObject("Music");
            goMusic.transform.SetParent(transform);

            AudioSource musicSrc = goMusic.AddComponent<AudioSource>();
            musicSrc.playOnAwake = false;
            musicSrc.outputAudioMixerGroup = musicMG;

            BackgroundMusic = new Sound(musicSrc);
            // Sound Effect
            GameObject goSoundEffect = new GameObject("SoundEffect");
            goSoundEffect.transform.SetParent(transform);

            for(int i=0; i<MAX_EFFECT_SOUND_CAPACITY; i++)
            {
                AudioSource effectSrc = goSoundEffect.AddComponent<AudioSource>();
                effectSrc.playOnAwake = false;
                effectSrc.outputAudioMixerGroup = effectMG;

                Sound effectSound = new Sound(effectSrc);
                effectSound.onStop += EffectSoundStopped;

                _availableEffectSounds.Push(effectSound);
            }
            // UI
            GameObject goUI = new GameObject("UI");
            goUI.transform.SetParent(transform);

            for(int i=0; i<MAX_UI_SOUND_CAPACITY; i++)
            {
                AudioSource uiSrc = goUI.AddComponent<AudioSource>();
                uiSrc.playOnAwake = false;
                uiSrc.outputAudioMixerGroup = uiMG;

                Sound uiSound = new Sound(uiSrc);
                uiSound.onStop += UISoundStopped;

                _availableUISounds.Push(uiSound);
            }

            LoadSettings();

            Status = GameSystemStatus.Success;
        }

        void Update()
        {
            if(Status == GameSystemStatus.Success)
            {
                for(int i=_playingEffectSounds.Count-1; i>=0; i--)
                {
                    if(!_playingEffectSounds[i].IsPlaying)
                    {
                        _playingEffectSounds[i].Stop();
                    }
                }

                for(int i=_playingUISounds.Count-1; i>=0; i--)
                {
                    if(!_playingUISounds[i].IsPlaying)
                    {
                        _playingUISounds[i].Stop();
                    }
                }
            }
        }
    }
}
