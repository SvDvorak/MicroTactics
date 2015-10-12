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
    public const int Force = 13;
    public const int Ground = 14;
    public const int Health = 15;
    public const int Hidden = 16;
    public const int Input = 17;
    public const int LeavingBody = 18;
    public const int MoveInput = 19;
    public const int Movement = 20;
    public const int MoveOrder = 21;
    public const int Parent = 22;
    public const int Physics = 23;
    public const int PitchFromVelocity = 24;
    public const int Player = 25;
    public const int Position = 26;
    public const int Reload = 27;
    public const int Resource = 28;
    public const int Rotation = 29;
    public const int Selected = 30;
    public const int SelectionArea = 31;
    public const int Squad = 32;
    public const int Stickable = 33;
    public const int Unit = 34;
    public const int UnitsCache = 35;
    public const int Velocity = 36;
    public const int View = 37;

    public const int TotalComponents = 38;

    public static readonly string[] componentNames = {
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
        "PitchFromVelocity",
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
        typeof(AttachRootComponent),
        typeof(AttachToComponent),
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