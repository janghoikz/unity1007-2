using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    protected GameObject _lockTarget;
    public Vector3 _destpos;

    //애니메이션 
    [SerializeField]
    public AnimationClip idle;
    [SerializeField]
    public AnimationClip Run;
    [SerializeField]
    public AnimationClip Attack;

    [SerializeField]
    public float _scanRange = 10;

    [SerializeField]
    public float _attackRange = 1.5f;

    
    private void start()
    {
    }
    
}
