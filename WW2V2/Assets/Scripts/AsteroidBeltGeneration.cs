using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltGeneration : MonoBehaviour
{
    public GameObject[] asteroids;
    [Range(10, 30)]
    public int concentation;
    void Awake()
    {
        generateField();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void generateField()
    {
        float previousX = 0;
        float previousZ = 0;
        for(int row = -100; row < 300; row+= concentation)
        {
            for(int col = -100; col < 300; col+= concentation)
            {
                Vector3 asteroidPos = new Vector3(col, generatePerliNoise(previousX + row, previousZ + col), row);
                previousX+= 10;
                previousZ+= 10;
                int asteroidIndex = Mathf.Abs(row)% asteroids.Length;
                GameObject asteroid = Instantiate(asteroids[asteroidIndex], asteroidPos, Quaternion.identity);
                asteroid.transform.SetParent(transform);
            }
        }
    }

    public float generatePerliNoise(float x, float y)
    {
        return (
        Mathf.PerlinNoise(10000 + x / 100, 10000 + y / 100) * 100)
        + (Mathf.PerlinNoise(10000 + x / 1000, 10000 + y / 1000) * 300)
        + (Mathf.PerlinNoise(1000 + x / 5, 100 + y / 5) * 2);
    }
}
