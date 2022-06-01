using System.Collections;
using UnityEngine;

public class PlayerSphere : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speedReduction;
    [SerializeField] private SphereBullet _bullet;

    private IEnumerator _reductionCoroutine;
    private Vector3 _targetScale;
    private PlayerSphere _playerSphere;
    private Vector3 _originScale;

    private void Awake()
    {
        _playerSphere = GetComponent<PlayerSphere>();
        _originScale = transform.localScale;
    }

    private void OnEnable()
    {
        _playerInput.OnMouseDoun += OnPointerDown;

        _bullet.ChangedBulletSphere += OnChangedBulletScale;

        _playerInput.OnMouseUp += OnPointerUp;
    }

    private void OnDisable()
    {
        _playerInput.OnMouseDoun -= OnPointerDown;

        _bullet.ChangedBulletSphere -= OnChangedBulletScale;

        _playerInput.OnMouseUp -= OnPointerUp;
    }

    private void OnPointerDown()
    {
        _targetScale = Vector3.zero;
        //ReductionSphere();
    }
    
    private void OnPointerUp()
    {
        _originScale = transform.localScale;
        //StopRediction();
        //ForbidReduction();
    }

    private void OnChangedBulletScale(float scaleValue)
    {
        transform.localScale = Vector3.Lerp(_originScale, Vector3.zero, scaleValue);
    }

    private void ReductionSphere()
    {
        Reduction();
    }

    private void StopRediction()
    {
        _targetScale = transform.localScale;
        StopCoroutine(_reductionCoroutine);
    }

    private void Reduction()
    {
        _reductionCoroutine = ReductionCoroutine();
        StartCoroutine(_reductionCoroutine);
    }

    private void ForbidReduction()
    {
        _playerSphere.enabled = false;
    }


    private IEnumerator ReductionCoroutine()
    {
        Vector3 originScale = transform.localScale;
        float lerpValue = 0;

        while (transform.localScale != _targetScale)
        {
            transform.localScale = Vector3.Lerp(originScale, Vector3.zero, lerpValue);
            lerpValue += _speedReduction * Time.deltaTime;
            yield return null;
        }
    }
}
