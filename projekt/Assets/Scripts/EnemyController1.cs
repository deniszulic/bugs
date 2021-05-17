using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController1 : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Forward");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    private float elapsed;
    private bool antibiotik=false;
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
        /*if (following)
        {
            navAgent.SetDestination(following.position);
            //Debug.Log("following.position:" + following.position);
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        if (antibiotik == true)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 2f)
            {
                elapsed = 0f;
                anim.SetInteger("seizure", 2);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
            Debug.Log("following:" + following);
        }
        if (other.CompareTag("object"))
        {
            anim.SetInteger("seizure", 1);
            antibiotik = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = null;
        }
    }
}
