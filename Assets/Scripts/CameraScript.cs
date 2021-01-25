using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 _offset;

    private void Start()
    {
        _offset = _player.position - transform.position;
    }

    private void Update()
    {
        transform.position = _player.position - _offset;
    }

}
