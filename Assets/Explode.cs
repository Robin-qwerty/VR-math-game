using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public int cubesPerAxis = 1;
    public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;
    public bool answer = false;

    // Start is called before the first frame update
    void Start()
    {
        //invoke("main", delay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Main();
        }
    }

    void Main()
    {
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        Destroy(gameObject);
        if (answer) 
        {
            ScoreManager.scoreCount += 1;
        } else
        {
            ScoreManager.scoreCount -= 1;
        }
        
    }

    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 firstcube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstcube + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, radius);
    }
}
