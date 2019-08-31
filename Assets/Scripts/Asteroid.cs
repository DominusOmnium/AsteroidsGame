using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : BaseGameClass
{
    public float speed = 1f;
    public float health = 5f;
    public GameObject Bonus;

    private int points = 5;

    private void Start()
    {
        if (transform.localScale.x <= 0.5f)
        {
            points = 3;
            health = 3f;
        }
        else if (transform.localScale.x <= 0.75f)
        {
            points = 5;
            health = 5f;
        }
        else
        {
            points = 10;
            health = 10f;
        }
    }

    void FixedUpdate()
    {
        if (!Play)
            return;
        if (health <= 0f)
        {
            Score += points;
            float rand = Random.value;
            if (transform.localScale.x * rand >= 0.25f)
                Instantiate(Bonus, transform.position, transform.rotation, Objects.transform);
            Destroy(gameObject);
        }
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.down * speed, Space.Self);
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}
