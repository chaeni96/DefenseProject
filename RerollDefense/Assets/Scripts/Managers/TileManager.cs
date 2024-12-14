using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    public static TileManager _instance;

    public Tilemap tileMap;
    private Dictionary<Vector3Int, TileData> tileDataMap = new Dictionary<Vector3Int, TileData>();

    public static TileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TileManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("TileManager");
                    _instance = singleton.AddComponent<TileManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        setTileMap();
    }

    //������ Ÿ�ϸ� �ʱ⼼��
    public void setTileMap()
    {
        //Ÿ�� ������ �ʱ�ȭ, ��� Ÿ�� ��ġ���� ���·� ����

        foreach(var position in tileMap.cellBounds.allPositionsWithin) //Ÿ�ϸ��� ���� �� ��� �� ��ǥ
        {
            //�ش� ��ǥ�� Ÿ���� ���� �����ϸ� ��ġ���� ���·� ����
            if(tileMap.HasTile(position))
            {
                tileDataMap[position] = new TileData();
            }

        }

        ResetTileColors(new Color(1,1,1,0));

    }

    //Ư�� Ÿ���� ������ ���������
    public TileData GetTileData(Vector3Int position)
    {
        return tileDataMap.ContainsKey(position) ? tileDataMap[position] : null;
    }

    //Ư�� Ÿ�� ��ġ �������� Ȯ��

    public bool IsTileAvailable(Vector3Int postion)
    {
        TileData tileData = GetTileData(postion);


        if(tileData != null && tileData.isAvailable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Ư�� Ÿ�ϻ� ����
    public void SetTileColor(Vector3Int position, Color color)
    {
        if(tileMap.HasTile(position))
        {
            tileMap.SetTileFlags(position, TileFlags.None);

            tileMap.SetColor(position, color);
        }
    }


    //��� Ÿ�ϻ� �ʱ�ȭ
    public void ResetTileColors(Color color)
    {
        foreach(var position in tileMap.cellBounds.allPositionsWithin)
        {
            if(tileMap.HasTile(position))
            {
                tileMap.SetTileFlags(position, TileFlags.None);
                tileMap.SetColor(position, color); //�������� �ٲٱ�
            }
        }
    }

    //Ư�� Ÿ�� ��ġ �Ұ��� ���·� ����
    public void SetTileUnavailable(Vector3Int position)
    {
        if(tileDataMap.ContainsKey(position))
        {
            tileDataMap[position].isAvailable = false;
        }
    }

}
