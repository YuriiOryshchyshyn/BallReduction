using UnityEngine;
using UnityEngine.Events;

public class PlayerSphere : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speedReduction;
    [SerializeField] private SphereBullet _bullet;
    [SerializeField] private PlayerJumpToFinish _playerJumpToFinish;
    [SerializeField] private GameLoseCanvas _gameLoseCanvas;

    public event UnityAction<float> ChangePlayerSphere;

    private Vector3 _targetScale;
    private Vector3 _originScale;

    private void Awake()
    {
        _originScale = transform.localScale;
    }

    private void OnEnable()
    {
        _playerInput.OnMouseDoun += OnPointerDown;

        _bullet.ChangedBulletSphere += OnChangedBulletScale;

        _bullet.ObstaclesDestroyed += OnObstaclesDestroyed;

        _playerInput.OnMouseUp += OnPointerUp;
    }

    private void OnDisable()
    {
        _playerInput.OnMouseDoun -= OnPointerDown;

        _bullet.ChangedBulletSphere -= OnChangedBulletScale;

        _bullet.ObstaclesDestroyed += OnObstaclesDestroyed;

        _playerInput.OnMouseUp -= OnPointerUp;
    }

    private void OnPointerDown()
    {
        _targetScale = Vector3.zero;
    }
    
    private void OnPointerUp()
    {
        _originScale = transform.localScale;
    }

    private void OnChangedBulletScale(float scaleValue)
    {
        transform.localScale = Vector3.Lerp(_originScale, Vector3.zero, scaleValue);
        ChangePlayerSphere?.Invoke(transform.localScale.x);

        if (transform.localScale.x < 0.1f)
        {
            _gameLoseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnObstaclesDestroyed()
    {
        Invoke("CheckRoad", 0.5f);
    }

    private void CheckRoad()
    {
        RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, transform.localScale.x, transform.forward);

        foreach (var item in hitInfo)
        {
            if (item.transform.TryGetComponent(out Obstacle obstacle))
            {
                return;
            }
        }

        print("Road Clear!");
        _playerJumpToFinish.enabled = true;
    }
}
