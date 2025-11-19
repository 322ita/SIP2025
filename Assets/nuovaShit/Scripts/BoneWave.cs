using UnityEngine;

public class BoneWave : MonoBehaviour
{
    public float expansionSpeed = 10f;
    public float maxSize = 30f;
    public int damage = 1;

    void Update()
    {
        transform.localScale += new Vector3(expansionSpeed, 0, expansionSpeed) * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}