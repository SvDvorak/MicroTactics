﻿using UnityEngine;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class EntitasSetup : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        _systems = CreateSystems(Pools.pool);
        _systems.Initialize();
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
            .Add(pool.CreateReadPhysicsSystem())

            .Add(pool.CreateCreateSquadSystem())
            .Add(pool.CreateSelectionAreaDecoratorSystem())
            .Add(pool.CreateUnitsCacheSystem())
            .Add(pool.CreateSquadCenterPositionSystem())
            .Add(pool.CreateSelectionAreaUpdateSystem())
            .Add(pool.CreateMeshUpdateSystem())

            .Add(pool.CreateAddViewSystem())
            .Add(pool.CreateAddViewParentSystem())

            .Add(pool.CreateAiMoveOrderSystem())
            .Add(pool.CreateAiAttackOrderSystem())

            .Add(pool.CreateSelectInteractionSystem())
            .Add(pool.CreateSelectedInteractionSystem())
            .Add(pool.CreateMoveInteractionSystem())
            .Add(pool.CreateAttackInteractionSystem())
            .Add(pool.CreateRenderAttackArrowSystem())
            .Add(pool.CreateRenderMoveArrowSystem())
            .Add(pool.CreateShowSelectedIndicatorForSquadSystem())

            .Add(pool.CreateSquadMoveOrderSystem())
            .Add(pool.CreateSquadAttackOrderSystem())
            .Add(pool.CreateMoveSystem())
            .Add(pool.CreateAttackSystem())
            .Add(pool.CreateReloadSystem())

            .Add(pool.CreateArrowStickToCollidedSystem())
            .Add(pool.CreateCollisionDamageSystem())

            .Add(pool.CreateApplyPhysicsSystem())
            .Add(pool.CreateRenderPositionSystem())
            .Add(pool.CreateRenderRotationSystem())
            .Add(pool.CreateHideHiddenSystem())
            .Add(pool.CreateAnimateUnitSystem())
            .Add(pool.CreateSquadAudioSystem())

            .Add(pool.CreateDelayedDestroySystem())
            .Add(pool.CreateRemoveMoveOrderSystem())
            .Add(pool.CreateRemoveViewSystem())
            .Add(pool.CreateRemovePhysicsSystem())
            .Add(pool.CreateClearCollisionsSystem())
            .Add(pool.CreateDestroySystem());
    }
}