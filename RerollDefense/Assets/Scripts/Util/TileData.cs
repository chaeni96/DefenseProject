using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData 
{
    //Ÿ�Ͽ� ���� �����ϱ� ���� ������ Ŭ����

    //�����Ϳ� �����Ѱ� �ϴ� ��ġ���θ�
    public bool isAvailable { get; set; }

    public TileData(bool isAvailable = true)
    {
        this.isAvailable = isAvailable;
    }
}
