using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidBody;
    private BulletData.Bullet _projectileData;
    private Vector3 _moveDir = new Vector3();


    public void Init(Vector3 direction, BulletData.Bullet data)
    {
        _projectileData = data;
        _moveDir = direction;

        GetComponent<Renderer>().material.color = _projectileData._color;
        transform.localScale *= _projectileData._size;
    }


    private void Update()
    {
        _rigidBody.velocity = _moveDir * _projectileData._speed;
    }

    // Collision with enemy
}
