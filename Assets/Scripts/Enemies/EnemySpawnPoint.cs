using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSpawnTrigger") && enemy != null)
        {
            //TODO: play spawn animation
            var enemyObject = Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Debug.Log(enemy);
            StartCoroutine(DestroyCO());
        }
    }

    private IEnumerator DestroyCO()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
