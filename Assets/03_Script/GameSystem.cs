using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//게임 전체에서 사용될 모든 상수의 집합.
namespace Gamesystem
{
    //State - 게임 글로벌 진행도와 관련된 상수 명칭
    //첫글자 첨자에 따라 단계 구분
    //O : 1단계
    //T : 2단계
    //TH : 3단계
    public static class State
    {
        public const int START = 0,
            O_MNQ = 1,
            O_DIARY = O_MNQ + 1,
            O_FISH_SCARF = O_DIARY + 1,
            O_SECOND_DIARY = O_FISH_SCARF + 1,
            O_MEDICINE = O_SECOND_DIARY + 1,//5
            O_THIRD_DIARY = O_MEDICINE + 1,
            O_SCHOOL = O_THIRD_DIARY + 1,
            O_DIARY_COMPLETE = O_SCHOOL + 1,
            O_MNQ_MOVE = O_DIARY_COMPLETE + 1,
            O_PIANO_PUZZLE = O_MNQ_MOVE + 1,//10
            O_DOOR = O_PIANO_PUZZLE + 1,
            T_CANDLE = O_DOOR + 1,
            T_DOOR = T_CANDLE + 1,
            T_SOJU = T_DOOR + 1,
            T_SPOT_FIRST = T_SOJU + 1,//15
            T_SHEET = T_SPOT_FIRST + 1,
            T_MNQ_FIRST = T_SHEET + 1,
            T_PIANO = T_MNQ_FIRST + 1,
            T_LEAVES = T_PIANO + 1,
            T_SPOT_SECOND = T_LEAVES + 1,//20
            T_FLOWER = T_SPOT_SECOND + 1,
            T_MNQ_SECOND = T_FLOWER + 1,
            T_ACCEPTANCE = T_MNQ_SECOND + 1,
            T_MNQ_THIRD = T_ACCEPTANCE + 1,
            T_LASSO = T_MNQ_THIRD + 1,//25
            T_TELEVISION = T_LASSO + 1,
            T_END_DOOR = T_TELEVISION + 1,
            TH_START_DOOR = T_END_DOOR + 1;
    }

    //Puzzle : 렌더링 할 타겟  퍼즐 UI 종류를 구분짓기 위한 상수집합
    public static class Puzzle
    {
        public const int Diary = 0, // 일기장
            Piano = 1, // 피아노 퍼즐
            Mask = 2,
            End = 3;
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

}
