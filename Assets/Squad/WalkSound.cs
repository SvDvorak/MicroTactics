using UnityEngine;
using System.Collections;
using System.Linq;

public class WalkSound : MonoBehaviour
{
    public AudioClip[] WalkSounds;
    private AudioSource _audioSource;
    private SquadState _squadState;
    private float _pauseDelay;
    public float MaxPauseDelay;

    void Start ()
    {
        _audioSource = GetComponentInParent<AudioSource>();
        _squadState = GetComponentInParent<SquadState>();
        _pauseDelay = 0;
    }

    void Update()
    {
        if (_squadState.IsMoving)
        {
            PlayWalkSound();
        }
    }

    public void PlayWalkSound()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }

        _pauseDelay += Time.deltaTime;

        if (_pauseDelay > MaxPauseDelay)
        {
            _audioSource.clip = WalkSounds[Random.Range(0, WalkSounds.Count())];
            _audioSource.pitch = Random.Range(0.8f, 1.1f);
            _audioSource.Play();
            _pauseDelay = 0;
        }
    }
}
