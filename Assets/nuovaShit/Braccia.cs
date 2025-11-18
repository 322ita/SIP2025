using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Atouas;
using UnityEngine.Animations;

public class Braccia : MonoBehaviour
{
    [SerializeField] private int bracciaIndex = 1;
    private int incremento=0;
    private bool attacking = false;
    private double timer;
    [SerializeField] private Atouas.Braccia[] braccie;
    [SerializeField] public RenderTexture re;
    [SerializeField] public VideoPlayer vp;
    [SerializeField] public RawImage ri;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        vp = GameObject.FindAnyObjectByType<VideoPlayer>();
        ri = GameObject.FindGameObjectWithTag("BracciaUI").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        incremento+=(int)(Input.GetAxis("Mouse ScrollWheel")*10);
        if(incremento<0) incremento=braccie.Length-1;
        bracciaIndex = incremento%braccie.Length;

        float magnitude = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        if (!attacking)
        {
            if( magnitude < 0.1f)
            {
                vp.clip = braccie[bracciaIndex].idle;
                vp.Play();
                ri.gameObject.transform.localPosition = braccie[bracciaIndex].posIdle;
            }
            else
            {
                vp.clip = braccie[bracciaIndex].run;
                vp.Play();
                ri.gameObject.transform.localPosition = braccie[bracciaIndex].posRun;
            }
        }
        Attack();
    }

    void Attack()
    {
        if(braccie[bracciaIndex].attack.Length > 0)
        {
            if(attacking)
            {
                timer += Time.deltaTime;
                if(timer >= vp.clip.length)
                {
                    timer = 0;
                    attacking = false;
                }
                return;
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                vp.clip = braccie[bracciaIndex].attack[0];
                vp.Play();
                attacking = true;
                ri.gameObject.transform.localPosition = braccie[bracciaIndex].posAttack;
                
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1) && braccie[bracciaIndex].attack.Length > 1)
            {
                vp.clip = braccie[bracciaIndex].attack[1];
                vp.Play();
                attacking = true;
                ri.gameObject.transform.localPosition = braccie[bracciaIndex].posAttack;
            }
        }
    }
}
