using UnityEngine;
using UnityEngine.Audio;
public class PlayerSoundSystem : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] AudioSource WalkSD;
    [SerializeField] AudioSource SlideSD;
    [SerializeField] AudioSource WallRunSD;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = movement.grounded;
        bool isSliding = movement.isSliding;
        bool isWallRunning = movement.isWallRiding;
        
        Vector2 moveV = movement.moveV;
        if (grounded && !isSliding && !isWallRunning && moveV != Vector2.zero && !WalkSD.isPlaying )
            WalkSD.Play();
        if((!grounded || moveV==Vector2.zero) || (isSliding || isWallRunning))
            WalkSD.Stop();
        if(isSliding && !SlideSD.isPlaying && grounded)
            SlideSD.Play();
        if(!isSliding || (isSliding && !grounded))
            SlideSD.Stop();
        if(isWallRunning && !WallRunSD.isPlaying && !grounded)
            WallRunSD.Play();
        if(!isWallRunning)
            WallRunSD.Stop();
        
    }
}
