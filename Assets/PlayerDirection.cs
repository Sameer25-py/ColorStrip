using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerDirection : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;

        private SpriteRenderer _renderer;

        private void OnEnable()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeDirectionSprite(Sprite newSprite)
        {
            _renderer.sprite = newSprite;
        }

        private void FixedUpdate()
        {
            transform.RotateAround(transform.parent.position, Vector3.forward, (Time.deltaTime * speed));
        }
    }
}