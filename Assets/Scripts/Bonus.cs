using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : BaseGameClass
{
    public float speed = 1f;
    public Sprite HeartSprite;
    public Sprite DoubleDamageSprite;
    public Sprite ShieldSprite;
    public Sprite ThroughBulletsSprite;
    public float value;
    public Bonuses bonus;

    private void Start()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = DoubleDamageSprite;
                bonus = Bonuses.DoubleDamage;
                value = 5f;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = HeartSprite;
                bonus = Bonuses.Heal;
                value = 10f;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = ShieldSprite;
                bonus = Bonuses.Shield;
                value = 5f;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = ThroughBulletsSprite;
                bonus = Bonuses.ThroughBullets;
                value = 5f;
                break;
        }
    }

    void FixedUpdate()
    {
        if (!Play)
            return;

        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.down * speed, Space.Self);
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}
