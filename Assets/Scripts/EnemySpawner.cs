using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public ZombieNavigation EnemyPrefab;
    public int minEnemiesPerBatch = 1;
    public int maxEnemiesPerBatch = 3;
    public float spawnRadius = 5f;
    public float respawnDelay = 60f;
    public Transform player;
    public float minSpawnDistanceToPlayer = 15f;
    public float maxSpawnDistanceToPlayer = 60f;

    private readonly List<ZombieNavigation> aliveZombies = new();
    private bool respawnScheduled = false;

    void Start()
    {
        SpawnBatch();
    }


    void Update()
    {

    }

    private void SpawnBatch()
    {
        respawnScheduled = false;
        aliveZombies.Clear();

        int count = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch + 1);

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = GetSpawnPosition();
            ZombieNavigation zombie = Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);

            aliveZombies.Add(zombie);
            zombie.OnDeath += HandleZombieDeath;
        }

    }

    private Vector3 GetSpawnPosition()
    {
        //random nearby spawn on our navMesh
        Vector3 origin = transform.position;

        Vector2 rnd = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPoint = origin + new Vector3(rnd.x, 0f, rnd.y);

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // fallback: spawner position
        return origin;
    }

    private void HandleZombieDeath(ZombieNavigation zombie)
    {
        zombie.OnDeath -= HandleZombieDeath;
        aliveZombies.Remove(zombie);

        // when all are dead, schedule respawn
        if (aliveZombies.Count == 0 && !respawnScheduled)
        {
            respawnScheduled = true;
            StartCoroutine(AttemptSpawn());
        }
    }

    private bool CanSpawnNow()
    {
        if (player == null) return true;

        float sqrDist = (player.position - transform.position).sqrMagnitude;
        if (maxSpawnDistanceToPlayer * maxSpawnDistanceToPlayer < sqrDist || sqrDist < minSpawnDistanceToPlayer * minSpawnDistanceToPlayer)
            return false;

        return true;
    }

    private IEnumerator AttemptSpawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        while (true)
        {
            if (CanSpawnNow())
            {
                SpawnBatch();
                yield break;
            }
            yield return new WaitForSeconds(10);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSpawnDistanceToPlayer);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistanceToPlayer);
    }
}
