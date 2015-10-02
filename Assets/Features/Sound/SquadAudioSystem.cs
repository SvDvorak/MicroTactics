using Entitas;

namespace Assets.Features
{
    public class SquadAudioSystem : IExecuteSystem, ISetPool
    {
        private Group _audioGroup;

        public void SetPool(Pool pool)
        {
            _audioGroup = pool.GetGroup(Matcher.Audio);
        }

        public void Execute()
        {
            foreach (var entity in _audioGroup.GetEntities())
            {
                var audioSource = entity.audio.AudioSource;

                if (!audioSource.isPlaying && entity.hasMoveOrder)
                {
                    audioSource.clip = entity.audio.SoundOptions[UnityEngine.Random.Range(0, entity.audio.SoundOptions.Count)];
                    audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                    audioSource.Play();
                }
                else if (!entity.hasMoveOrder)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}