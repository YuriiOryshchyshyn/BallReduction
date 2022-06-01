using UnityEngine;

public class BulletSphereMoveble : MonoBehaviour
{
    [SerializeField] private float _speedMove;

    private void Update()
    {
        transform.Translate(transform.forward * _speedMove * Time.deltaTime);
    }
}
