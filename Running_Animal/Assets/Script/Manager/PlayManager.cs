using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager
{
    public GameObject Player = null; // 게임을 진행하는 Object.
    public bool Playing = false; // 게임이 진행중이였는지 아니었는지 체크.  

    public Character Status = new Character(0, 0, new Ability(
            new Status<float>(0 ,0, 100),
            new Status<float>(0 ,0, 100),
            new Status<float>(0, 0, 8),
            new Status<int>(0, 0, 2),
            new Status<float>(0, 0, 10),
            new Status<float>(0, 0, 20),
            new Status<float>(0, 0, 0),
            new Status<short>(0, 0, 5),
            new Status<float>(0, 0, 0))
            , Active.ACTIVE_CODE.None); // 게임을 진행할 Player의 능력치 저장.

    public DataContinue DC = new DataContinue(); // 계속해서 누적되는 데이터 셋.
    public DataShot DS = new DataShot(); // 스테이지 넘어갈 때마다 초기화 되는 데이터 셋.

    public List<T> ShuffleList<T>(List<T> list) // 함정 리스트를 섞어주는 함수.
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
}
