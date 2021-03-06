using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;
using Vector3 = Mono.GameMath.Vector3;
using Quaternion = Mono.GameMath.Quaternion;

public class RenderMoveArrowSystem : IReactiveSystem, IInitializeSystem
{
    private GameObject _arrow;
    private Transform _arrowBase;
    private Transform _arrowHead;
    private float _minLength;

    public TriggerOnEvent trigger { get { return Matcher.MoveInput.OnEntityAddedOrRemoved(); } }


    public void Initialize()
    {
        _arrow = (GameObject)Object.Instantiate(Resources.Load("MoveArrow"));
        _arrowBase = _arrow.transform.GetChild(0);
        _arrowHead = _arrow.transform.GetChild(1);
        _minLength = _arrowHead.localScale.z;
    }

    public void Execute(List<Entity> entities)
    {
        var inputEntity = entities.SingleEntity();
        if (inputEntity.hasMoveInput)
        {
            var moveInput = inputEntity.moveInput;
            var startToEnd = moveInput.Target - moveInput.Start;

            SetPositions(moveInput.Start, moveInput.Target);
            SetRotation(Quaternion.LookAt(-startToEnd.Normalized()));
            HideIfTooShort(startToEnd);
        }
        else
        {
            _arrow.SetActive(false);
        }
    }

    private void SetPositions(Vector3 start, Vector3 end)
    {
        _arrowHead.position = end.ToUnityV3();
        _arrowBase.position = start.ToUnityV3();
        var baseScale = (end - start).Length() - _arrowHead.localScale.z;
        _arrowBase.localScale = _arrowBase.localScale.SetZ(baseScale);
    }

    private void SetRotation(Quaternion rotation)
    {
        _arrowHead.rotation = rotation.ToUnityQ();
        _arrowBase.rotation = rotation.ToUnityQ();
    }

    private void HideIfTooShort(Vector3 startToEnd)
    {
        _arrow.SetActive(startToEnd.Length() >= _minLength);
    }
}