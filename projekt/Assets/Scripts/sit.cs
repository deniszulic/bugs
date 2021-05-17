using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sit : MonoBehaviour
{
    public GameObject character;
    Animator anim;
    bool iswalkingtowards = false;
    bool sittingon = false;
    void OnMouseDown()
    {
        if (!sittingon)
        {
            anim.SetTrigger("iswalking");
            iswalkingtowards = true;
        }
    }
    void Start()
    {
        anim = character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iswalkingtowards)
        {
            Vector3 targetdir;
            targetdir = new Vector3(transform.position.x - character.transform.position.x, 0f, transform.position.z - character.transform.position.z);
            Quaternion rot = Quaternion.LookRotation(targetdir);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
            character.transform.Translate(Vector3.forward * 0.01f);

            if(Vector3.Distance(character.transform.position, this.transform.position) < 0.6)
            {
                anim.SetTrigger("issitting");
                character.transform.rotation = this.transform.rotation;
                iswalkingtowards = false;
                sittingon = true;
            }
        }
    }
}
