using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Forward");
    private static readonly int diedInt = Animator.StringToHash("died");
    static readonly int punch = Animator.StringToHash("Punch");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    public GameObject napadni;
    public GameObject collider;
    public Transform teleportTarget;
    public GameObject gledaj;
    public GameObject chair;
    public bool napad = false;
    bool walking = true;
    float timer = 0.0f;
    public bool antibiotik = false;
    public bool jednom = false;
    bool lijek = false;
    float dist;
    public bool provjera=false;
    Vector3 pozicija;
    Igrac igrac;
    private float elapsed;
    [HideInInspector]
    public bool sjestinav1 = false;
    float chairdist;
    float wait;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        pozicija = new Vector3(teleportTarget.transform.position.x, (teleportTarget.transform.position.y + 0.55f), teleportTarget.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = navAgent.velocity.magnitude / navAgent.speed;
        anim.SetFloat(SpeedFloat, velocity);
        dist = Vector3.Distance(napadni.transform.position, transform.position);
        chairdist = Vector3.Distance(chair.transform.position, transform.position);
        if (Input.GetKey(KeyCode.Alpha1) && napad == false /*&& anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")*/ /*&& anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit")*/ /*&& walking==true *//*&& napad==false*/)
        {
            Debug.Log("prvi");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        //Debug.Log(sjestinav1);
        if (/*Input.GetKey(KeyCode.Alpha1) && */sjestinav1 == true && Input.GetKeyDown(KeyCode.T) && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit")) /*&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))*/ //*&& chairdist<=0.9*/ /*&& walking==true *//*&& napad==false*/)
        {
            Debug.Log("drugi");
            //anim.SetTrigger("sjedni");
            anim.SetInteger("sjedni", 1);
            //sjestinav1 = false;
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }*/
        }
        //Debug.Log(sjestinav1);
        if(/*sjestinav1==false && *//*anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit") &&*/ Input.GetKey(KeyCode.Alpha7) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit")/*&& chairdist>1.5 /*&&*//* chairdist>0.9*/ /*Input.GetKeyDown(KeyCode.Alpha1)*/)
        {
            Debug.Log("treci");
            /*anim.SetTrigger("notsitting");
            anim.SetTrigger("idle");*/
            //navAgent.enabled = false;
            anim.SetInteger("notsitting", 1);
            //anim.SetInteger("notsitting", 2);
            //sjestinav1 = false;
            //anim.SetInteger("idle", 1);
            //navAgent.enabled = false;
        }
        /*if (Input.GetKey(KeyCode.Alpha8))/*anim.GetCurrentAnimatorStateInfo(0).IsName("Sit To Stand"))*/ /*&& Input.GetKey(KeyCode.Alpha7)*///)
       /* {
            Debug.Log("73");
            /*wait += Time.deltaTime;
            if (wait >= 2f)
            {*/
         /*       wait = 0;
            //anim.SetInteger("idle", 1);
            //sjestinav1 = false;
            //anim.SetInteger("notsitting", 2);
            //anim.SetFloat(SpeedFloat, 0);
            //sjestinav1 = false;
            anim.SetFloat("idle", 0);
            //}
        }*/
        if (napad == true && !anim.GetCurrentAnimatorStateInfo(0).IsTag("sleep") && napadni.GetComponent<Igrac>().counter != 5)
        {
            
            transform.LookAt(new Vector3(napadni.transform.position.x, transform.position.y, napadni.transform.position.z));

            anim.SetFloat(punch, 3);
        }
        //Debug.Log(napadni.GetComponent<Igrac>().counter);
        if (napadni.GetComponent<Igrac>().counter == 5 /*&& napad == true*/ && !jednom)
        {
            //Debug.Log("ušao sam");
            anim.SetInteger(diedInt, 1);
            //navAgent.updateRotation = false;
            //navAgent.isStopped = true;
            navAgent.enabled = false;
            elapsed += Time.deltaTime;
            if (elapsed >= 2f)
            {
                elapsed = 0f;
                collider.GetComponent<BoxCollider>().enabled = false;
                promjenapozicije();
                //anim.SetInteger("condition", 1);
                //navAgent.isStopped = true;
                //navAgent.SetDestination(false);
                elapsed += Time.deltaTime;
                var y = 180 - transform.position.y;
                var rotate = new Vector3(0f, (transform.rotation.y + y), transform.rotation.z);
                transform.Rotate(rotate);
                //Debug.Log("rotate:" + rotate);
                //transform.Rotate(rotate);
                //navAgent.transform.rotation = Quaternion.LookRotation(rotate);
                //transform.rotation = Quaternion.LookRotation(navAgent.velocity.normalized);
                //navAgent.updateRotation = true;
                //transform.LookAt(new Vector3(gledaj.transform.position.x, gledaj.transform.position.y, gledaj.transform.position.z));
                //navAgent.ResetPath();
                jednom = true;
            }
        }
        if (collider.GetComponent<BoxCollider>().enabled == true && dist <= 9 && napad == false && navAgent.enabled==true /*&& lijek==true*/)
            {
                navAgent.SetDestination(napadni.transform.position);
            }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
        }
        if (other.CompareTag("napad"))
        {
            napad= true;
            walking = false;
            check(!provjera);
        }
        if (other.CompareTag("object"))
        {
            antibiotik = true;
            lijek = true;
            if (antibiotik == true)
            {
                //collider.SetActive(true);
                collider.GetComponent<BoxCollider>().enabled = true;
            }
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = null;
        }
        if (other.CompareTag("napad"))
        {
            napad = false;
            walking = true;
            if (napad == false /*&& CompareTag("Player")*/)
            {
                anim.SetFloat(punch, 0);
            }
        }
        if (other.CompareTag("object"))
        {
            antibiotik = false;
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = false;
        }
    }
    void promjenapozicije()
    {
        transform.position = pozicija;
        //transform.LookAt(new Vector3(gledaj.transform.position.x, gledaj.transform.position.y, gledaj.transform.position.z));
    }

    void check(bool check)
    {
        check = true;
    }
    private static EnemyController enemy;
    public static EnemyController enem
    {
        get
        {
            return enemy;
        }
    }
}
