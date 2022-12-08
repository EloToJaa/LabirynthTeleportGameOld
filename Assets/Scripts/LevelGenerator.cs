using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    public Material material01;
    public Material material02;

    void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        if(pixelColor.a == 0)
        {
            return;
        }

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 0, z) * offset;
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    public void GenerateLabirynth()
    {
        for(int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
        ColorTheChildren();
    }

    public void ColorTheChildren()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Wall")
            {
                if(Random.Range(1, 100) % 3 == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material = material02;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material = material01;
                }
            }

            if(child.childCount > 0)
            {
                foreach(Transform grandchild in child.transform)
                {
                    if (child.tag == "Wall")
                    {
                        if (Random.Range(1, 100) % 3 == 0)
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material = material02;
                        }
                        else
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material = material01;
                        }
                    }
                }
            }
        }
    }
}
