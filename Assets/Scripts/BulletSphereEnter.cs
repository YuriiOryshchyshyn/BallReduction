using UnityEngine;
using UnityEngine.Events;

public class BulletSphereEnter : MonoBehaviour
{
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private Collider _parentBall;

    public UnityAction SphereEnter;

    private void Awake()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), _groundCollider);
        Physics.IgnoreCollision(GetComponent<Collider>(), _parentBall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            SphereEnter?.Invoke();
        }
    }
}
