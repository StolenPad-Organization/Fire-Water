using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Database : MonoBehaviour
{
    public static Database Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Database>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(Database).Name;
                    instance = obj.AddComponent<Database>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static Database instance = null;
    [SerializeField] private DataList updaterData;
    private DataList data;

    public bool EditorMode = false;
    public bool ABMode = false;

    private void Awake()
    {
        #if UNITY_EDITOR
            EditorMode = true;
        #endif
        if(instance == null)
        {
            instance = this;
            data = LoadData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!EditorMode)
        {
            CheckUpdate();
        }
    }

    private DataList LoadData()
    {
        DataList data = null;
        if (!EditorMode)
        {
            if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "Database.txt"))
            {
                data = ScriptableObject.CreateInstance<DataList>();
                var json = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "Database.txt");
                JsonUtility.FromJsonOverwrite(json, data);
            }
            else
            {
                data = Resources.Load<DataList>("Data");
                var json = JsonUtility.ToJson(data, true);
                File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "Database.txt", json);
            }
        }
        else
        {
            data = Resources.Load<DataList>("Data");
        }

        return data;
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "Database.txt", json);
    }

    private void CheckUpdate()
    {
        //if(data.CharacterItemData.Count < updaterData.CharacterItemData.Count)
        //{
        //    for (int i = 0; i < data.CharacterItemData.Count; i++)
        //    {
        //        updaterData.CharacterItemData[i] = data.CharacterItemData[i];
        //    }
        //    data.CharacterItemData = updaterData.CharacterItemData;
        //}
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public LevelData GetLevelData()
    {
        return data.LevelData;
    }

    public void SetLevelData(LevelData value)
    {
        data.LevelData = value;
    }

    public int GetMoney()
    {
        return data.Money;
    }

    public void SetMoney(int value)
    {
        data.Money = value;
    }
}