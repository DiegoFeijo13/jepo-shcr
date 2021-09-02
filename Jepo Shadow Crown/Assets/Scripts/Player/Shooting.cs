using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public PlayerControl playerControl;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        firePoint.rotation = Quaternion.Euler(BulletDirection());

        if (Input.GetButtonDown("Fire1"))
        {
            if(playerControl.CanAttack())
                Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    Vector3 BulletDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - playerControl.BodyPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        return new Vector3(0, 0, angle);
    }
}
