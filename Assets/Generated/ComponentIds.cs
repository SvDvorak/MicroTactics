using System;
using System.Collections.Generic;
using Entitas;

public static class ComponentIds {
    public const int Ai = 0;
    public const int Animate = 1;
    public const int Arrow = 2;
    public const int ArrowTemplate = 3;
    public const int Ground = 4;
    public const int AttackOrder = 5;
    public const int Audio = 6;
    public const int BoundingMesh = 7;
    public const int BoxFormation = 8;
    public const int Collision = 9;
    public const int Destroy = 10;
    public const int Enemy = 11;
    public const int Input = 12;
    public const int Movement = 13;
    public const int MoveOrder = 14;
    public const int Physics = 15;
    public const int Position = 16;
    public const int Resource = 17;
    public const int Rotation = 18;
    public const int Selected = 19;
    public const int SelectionArea = 20;
    public const int Squad = 21;
    public const int Unit = 22;
    public const int UnitsCache = 23;
    public const int UnitTemplate = 24;
    public const int Velocity = 25;
    public const int View = 26;

    public const int TotalComponents = 27;

    static readonly string[] components = {
        "Ai",
        "Animate",
        "Arrow",
        "ArrowTemplate",
        "Ground",
        "AttackOrder",
        "Audio",
        "BoundingMesh",
        "BoxFormation",
        "Collision",
        "Destroy",
        "Enemy",
        "Input",
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
        { typeof (GroundComponent), Ground },
        { typeof (AttackOrderComponent), AttackOrder },
        { typeof (AudioComponent), Audio },
        { typeof (BoundingMeshComponent), BoundingMesh },
        { typeof (BoxFormationComponent), BoxFormation },
        { typeof (CollisionComponent), Collision },
        { typeof (DestroyComponent), Destroy },
        { typeof (EnemyComponent), Enemy },
        { typeof (InputComponent), Input },
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