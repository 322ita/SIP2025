using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public HealthScript healthScript;
    [SerializeField] GameObject CuorePieno;
    [SerializeField] GameObject CuoreRossa;
    [SerializeField] GameObject[] cuoriPieni;
    [SerializeField] GameObject[] cuoriRossi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GameObject.FindAnyObjectByType<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthScript.currentHealth > 2)
        {
            CuorePieno.SetActive(true);
            CuoreRossa.SetActive(false);
        }
        else
        {
            CuorePieno.SetActive(false);
            CuoreRossa.SetActive(true);
        }
        for( int i = 0; i < cuoriPieni.Length; i++)
        {
            if(i < healthScript.currentHealth)
            {
                cuoriPieni[i].SetActive(true);
                cuoriRossi[i].SetActive(false);
            }
            else
            {
                cuoriPieni[i].SetActive(false);
                cuoriRossi[i].SetActive(true);
            }
        }
    }
}
