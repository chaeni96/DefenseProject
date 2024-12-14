using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ObjectDragHandler : MonoBehaviour
{
    private Vector3Int previousTilePosition; // ���� Ÿ�� ��ġ ����
    private SpriteRenderer spriteRenderer; // �巡�� ���� ������Ʈ�� SpriteRenderer

    private Vector3 originalPos;
    private Color originColor;

    private int testGold;

    D_UserData userData;

    public testGold test;

 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousTilePosition = Vector3Int.zero; // �ʱ�ȭ

        originalPos = transform.position;
        originColor = spriteRenderer.color;

    }

    void Start()
    {
        userData = D_UserData.GetEntity(0);

        if(userData != null )
        {
            testGold = userData.f_Gold;
            Debug.Log($"���� ��� : {testGold}");
        }

    }

    void OnMouseDown()
    {
        TileManager.Instance.ResetTileColors(new Color(1, 1, 1, 0.1f));
    }
    void OnMouseDrag()
    {
        // ���콺 ��ġ�� Ÿ�ϸ��� �� ��ǥ�� ��ȯ
        Vector3 mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; //2D�� Z�� �����ؾߵ�
        Vector3Int tilePosition = TileManager.Instance.tileMap.WorldToCell(mousePosition);

        Color newColor = spriteRenderer.color;
        newColor.a = 0.3f;
        spriteRenderer.color = newColor;    

        // ���� Ÿ�ϰ� ���� Ÿ���� �ٸ� ��� ���� ����
        if (tilePosition != previousTilePosition)
        {
            // ���� Ÿ�� ���� �ʱ�ȭ
            TileManager.Instance.ResetTileColors(new Color(1, 1, 1, 0.1f));
            // ���� Ÿ�� ���� ����
            
            if (TileManager.Instance.IsTileAvailable(tilePosition))
            {
                TileManager.Instance.SetTileColor(tilePosition, new Color(0, 1, 0, 0.5f)); // �ʷϻ� ������
            }
            else
            {
                TileManager.Instance.SetTileColor(tilePosition, new Color(1, 0, 0, 0.5f)); // ������ ������
            }

            // ���� Ÿ�� ��ġ�� ���� Ÿ�Ϸ� ������Ʈ
            previousTilePosition = tilePosition;
        }

        // �巡�� ���� ������Ʈ�� ��ġ ����
        transform.position = TileManager.Instance.tileMap.GetCellCenterWorld(tilePosition);
    }

    void OnMouseUp()
    {
        // �巡�� ���� �� Ÿ�� �� �ʱ�ȭ
        TileManager.Instance.ResetTileColors(new Color(1, 1, 1, 0));

        // ��ġ ���� ���ο� ���� Ÿ�� ��ġ �Ǵ� ���� ��ġ�� �ǵ���
        if (TileManager.Instance.IsTileAvailable(previousTilePosition))
        {
            // ��ġ ����: Ÿ���� Ÿ�� �߽ɿ� �����ϰ� Ÿ���� ��ġ �Ұ������� ����
            transform.position = TileManager.Instance.tileMap.GetCellCenterWorld(previousTilePosition);
            TileManager.Instance.SetTileUnavailable(previousTilePosition);
            DecreaseGold(10);
            test.UpdateGoldText();
            Debug.Log("Ÿ�� ��ġ �Ϸ�!");
        }
        else
        {
            Debug.Log("��ġ �Ұ����� ��ġ�Դϴ�.");

            transform.position = originalPos;
            spriteRenderer.color = originColor;
            //���� ���������� ���ƿ;���
        }
    }


    //test�� ��� ����

    public void DecreaseGold(int amount)
    {
        if(testGold >= amount)
        {
            testGold -= amount;

            userData.f_Gold = testGold;

            //������ ����
            SaveLoadManager.Instance.SaveData();

        }
    }

}
