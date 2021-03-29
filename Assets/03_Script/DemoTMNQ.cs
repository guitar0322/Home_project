using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemoTMNQ : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public NavMeshAgent agent;
    public bool followFlag;
    public Transform idlePoints;
    int point;
    bool isMove;
    void Start()
    {
        followFlag = true;
        point = Random.Range(0, idlePoints.childCount);
        Debug.Log(point);
        agent.SetDestination(idlePoints.GetChild(point).transform.position);
        isMove = true;
    }

    IEnumerator WaitAndSetDestination(){
        yield return new WaitForSeconds(Random.Range(GameManager.instance.mnqIdleMinTime, GameManager.instance.mnqIdleMaxTime));
        int tmpPoint;
        while(true){
            tmpPoint = Random.Range(0, idlePoints.childCount);
            if(tmpPoint != point)
                break;
        }
        point = tmpPoint;
        agent.SetDestination(idlePoints.GetChild(point).transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f && isMove){
            StartCoroutine("WaitAndSetDestination");
            isMove = false;
        }
    }
}
