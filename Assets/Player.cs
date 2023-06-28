using System;
using System.Timers;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

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

    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!IsGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            _startCountingTime = true;
            _direction         = (direction.transform.position - transform.position).normalized;
            direction.gameObject.SetActive(false);
        }

        if (_startCountingTime)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > maxTravelTime)
            {
                _elapsedTime            = 0f;
                _startCountingTime      = false;
                IsGameStarted           = false;
                transform.position      = Vector3.zero;
                direction.IsGameStarted = false;
                GameManager.GameEnd?.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsGameStarted || !_startCountingTime) return;
        transform.position += _direction * (Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Circle"))
        {
            Circle circle = col.GetComponent<Circle>();
            trail.ChangeTrailMaterial(circle.TrailTexture);
            direction.ChangeDirectionSprite(circle.DirectionSprite);
            _renderer.sprite   = circle.PlayerSprite;
            _startCountingTime = false;
            _elapsedTime       = 0f;
            direction.gameObject.SetActive(true);
            CircleController.Collided?.Invoke();
        }
    }

    public void Preview()
    {
        direction.IsGameStarted = true;
    }

    public void StartGame()
    {
        direction.IsGameStarted = true;
        trail.EnableTrail();
        IsGameStarted      = true;
        _startCountingTime = true;
        _elapsedTime       = 0f;
    }

    public void PauseGame()
    {
        direction.IsGameStarted = false;
        trail.DisableTrail();
        IsGameStarted      = false;
        _startCountingTime = false;
    }

    public void ResumeGame()
    {
        direction.IsGameStarted = true;
        trail.EnableTrail();
        IsGameStarted      = true;
        _startCountingTime = true;
    }
}