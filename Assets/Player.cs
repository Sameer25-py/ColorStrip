using System.Timers;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerDirection direction;
    [SerializeField] private PlayerTrail     trail;
    [SerializeField] private float           moveSpeed     = 100f;
    [SerializeField] private float           maxTravelTime = 2f;

    private float _elapsedTime = 0f;
    public  bool  IsGameStarted;

    private bool    _startCountingTime = false;
    private Vector3 _direction;

    private void Update()
    {
        if (!IsGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            _startCountingTime = true;
            _direction         = (direction.transform.position - transform.position).normalized;
        }

        if (_startCountingTime)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > maxTravelTime)
            {
                _elapsedTime       = 0f;
                _startCountingTime = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsGameStarted || !_startCountingTime) return;
        transform.position += _direction * (Time.deltaTime * moveSpeed);
    }
}