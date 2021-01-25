using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int _bulletIndex = 0;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _moveSpeed = 250f;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private BulletData _bulletData;

    private int _direction = 0;
    private Vector3 _velocity;


    private void Update()
    {
        CheckInput();
        Move();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _direction = -1;
            _renderer.flipX = true;
            _animator.SetBool("Run", true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = 1;
            _renderer.flipX = false;
            _animator.SetBool("Run", true);
        }
        else
        {
            _direction = 0;
            _animator.SetBool("Run", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    private void Move()
    {
        _velocity = _rigidBody.velocity;
        _velocity.x = _moveSpeed * _direction * Time.deltaTime;
        _rigidBody.velocity = _velocity;
    }

    private void ShootProjectile()
    {
        GameObject go = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Projectile bullet = go.GetComponent<Projectile>();

        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0;

        bullet.Init(direction.normalized, _bulletData._bullets[_bulletIndex]);
    }
}
