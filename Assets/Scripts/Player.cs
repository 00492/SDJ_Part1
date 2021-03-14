using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string STEP_COUNTS = "Step Count : ";

    [SerializeField] private HUDExample _hud;

    [Header("Player")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _moveSpeed = 250f;
    [SerializeField] private Animator _animator;

    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private BulletData _bulletData;

    [Header("Steps")]
    [SerializeField] private TextMeshProUGUI _tmProSteps;
    [SerializeField] private AudioSource _audioSourceFootStep;
    private int _stepCount = 0;


    private int _direction = 0;
    private Vector3 _velocity;

    public static Action _onPlayerHit;
    public static Action<int> _onKill;

    public int _bulletIndex = 0;



    public Action _someAction;



    private void Awake()
    {
        _onPlayerHit += HitFeedback;


        if(_someAction != null)
        {
            _someAction();
        }

    }




    private void Update()
    {
        CheckInput();
        Move();
    }

    private void OnDestroy()
    {
        _onPlayerHit -= HitFeedback;
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
        else if(Input.GetMouseButtonDown(1))
        {
            _onPlayerHit?.Invoke();
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

    public void StepCount()
    {
        _stepCount++;
        _audioSourceFootStep.Play();
        _tmProSteps.SetText(STEP_COUNTS + _stepCount);
    }

    private void HitFeedback()
    {
        transform.localScale /= 2;
    }
}
