using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BaseGameClass
{
    public float speed = 10f;
    public float reloadTime = 1f;
    private float health = 100f;
    public GameObject bullet;
    public GameObject Shield;
    public Slider healthBar;

    private float doubleDamageTime = 0f;
    private float shieldTime = 0f;
    private float throughBulletsTime = 0;

    public float Health
    {
        get { return health; }
        set
        {
            if (value < 0)
                value = 0;
            else if (value > 100f)
                value = 100f;
            health = value;
            healthBar.value = value;
        }
    }

    public float DoubleDamageTime
    {
        get { return doubleDamageTime; }
        set
        {
            if (value < 0)
                doubleDamageTime = 0;
            else
                doubleDamageTime = value;
        }
    }

    public float ShieldTime
    {
        get { return shieldTime; }
        set
        {
            if (value < 0)
                shieldTime = 0;
            else
                shieldTime = value;
        }
    }

    public float ThroughBulletsTime
    {
        get { return throughBulletsTime; }
        set
        {
            if (value < 0)
                throughBulletsTime = 0;
            else
                throughBulletsTime = value;
        }
    }

    private float offset;
    private float timeFromLastShoot = 0f;

    private void Start()
    {
        offset = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void Update()
    {
        if (!Play)
            return;
        if (Health <= 0)
        {
            PlayerKilled();
        }

        if (ShieldTime > 0)
        {
            if (ShieldTime <= 1.5f && !Shield.GetComponent<Animation>().isPlaying)
                Shield.GetComponent<Animation>().Play();
            if ((ShieldTime -= Time.deltaTime) <= 0)
            {
                Shield.GetComponent<Animation>().Stop();
                Shield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
                Shield.SetActive(false);
            }
        }
        if (ThroughBulletsTime > 0)
        {
            ThroughBulletsTime -= Time.deltaTime;
        }
        if (DoubleDamageTime > 0)
        {
            DoubleDamageTime -= Time.deltaTime;
        }

        timeFromLastShoot += Time.deltaTime;
        if (timeFromLastShoot >= reloadTime)
        {
            timeFromLastShoot = 0f;
            GameObject bul = Instantiate(bullet, transform.position + new Vector3(0, offset, 0), transform.rotation, Objects.transform);
            if (DoubleDamageTime > 0f)
            {
                bul.GetComponent<Bullet>().damage *= 2;
                bul.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (ThroughBulletsTime > 0)
                bul.GetComponent<Bullet>().ThroughBullet = true;
        }
    }

    void FixedUpdate()
    {
        if (!Play)
            return;
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pos.y <= 0)
            {
                pos.z = 0;
                pos.y = -4;
                transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                if (ShieldTime <= 0)
                    Health -= 10f;
                collision.GetComponent<Enemy>().health = 0f;
                break;
            case "Asteroid":
                if (ShieldTime <= 0)
                    Health -= 10f;
                collision.GetComponent<Asteroid>().health = 0f;
                break;
            case "Bonus":
                SetBonus(collision.gameObject.GetComponent<Bonus>().bonus, collision.gameObject.GetComponent<Bonus>().value);
                Destroy(collision.gameObject);
                break;
        }
    }

    public void Prepare()
    {
        doubleDamageTime = 0f;
        shieldTime = 0f;
        throughBulletsTime = 0;
        timeFromLastShoot = 0f;
        Shield.SetActive(false);
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        Health = 100f;
    }

    public void SendDamage(float dmg)
    {
        if (ShieldTime <= 0)
            Health -= dmg;
    }

    public void SetBonus(Bonuses bonus, float value)
    {
        switch(bonus)
        {
            case Bonuses.DoubleDamage:
                DoubleDamageTime += value;
                break;
            case Bonuses.Heal:
                Health += value;
                break;
            case Bonuses.Shield:
                ShieldTime += value;
                Shield.SetActive(true);
                break;
            case Bonuses.ThroughBullets:
                ThroughBulletsTime += value;
                break;
        }
    }
}
