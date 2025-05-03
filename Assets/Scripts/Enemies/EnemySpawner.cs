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
    private Enemy currentEnemy;

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
        }
        else
        {
            // Win game
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
                    currentEnemy = Instantiate(currentWave.enemiesinWave[enemyIndex], spawnPoint).GetComponent<Enemy>();
                    currentEnemy.SetPath(pathPoints);
                    enemyIndex += 1;
                    _timer = timeBetweenSpawns;
                }
            }
            else
            {
                if (currentEnemy == null)
                {
                    _started = false;
                     UIUpdater.Instance.nextWaveButton.SetActive(true);
                }
            }
        }
    }

}
