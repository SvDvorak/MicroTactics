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
                //_pauseDelay += Time.deltaTime;

                //if (_pauseDelay > MaxPauseDelay)
                //{
                //_audioSource.clip = WalkSounds[Random.Range(0, WalkSounds.Count())];
                //_audioSource.pitch = Random.Range(0.8f, 1.1f);
                //_audioSource.Play();
                var audioSource = entity.audio.AudioSource;

                if (!audioSource.isPlaying && entity.hasMoveOrder)
                {
                    audioSource.Play();
                }
                if (!entity.hasMoveOrder)
                {
                    audioSource.Stop();
                }
                //    _pauseDelay = 0;
                //}
            }
        }
    }
}