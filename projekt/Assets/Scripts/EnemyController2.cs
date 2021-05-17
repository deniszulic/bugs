using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController2 : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Speed");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    bool sjestinav1 = false;
    bool jednom = false;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = navAgent.velocity.magnitude / navAgent.speed;
        anim.SetFloat(SpeedFloat, velocity);
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        if (sjestinav1 == true && Input.GetKeyDown(KeyCode.T) &&!jednom && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Debug.Log("drugi");
            anim.SetFloat("sjedni", 1);
            jednom = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Idle") && Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetInteger("standingidle", 1);
        }
        //Debug.Log(sjestinav1);
        if (Input.GetKeyDown(KeyCode.Alpha7) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            Debug.Log("treci");
            anim.SetInteger("notsitting", 1);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
            Debug.Log("following:" + following);
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = null;
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = false;
        }
    }
}
