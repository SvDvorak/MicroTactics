using Entitas;
using Mono.GameMath;

namespace MicroTactics.Tests.Features
{
    public abstract class AiOrderTestsBase
    {
        protected readonly TestPool Pool;
        private int _squadNumberIterator;

        protected AiOrderTestsBase()
        {
            Pool = new TestPool();
        }

        protected Entity CreateEnemyAt(float x, float y, float z)
        {
            return CreateSquadAt(x, y, z).IsPlayer(true);
        }

        protected Entity CreateAiAt(float range, Vector3 vector)
        {
            return CreateSquadAt(vector.X, vector.Y, vector.Z).AddAi(range);
        }

        private Entity CreateSquadAt(float x, float y, float z)
        {
            return Pool.CreateEntity().AddSquad(_squadNumberIterator++).AddPosition(x, y, z);
        }
    }
}