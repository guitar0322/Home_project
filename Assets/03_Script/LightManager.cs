using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    [Header ("LightManager")]
    public Light light;
    // Start is called before the first frame update
    public void PartialTest()
    {
        Debug.Log("partial");
    }

    public void SwapLightSetting(bool flag)
    {
        //true 일경우 밝기감소 false일 경우 복구
        //2단계 오브젝트와 상호작용할때 flag에 따라 빛 셋팅을 교체
    }

    public void DisableMainLight()
    {
        //스폿라이트만 활성화하고 메인 빛은 사라짐
    }
}
