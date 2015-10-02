using System.Linq;
using Entitas;
using UnityEngine;
using Quaternion = Mono.GameMath.Quaternion;
using Vector3 = Mono.GameMath.Vector3;

public class SquadBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    private static int _squadNumber;
    private static int GetNextSquadNumber()
    {
        return _squadNumber++;
    }

    public bool IsPlayer;
    public float AiSeeingRange;

    public int Columns;
    public int Rows;
    public int Spacing;

    public void OnAttachEntity(Entity entity)
    {
        var moveSounds = Resources.LoadAll<AudioClip>("Squad/Move/").ToList();

        entity
            .AddSquad(GetNextSquadNumber())
            .AddBoxFormation(Columns, Rows, Spacing)
            .IsPlayer(IsPlayer)
            .AddPosition(Vector3.Zero)
            .AddRotation(Quaternion.Identity)
            .AddAudio(GetComponent<AudioSource>(), moveSounds);

        if (!IsPlayer)
        {
            entity.AddAi(AiSeeingRange);
        }
    }

    public void OnDetachEntity(Entity entity)
    {
    }
}