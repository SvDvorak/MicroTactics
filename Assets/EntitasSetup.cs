using UnityEngine;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class EntitasSetup : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        _systems = CreateSystems(Pools.pool);
        _systems.Start();
    }

    void Update()
    {
        _systems.Execute();
    }

    private Systems CreateSystems(Pool pool)
    {
#if (UNITY_EDITOR)
        return new DebugSystems()
#else
        return new Systems()
#endif
            .Add(pool.CreateLinkViewsStartSystem())
            .Add(pool.CreateSquadCreationSystem())
            .Add(pool.CreateUnitsCacheSystem())
            .Add(pool.CreateSquadCenterPositionSystem())

            .Add(pool.CreateAddUnitViewSystem())

            .Add(pool.CreateAiMoveOrderSystem())
            .Add(pool.CreateAiAttackOrderSystem())

            .Add(pool.CreateSquadMoveOrderSystem())
            .Add(pool.CreateMoveSystem())

            .Add(pool.CreateRenderPositionSystem())
            .Add(pool.CreateAnimateUnitSystem())
            .Add(pool.CreateSquadAudioSystem())

            .Add(pool.CreateRemoveViewSystem())
            .Add(pool.CreateRemoveMoveOrderSystem())
            .Add(pool.CreateDestroySystem());
    }
}