using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField] private List<Circle> circles;
        [SerializeField] private Transform    player;
        private                  Circle       _activeCircle;
        public static            Action       Collided;

        public void SpawnCircle()
        {
            if (_activeCircle && _activeCircle.gameObject.activeSelf)
            {
                return;
            }

            Circle  randomCircle = circles[Random.Range(0, circles.Count)];
            Vector3 randomPos    = Random.insideUnitCircle * 2f;
            if (Vector3.Distance(randomPos, player.position) < 1f)
            {
                SpawnCircle();
            }
            else
            {
                randomCircle.transform.position = randomPos;
                if (_activeCircle)
                {
                    _activeCircle.gameObject.SetActive(false);
                }

                _activeCircle = randomCircle;
                _activeCircle.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            Collided += OnPlayerCollided;
        }

        private void OnPlayerCollided()
        {
            _activeCircle.gameObject.SetActive(false);
            SpawnCircle();
        }
    }
}