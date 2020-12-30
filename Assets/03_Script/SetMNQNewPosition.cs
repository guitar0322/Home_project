using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMNQNewPosition : MonoBehaviour
{
    public Transform target;
    public GameObject mNQ_D;
    bool isMNQHrer = false;
    public GameObject mNQ_D_Prefabs;

    public Transform[] randomSet_MNQ_Positions;
    public Transform[] round_MNQ_Position;

    public int i;
    
    public void Instantiate_MNQ()
    {        
        for (i = 0; i <randomSet_MNQ_Positions.Length; i++)
        {
            randomSet_MNQ_Positions[i].GetComponent<Transform>();            
        }
        
        if (!isMNQHrer)
        {
            int randomTime = Random.Range(1, 5);

            mNQ_D_Prefabs = Instantiate(mNQ_D,
            randomSet_MNQ_Positions[Random.Range(0, randomSet_MNQ_Positions.Length)].position,
            Quaternion.identity);

            //Debug.Log(randomSet_MNQ_Positions[Random.Range(0, randomSet_MNQ_Positions.Length)].name);
            //isMNQHrer = true;
        }
        else
        {
            
        }
        //mNQ_D.transform.position = setMNQNewPosition01.transform.position;
    }
    
    public void MobileAfterInteractive()
    {
        //Debug.Log("MObileeeee");                    
        float randRotation = Random.Range(0, 360);

        for (i = 0; i < round_MNQ_Position.Length; i++)
        {
            mNQ_D_Prefabs =
                Instantiate(mNQ_D, round_MNQ_Position[i].position, Quaternion.Euler(0, i * randRotation, 0));
        }
        
    }
}