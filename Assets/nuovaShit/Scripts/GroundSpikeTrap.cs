using UnityEngine;
using System.Collections;

public class GroundSpikeTrap : MonoBehaviour
{
    public int damage = 2;
    public float warningTime = 2f;
    public float activeTime = 1f;

    public GameObject warningVisual;
    public GameObject spikeVisual;
    public Collider damageCollider;

    void Start()
    {
        StartCoroutine(SpikeSequence());
    }

    IEnumerator SpikeSequence()
    {
        if(warningVisual != null) warningVisual.SetActive(true);
        if(spikeVisual != null) spikeVisual.SetActive(false);
        if(damageCollider != null) damageCollider.enabled = false;

        yield return new WaitForSeconds(warningTime);

        if(warningVisual != null) warningVisual.SetActive(false);
        if(spikeVisual != null) spikeVisual.SetActive(true);
        if(damageCollider != null) damageCollider.enabled = true;

        yield return new WaitForSeconds(activeTime);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }
}