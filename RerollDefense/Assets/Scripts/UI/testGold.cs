using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testGold : MonoBehaviour
{
    //�׽�Ʈ��
    public Text goldText;

    D_UserData userData;

    private void Start()
    {
        userData = D_UserData.GetEntity(0);
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        // �ؽ�Ʈ UI ������Ʈ
        if (goldText != null)
        {
            goldText.text = $"Gold: {userData.f_Gold}";
        }
    }




}
