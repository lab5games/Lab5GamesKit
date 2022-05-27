using System.Collections.Generic;
using UnityEngine.Audio;


namespace Lab5Games
{
    public partial class AudioSystem : Singleton<AudioSystem>, ISystem
    {
        public SystemStatus Status { get; private set; }
        public string Message { get; private set; }

        public bool ShowLog = true;

        AudioMixer _audioMixer;

        Stack<Sound> _availableEffectSounds = new Stack<Sound>();
        Stack<Sound> _availableUISounds = new Stack<Sound>();
        List<Sound> _playingEffectSounds = new List<Sound>();
        List<Sound> _playingUISounds = new List<Sound>();

        const string KEY_AUDIO_MIXER_ADDRESS = "AudioMixer";

        public static int MAX_EFFECT_SOUND_CAPACITY = 14;
        public static int MAX_UI_SOUND_CAPACITY = 6;
       
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
                    GLogger.LogToFilter($"[AudioSystem] Effect sound({indx}) stopped", GLogFilter.System, this);
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
                    GLogger.LogToFilter($"[AudioSystem] UI sound({indx}) stopped", GLogFilter.System, this);
            }
        }

        void Update()
        {
            if(Status == SystemStatus.Success)
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
