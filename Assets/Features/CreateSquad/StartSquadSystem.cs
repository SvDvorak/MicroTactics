using Entitas;

class StartSquadSystem : IStartSystem, ISetPool
{
    private Pool _pool;

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Start()
    {
        _pool.CreateEntity().AddSquad(4, 4);
    }
}