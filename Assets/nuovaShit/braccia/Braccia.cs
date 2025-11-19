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
    private  bool attackingSX = false;
    private  bool attackingDX = false;
    private double timer;
    private double timerSX;
    private double timerDX;
    [SerializeField] private Atouas.Braccia[] braccie;
    [SerializeField] public RenderTexture re;
    [SerializeField] public VideoPlayer vpSX;
    [SerializeField] public VideoPlayer vpDX;
    [SerializeField] public VideoPlayer vp;
    [SerializeField] public RawImage ri;
    [SerializeField] public RawImage riSX;
    [SerializeField] public RawImage riDX;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        vpSX = GameObject.Find("VideoSX").GetComponent<VideoPlayer>();
        vpDX = GameObject.Find("VideoDX").GetComponent<VideoPlayer>();
        vp = GameObject.Find("VideoSingolo").GetComponent<VideoPlayer>();
        riSX = GameObject.Find("BraccioSX").GetComponent<RawImage>();
        riDX = GameObject.Find("BraccioDX").GetComponent<RawImage>();
        ri = GameObject.Find("BracciaSingolo").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // incremento+=(int)(Input.GetAxis("Mouse ScrollWheel")*10);
        // if(incremento<0) incremento=braccie.Length-1;
        // bracciaIndex = incremento%braccie.Length;

        float magnitude = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        bool attaccando = braccie[bracciaIndex].singolo ? attacking : (attackingSX || attackingDX);
        if (!attaccando)
        {
            if( magnitude < 0.1f)
            {
                if (braccie[bracciaIndex].singolo)
                {
                    vp.clip = braccie[bracciaIndex].idle;
                    vp.Play();
                    ri.gameObject.transform.localPosition = braccie[bracciaIndex].posIdle;
                    riSX.gameObject.SetActive(false);
                    riDX.gameObject.SetActive(false);
                    ri.gameObject.SetActive(true);
                }
                else
                {
                    ri.gameObject.SetActive(false);
                    riSX.gameObject.SetActive(true);
                    riDX.gameObject.SetActive(true);
                    vpSX.clip = braccie[bracciaIndex].idleSX;
                    vpDX.clip = braccie[bracciaIndex].idleDX;
                    vpSX.Play();
                    vpDX.Play();
                    riSX.gameObject.transform.localPosition = braccie[bracciaIndex].posIdleSX;
                    riDX.gameObject.transform.localPosition = braccie[bracciaIndex].posIdleDX;
                }
            }
            else
            {
                if (braccie[bracciaIndex].singolo)
                {
                    vp.clip = braccie[bracciaIndex].run;
                    vp.Play();
                    ri.gameObject.transform.localPosition = braccie[bracciaIndex].posRun;
                    riSX.gameObject.SetActive(false);
                    riDX.gameObject.SetActive(false);
                    ri.gameObject.SetActive(true);
                }
                else
                {
                    ri.gameObject.SetActive(false);
                    riSX.gameObject.SetActive(true);
                    riDX.gameObject.SetActive(true);
                    vpSX.clip = braccie[bracciaIndex].runSX;
                    vpDX.clip = braccie[bracciaIndex].runDX;
                    vpSX.Play();
                    vpDX.Play();
                    riSX.gameObject.transform.localPosition = braccie[bracciaIndex].posRunSX;
                    riDX.gameObject.transform.localPosition = braccie[bracciaIndex].posRunDX;
                }
            }
            
        }
        Attack();
    }

    void Attack()
    {
        if(true)
        {
            if(!braccie[bracciaIndex].singolo)
            {
                if(attackingSX)
                {
                    timerSX += Time.deltaTime;
                    if(timerSX >= vpSX.clip.length)
                    {
                        timerSX = 0;
                        attackingSX = false;
                    }
                    return;
                }
                if(attackingDX)
                {
                    timerDX += Time.deltaTime;
                    if(timerDX >= vpDX.clip.length)
                    {
                        timerDX = 0;
                        attackingDX = false;
                    }
                    return;
                }
            }
            else
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
            }
            
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
                if(!braccie[bracciaIndex].singolo)
                {
                    vpSX.clip = braccie[bracciaIndex].attackSX;
                    vpSX.Play();
                    attackingSX = true;
                    riSX.gameObject.transform.localPosition = braccie[bracciaIndex].posAttackSX;
                }
                else
                {
                    vp.clip = braccie[bracciaIndex].attackSingolo;
                    vp.Play();
                    attacking = true;
                    ri.gameObject.transform.localPosition = braccie[bracciaIndex].posAttackSingolo;
                }

            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(!braccie[bracciaIndex].singolo)
                {
                    vpDX.clip = braccie[bracciaIndex].attackDX;
                    vpDX.Play();
                    attackingDX = true;
                    riDX.gameObject.transform.localPosition = braccie[bracciaIndex].posAttackDX;
                }
                else
                {
                    vp.clip = braccie[bracciaIndex].carica;
                    vp.Play();
                    attacking = true;
                    ri.gameObject.transform.localPosition = braccie[bracciaIndex].posCarica;
                }
            }
        }
    }
}
