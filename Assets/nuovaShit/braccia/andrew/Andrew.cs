using UnityEngine;
using UnityEngine.Video;
using Atouas;
using UnityEngine.UI;

public class Andrew : MonoBehaviour
{
    private  bool attackingSX = false;
    private  bool attackingDX = false;
    private double timerSX;
    private double timerDX;
    [SerializeField] private float dist = 3f;
    private double timerDanno=0;
    [SerializeField] private Atouas.Braccia braccia;
    [SerializeField] public VideoPlayer vpSX;
    [SerializeField] public VideoPlayer vpDX;
    [SerializeField] public RawImage ri;
    [SerializeField] public RawImage riSX;
    [SerializeField] public RawImage riDX;
    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        vpSX = GameObject.Find("VideoSX").GetComponent<VideoPlayer>();
        vpDX = GameObject.Find("VideoDX").GetComponent<VideoPlayer>();
        rb = GetComponent<Rigidbody>();
        riSX = GameObject.Find("BraccioSX").GetComponent<RawImage>();
        riDX = GameObject.Find("BraccioDX").GetComponent<RawImage>();
        ri = GameObject.Find("BracciaSingolo").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // incremento+=(int)(Input.GetAxis("Mouse ScrollWheel")*10);
        // if(incremento<0) incremento=braccia.Length-1;
        // bracciaIndex = incremento%braccia.Length;

        float magnitude = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        bool attaccando = attackingSX || attackingDX;
        if (!attaccando)
        {
            if( magnitude < 0.1f)
            {
                    riSX.gameObject.transform.localPosition = braccia.posIdleSX;
                    riDX.gameObject.transform.localPosition = braccia.posIdleDX;
                    ri.gameObject.SetActive(false);
                    riSX.gameObject.SetActive(true);
                    riDX.gameObject.SetActive(true);
                    vpSX.clip = braccia.idleSX;
                    vpDX.clip = braccia.idleDX;
                    vpSX.Play();
                    vpDX.Play();
                
            }
            else
            {
                    riSX.gameObject.transform.localPosition = braccia.posRunSX;
                    riDX.gameObject.transform.localPosition = braccia.posRunDX;
                    ri.gameObject.SetActive(false);
                    riSX.gameObject.SetActive(true);
                    riDX.gameObject.SetActive(true);
                    vpSX.clip = braccia.runSX;
                    vpDX.clip = braccia.runDX;
                    vpSX.Play();
                    vpDX.Play();
                }
            }
        Attack();
        primoAttackHandler();
    }

    void Attack()
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
            

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                    attackingSX = true;
                    riSX.gameObject.transform.localPosition = braccia.posAttackSX;
                    vpSX.clip = braccia.attackSX;
                    vpSX.Play();

            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                
                    attackingDX = true;
                    riDX.gameObject.transform.localPosition = braccia.posAttackDX;
                    vpDX.clip = braccia.attackDX;
                    vpDX.Play();
                }
            }
            void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * dist);
    }
    void primoAttackHandler()
    {
        if(!attackingDX && !attackingSX) return;
        timerDanno += Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, dist))
        {
            if(hit.collider.gameObject.CompareTag("Nemico"))
            {
                Debug.Log("Colpito nemico");
                TestaLimoneAI hs = hit.collider.gameObject.GetComponent<TestaLimoneAI>();
                if(hs != null && timerDanno >= 1)
                {
                    hs.TakeDamage(200);
                    timerDanno = 0;
                }
            }
        }
    }
        }

