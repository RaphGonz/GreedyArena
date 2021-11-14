using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public Transform[] enemy;
        public int count;
        public float rate;
    }

    public Transform[] weapon;
    public Transform weaponSpawn;

    Transform instantiatedWeapon;

    public Wave[] waves;
    private int nextWave = 0;

    public float weaponCooldown = 10;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public UIManager uiManager;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }

        waveCountdown = timeBetweenWaves;

        uiManager.EnableDialogue(5);
        uiManager.SetDialogueText("allez, tue les tous ! une meilleure arme sera livree tout a l'heure...");
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //Begin new round
                WaveCompleted();

            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        if (nextWave < 3)
        {
            uiManager.EnableDialogue(3);
            uiManager.SetDialogueText("oh une nouvelle arme ! va la prendre ! je ramasse celle que tu jette t'inquiete !");

            instantiatedWeapon = Instantiate(weapon[nextWave], weaponSpawn.position, weaponSpawn.rotation);

            StartCoroutine(DespawnWeapon());
        }
        if (nextWave == 5)
        {
            uiManager.EnableDialogue(3);
            uiManager.SetDialogueText("AHAH ! t'as vraiment cru que l'agence allait recruter un raton laveur ? merci pour les armes !");
        }
        
        

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        

        if (nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("ALL WAVES COMPLETED");
            gameObject.SetActive(false);
            return;
        }

        nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            return GameObject.FindGameObjectWithTag("Enemy") != null;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave");
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            int randEnemy = Random.Range(0, _wave.enemy.Length);
            SpawnEnemy(_wave.enemy[randEnemy]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Enemy Spawn : " + _enemy);

        int i = Random.Range(0, spawnPoints.Length);

        Transform _sp = spawnPoints[i];

        spawnPoints[i].gameObject.GetComponentInChildren<ParticleSystem>().Play();

        Instantiate(_enemy, _sp.position, _sp.rotation);

    }

    IEnumerator DespawnWeapon()
    {
        yield return new WaitForSeconds(weaponCooldown);

        Debug.Log("on est en coroutine (arme mourir)");
        if (!instantiatedWeapon.gameObject.GetComponent<Rigidbody2D>().isKinematic)
        {
            Destroy(instantiatedWeapon.gameObject);
        }
    }

}
