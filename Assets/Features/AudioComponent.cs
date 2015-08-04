using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AudioComponent : IComponent
{
    public AudioSource AudioSource;
    public List<AudioClip> SoundOptions;
}