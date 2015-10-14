public static class ComponentIds {
    public const int Ai = 0;
    public const int Animate = 1;
    public const int AttackInput = 2;
    public const int AttackOrder = 3;
    public const int Audio = 4;
    public const int BoundingMesh = 5;
    public const int BoxFormation = 6;
    public const int Children = 7;
    public const int Collision = 8;
    public const int DelayedDestroy = 9;
    public const int Destroy = 10;
    public const int Force = 11;
    public const int Ground = 12;
    public const int Health = 13;
    public const int Hidden = 14;
    public const int Input = 15;
    public const int LeavingBody = 16;
    public const int MoveInput = 17;
    public const int Movement = 18;
    public const int MoveOrder = 19;
    public const int Parent = 20;
    public const int Physics = 21;
    public const int Player = 22;
    public const int Position = 23;
    public const int Reload = 24;
    public const int Resource = 25;
    public const int Rotation = 26;
    public const int Selected = 27;
    public const int SelectionArea = 28;
    public const int Squad = 29;
    public const int Stickable = 30;
    public const int Unit = 31;
    public const int UnitsCache = 32;
    public const int Velocity = 33;
    public const int View = 34;

    public const int TotalComponents = 35;

    public static readonly string[] componentNames = {
        "Ai",
        "Animate",
        "AttackInput",
        "AttackOrder",
        "Audio",
        "BoundingMesh",
        "BoxFormation",
        "Children",
        "Collision",
        "DelayedDestroy",
        "Destroy",
        "Force",
        "Ground",
        "Health",
        "Hidden",
        "Input",
        "LeavingBody",
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

    public static readonly System.Type[] componentTypes = {
        typeof(AiComponent),
        typeof(AnimateComponent),
        typeof(AttackInputComponent),
        typeof(AttackOrderComponent),
        typeof(AudioComponent),
        typeof(BoundingMeshComponent),
        typeof(BoxFormationComponent),
        typeof(ChildrenComponent),
        typeof(CollisionComponent),
        typeof(DelayedDestroyComponent),
        typeof(DestroyComponent),
        typeof(ForceComponent),
        typeof(GroundComponent),
        typeof(HealthComponent),
        typeof(HiddenComponent),
        typeof(InputComponent),
        typeof(LeavingBodyComponent),
        typeof(MoveInputComponent),
        typeof(MovementComponent),
        typeof(MoveOrderComponent),
        typeof(ParentComponent),
        typeof(PhysicsComponent),
        typeof(PlayerComponent),
        typeof(PositionComponent),
        typeof(ReloadComponent),
        typeof(ResourceComponent),
        typeof(RotationComponent),
        typeof(SelectedComponent),
        typeof(SelectionAreaComponent),
        typeof(SquadComponent),
        typeof(StickableComponent),
        typeof(UnitComponent),
        typeof(UnitsCacheComponent),
        typeof(VelocityComponent),
        typeof(ViewComponent)
    };
}