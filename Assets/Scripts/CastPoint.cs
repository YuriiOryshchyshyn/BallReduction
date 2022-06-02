using UnityEngine;

public class CastPoint : MonoBehaviour
{
    [SerializeField] private SphereBullet _sphereBullet;
    [SerializeField] private Transform _playerSphere;

    private void OnEnable()
    {
        _sphereBullet.ObstaclesDestroyed += OnObstacleDestroyed;
    }

    private void OnDisable()
    {
        _sphereBullet.ObstaclesDestroyed -= OnObstacleDestroyed;
    }

    private void OnObstacleDestroyed()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
            _playerSphere.localPosition.y,
            transform.localPosition.z);
    }
}
