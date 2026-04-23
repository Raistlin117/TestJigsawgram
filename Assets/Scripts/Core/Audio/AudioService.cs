using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    public sealed class AudioService
    {
        private readonly List<AudioSource> _sources = new();
        private readonly Dictionary<AudioId, AudioClip> _clipMap = new();
        private readonly AudioSource _bgm;

        public AudioService(AudioServiceHost host)
        {
            _sources.AddRange(host.Sources);
            _bgm = host.BgmSource;
            foreach (var entry in host.Config.Audio)
                if (entry.Clip != null)
                    _clipMap[entry.Id] = entry.Clip;
            _bgm.loop = true;
            _bgm.playOnAwake = false;
        }

        public void Play(AudioId id)
        {
            if (_clipMap.TryGetValue(id, out var clip))
                Play(clip);
        }

        public void Play(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            var src = GetFreeSource();
            if (src == null) return;
            src.volume = volume;
            src.PlayOneShot(clip);
        }

        public void PlayMusic(AudioId id) => PlayMusic(GetClip(id));

        public void PlayMusic(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            _bgm.clip = clip;
            _bgm.volume = volume;
            _bgm.Play();
        }

        public void StopMusic() => _bgm.Stop();

        private AudioClip GetClip(AudioId id)
        {
            _clipMap.TryGetValue(id, out var clip);
            return clip;
        }

        private AudioSource GetFreeSource()
        {
            foreach (var src in _sources)
                if (!src.isPlaying)
                    return src;
            return null;
        }
    }
}