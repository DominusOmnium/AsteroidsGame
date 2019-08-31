using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : BaseGameClass
{
    public GameObject asteroid;
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;

    public float generationTime = 1f;

    private float timeFromLastGen = 0f;

    void Update()
    {
        if (!Play)
            return;
        timeFromLastGen += Time.deltaTime;
        if (timeFromLastGen >= generationTime)
        {
            timeFromLastGen = 0f;
            switch (Random.Range(0, 4))
            {
                case 0:
                    GameObject a = Instantiate(asteroid, new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, 90), transform.rotation, Objects.transform);
                    float scale = Random.Range(0.25f, 1f);
                    a.transform.localScale = new Vector3(scale, scale, 1);
                    break;
                case 1:
                    Instantiate(ship1, new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, 90), transform.rotation, Objects.transform);
                    break;
                case 2:
                    Instantiate(ship2, new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, 90), transform.rotation, Objects.transform);
                    break;
                case 3:
                    Instantiate(ship3, new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, 90), transform.rotation, Objects.transform);
                    break;
            }
        }
    }
}
