using Entitas;

public class TestPool : Pool
{
    public TestPool() : base(ComponentIds.TotalComponents)
    {
    }

    public TestPool(int startCreationIndex) : base(ComponentIds.TotalComponents, startCreationIndex)
    {
    }
}