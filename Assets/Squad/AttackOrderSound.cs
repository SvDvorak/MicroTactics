using UnityEngine;
using System.Collections;

public class AttackOrderSound : MonoBehaviour
{
    public AudioClip OrderSound;

    private AudioSource _audioSource;
    private SquadState _squadState;
    private Interaction _previousInteractState;

    void Start ()
	{
        _audioSource = GetComponentInParent<AudioSource>();
        _squadState = GetComponentInParent<SquadState>();
    }

    void Update ()
    {
        if (_previousInteractState != _squadState.InteractState && _squadState.InteractState == Interaction.Attack)
        {
            _audioSource.clip = OrderSound;
            _audioSource.Play();
        }

        _previousInteractState = _squadState.InteractState;
    }
}
