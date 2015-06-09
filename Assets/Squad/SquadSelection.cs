using UnityEngine;
using System.Collections;

public class SquadSelectedEvent : GameEvent
{
    public SquadState SelectedSquad { get; private set; }

    public SquadSelectedEvent(SquadState selectedSquad)
    {
        SelectedSquad = selectedSquad;
    }
}

public class SquadSelection : SquadInteractionBase
{
    private SquadState _squadState;
    private int _groupLayer;
    private bool _justSelected;
    private bool _startedSelection;

    void Start ()
    {
        _groupLayer = 1 << LayerMask.NameToLayer("Squad");
        _squadState = GetComponent<SquadState>();
    }

    public void OnEnable()
    {
        Events.instance.AddListener<SquadSelectedEvent>(Select);
    }

    public void OnDisable()
    {
        Events.instance.RemoveListener<SquadSelectedEvent>(Select);
    }

    public override void MouseDown(RaycastHit value)
    {
        if (value.transform.parent == transform)
        {
            _startedSelection = true;
        }
    }

    public override void MouseUp(RaycastHit value)
    {
        if (_startedSelection && value.transform.parent == transform)
        {
            Events.instance.Raise(new SquadSelectedEvent(_squadState));
        }

        _startedSelection = false;
    }

    public void Select(SquadSelectedEvent eventInfo)
    {
        if (eventInfo.SelectedSquad == _squadState)
        {
            _squadState.InteractState = Interaction.Idle;
            _justSelected = true;
        }
        else
        {
            _squadState.InteractState = Interaction.Unselected;
        }
    }

    public override int GetLayersToUse()
    {
        return _groupLayer;
    }

    public override bool IsDominant()
    {
        if (_justSelected)
        {
            _justSelected = false;
            return true;
        }

        return false;
    }
}