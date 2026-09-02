using Unity.Microsoft.GDK;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown;
    [Space]
    [SerializeField] private float cooldownDecreaseRate = .05f;
    [SerializeField] private float cooldownCap = .7f;

    private float timer;

    private Transform player;

    private void Awake()
    {
        player = FindAnyObjectByType<player>().transform;
    }

    private void Update()
    {
        timer -=Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;
            CreateNewEnemy();

            cooldown =Mathf.Max(cooldownCap,cooldown-cooldownDecreaseRate);
        }
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0,respawnPoints.Length);
        Vector3 spawnPoint = respawnPoints[respawnPointIndex].position;
        GameObject newEnemy = Instantiate(enemyPrefab,respawnPoints[respawnPointIndex].position,Quaternion.identity);

        bool createdOnTheRight = newEnemy.transform.position.x>player.transform.position.x;

        if(createdOnTheRight)
            newEnemy.GetComponent<Enemy>().Flip();
    }



}