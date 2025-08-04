using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController instance;
    [SerializeField] Transform StartPoint1;
    [SerializeField] Transform target;
    [SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private float bulletSpeed = 10f;
    Vector3 localOffset1 = new Vector3(-23.18f, -6.27f, -13.3899f);
    Vector3 localOffset2 = new Vector3(23.18f, -6.27f, -13.3899f);
    [SerializeField] private Rigidbody _shipVelocity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Fire()
    {
        Vector3 position1 = StartPoint1.TransformPoint(localOffset1);
        Vector3 position2 = StartPoint1.TransformPoint(localOffset2);

        GameObject bullet = Instantiate(bulletPrefab, position1, StartPoint1.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, position2, StartPoint1.rotation);

        Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
        Rigidbody bullet_rb2 = bullet2.GetComponent<Rigidbody>();

        Vector3 shipVelocity = GetShipVelocity();

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Bullet bulletScript2 = bullet2.GetComponent<Bullet>();

        bulletScript.SetShipVelocity(shipVelocity);
        bulletScript2.SetShipVelocity(shipVelocity);

        //bullet_rb.velocity = StartPoint1.forward * bulletSpeed;
        //bullet_rb2.velocity = StartPoint1.forward * bulletSpeed;
    }

    public Vector3 GetTarget ()
    {
        return target.position;
    }

    public Vector3 GetShipVelocity ()
    {
        return _shipVelocity.linearVelocity;
    }
}
