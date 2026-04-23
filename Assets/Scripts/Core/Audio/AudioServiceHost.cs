using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    public sealed class AudioServiceHost : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _audioSources;
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioConfig _soundConfig;

        public IReadOnlyList<AudioSource> Sources => _audioSources;
        public AudioSource BgmSource => _bgmSource;
        public AudioConfig Config => _soundConfig;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}