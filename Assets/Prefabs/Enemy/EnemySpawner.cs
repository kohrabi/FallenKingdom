using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    // Types of enemy to spawn in Waves
    public class EnemyWave
    {
        public EnemyWave(EnemyWave e)
        {
            Prefab = e.Prefab;
            StartWave = e.StartWave;
            EnemyCount = e.EnemyCount;
            CountAddPerWave = e.CountAddPerWave;    
        }
        public GameObject Prefab;
        public int StartWave; // The first wave the enemy appear in;
        public float EnemyCount;
        public float CountAddPerWave;
    }
    public float TransitionDayNightDelay = 1f;
    public float PlayableZone = 5f; // The place where enemy cannot spawn
    public float PlayableZoneOffset = 2f;
    public float SpawnZone = 10f;
    public float ZoneDistance = 5f;
    public int CurrentWave = 0;
    public float SpawnDelay = 6f;
    public bool isDay = true;
    public bool isNight = false;
    public List<GameObject> SpawnedEnemy;

    public EnemyWave[] Waves;
    private List<EnemyWave> enemyToSpawn;

    float nextSpawnTime = 0f;
    bool canSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = new();
        SpawnedEnemy = new();
    }

    public void ChangeZone(float posMaxXY)
    {
        if (posMaxXY + PlayableZoneOffset < PlayableZone)
            return;
        PlayableZone = posMaxXY + PlayableZoneOffset;
        SpawnZone = posMaxXY + ZoneDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSpawn)
        {
            SpawnWave();
            if (SpawnedEnemy.Count <= 0 && enemyToSpawn.Count <= 0)
            {
                // Set The Time to day
                StartCoroutine(DelayTransitionDayNight());
            }
        }
        for (int i = 0; i < SpawnedEnemy.Count; i++)
            if (SpawnedEnemy[i] == null)
                SpawnedEnemy.RemoveAt(i);
    }

    private IEnumerator DelayTransitionDayNight()
    {
        yield return new WaitForSeconds(TransitionDayNightDelay);
        isDay = true;
        isNight = false;
        canSpawn = false;
    }

    public void SwitchNightTime()
    {
        //Reset Spawn
        for (int i = 0; i < Waves.Length; i++)
        {
            if (CurrentWave >= Waves[i].StartWave)
            {
                EnemyWave wave = new EnemyWave(Waves[i]);
                wave.EnemyCount += wave.CountAddPerWave;
                enemyToSpawn.Add(wave);
            }
            else
                break;
        }
        CurrentWave++;
        isDay = false;
        canSpawn = true;
        isNight = true;
        foreach (GameObject wave in GameObject.FindGameObjectsWithTag("WaveText"))
               wave.GetComponent<TMP_Text>().text = (CurrentWave - 1).ToString();
        var player = GameObject.FindWithTag("Player").GetComponent<DestroyableEntity>();
        player.currentHP = player.HealthPoint;
    }

    void SpawnWave()
    {
        if (enemyToSpawn.Count > 0 && nextSpawnTime < Time.time)
        {
            nextSpawnTime = Time.time + SpawnDelay / CurrentWave;

            EnemyWave randomEnemy = enemyToSpawn[Random.Range(0, enemyToSpawn.Count - 1)];
            randomEnemy.EnemyCount--;
            int random = Random.Range(0, 2);
            float X = 0;
            float Y = 0;
            if (random == 0)
                X = Random.Range(PlayableZone, SpawnZone);
            else
                X = Random.Range(-SpawnZone, -PlayableZone);
            random = Random.Range(0, 2);
            if (random == 0)
                Y = Random.Range(PlayableZone, SpawnZone);
            else
                Y = Random.Range(-SpawnZone, -PlayableZone);

            GameObject enemy = Instantiate(randomEnemy.Prefab, new Vector2(X, Y), Quaternion.identity);
            SpawnedEnemy.Add(enemy);
            if (randomEnemy.EnemyCount <= 0)
                enemyToSpawn.Remove(randomEnemy);
        }
    }
}
