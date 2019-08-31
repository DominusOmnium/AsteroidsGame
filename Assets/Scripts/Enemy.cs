using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseGameClass
{
    public float speed = 1f;
    public float reloadTime = 0.75f;
    public float health = 5f;
    public float bulletSpeed = 2f;
    public float bulletSize = 1f;
    public int points = 5;
    public MooveType moveType = MooveType.SimpleForward;
    public GameObject bullet;

    private float timeFromLastShoot = 0f;
    private float offset;

    private void Start()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                moveType = MooveType.ChasePlayer;
                break;
            case 1:
                moveType = MooveType.SimpleForward;
                break;
            case 2:
                moveType = MooveType.ZigZag;
                speed /= 2;
                break;
        }
        offset = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        if (!Play)
            return;
        if (health <= 0f)
        {
            Score += points;
            Destroy(gameObject);
        }
        timeFromLastShoot += Time.deltaTime;
        if (timeFromLastShoot >= reloadTime)
        {
            timeFromLastShoot = 0f;
            Bullet b = Instantiate(bullet, transform.position + new Vector3(0, -offset, 0), transform.rotation, transform.parent).GetComponent<Bullet>();
            b.speed = bulletSpeed;
            b.transform.localScale = new Vector3(bulletSize, bulletSize, 1);
            b.damage *= bulletSize;
        }
        switch (moveType)
        {
            case MooveType.SimpleForward:
                transform.Translate(Vector3.down * speed, Space.Self);
                if (transform.position.y < -7)
                {
                    Destroy(gameObject);
                }
                break;
            case MooveType.ZigZag:
                transform.position = new Vector3(2*Mathf.Sin(transform.position.y*4), transform.position.y - speed, transform.position.z);
                if (transform.position.y < -7)
                {
                    Destroy(gameObject);
                }
                break;
            case MooveType.ChasePlayer:
                if (transform.position.y >= 3)
                    transform.Translate(Vector3.down * speed, Space.Self);
                else if (transform.position.x - speed > Player.transform.position.x)
                    transform.Translate(Vector3.left * speed, Space.Self);
                else if (transform.position.x + speed < Player.transform.position.x)
                    transform.Translate(Vector3.right * speed, Space.Self);
                break;
        }
    }
}
