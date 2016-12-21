using UnityEngine;

/*
 * Taken from http://answers.unity3d.com/questions/726932/low-poly-water.html
 */
public class TempWater : MonoBehaviour
{
    public Vector2 range = new Vector2(0.1f, 1);
    public float speed = 1;
    float[] randomTimes;
    Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        int i = 0;
        Debug.Log("VERTICES " + mesh.vertices.Length + " NORMALS " + mesh.normals.Length);
        randomTimes = new float[mesh.vertices.Length];

        while (i < mesh.vertices.Length)
        {
            randomTimes[i] = Random.Range(range.x, range.y);

            i++;
        }

    }

    void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i].y = 1 * Mathf.PingPong(Time.time * speed, randomTimes[i]);
            i++;
        }
        mesh.vertices = vertices;
    }
}
