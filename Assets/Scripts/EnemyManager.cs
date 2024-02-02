using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public event Action OnStartNewWawe;

    public static EnemyManager Instance;

    [SerializeField] private GameMenager _gameMenager;
    [SerializeField] private EnemyFabric _fabric;
    [SerializeField] private int _waweIndex;
    [SerializeField] private int _enemyCount;
    [SerializeField] private Transform _destinationTarget;

    [SerializeField] private bool _started = false;

    public List<Enemy> EnemyList = new List<Enemy>();

    public int Wawe { get { return _waweIndex; } set { _waweIndex = value; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _gameMenager.OnGameStart += OnGameStart;
        OnStartNewWawe += EnemyManagerOnStartNewWawe;
    }

    private void OnDisable()
    {
        _gameMenager.OnGameStart -= OnGameStart;
        OnStartNewWawe -= EnemyManagerOnStartNewWawe;
    }

    private void OnGameStart()
    {
        StartCoroutine(CreateWawe(_enemyCount, _fabric.GetNextWave(0).WaveDeley));
    }

    private void EnemyManagerOnStartNewWawe()
    {
        _started = true;
        StartCoroutine(CreateWawe(_enemyCount, _fabric.GetNextWave(_waweIndex).WaveDeley));
    }

    private void Update()
    {
        if (EnemyList.Count > 0 && EnemyList.All(x => x.gameObject.activeSelf == false) && !_started)
        {
            OnStartNewWawe?.Invoke();
        }
    }

    public IEnumerator CreateWawe(int enemyCount, float waweDelay = 0)
    {
        _waweIndex++;

        yield return new WaitForSeconds(waweDelay);

        if (EnemyList.Count > 0)
        {
            foreach (Enemy enemy in EnemyList)
            {
                Destroy(enemy.gameObject);
            }
        }

        EnemyList = new List<Enemy>();
        var wave = _fabric.GetNextWave(_waweIndex);

        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(wave.Enemy);
            enemy.SetDestination(_destinationTarget.position);
            EnemyList.Add(enemy);
            yield return new WaitForSeconds(2f);
        }

        _started = false;
    }
}