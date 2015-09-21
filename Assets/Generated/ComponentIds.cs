using System;
using System.Collections.Generic;
using Entitas;

public static class ComponentIds {
    public const int Ai = 0;
    public const int Animate = 1;
    public const int AttachRoot = 2;
    public const int AttachTo = 3;
    public const int AttackInput = 4;
    public const int AttackOrder = 5;
    public const int Audio = 6;
    public const int BoundingMesh = 7;
    public const int BoxFormation = 8;
    public const int Children = 9;
    public const int Collision = 10;
    public const int DelayedDestroy = 11;
    public const int Destroy = 12;
    public const int Enemy = 13;
    public const int Force = 14;
    public const int Ground = 15;
    public const int Hidden = 16;
    public const int Input = 17;
    public const int MoveInput = 18;
    public const int Movement = 19;
    public const int MoveOrder = 20;
    public const int Parent = 21;
    public const int Physics = 22;
    public const int Player = 23;
    public const int Position = 24;
    public const int Reload = 25;
    public const int Resource = 26;
    public const int Rotation = 27;
    public const int Selected = 28;
    public const int SelectionArea = 29;
    public const int Squad = 30;
    public const int Stickable = 31;
    public const int Unit = 32;
    public const int UnitsCache = 33;
    public const int Velocity = 34;
    public const int View = 35;

    public const int TotalComponents = 36;

    static readonly string[] components = {
        "Ai",
        "Animate",
        "AttachRoot",
        "AttachTo",
        "AttackInput",
        "AttackOrder",
        "Audio",
        "BoundingMesh",
        "BoxFormation",
        "Children",
        "Collision",
        "DelayedDestroy",
        "Destroy",
        "Enemy",
        "Force",
        "Ground",
        "Hidden",
        "Input",
        "MoveInput",
        "Movement",
        "MoveOrder",
        "Parent",
        "Physics",
        "Player",
        "Position",
        "Reload",
        "Resource",
        "Rotation",
        "Selected",
        "SelectionArea",
        "Squad",
        "Stickable",
        "Unit",
        "UnitsCache",
        "Velocity",
        "View"
    };

    public static string IdToString(int componentId) {
        return components[componentId];
    }

    private static readonly IDictionary<Type, int> componentIds = new Dictionary<Type, int>() {
        { typeof (AiComponent), Ai },
        { typeof (AnimateComponent), Animate },
        { typeof (AttachRootComponent), AttachRoot },
        { typeof (AttachToComponent), AttachTo },
        { typeof (AttackInputComponent), AttackInput },
        { typeof (AttackOrderComponent), AttackOrder },
        { typeof (AudioComponent), Audio },
        { typeof (BoundingMeshComponent), BoundingMesh },
        { typeof (BoxFormationComponent), BoxFormation },
        { typeof (ChildrenComponent), Children },
        { typeof (CollisionComponent), Collision },
        { typeof (DelayedDestroyComponent), DelayedDestroy },
        { typeof (DestroyComponent), Destroy },
        { typeof (EnemyComponent), Enemy },
        { typeof (ForceComponent), Force },
        { typeof (GroundComponent), Ground },
        { typeof (HiddenComponent), Hidden },
        { typeof (InputComponent), Input },
        { typeof (MoveInputComponent), MoveInput },
        { typeof (MovementComponent), Movement },
        { typeof (MoveOrderComponent), MoveOrder },
        { typeof (ParentComponent), Parent },
        { typeof (PhysicsComponent), Physics },
        { typeof (PlayerComponent), Player },
        { typeof (PositionComponent), Position },
        { typeof (ReloadComponent), Reload },
        { typeof (ResourceComponent), Resource },
        { typeof (RotationComponent), Rotation },
        { typeof (SelectedComponent), Selected },
        { typeof (SelectionAreaComponent), SelectionArea },
        { typeof (SquadComponent), Squad },
        { typeof (StickableComponent), Stickable },
        { typeof (UnitComponent), Unit },
        { typeof (UnitsCacheComponent), UnitsCache },
        { typeof (VelocityComponent), Velocity },
        { typeof (ViewComponent), View }
    };

    public static int ComponentToId(IComponent component) {
        return componentIds[component.GetType()];
    }
}

namespace Entitas {
    public partial class Matcher : AllOfMatcher {
        public Matcher(int index) : base(new [] { index }) {
        }

        public override string ToString() {
            return ComponentIds.IdToString(indices[0]);
        }
    }
}