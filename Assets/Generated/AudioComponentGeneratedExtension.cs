using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AudioComponent audio { get { return (AudioComponent)GetComponent(ComponentIds.Audio); } }

        public bool hasAudio { get { return HasComponent(ComponentIds.Audio); } }

        static readonly Stack<AudioComponent> _audioComponentPool = new Stack<AudioComponent>();

        public static void ClearAudioComponentPool() {
            _audioComponentPool.Clear();
        }

        public Entity AddAudio(UnityEngine.AudioSource newAudioSource, System.Collections.Generic.List<UnityEngine.AudioClip> newSoundOptions) {
            var component = _audioComponentPool.Count > 0 ? _audioComponentPool.Pop() : new AudioComponent();
            component.AudioSource = newAudioSource;
            component.SoundOptions = newSoundOptions;
            return AddComponent(ComponentIds.Audio, component);
        }

        public Entity ReplaceAudio(UnityEngine.AudioSource newAudioSource, System.Collections.Generic.List<UnityEngine.AudioClip> newSoundOptions) {
            var previousComponent = hasAudio ? audio : null;
            var component = _audioComponentPool.Count > 0 ? _audioComponentPool.Pop() : new AudioComponent();
            component.AudioSource = newAudioSource;
            component.SoundOptions = newSoundOptions;
            ReplaceComponent(ComponentIds.Audio, component);
            if (previousComponent != null) {
                _audioComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAudio() {
            var component = audio;
            RemoveComponent(ComponentIds.Audio);
            _audioComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAudio;

        public static AllOfMatcher Audio {
            get {
                if (_matcherAudio == null) {
                    _matcherAudio = new Matcher(ComponentIds.Audio);
                }

                return _matcherAudio;
            }
        }
    }
}
