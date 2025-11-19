using UnityEngine;
using UnityEngine.UI;


enum ImageState
{
    npc,
    image
}
public class MovingImage : MonoBehaviour
{
    [SerializeField] RawImage rawImage;
    [SerializeField] Texture[] images;
    [SerializeField] float timeToChange = 1f;
    [SerializeField] ImageState imageState = ImageState.npc;
    int inizio = 0;

    float time=0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int fine = images.Length - 1;
        time += Time.deltaTime;
        //Debug.Log($"Time: {time}, inizio: {inizio}, fine: {fine}");
        if (time >= timeToChange)
        {
            time = 0f;
            inizio++;
            if (inizio > fine)
                inizio = 0;
            switch (imageState)
            {
                case ImageState.image:
                    rawImage.texture = images[inizio];
                    break;
                default:
                    break;
            }
                
        }
    }
}
