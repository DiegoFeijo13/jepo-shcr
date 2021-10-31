using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public PlayerControl playerControl;
    public Transform spawnPoint;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public SpriteRenderer weaponSprite;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        firePoint.rotation = Quaternion.Euler(BulletDirection());

        UpdateLayer();

        if (Input.GetButtonDown("Fire1"))
        {
            if(playerControl.CanAttack())
                Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(spawnPoint.up * bulletForce, ForceMode2D.Impulse);
    }

    Vector3 BulletDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - playerControl.BodyPos;   
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        return new Vector3(0, 0, angle);
    }

    void UpdateLayer()
    {
        Vector3 dir = playerControl.GetFacingDirection();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 scale = weaponSprite.gameObject.transform.localScale;
        scale.y = dir == Vector3.left ? -1 : 1;
        weaponSprite.transform.localScale = scale;

        if ((dir == Vector3.up || dir == Vector3.down) && mousePos.y > 0)
        {
            weaponSprite.sortingOrder = 40;
        }
        else
        {
            weaponSprite.sortingOrder = 60;
        }
    }
}
