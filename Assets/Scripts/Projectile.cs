using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidBody;
    private BulletData.Bullet _projectileData;
    private Vector3 _moveDir = new Vector3();


    public void Init(Vector3 direction, BulletData.Bullet data)
    {
        _projectileData = data;
        _moveDir = direction;

        GetComponent<Renderer>().material.color = _projectileData._color;
        transform.localScale *= _projectileData._size;

        StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        _rigidBody.velocity = _moveDir * _projectileData._speed;
    }

    private IEnumerator DestroySelf()
    {
        float timer = 0f;
        while(timer < _projectileData._lifeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
