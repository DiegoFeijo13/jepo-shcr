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
    void Update()
    {
        if (GameManager.Instance.IsPaused())
            return;

        firePoint.rotation = Quaternion.Euler(BulletDirection());

        UpdateLayer();

        if (Input.GetButtonDown("Fire1") && playerMovement.CanAttack())
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

    void UpdateLayer()
    {
        Vector3 dir = playerMovement.GetFacingDirection();
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

    IEnumerator CooldownCo()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);

        canShoot = true;
    }
}
