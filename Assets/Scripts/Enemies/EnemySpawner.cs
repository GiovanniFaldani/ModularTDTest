using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SO_Wave[] waves;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform[] pathPoints;

    private int waveIndex = 0;
    private SO_Wave currentWave;
    private float _timer = 0;
    private bool _started = false;
    private List<Enemy> liveEnemies = new List<Enemy>();

    private int enemyIndex = 0;

    public static EnemySpawner Instance;

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

    private void Update()
    {
        SpawnEnemies();
    }

    public void SpawnWave()
    {
        if(waveIndex < waves.Length)
        {
            currentWave = waves[waveIndex];
            enemyIndex = 0;
            _started = true;
            waveIndex++;
        }
    }

    public void SpawnEnemies()
    {
        if (_started)
        {
            _timer -= Time.deltaTime;
            if (enemyIndex < currentWave.enemiesinWave.Length)
            {
                if (_timer <= 0) { 
                    Enemy currentEnemy = Instantiate(currentWave.enemiesinWave[enemyIndex], spawnPoint).GetComponent<Enemy>();
                    liveEnemies.Add(currentEnemy);
                    currentEnemy.SetPath(pathPoints);
                    enemyIndex += 1;
                    _timer = timeBetweenSpawns;
                }
            }
            else
            {
                if (liveEnemies.Count <= 0)
                {
                    _started = false;
                    UIUpdater.Instance.nextWaveButton.SetActive(true);
                    if(waveIndex >= waves.Length)
                    {
                        GameManager.Instance.WinGame();
                    }
                }
            }
        }
    }

    public void UntrackEnemy(Enemy _enemy)
    {
        liveEnemies.Remove(_enemy);
    }

}
