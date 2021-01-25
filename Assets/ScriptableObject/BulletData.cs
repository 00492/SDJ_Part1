using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    [System.Serializable]
    public struct Bullet
    {
        public enum BulletType
        {
            Fire,
            Ice,
            Dark
        }

        public float _speed;
        public float _size;
        public int _damage;
        public Color _color;
        public BulletType _type;
    }

    public List<Bullet> _bullets = new List<Bullet>();

}
