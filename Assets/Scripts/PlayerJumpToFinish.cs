using UnityEngine;

public class PlayerJumpToFinish : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private FinishDoor _finishDoor;

    private bool _isGrounded;

    private void Update()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
        
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);

        Physics.SphereCast(transform.position, transform.localScale.x, transform.forward, out RaycastHit hitInfo);

        if (hitInfo.distance < 5)
        {
            _finishDoor.Open();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
