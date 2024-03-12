using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{

    float _randomVertex => Random.Range(0.7f, 1.3f);
    private void Start()
    {
        CreateCube();
    }

    private void CreateCube()
    {
        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (_randomVertex, 0, 0),
            new Vector3 (_randomVertex, _randomVertex, 0),
            new Vector3 (0, _randomVertex, 0),
            new Vector3 (0, _randomVertex, _randomVertex),
            new Vector3 (_randomVertex, _randomVertex, _randomVertex),
            new Vector3 (_randomVertex, 0, _randomVertex),
            new Vector3 (0, 0, _randomVertex),
        };

        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}