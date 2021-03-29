using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMNQ : MonoBehaviour
{
    public Transform playerTransform;
    public NavMeshAgent agent;
    public bool followFlag;
    public Transform idlePoints;
    int point;
    bool isMove;
    bool isTrace;
    float distance;
    float time;
    float moveTime;
    // Start is called before the first frame update
    void Start()
    {
        followFlag = true;
    }

    private void OnEnable()
    {
        point = Random.Range(0, idlePoints.childCount);
        agent.SetDestination(idlePoints.GetChild(point).transform.position);
        isMove = true;
        moveTime = Random.Range(GameManager.instance.mnqPatrolMinTime, GameManager.instance.mnqPatrolMaxTime);
        agent.speed = GameManager.instance.mnqPatrolSpeed;
    }
    IEnumerator WaitAndSetDestination(){
        yield return new WaitForSeconds(Random.Range(GameManager.instance.mnqIdleMinTime, GameManager.instance.mnqIdleMaxTime));
        int tmpPoint;
        agent.isStopped = false;
        while(true){
            tmpPoint = Random.Range(0, idlePoints.childCount);
            if(tmpPoint != point)
                break;
        }
        point = tmpPoint;
        agent.SetDestination(idlePoints.GetChild(point).transform.position);
        isMove = true;
    }

    public void SetSpeed()
    {
        agent.speed = Random.Range(GameManager.instance.TMNQMinSpeed, GameManager.instance.TMNQMaxSpeed) * GameManager.instance.slowWeight;
    }
    public void StopFollow()
    {
        agent.isStopped = true;
        followFlag = false;
    }
    // Update is called once per frame
    void Update()
    {
        // if(followFlag == true)
        //     agent.SetDestination(playerTransform.position);
        if(isMove == true){

            time += Time.deltaTime;
            if (time > moveTime)
            {

                agent.isStopped = true;
                StartCoroutine("WaitAndSetDestination");
                isMove = false;
                time = 0;
                moveTime = Random.Range(GameManager.instance.mnqPatrolMinTime, GameManager.instance.mnqPatrolMaxTime);
            }
            if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            {
                Debug.Log("sadf");
                StartCoroutine("WaitAndSetDestination");
                time = 0;
                isMove = false;
            }
        }
        distance = Vector3.Distance(this.transform.position, playerTransform.position);
        if(distance < GameManager.instance.mnqTraceRange){
            isTrace = true;
            isMove = false;
            agent.stoppingDistance = GameManager.instance.attackRange;
            agent.speed = GameManager.instance.mnqTraceSpeed;
            time = 0;
            agent.SetDestination(playerTransform.transform.position);
            Debug.Log("trace");
        }
        if(isTrace == true){
            if(distance >= GameManager.instance.mnqTraceRange){
                StartCoroutine("WaitAndSetDestination");
                agent.stoppingDistance = 0.2f;
                agent.speed = GameManager.instance.mnqPatrolSpeed;
                agent.isStopped = true;
                isTrace = false;
            }
            if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= agent.stoppingDistance+0.1f)
            {
                Debug.Log("dead");
                GameManager.instance.playerControler.enabled = false;
            }
        }
    }
}
