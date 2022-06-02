using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSphere playerSphere))
        {
            Time.timeScale = 0;
            _winCanvas.SetActive(true);
        }
    }
}
