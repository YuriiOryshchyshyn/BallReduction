using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RoadToTheFinish : MonoBehaviour
{
    [SerializeField] private PlayerSphere _playerSphere;
    [SerializeField] private Transform _finishTrone;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetLinePoints(new Vector3(_playerSphere.transform.position.x, _lineRenderer.GetPosition(0).y, _playerSphere.transform.position.z),
            new Vector3(_finishTrone.position.x, _lineRenderer.GetPosition(1).y, _finishTrone.position.z));
        SetLineWidth(_playerSphere.transform.localScale.x);
    }

    private void OnEnable()
    {
        _playerSphere.ChangePlayerSphere += OnChangePlayerSphere;
    }

    private void OnDisable()
    {
        _playerSphere.ChangePlayerSphere -= OnChangePlayerSphere;
    }

    public void SetLinePoints(Vector3 pointStart, Vector3 pointEnd)
    {
        _lineRenderer.SetPosition(0, pointStart);
        _lineRenderer.SetPosition(1, pointEnd);
    }

    public void SetLineWidth(float width)
    {
        _lineRenderer.transform.localScale = new Vector3(width,
            _lineRenderer.transform.localScale.y, _lineRenderer.transform.localScale.z);
    }

    private void OnChangePlayerSphere(float width)
    {
        transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
    }
}
