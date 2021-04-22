using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    [Header("Level1 Property")]
    public Transform diaryDropPosition;
    public Vector3 candleEquipPostion;
    public Vector3 mnqEquipPostion;

    public Transform mnqSnapTransform;

    [Header("Level2 Property")]
    [Tooltip("플레이어와 마네킹스폰 최소거리")]
    public float mnqSpawnMinRange;
    [Tooltip("마네킹 추적 범위")]
    public float mnqTraceRange;
    [Tooltip("마네킹 이동 후 대기시간")]
    public float mnqIdleMinTime;
    public float mnqIdleMaxTime;
    [Tooltip("마네킹 이동시간")]
    public float mnqPatrolMinTime;
    public float mnqPatrolMaxTime;
    [Tooltip("마네킹 이동속도")]
    public float mnqPatrolSpeed;
    public float mnqTraceSpeed;
    [Tooltip("마네킹 공격범위")]
    public float attackRange;
    [Tooltip("마네킹 생성시간")]
    public float mnqSpawnMinTime;
    public float mnqSpawnMaxTime;
    [Tooltip("마네킹 파괴시간")]
    public float mnqDestroyMinTime;
    public float mnqDestroyMaxTime;
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
    [Tooltip("전자제품 생성 대기시간")]
    public float waitSpawnElectronicTime;
    [Tooltip("거실문 열리는 대기시간")]
    public float waitOpenRightDoor;

    [Header("Level3 Property")]
    [Tooltip("회전통 회전속도")]
    public float caseBoxSpeed;
    [Tooltip("사진으로 줌인시, 카메라 위치")]
    public Transform zoomCamPos;
    public float zoomTime;
    [Tooltip("캐릭터 이동 위치")]
    public Transform playerTargetPos;
    [Tooltip("마네킹 소환 대기시간")]
    public float waitSpawnTHMNQMinTime;
    public float waitSpawnTHMNQMaxTime;
    [Tooltip("마네킹 소환 수")]
    public int spawnTHMNQMinNum;
    public int spawnTHMNQMaxNum;
    [Tooltip("촬영해야할 마네킹 수")]
    public int interactMNQNum;

}
