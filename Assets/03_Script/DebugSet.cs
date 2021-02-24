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
    public float disableControlSec;
    [Tooltip ("마네킹 대기 시간")]
    public float TMNQWaitMinTime;
    public float TMNQWaitMaxTime;
    [Tooltip("마네킹 생성 개수")]
    public int TMNQSpawnNum;
    [Tooltip("마네킹 추격 속도")]
    public float TMNQMinSpeed;
    public float TMNQMaxSpeed;
    [Tooltip("슬로우 크기")]
    [Range(0, 1)]
    public float slowScale;
    [Tooltip ("마네킹이 스폰되고 눈이 스폰되는 사이의 시간")]
    public float waitSpawnEyeTime;
    [Tooltip("눈이 플레이어를 바라보기 전 대기하는 시간")]
    public float waitLookPlayerTime;
    [Tooltip("눈 스케일 조절")]
    public float eyeScalingTime;
    public float eyeScaleSize;

}
