using System;
using BehaviourMachine;

[NodeInfo(category = "Test/")]
public class FakeClick : ActionNode
{
    public int Button;

    public override void Start()
    {
        var input = new TestInput();
        input.SetMouseButtonDown(Button);
        WilInput.Instance = input;

        base.Start();
    }

    public override Status Update()
    {
        return Status.Success;
    }
}