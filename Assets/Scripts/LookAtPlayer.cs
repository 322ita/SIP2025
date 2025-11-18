using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.parent.forward;
        direction.y = 0;
        Vector3 directionPlayer = (player.transform.position - transform.position).normalized;
        directionPlayer.y = 0;
        float angle = Vector3.Angle(direction, directionPlayer);
        if (angle <= 60)
        {
            Quaternion target = Quaternion.LookRotation(directionPlayer);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, target, 10 * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 10 * Time.deltaTime);
        }
        
    }
}
