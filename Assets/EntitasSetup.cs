﻿using UnityEngine;
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
            .Add(pool.CreateStartSquadSystem())
            .Add(pool.CreateSquadCreationSystem());
    }
}