using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public Transform[] patrolPos;
    public int[] flip;
    public int _currentPatrolID;
    private int CurrentPatrolID
    {
        get{return _currentPatrolID;}
        set
        {
            if(_currentPatrolID >= patrolPos.Length - 1)
            {
                _currentPatrolID = 0;
            }
            else
            {
                _currentPatrolID =value;
            }
        }
    }
    public float speed;
    private Transform _fish;
    // Start is called before the first frame update
    void Start()
    {
        _fish = this.transform;
      _currentPatrolID = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _dist = Vector2.Distance(patrolPos[CurrentPatrolID].position,this.transform.position);
        if(_dist > 0.1f)
        {
            //Move the enemy towards the goal point
            transform.position = Vector2.MoveTowards(transform.position,patrolPos[CurrentPatrolID].position,speed*Time.deltaTime);
        }
        else
        {
            CurrentPatrolID++;
            _fish.transform.localScale = new Vector3(flip[CurrentPatrolID],1,1);
        }
        
    }
}
