using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio/AudioConfig", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        public List<SoundEntry> Audio;

        [Serializable]
        public class SoundEntry
        {
            public AudioId Id;
            public AudioClip Clip;
        }
    }
}