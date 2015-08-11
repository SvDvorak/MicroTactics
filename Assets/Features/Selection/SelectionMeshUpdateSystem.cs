using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class SelectionMeshUpdateSystem : IReactiveSystem
{
    private readonly Assets.Features.Triangulator _triangulator;
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.BoundingMesh, Matcher.View); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public SelectionMeshUpdateSystem()
    {
        _triangulator = new Assets.Features.Triangulator();
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
            entity.view.GameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}