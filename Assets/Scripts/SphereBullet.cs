using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class SphereBullet : MonoBehaviour
{
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _detonateScaleFactor;
    [SerializeField] private Color _targetColorSphere;
    [SerializeField] private float _colorChangeSpeed;
    [SerializeField] private BulletSphereEnter _bulletSphereEnter;
    [SerializeField] private BulletSphereMoveble _bulletSphereMoveble;
    [SerializeField] private CastSphere _castSphere;
    [SerializeField] private PlayerSphere _playerSphere;
    [SerializeField] private float _radiusValue;

    public event UnityAction SphereDetonated;
    public event UnityAction<float> ChangedBulletSphere;
    public event UnityAction ObstaclesDestroyed;

    private IEnumerator _growCoroutine;
    private Vector3 _targetScale;
    private MeshRenderer _renderer;
    private Vector3 _detonateTargetScale;
    private Color _originColor;
    private Vector3 _maxScale;
    private List<Obstacle> _obstaclesToDestroy;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _maxScale = _playerSphere.transform.localScale;
        _targetScale = _maxScale;
        _originColor = _renderer.material.color;
        _obstaclesToDestroy = new List<Obstacle>();
    }

    private void OnEnable()
    {
        _bulletSphereEnter.SphereEnter += OnSphereEnter;
        SphereDetonated += SetOriginOptions;
    }

    private void OnDisable()
    {
        _bulletSphereEnter.SphereEnter -= OnSphereEnter;
        SphereDetonated -= SetOriginOptions;
    }

    private void OnSphereEnter()
    {
        Detonate();
    }

    private void DestroyObstacles()
    {
        foreach (var obstacle in _obstaclesToDestroy)
            obstacle.DestroyObstacle();
        _obstaclesToDestroy.RemoveAll(obstacle => obstacle);
        ObstaclesDestroyed?.Invoke();
    }

    public void GrowSphere(bool isDetonateGrow = false)
    {
        _growCoroutine = GrowSphereCoroutine(isDetonateGrow);
        StartCoroutine(_growCoroutine);
    }

    public void StopGrow()
    {
        _targetScale = transform.localScale;
        _detonateTargetScale = _targetScale * _detonateScaleFactor;
        StopCoroutine(_growCoroutine);
    }

    public void Detonate()
    {
        _targetScale = _detonateTargetScale;
        _bulletSphereMoveble.enabled = false;
        GrowSphere(true);
        StartCoroutine(ColoringSphere(_targetColorSphere));
        StartCoroutine(WaitDetonationComplete());
    }

    private IEnumerator WaitDetonationComplete()
    {
        while (transform.localScale != _targetScale)
        {
            yield return null;
        }
        DestroyObstacles();
        StopGrow();
        SphereDetonated?.Invoke();
    }

    public void SetOriginOptions()
    {
        _maxScale = _playerSphere.transform.localScale;
        _targetScale = _maxScale;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        _renderer.material.color = _originColor;
        _bulletSphereMoveble.enabled = false;
        _castSphere.enabled = true;
        _playerSphere.enabled = true;
        gameObject.SetActive(false);
    }

    private IEnumerator GrowSphereCoroutine(bool isDetonateGrow = false)
    {
        Vector3 originScale = transform.localScale;
        float lerpValue = 0;

        while (transform.localScale != _targetScale)
        {
            transform.localScale = Vector3.Lerp(originScale, _targetScale, lerpValue);
            lerpValue += _growSpeed * Time.deltaTime;
            if (!isDetonateGrow)
                ChangedBulletSphere?.Invoke(lerpValue);
            yield return null;
        }
    }

    private IEnumerator ColoringSphere(Color targetColor)
    {
        Color originColor = _renderer.material.color;
        float lerpValue = 0;

        while (_renderer.material.color != targetColor)
        {
            _renderer.material.color = Color.Lerp(originColor, targetColor, lerpValue);
            lerpValue += _colorChangeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            _obstaclesToDestroy.Add(obstacle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, transform.localScale.x * _radiusValue);
    }
}