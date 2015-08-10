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
    public const int BoxFormation = 6;
    public const int Collision = 7;
    public const int Destroy = 8;
    public const int Enemy = 9;
    public const int InputPositionChanged = 10;
    public const int InputPress = 11;
    public const int InputRelease = 12;
    public const int Movement = 13;
    public const int MoveOrder = 14;
    public const int Physics = 15;
    public const int Position = 16;
    public const int Rotation = 17;
    public const int Squad = 18;
    public const int Unit = 19;
    public const int UnitsCache = 20;
    public const int UnitTemplate = 21;
    public const int Velocity = 22;
    public const int View = 23;

    public const int TotalComponents = 24;

    static readonly string[] components = {
        "Ai",
        "Animate",
        "Arrow",
        "ArrowTemplate",
        "AttackOrder",
        "Audio",
        "BoxFormation",
        "Collision",
        "Destroy",
        "Enemy",
        "InputPositionChanged",
        "InputPress",
        "InputRelease",
        "Movement",
        "MoveOrder",
        "Physics",
        "Position",
        "Rotation",
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
        { typeof (BoxFormationComponent), BoxFormation },
        { typeof (CollisionComponent), Collision },
        { typeof (DestroyComponent), Destroy },
        { typeof (EnemyComponent), Enemy },
        { typeof (InputPositionChangedComponent), InputPositionChanged },
        { typeof (InputPressComponent), InputPress },
        { typeof (InputReleaseComponent), InputRelease },
        { typeof (MovementComponent), Movement },
        { typeof (MoveOrderComponent), MoveOrder },
        { typeof (PhysicsComponent), Physics },
        { typeof (PositionComponent), Position },
        { typeof (RotationComponent), Rotation },
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