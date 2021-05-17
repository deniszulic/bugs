using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrac : MonoBehaviour
{
    private Animator anim;
    private static readonly int SpeedFloat = Animator.StringToHash("Speed");
    static readonly int punchTrigger = Animator.StringToHash("Punch");
    static readonly int idlepunchTrigger = Animator.StringToHash("idle_punch");
    static readonly int conditionTrigger = Animator.StringToHash("condition");
    static readonly int conditionState = Animator.StringToHash("condition");
    public Transform teleportTarget;
    private Animation animation;
    [HideInInspector]
    public int counter = 0;
    int loops = 0;
    private bool loadingGun = false;
    private float atGunTime = 1.6f;
    private float atGun;
    private float wait = 2000001.6f;
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    private float scrollBar = 1.0f;
    private int i = 0;
    private float timerMax = 0;
    string time;
    float seconds = 0;
    public GameObject napadni;
    public GameObject pomocnik;
    public GameObject collider;
    public GameObject character;
    private int hFloat;                                   // Animator variable related to Horizontal Axis.
    private int vFloat;
    private float h;                                      // Horizontal Axis.
    private float v;
    bool iswalkingtowards = false;
    bool sittingon = false;
    //public bool dead = false;
    //public GameObject thePlayer;
    // Start is called before the first frame update
    void Awake()
    {
        collider.GetComponent<BoxCollider>().enabled = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        //collider.GetComponent<BoxCollider>().enabled = false;
        //shutdown.
    }

    // Update is called once per frame
    void Update()
    {
        var punch = Input.GetMouseButton(0);
        var notpunch = Input.GetMouseButtonUp(0);
        var pickup = Input.GetKey(KeyCode.E);
        var notpickup = Input.GetKeyUp(KeyCode.E);

        // Set the input axes on the Animator Controller.
        if (punch )
        {
            timer += Time.deltaTime;
            /*if (timer >= 8f)
            {*/
                anim.SetInteger(conditionTrigger, 1);
            //}
            //timer = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        /*else
        {
            timer = 0;
        }*/
        //Debug.Log(timer);
        if (notpunch /*&& anim.GetFloat("Speed") < 0.1*/)
        {
            //anim.SetTrigger(idlepunchTrigger);
            timer = 0;
            anim.SetInteger(conditionTrigger, 0);
            //counter += 1;
        }
        if (timer >= 7.5f && napadni.GetComponent<EnemyController>().napad == true)
        {
            //counter = 1;
            counter = 5;
            //timer = 0;
            //dead = true;
        }
        /*else
        {
            //timer = 0;
            counter = 0;
        }*/
        //Debug.Log(counter);
        /*if (notpunch && anim.GetFloat("Speed") < 0.1 && pickup)
        {
            anim.SetInteger(conditionTrigger, 0);
        }
        if (notpunch && anim.GetFloat("Speed") < 0.1 && notpickup)
        {
            anim.SetInteger(conditionTrigger, 0);
        }*/
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(270f, 0, 0, 0);
            Vector3 pozicija = new Vector3(teleportTarget.transform.position.x, (teleportTarget.transform.position.y + 0.55f), teleportTarget.transform.position.z);
            transform.position = pozicija;
        }
        
    }
    private static Igrac igr;
    public static Igrac igrac
    {
        get
        {
            return igr;
        }
    }
    private bool Waited(float seconds)
    {
        timerMax = seconds;

        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            return true; 
        }

        return false;
    }
    int broji()
    {
        return i++;
    }
 
}
