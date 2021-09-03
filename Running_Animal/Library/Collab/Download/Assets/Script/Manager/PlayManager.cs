using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager
{
    public GameObject Player = null; // ������ �����ϴ� Object.
    public bool Playing = false; // ������ �������̿����� �ƴϾ����� üũ.  

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
            , Active.ACTIVE_CODE.None); // ������ ������ Player�� �ɷ�ġ ����.

    public DataContinue DC = new DataContinue(); // ����ؼ� �����Ǵ� ������ ��.
    public DataShot DS = new DataShot(); // �������� �Ѿ ������ �ʱ�ȭ �Ǵ� ������ ��.

    public List<T> ShuffleList<T>(List<T> list) // ���� ����Ʈ�� �����ִ� �Լ�.
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
