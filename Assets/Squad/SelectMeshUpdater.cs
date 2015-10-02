using System.Linq;
using Assets;
using UnityEngine;
using ConvexHullCalculator = Assets.GoodMonotoneChain.ConvexHullCalculator;

public class SelectMeshUpdater : MonoBehaviour
{
    public float ExpandAmount = 1.5f;

    private Triangulator _triangulator;
    private SquadState _squadState;
    private MeshCollider _collider;

    private void Start()
    {
        _triangulator = new Triangulator();
        _squadState = GetComponentInParent<SquadState>();
        _collider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        var squadPositions = _squadState.Units
            .Select(x => x.transform.position)
            .Select(x => ExpandFromCenter(x, _squadState.CenterPosition))
            .Select(x => Get2DPosition(x - transform.position))
            .ToList();

        var hullPoints = ConvexHullCalculator.Calculate(squadPositions).ToList();
        var triangulatedIndices = _triangulator.Triangulate(hullPoints.ToArray());

        var mesh = new Mesh
            {
                vertices = hullPoints.Select(x => Get3DPosition(x)).ToArray(),
                triangles = triangulatedIndices
            };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        _collider.sharedMesh = mesh;
    }

    private Vector3 ExpandFromCenter(Vector3 position, Vector3 centerPosition)
    {
        var fromCenterVector = (position - centerPosition).normalized;
        return position + fromCenterVector*ExpandAmount;
    }

    private static Vector2 Get2DPosition(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }

    private static Vector3 Get3DPosition(Vector2 position)
    {
        return new Vector3(position.x, 0, position.y);
    }
}