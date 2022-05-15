using UnityEngine;

namespace Lab5Games
{
    public class SoundPlaying
    {
        public SoundType soundType = SoundType.Effect;

        [Range(0f, 1f)]
        public float volume = 1f;

        Sound _sound;

        public void Play(AudioClip clip)
        {
            switch(soundType)
            {
                case SoundType.Music:
                    AudioSystem.Instance.PlayBackgroundMusic(clip, volume);
                    _sound = AudioSystem.Instance.BackgroundMusic;
                    break;
                case SoundType.Effect:
                    _sound =  AudioSystem.Instance.PlayEffectSound(clip, volume);
                    break;
                case SoundType.UI:
                    _sound = AudioSystem.Instance.PlayUISound(clip, volume);
                    break;
            }
        }

        public void Stop()
        {
            if(_sound != null)
            {
                _sound.Stop();
            }
        }
    }
}
