using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfController : MonsterBase
{
    Animation anim;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (_lockTarget == null)
        {
            _lockTarget = GameObject.Find("Player_aya");
        }
    }
    void Update()
    {
        WolfMoving();
    }
    void WolfMoving()
    {
        anim = GetComponent<Animation>();
        _destpos = _lockTarget.transform.position;
        GameObject player = _lockTarget;
        float distance = (player.transform.position - transform.position).magnitude;
        Vector3 dir = _destpos - transform.position;
        if (dir.magnitude <= _scanRange)
        {
            if (dir.magnitude < _attackRange)
            {
                anim.clip = Attack;
                anim.Play();
            }
            else
            {
                agent.SetDestination(_destpos);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
                //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * speed);
                anim.clip = Run;
                anim.Play();
            }
        }
        else
        {
            anim.clip = idle;
            anim.Play();
        }
    }
}
