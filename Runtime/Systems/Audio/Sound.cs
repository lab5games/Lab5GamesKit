using System;
using System.Collections;
using UnityEngine;

namespace Lab5Games
{
    public class Sound
    {
        AudioSource _audio;

        public bool Loop
        {
            get => _audio.loop;
            set => _audio.loop = value;
        }

        public bool IsPlaying
        {
            get => _audio.isPlaying;
        }

        public event Action<Sound> onStop;

        public Sound(AudioSource audioSource)
        {
            if (audioSource == null)
                throw new ArgumentNullException(nameof(audioSource));

            _audio = audioSource;
        }

        public void Release()
        {
            UnityEngine.Object.Destroy(_audio);
        }

        public void Stop()
        {
            _audio.Stop();
            _audio.clip = null;

            onStop?.Invoke(this);
        }

        public void Play(AudioClip clip, float volume, float pitch, float pan)
        {
            _audio.clip = clip;
            _audio.volume = volume;
            _audio.pitch = pitch;
            _audio.panStereo = pan;
            _audio.loop = false;

            _audio.Play();
        }

        public async void FadeOutAndStop(float duration)
        {
            await RoutineSchedule.Create(TaskFadeOut(duration));

            Stop();
        }

        IEnumerator TaskFadeOut(float duration)
        {
            float startVolume = _audio.volume;

            while(_audio.volume > 0)
            {
                _audio.volume -= Time.deltaTime * startVolume / duration;
                yield return Yielders.EndOfFrame;
            }
        }
    }
}
