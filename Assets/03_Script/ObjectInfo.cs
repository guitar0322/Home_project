using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [Tooltip ("뷰 모드가 실행되는 오브젝트시 체크")]
    public bool viewModeFlag;
    [Tooltip ("다이어리에서 변하는 페이지가 1개의 쌍중에서 2번쨰에 해당하면 체크")]
    public bool diarySecondFlag;
    public int gameStateCondition;
    [Tooltip ("생성되어야 할 위치 Transform을 할당")]
    public Transform[] spawnTransform;

}
