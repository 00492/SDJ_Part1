using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxeLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private float _spriteSizeX = 0f;
    private List<GameObject> _images = new List<GameObject>();

    void Start()
    {
        SpawnSideImages();
    }

    private void SpawnSideImages()
    {
        _images.Add(_renderer.gameObject);

        _spriteSizeX = _renderer.sprite.texture.width / _renderer.sprite.pixelsPerUnit;
        Vector3 distance = new Vector3(_spriteSizeX, 0, 0);

        bool left = true;

        for (int i = 0; i < 2; i++)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(transform);
            go.transform.position = left ? go.transform.position -= distance : go.transform.position += distance;

            left = false;

            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sortingOrder = _renderer.sortingOrder;
            renderer.sprite = _renderer.sprite;

            _images.Add(go);
        }
    }

    void LateUpdate()
    {
        Vector3 camPos = Camera.main.transform.position;
        foreach (GameObject go in _images)
        {
            if(camPos.x - go.transform.position.x > _spriteSizeX)
            {
                go.transform.position += new Vector3((_spriteSizeX * 2f), 0, 0);
            }
            else if (go.transform.position.x - camPos.x > _spriteSizeX)
            {
                go.transform.position -= new Vector3((_spriteSizeX * 2f), 0, 0);
            }
        }
    }
}
