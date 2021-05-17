using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sitting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject character;
    public GameObject rotacija;
    Animator anim;
    bool iswalkingtowards = false;
    bool sittingon = false;
    private float h;                                      // Horizontal Axis.
    private float v;
    private int hFloat;                                   // Animator variable related to Horizontal Axis.
    private int vFloat;
    bool jednom = false;
    private float elapsed;
    private float dist;
    void Awake()
    {
        //collider.GetComponent<BoxCollider>().enabled = false;
        hFloat = Animator.StringToHash("H");
        vFloat = Animator.StringToHash("V");
    }
    void Start()
    {
        anim = character.GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        /*if (!sittingon)
        {
            anim.SetFloat(hFloat, h, 0.1f, Time.deltaTime);
            anim.SetFloat(vFloat, v, 0.1f, Time.deltaTime);
            iswalkingtowards = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");*/
        /*if (!sittingon)
        {
            //anim.SetTrigger("iswalking");
            anim.SetFloat(hFloat, h, 0.1f, Time.deltaTime);
            anim.SetFloat(vFloat, v, 0.1f, Time.deltaTime);
            iswalkingtowards = true;
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!sittingon)
            {
                //anim.SetFloat("Speed", 0.5f);
                anim.SetTrigger("iswalking");
                iswalkingtowards = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sittingon)
            {
                //anim.SetFloat("Speed", 0.5f);
                anim.SetTrigger("notsitting");
                //iswalkingtowards = false;
                sittingon = false;
                //elapsed += Time.deltaTime;
                anim.SetTrigger("idle");
            }
        }
        dist = Vector3.Distance(character.transform.position, transform.position);
        //Debug.Log(dist);
        if (iswalkingtowards)
        {
            Vector3 targetdir;
            targetdir = new Vector3(transform.position.x - character.transform.position.x, 0f, transform.position.z - character.transform.position.z);
            Quaternion rot = Quaternion.LookRotation(targetdir);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 15f);
            Debug.Log(character.transform.rotation);

            //transform.LookAt(new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z));

            /*Vector3 rotate = new Vector3(character.transform.rotation.x - character.transform.rotation.x, character.transform.rotation.y - character.transform.rotation.y, character.transform.rotation.z - character.transform.rotation.z);
            character.transform.Rotate(rotate);*/
            character.transform.Translate(Vector3.forward * 0.01f);

            if (dist<=0.56 && character.transform.rotation.eulerAngles.y>= 0/*Vector3.Distance(character.transform.position, this.transform.position) < 0.6 && !jednom*/)
            {
                Debug.Log("aaa");
                anim.SetTrigger("issitting");
                //Vector3 rotate = new Vector4(0, 0, 0,1);
                character.transform.rotation = rotacija.transform.rotation;
                //character.transform.rotation = this.transform.rotation;
                //character.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                //character.transform.rotation = rotate;
                //Debug.Log(this.transform.rotation);
                //character.transform.rotation = rot;
                iswalkingtowards = false;
                sittingon = true;
                //jednom = true;
            }
            if (dist <= 0.56 && character.transform.rotation.eulerAngles.y<0)
            {
                Debug.Log("bbb");
                anim.SetTrigger("issitting");
                character.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y+180, transform.rotation.eulerAngles.z);
                //character.transform.rotation = rotacija.transform.rotation;
                iswalkingtowards = false;
                sittingon = true;
            }
        }
    }
}
