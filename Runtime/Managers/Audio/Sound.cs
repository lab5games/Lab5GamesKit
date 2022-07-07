using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Lab5Games
{
    public class Sound
    {
        public readonly AudioSource AudioSource;
        public readonly bool BelongAsAudioManager;

        public bool Loop
        {
            get => AudioSource.loop;
            set => AudioSource.loop = value;
        }

        public bool IsPlaying
        {
            get => AudioSource.isPlaying;
        }

        public event Action<Sound> onStop;

        public Sound(AudioSource soruce, bool belongAsAudioManager)
        {
            if (soruce == null)
                throw new ArgumentNullException(nameof(soruce));

            AudioSource = soruce;
            BelongAsAudioManager = belongAsAudioManager;
        }

        public void Stop()
        {
            AudioSource.Stop();
            AudioSource.clip = null;

            onStop?.Invoke(this);
        }

        public void Play(AudioClip clip, float volume, float pitch, float pan)
        {
            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.pitch = pitch;
            AudioSource.panStereo = pan;
            AudioSource.loop = false;

            AudioSource.Play();
        }

        public async void FadeOutAndStop(float duration)
        {
            float startVolume = AudioSource.volume;

            while(AudioSource.volume > 0)
            {
                AudioSource.volume -= ((startVolume / duration) * Time.deltaTime);
                await Task.Yield();
            }

            Stop();
        }
    }
}
