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
            O_MEDICINE = 4,
            O_SCHOOL = 5,
            O_MNQ_MOVE = 6,
            O_PIANO_PUZZLE = 7;
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
