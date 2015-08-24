using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Features
{
    public class SquadAudioSystem : IReactiveSystem
    {
        public IMatcher trigger { get { return Matcher.AllOf(Matcher.Audio, Matcher.MoveOrder); } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAddedOrRemoved; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
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