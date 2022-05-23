using UnityEngine;
using Sirenix.OdinInspector;

namespace Lab5Games
{
    public partial class AudioSystem
    {
        float _masterVolume;
        float _musicVolume;
        float _effectVolume;
        float _uiVolume;

        const string KEY_MASTER_VOLUME = "MasterVolume";
        const string KEY_MUSIC_VOLUME = "MusicVolume";
        const string KEY_EFFECT_VOLUME = "EffectVolume";
        const string KEY_UI_VOLUME = "UIVolume";


        [ShowInInspector, PropertyRange(0, 1)]
        public float MasterVolume
        {
            get => _masterVolume;

            set
            {
                _masterVolume = Mathf.Clamp01(value);

                if (Status == GameSystemStatus.Success)
                {
                    _audioMixer.SetFloat(KEY_MASTER_VOLUME, Mathf.Lerp(-80, 0, _masterVolume));
                }
            }
        }

        [ShowInInspector, PropertyRange(0, 1)]
        public float MusicVolume
        {
            get => _musicVolume;

            set
            {
                _musicVolume = Mathf.Clamp01(value);

                if (Status == GameSystemStatus.Success)
                {
                    _audioMixer.SetFloat(KEY_MUSIC_VOLUME, Mathf.Lerp(-80, 0, _musicVolume));
                }
            }
        }

        [ShowInInspector, PropertyRange(0, 1)]
        public float EffectVolume
        {
            get => _effectVolume;

            set
            {
                _effectVolume = Mathf.Clamp01(value);

                if (Status == GameSystemStatus.Success)
                {
                    _audioMixer.SetFloat(KEY_EFFECT_VOLUME, Mathf.Lerp(-80, 0, _effectVolume));
                }
            }
        }

        [ShowInInspector, PropertyRange(0, 1)]
        public float UIVolume
        {
            get => _uiVolume;

            set
            {
                _uiVolume = Mathf.Clamp01(value);

                if(Status == GameSystemStatus.Success)
                {
                    _audioMixer.SetFloat(KEY_UI_VOLUME, Mathf.Lerp(-80, 0, _uiVolume));
                }
            }
        }

        public void StopAll()
        {
            BackgroundMusic.Stop();

            for (int i = _playingEffectSounds.Count - 1; i >= 0; i--)
            {
                _playingEffectSounds[i].Stop();
            }

            for (int i = _playingUISounds.Count - 1; i >= 0; i--)
            {
                _playingUISounds[i].Stop();
            }
        }

        public void LoadSettings()
        {
            if(ShowLog)
                Logger.LogToFilter("[AudioSystem] Load settings", LogFilter.System, this);

            MasterVolume = PlayerPrefs.GetFloat(KEY_MASTER_VOLUME, 1);
            MusicVolume = PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 1);
            EffectVolume = PlayerPrefs.GetFloat(KEY_EFFECT_VOLUME, 1);
            UIVolume = PlayerPrefs.GetFloat(KEY_UI_VOLUME, 1);
        }

        public void SaveSettings()
        {
            if(ShowLog)
                Logger.LogToFilter("[AudioSystem] Save settings", LogFilter.System, this);

            PlayerPrefs.SetFloat(KEY_MASTER_VOLUME, MasterVolume);
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, MusicVolume);
            PlayerPrefs.SetFloat(KEY_EFFECT_VOLUME, EffectVolume);
            PlayerPrefs.SetFloat(KEY_UI_VOLUME, UIVolume);
        }

        public void PlayBackgroundMusic(AudioClip clip, float volume)
        {
            if(clip == null)
            {
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] Failed to play background music, playing clip is null", LogType.Warning, this);
                return;
            }

            BackgroundMusic.Play(clip, volume, 1, 0);
        }

        public void StopBackgroundMusic()
        {
            BackgroundMusic.Stop();
        }

        public Sound PlayEffectSound(AudioClip clip, float volume)
        {
            return PlayEffectSound(clip, volume, 1, 0);
        }

        public Sound PlayEffectSound(AudioClip clip, float volume, float pitch, float pan)
        {
            if(clip == null)
            {
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] Failed to play effect sound, playing clip is null",  LogType.Warning, this);
                return null;
            }

            Sound sound = NextAvailableSound(SoundType.Effect);

            if(sound == null)
            {
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] Failed to play effect sound, no available effect sound can used",  LogType.Warning, this);
                return null;
            }

            sound.Play(clip, volume, pitch, pan);
            _playingEffectSounds.Add(sound);

            return sound;
        }

        public Sound PlayUISound(AudioClip clip, float volume)
        {
            return PlayUISound(clip, volume, 1, 0);
        }

        public Sound PlayUISound(AudioClip clip, float volume, float pitch, float pan)
        {
            if (clip == null)
            {
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] Failed to play ui sound, playing clip is null", LogType.Warning, this);
                return null;
            }

            Sound sound = NextAvailableSound(SoundType.UI);

            if (sound == null)
            {
                if(ShowLog)
                    Logger.LogAsType("[AudioSystem] Failed to play ui sound, no available ui sound can used", LogType.Warning, this);
                return null;
            }

            sound.Play(clip, volume, pitch, pan);
            _playingUISounds.Add(sound);

            return sound;
        }
    }
}
