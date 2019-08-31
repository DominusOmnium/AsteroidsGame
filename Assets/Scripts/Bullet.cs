using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : BaseGameClass
{
    public float speed = 5f;
    public int direction = 1;
    public float damage = 1f;
    public bool ThroughBullet = false;

    private void Start()
    {
        if (ThroughBullet)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2.5f, 1);
    }

    void FixedUpdate()
    {
        if (!Play)
            return;
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.up * speed * direction, Space.Self);
        if (direction == 1 && transform.position.y > 7)
            Destroy(gameObject);
        else if (direction == -1 && transform.position.y < -7)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Asteroid":
                if (direction == 1)
                {
                    collision.GetComponent<Asteroid>().health -= damage;
                    if (!ThroughBullet)
                        Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (direction == 1)
                {
                    collision.GetComponent<Enemy>().health -= damage;
                    if (!ThroughBullet)
                        Destroy(gameObject);
                }
                break;
            case "Player":
                if (direction == -1)
                {
                    collision.GetComponent<Player>().SendDamage(damage);
                    if (!ThroughBullet)
                        Destroy(gameObject);
                }
                break;
            case "EnemyBullet":
                if (direction == 1)
                {
                    Destroy(collision.gameObject);
                    if (!ThroughBullet)
                        Destroy(gameObject);
                }
                break;
        }
    }
}
