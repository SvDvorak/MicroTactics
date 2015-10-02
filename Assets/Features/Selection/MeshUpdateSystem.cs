using System.Collections.Generic;
using System.Linq;
using Assets.Features;
using Entitas;
using UnityEngine;

public class MeshUpdateSystem : IReactiveSystem
{
    private readonly Triangulator _triangulator;
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.BoundingMesh, Matcher.View).OnEntityAdded(); } }


    public MeshUpdateSystem()
    {
        _triangulator = new Triangulator();
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var hullPoints = entity.boundingMesh.Points;
            var triangulatedIndices = _triangulator.Triangulate(hullPoints.ToArray());

            var mesh = new Mesh
                {
                    vertices = hullPoints.Select(point => new Vector3(point.X, 0, point.Y)).ToArray(),
                    triangles = triangulatedIndices
                };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            entity.view.Value.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}