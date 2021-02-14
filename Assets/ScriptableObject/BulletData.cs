using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    [System.Serializable]
    public struct Bullet
    {
        public float _speed;
        public float _size;
        public float _lifeTime;
        public int _damage;
        public Color _color;
    }

    public List<Bullet> _bullets = new List<Bullet>();

}
