using System.Collections;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    [SerializeField] private Transform _doorOpenPosition;
    [SerializeField] private float _doorOpenSpeed;

    public void Open()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        Vector3 originPosition = transform.localPosition;
        float lerpValue = 0;

        while (transform.localPosition != _doorOpenPosition.localPosition)
        {
            transform.localPosition = Vector3.Lerp(originPosition, _doorOpenPosition.localPosition, lerpValue);
            lerpValue += _doorOpenSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
