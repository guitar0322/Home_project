using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEye : MonoBehaviour
{
    public Transform playerTr;
    private float scalingTime;
    private float scalesize;
    private float scalePerFrame;
    private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = this.transform.localScale;
        scalingTime = GameManager.instance.eyeScalingTime;
        scalesize = GameManager.instance.eyeScaleSize;
        scalePerFrame = Time.deltaTime * (scalesize - scale.x) / scalingTime;
        StartCoroutine("LookAtPlayer", scalingTime + GameManager.instance.waitLookPlayerTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale.x <= scalesize)
            this.transform.localScale += new Vector3(scalePerFrame, scalePerFrame, scalePerFrame);
    }

    IEnumerator LookAtPlayer(int time)
    {
        yield return new WaitForSeconds(time);
        this.transform.LookAt(playerTr);
    }
}
