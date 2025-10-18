using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnHandler : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private Rainable _prefabRainable;
    [SerializeField] private Color _defaultPrefabColor;
    [SerializeField] private float _spawnDelay = 1;
    [SerializeField] private Transform _spawnZoneStart;
    [SerializeField] private Transform _spawnZoneEnd;

    [Header("Collided Objects Lifetime")]
    [SerializeField, Min(2)] private float _lifetimeMin = 2;
    [SerializeField, Min(5)] private float _lifetimeMax = 5;

    [Header("Pool")]
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Rainable> _pool;

    private void OnValidate()
    {
        if (_lifetimeMax <= _lifetimeMin)
        {
            _lifetimeMax = _lifetimeMin + 1;
        }

        if (_poolMaxSize < _poolCapacity)
        {
            _poolMaxSize = _poolCapacity;
        }
    }

    private void Awake()
    {
        _pool = new ObjectPool<Rainable>(
            createFunc: CreatePooledObject,
            actionOnGet: (rainable) => OnTakeFromPool(rainable),
            actionOnRelease: (rainable) => rainable.gameObject.SetActive(false),
            actionOnDestroy: (rainable) => Destroy(rainable.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnObjectOfPool());
    }

    private Rainable CreatePooledObject()
    {
        Rainable rainable = Instantiate(_prefabRainable);
        rainable.ActionPlatformCollided += OnPlatformCollided;

        return rainable;
    }

    private void OnTakeFromPool(Rainable rainable)
    {
        rainable.transform.position = DevUtils.GetRandomVector3(_spawnZoneStart.position, _spawnZoneEnd.position);
        rainable.Rigidbody.velocity = Vector3.zero;
        rainable.Rigidbody.angularVelocity = Vector3.zero;

        rainable.Init(_defaultPrefabColor);
        rainable.gameObject.SetActive(true);
    }

    private IEnumerator SpawnObjectOfPool()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            _pool.Get();

            yield return wait;
        }
    }

    private void OnPlatformCollided(Rainable rainable)
    {
        StartCoroutine(StartTimerToDestroy(rainable));
    }

    private IEnumerator StartTimerToDestroy(Rainable rainable)
    {
        yield return new WaitForSeconds(DevUtils.GetRandomNumber(_lifetimeMin, _lifetimeMax + 1f));

        _pool.Release(rainable);
    }
}
