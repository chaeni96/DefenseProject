using BansheeGz.BGDatabase;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static BansheeGz.BGDatabase.BGDBField;

public class SaveLoadManager : MonoBehaviour
{
    //�̹������ ������ �ִ��� üũ�ϴ� �޼��� 
    //������ ���̺� �޼��� �Ű������� path �޾ƿ���
    //������ �ε� �޼��� �Ű������� path �޾ƿ���

    public static SaveLoadManager _instance;

    public static SaveLoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveLoadManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("SaveLoadManager");
                    _instance = singleton.AddComponent<SaveLoadManager>();
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

    }

    public bool HasSavedFile
    {
        get { return File.Exists(SaveFilePath); }
    }

    public string SaveFilePath
    {
        get { return Path.Combine(Application.persistentDataPath, "localDatabase.dat"); }
    }

    public void SaveData()
    {
        byte[] bytes = BGRepo.I.Addons.Get<BGAddonSaveLoad>().Save();
        File.WriteAllBytes(SaveFilePath, bytes);
    }

    public void LoadData()
    {
        if(HasSavedFile)
        {
            var content = File.ReadAllBytes(SaveFilePath);
            BGRepo.I.Addons.Get<BGAddonSaveLoad>().Load(content);
        }
        else
        {
            Debug.Log("no save file found at " + SaveFilePath);
            SaveData();
        }
    }

}
