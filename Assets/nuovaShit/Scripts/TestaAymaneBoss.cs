using UnityEngine;

public class TestaAymaneBoss : MonoBehaviour
{
    public int maxHealth = 6000;
    private int currentHealth;
    public bool isPhaseTwo = false;

    public GameObject boneWavePrefab;
    public float waveRate = 3f;
    private float nextWaveTime = 0f;

    public GameObject fireSpellPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    public GameObject groundSpikePrefab;
    public float spikeRate = 6f;
    private float nextSpikeTime = 0f;

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        HandleAttacks();
    }

    void HandleAttacks()
    {
        float time = Time.time;

        if (time >= nextWaveTime)
        {
            SpawnBoneWave();
            nextWaveTime = time + waveRate;
        }

        if (time >= nextFireTime)
        {
            SpawnFire();
            nextFireTime = time + fireRate;
        }

        if (currentHealth <= 3000)
        {
            isPhaseTwo = true;
            if (time >= nextSpikeTime)
            {
                SpawnGroundSpikes();
                nextSpikeTime = time + spikeRate;
            }
        }
    }

    void SpawnBoneWave()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Instantiate(boneWavePrefab, spawnPos, Quaternion.identity);
    }

    void SpawnFire()
    {
        if (firePoint != null && fireSpellPrefab != null)
        {
            Instantiate(fireSpellPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void SpawnGroundSpikes()
    {
        Vector3 spawnPos = new Vector3(player.position.x, player.position.y, player.position.z);
        Instantiate(groundSpikePrefab, spawnPos, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}