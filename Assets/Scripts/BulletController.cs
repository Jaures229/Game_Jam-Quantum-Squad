using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] Transform StartPoint1;
    //[SerializeField] Transform StartPoint2;
    [SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private float bulletSpeed = 10f;
    Transform P1;
    Transform P2;

    public void Fire()
    {
        Vector3 position1 = StartPoint1.position + new Vector3(-23.18f, -6.27f, -13.3899f);
        Vector3 position2 = StartPoint1.position + new Vector3(23.18f, -6.27f, -13.3899f);

        GameObject bullet = Instantiate(bulletPrefab, position1, StartPoint1.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, position2, StartPoint1.rotation);

        Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
        Rigidbody bullet_rb2 = bullet2.GetComponent<Rigidbody>();

        //bullet_rb.velocity = StartPoint1.forward * bulletSpeed;
        //bullet_rb2.velocity = StartPoint1.forward * bulletSpeed;
    }
}
