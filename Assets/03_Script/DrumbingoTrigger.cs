using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumbingoTrigger : MonoBehaviour
{
    public DrumbingoPuzzle puzzle;
    private void OnTriggerEnter(Collider other)
    {
        puzzle.numOfPill--;
        if(puzzle.numOfPill == 0)
        {
            Debug.Log("Clear");
            puzzle.Clear();
        }
    }
}
