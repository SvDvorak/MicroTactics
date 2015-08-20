using System;
using System.Collections.Generic;
using Entitas;

public static class ComponentIds {
    public const int Ai = 0;
    public const int Animate = 1;
    public const int Arrow = 2;
    public const int AttackInput = 3;
    public const int AttackOrder = 4;
    public const int Audio = 5;
    public const int BoundingMesh = 6;
    public const int BoxFormation = 7;
    public const int Child = 8;
    public const int Collision = 9;
    public const int Destroy = 10;
    public const int Enemy = 11;
    public const int Force = 12;
    public const int Ground = 13;
    public const int Hidden = 14;
    public const int Input = 15;
    public const int MoveInput = 16;
    public const int Movement = 17;
    public const int MoveOrder = 18;
    public const int Parent = 19;
    public const int Physics = 20;
    public const int Player = 21;
    public const int Position = 22;
    public const int Resource = 23;
    public const int Rotation = 24;
    public const int Selected = 25;
    public const int SelectionArea = 26;
    public const int Squad = 27;
    public const int Unit = 28;
    public const int UnitsCache = 29;
    public const int Velocity = 30;
    public const int View = 31;

    public const int TotalComponents = 32;

    static readonly string[] components = {
        "Ai",
        "Animate",
        "Arrow",
        "AttackInput",
        "AttackOrder",
        "Audio",
        "BoundingMesh",
        "BoxFormation",
        "Child",
        "Collision",
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
        "Resource",
        "Rotation",
        "Selected",
        "SelectionArea",
        "Squad",
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
        { typeof (ArrowComponent), Arrow },
        { typeof (AttackInputComponent), AttackInput },
        { typeof (AttackOrderComponent), AttackOrder },
        { typeof (AudioComponent), Audio },
        { typeof (BoundingMeshComponent), BoundingMesh },
        { typeof (BoxFormationComponent), BoxFormation },
        { typeof (ChildComponent), Child },
        { typeof (CollisionComponent), Collision },
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
        { typeof (ResourceComponent), Resource },
        { typeof (RotationComponent), Rotation },
        { typeof (SelectedComponent), Selected },
        { typeof (SelectionAreaComponent), SelectionArea },
        { typeof (SquadComponent), Squad },
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