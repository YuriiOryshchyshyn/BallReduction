using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction OnMouseDoun;
    public event UnityAction OnMouseUp;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnMouseDoun?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp?.Invoke();
        }
    }
}
