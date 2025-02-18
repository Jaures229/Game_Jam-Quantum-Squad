using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] Transform StartPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, StartPoint.position, StartPoint.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        bullet_rb.AddForce(StartPoint.up * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
