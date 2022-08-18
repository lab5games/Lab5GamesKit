using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Lab5Games
{
    public class AudioManager : ComponentSingleton<AudioManager>
    {
        public override bool IsPersistent => false;

        public static int DEFAULT_AUDIO_SOURCE_CAPACITY = 12;

        public const string KEY_MASTER_VOLUME = "Master_Volume";
        public const string KEY_MUSIC_VOLUME = "Music_Volume";
        public const string KEY_SFX_VOLUME = "SFX_Volume";
        public const string KEY_UI_VOLUME = "UI_Volume";

        [SerializeField]
        AudioMixer audioMixer;

#if ODIN_INSPECTOR
        [ShowInInspector, ShowIf("@this.audioMixer != null")] 
        [PropertyOrder(1), PropertyRange(0f, 1f)]
        [InfoBox("Exposed parameter named 'Master_Volume'")]
        [PropertySpace(SpaceAfter =20)]
#endif
        public float MasterVolume
        {
            get => _volume_master;

            set
            {
                _volume_master = Mathf.Clamp01(value);

                if(audioMixer)
                    audioMixer.SetFloat(KEY_MASTER_VOLUME, Mathf.Lerp(-80f, 0f, _volume_master));
            }
        }

#if ODIN_INSPECTOR
        [PropertyOrder(2)]
        [TabGroup("Music")]
        [LabelText("Group"), ShowIf("@this.audioMixer != null")]
#endif
        [SerializeField] AudioMixerGroup musicGroup;

#if ODIN_INSPECTOR
        [PropertyOrder(2), PropertyRange(0f, 1f)]
        [TabGroup("Music"), ShowInInspector]
        [InfoBox("Exposed parameter named 'Music_Volume'")]
        [LabelText("Volume"), ShowIf("@this.audioMixer != null && this.musicGroup != null")]
#endif
        public float MusicVolume
        {
            get => _volume_music;

            set
            {
                _volume_music = Mathf.Clamp01(value);

                if (audioMixer)
                    audioMixer.SetFloat(KEY_MUSIC_VOLUME, Mathf.Lerp(-80f, 0f, _volume_music));
            }
        }

#if ODIN_INSPECTOR
        [PropertyOrder(3)]
        [TabGroup("SFX")]
        [LabelText("Group"), ShowIf("@this.audioMixer != null")]
#endif
        [SerializeField] AudioMixerGroup sfxGroup;

#if ODIN_INSPECTOR
        [PropertyOrder(3), PropertyRange(0f, 1f)]
        [TabGroup("SFX"), ShowInInspector]
        [InfoBox("Exposed parameter named 'SFX_Volume'")]
        [LabelText("Volume"), ShowIf("@this.audioMixer != null && this.sfxGroup != null")]
#endif
        public float SFXVolume
        {
            get => _volume_sfx;

            set
            {
                _volume_sfx = Mathf.Clamp01(value);

                if (audioMixer)
                    audioMixer.SetFloat(KEY_SFX_VOLUME, Mathf.Lerp(-80f, 0f, _volume_sfx));
            }
        }

#if ODIN_INSPECTOR
        [PropertyOrder(4)]
        [TabGroup("UI")]
        [LabelText("Group"), ShowIf("@this.audioMixer != null")]
#endif
        [SerializeField] AudioMixerGroup uiGroup;

#if ODIN_INSPECTOR
        [PropertyOrder(4), PropertyRange(0f, 1f)]
        [TabGroup("UI"), ShowInInspector]
        [InfoBox("Exposed parameter named 'UI_Volume'")]
        [LabelText("Volume"), ShowIf("@this.audioMixer != null && this.uiGroup != null")]
#endif
        public float UIVolume
        {
            get => _volume_ui;

            set
            {
                _volume_ui = Mathf.Clamp01(value);

                if (audioMixer)
                    audioMixer.SetFloat(KEY_UI_VOLUME, Mathf.Lerp(-80f, 0f, _volume_ui));
            }
        }

        Sound _music;
        Stack<Sound> _availableSounds = new Stack<Sound>();
        List<Sound> _playingSounds = new List<Sound>();

        float _volume_master, _volume_music, _volume_sfx, _volume_ui;

        public Sound Music => _music;

        public void LoadSettings()
        {
            GLogger.LogToFilter("[AudioManager] Load audio settings", GLogFilter.System, this);

            MasterVolume = PlayerPrefs.GetFloat(KEY_MASTER_VOLUME, 1);
            MusicVolume = PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 1);
            SFXVolume = PlayerPrefs.GetFloat(KEY_SFX_VOLUME, 1);
            UIVolume = PlayerPrefs.GetFloat(KEY_UI_VOLUME, 1);
        }

        public void SaveSettings()
        {
            GLogger.LogToFilter("[AudioManager] Save audio settings", GLogFilter.System, this);

            PlayerPrefs.SetFloat(KEY_MASTER_VOLUME, MasterVolume);
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, MusicVolume);
            PlayerPrefs.SetFloat(KEY_SFX_VOLUME, SFXVolume);
            PlayerPrefs.SetFloat(KEY_UI_VOLUME, UIVolume);
        }

        public void StopAll()
        {
            GLogger.LogToFilter("[AudioManager] Stop all sounds.", GLogFilter.System, this);

            Music.Stop();

            for(int i=_playingSounds.Count-1; i>=0;i--)
            {
                _playingSounds[i].Stop();
            }
        }

        public Sound PlaySound(SoundType soundType, AudioClip clip, float volume = 1, float pitch = 1, float pan = 0, AudioSource source = null)
        {
            if (clip == null)
            {
                GLogger.LogAsType("[AudioManager] Cannot play null clip", GLogType.Warning, this);
                return null;
            }

            // play music 
            if(soundType == SoundType.Music)
            {
                PlayMusic(clip, volume);
                return Music;
            }

            // play effect sound
            Sound sound = null;

            if (source == null)
                sound = NextAvailableSound();
            else
                sound = new Sound(source, false); 

            if(sound == null)
            {
                return null;
            }

            // assign mixer group
            AudioMixerGroup mixerGroup = soundType switch
            {
                SoundType.SFX => sfxGroup,
                SoundType.UI => uiGroup,
                _ => null
            };

            sound.Play(clip, volume, pitch, pan);
            _playingSounds.Add(sound);

            return sound;
        }

        public void PlayMusic(AudioClip clip, float volume)
        {
            if (clip == null)
            {
                GLogger.LogAsType("[AudioManager] Cannot play null clip", GLogType.Warning, this);
                return;
            }

            Music.Play(clip, volume, 1, 0);
            Music.Loop = true;
        }


        private void Init()
        {
            // music
            AudioSource audiosrc = gameObject.AddComponent<AudioSource>();
            audiosrc.playOnAwake = false;   
            audiosrc.outputAudioMixerGroup = musicGroup;

            _music = new Sound(audiosrc, true);

            // sound effects
            for(int i= 0; i < DEFAULT_AUDIO_SOURCE_CAPACITY; i++)
            {
                audiosrc = gameObject.AddComponent<AudioSource>();
                audiosrc.playOnAwake = false;

                Sound sound = new Sound(audiosrc, true);
                sound.onStop += OnStopSound;

                _availableSounds.Push(sound);
            }
        }

        private void StopEndSounds()
        {
            for(int i=_playingSounds.Count-1; i>=0; i--)
            {
                if (!_playingSounds[i].IsPlaying)
                {
                    _playingSounds[i].Stop();
                }
            }
        }

        private Sound NextAvailableSound()
        {
            if (_availableSounds.Count > 0)
                return _availableSounds.Pop();


            GLogger.LogAsType($"[AudioManager] Not Enough AudioSource, Playing Sounds= {_playingSounds.Count}", GLogType.Warning, this);
            return null;
        }

        private void OnStopSound(Sound sound)
        {
            if(_playingSounds.Remove(sound))
            {
                if (sound.BelongAsAudioManager)
                {
                    _availableSounds.Push(sound);
                }
            }
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            StopEndSounds();
        }
    }
}
