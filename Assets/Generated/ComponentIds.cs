using System;
using System.Collections.Generic;
using Entitas;

public static class ComponentIds {
    public const int Ai = 0;
    public const int Animate = 1;
    public const int Arrow = 2;
    public const int ArrowTemplate = 3;
    public const int AttackOrder = 4;
    public const int Audio = 5;
    public const int BoundingMesh = 6;
    public const int BoxFormation = 7;
    public const int Collision = 8;
    public const int Destroy = 9;
    public const int Enemy = 10;
    public const int Ground = 11;
    public const int Input = 12;
    public const int MoveInput = 13;
    public const int Movement = 14;
    public const int MoveOrder = 15;
    public const int Physics = 16;
    public const int Position = 17;
    public const int Resource = 18;
    public const int Rotation = 19;
    public const int Selected = 20;
    public const int SelectionArea = 21;
    public const int Squad = 22;
    public const int Unit = 23;
    public const int UnitsCache = 24;
    public const int UnitTemplate = 25;
    public const int Velocity = 26;
    public const int View = 27;

    public const int TotalComponents = 28;

    static readonly string[] components = {
        "Ai",
        "Animate",
        "Arrow",
        "ArrowTemplate",
        "AttackOrder",
        "Audio",
        "BoundingMesh",
        "BoxFormation",
        "Collision",
        "Destroy",
        "Enemy",
        "Ground",
        "Input",
        "MoveInput",
        "Movement",
        "MoveOrder",
        "Physics",
        "Position",
        "Resource",
        "Rotation",
        "Selected",
        "SelectionArea",
        "Squad",
        "Unit",
        "UnitsCache",
        "UnitTemplate",
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
        { typeof (ArrowTemplateComponent), ArrowTemplate },
        { typeof (AttackOrderComponent), AttackOrder },
        { typeof (AudioComponent), Audio },
        { typeof (BoundingMeshComponent), BoundingMesh },
        { typeof (BoxFormationComponent), BoxFormation },
        { typeof (CollisionComponent), Collision },
        { typeof (DestroyComponent), Destroy },
        { typeof (EnemyComponent), Enemy },
        { typeof (GroundComponent), Ground },
        { typeof (InputComponent), Input },
        { typeof (MoveInputComponent), MoveInput },
        { typeof (MovementComponent), Movement },
        { typeof (MoveOrderComponent), MoveOrder },
        { typeof (PhysicsComponent), Physics },
        { typeof (PositionComponent), Position },
        { typeof (ResourceComponent), Resource },
        { typeof (RotationComponent), Rotation },
        { typeof (SelectedComponent), Selected },
        { typeof (SelectionAreaComponent), SelectionArea },
        { typeof (SquadComponent), Squad },
        { typeof (UnitComponent), Unit },
        { typeof (UnitsCacheComponent), UnitsCache },
        { typeof (UnitTemplateComponent), UnitTemplate },
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