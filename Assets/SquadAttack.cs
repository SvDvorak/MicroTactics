using UnityEngine;
using System.Collections;

public class SquadAttack : MonoBehaviour
{
    public Arrow AttackArrow;

    private SquadState _squadState;
    private int _groundLayer;

    void Start ()
    {
        _squadState = GetComponent<SquadState>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");

        // Create Vector2 vertices
        var vertices2D = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,5),
            new Vector2(5,5),
            new Vector2(5,10),
            new Vector2(0,10),
            new Vector2(0,15),
            new Vector2(15,15),
            new Vector2(15,10),
            new Vector2(10,10),
            new Vector2(10,5),
            new Vector2(15,5),
            new Vector2(15,0),
        };

        // Use the triangulator to get indices for creating triangles
        var tr = new Triangulator(vertices2D);
        var indices = tr.Triangulate();

        // Create the Vector3 vertices
        var vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        // Create the mesh
        var msh = new Mesh { vertices = vertices, triangles = indices };
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        gameObject.AddComponent(typeof(MeshRenderer));
        var filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = msh;
    }

    void Update ()
	{
        if (_squadState.InteractState == SquadState.State.Attack)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                AttackArrow.SetPositions(transform.position, hit.point);
            }
        }
    }

    public void OnMouseDown()
    {
        AttackArrow.IsVisible = true;
        _squadState.InteractState = SquadState.State.Attack;
    }

    public void OnMouseUp()
    {
        AttackArrow.IsVisible = false;
        _squadState.InteractState = SquadState.State.Idle;
    }
}