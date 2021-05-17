using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stolica : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Animator navanim;
    public GameObject character;
    public GameObject nav1;
    bool sjesti = false;
    bool sjestinav1 = false;
    private int counter = 0;
    bool iswalkingtowards = false;
    bool sittingon = false;
    void Start()
    {
        //anim = GetComponent<Animator>();
        anim = character.GetComponent<Animator>();
        navanim = nav1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sjesti==true && Input.GetKeyDown(KeyCode.R) && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            anim.SetTrigger("sjedni");
        }
        if (Input.GetKeyDown(KeyCode.R) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            anim.SetTrigger("notsitting");
            anim.SetTrigger("idle");
        }
        /*if (nav1.GetComponent<EnemyController>().sjestinav1==true /*&& Input.GetKeyDown(KeyCode.Alpha1)*/ /*&& !this.navanim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            navanim.SetTrigger("sjedni");
        }*/
        /*if (Input.GetKeyDown(KeyCode.Alpha1) && this.navanim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            navanim.SetTrigger("notsitting");
            navanim.SetTrigger("idle");
        }*/
        if (Input.GetKeyDown(KeyCode.H))
        {
            
            Vector3 rotacija = new Vector3(0, 90, 0);
            character.transform.Rotate(rotacija);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sjest"))
        {
            sjesti = true;
        }
        /*if (other.CompareTag("oof"))
        {
            Debug.Log("oof");
            sjestinav1 = true;
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sjest"))
        {
            sjesti = false;
        }
        /*if (other.CompareTag("sjestnavagent1"))
        {
            Debug.Log("banana");
            sjestinav1 = false;
        }*/
    }
}
