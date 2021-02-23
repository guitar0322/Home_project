using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    [Header("Level1 Property")]
    public Transform diaryDropPosition;
    public Vector3 candleEquipPostion;
    public Vector3 mnqEquipPostion;

    [Header("Level2 Property")]
    [Tooltip ("유저가 컨트롤 불가능해지는 시간")]
    public int disableControlSec;
    [Tooltip ("마네킹 대기 시간")]
    public int TMNQWaitMinTime;
    public int TMNQWaitMaxTime;
    [Tooltip("마네킹 생성 개수")]
    public int TMNQSpawnNum;
    [Tooltip("마네킹 추격 속도")]
    public int TMNQMinSpeed;
    public int TMNQMaxSpeed;
    [Tooltip("슬로우 크기")]
    [Range(0, 1)]
    public float slowScale;

}
