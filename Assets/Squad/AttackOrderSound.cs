using UnityEngine;
using System.Collections;

public class AttackOrderSound : MonoBehaviour
{
    public AudioClip OrderSound;

    private AudioSource _audioSource;
    private SquadState _squadState;
    private SquadState.Interaction _previousInteractState;

    void Start ()
	{
        _audioSource = GetComponentInParent<AudioSource>();
        _squadState = GetComponentInParent<SquadState>();
    }

    void Update ()
    {
        if (_previousInteractState != _squadState.InteractState && _squadState.InteractState == SquadState.Interaction.Attack)
        {
            _audioSource.clip = OrderSound;
            _audioSource.Play();
        }

        _previousInteractState = _squadState.InteractState;
    }
}
