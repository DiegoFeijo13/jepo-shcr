using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float bulletCooldown = 0.5f;

    private bool canShoot = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused())
            return;

        firePoint.rotation = Quaternion.Euler(BulletDirection());

        if (Input.GetButton("Fire1") && playerMovement.CanAttack())
            Shoot();
        
    }

    void Shoot()
    {
        if (!canShoot)
            return;

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(spawnPoint.up * bulletForce, ForceMode2D.Impulse);
        StartCoroutine(CooldownCo());
    }

    Vector3 BulletDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - playerMovement.BodyPos;   
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        return new Vector3(0, 0, angle);
    }

    IEnumerator CooldownCo()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);

        canShoot = true;
    }
}
