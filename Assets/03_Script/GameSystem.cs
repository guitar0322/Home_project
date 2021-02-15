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
            O_DIARY = 2,
            O_FISH_SCARF = 3,
            O_SECOND_DIARY = 4,
            O_MEDICINE = 5,
            O_THIRD_DIARY = 6,
            O_SCHOOL = 7,
            O_DIARY_COMPLETE = 8,
            O_MNQ_MOVE = 9,
            O_PIANO_PUZZLE = 10,
            T_CANDLE = 11,
            T_SOJU = 12,
            T_SPOT_FIRST = 13,
            T_SHEET = 14,
            T_MNQ_FIRST = 15,
            T_PIANO = 16,
            T_LEAVES = 17,
            T_SPOT_SECOND = 18,
            T_FLOWER = 19,
            T_MNQ_SECOND = 20,
            T_ACCEPTANCE = 21,
            T_MNQ_THIRD = 22;
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
