using UnityEngine;
using UnityEngine.Events;

public class CastSphere : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SphereBullet _sphere;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private BulletSphereMoveble bulletSphereMoveble;

    public UnityAction SphereCasted;
    private CastSphere _castSphere;

    private void Awake()
    {
        _castSphere = GetComponent<CastSphere>();
    }

    private void OnEnable()
    {
        _input.OnMouseDoun += OnPointerDown;

        _input.OnMouseUp += OnPointerUp;
    }

    private void OnDisable()
    {
        _input.OnMouseDoun -= OnPointerDown;

        _input.OnMouseUp -= OnPointerUp;
    }

    public void OnPointerDown()
    {
        ActivateBullet();
        Cast();
        SetSphereOptions();
        GrowSphere();
    }

    public void OnPointerUp()
    {
        StopGrow();
        ShootSphere();
        ForbidCreatingNewSphere();
    }

    private void ActivateBullet()
    {
        _sphere.gameObject.SetActive(true);
    }

    private void Cast()
    {
        SetCastPosition(_castPoint);
    }

    private void SetCastPosition(Transform point)
    {
        _sphere.transform.position = point.position;
    }

    private void SetSphereOptions()
    {
        _sphere.transform.localScale = Vector3.zero;
    }

    private void GrowSphere()
    {
        _sphere.GrowSphere();
    }

    private void StopGrow()
    {
        _sphere.StopGrow();
    }

    private void ShootSphere()
    {
        bulletSphereMoveble.enabled = true;
    }

    private void ForbidCreatingNewSphere()
    {
        _castSphere.enabled = false;
    }
}
