using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public int[,] Map_saver = new int[10,23]{
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    };
    [SerializeField] private SavingData sd;

    //test
    public GameObject test;

    private int a = 0;
    private int x_p = 0;
    private int y_p = 0;

    void Start()
    {
        //sd reset
        sd = new SavingData();
        for (int i = 0; i < sd.yValue.Length; i++)
        {
            sd.yValue[i] = new YValue();
            for (int k = 0; k < sd.yValue[i].xValue.Length; k++)
            {
                sd.yValue[i].xValue[k] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Save
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheackMapSave();
            saveData_save(sd);
        }
        //Load
        if (Input.GetKeyDown(KeyCode.L))
        {
            Savedata_input();
            CheackMapSave();
            
        }
        //test 生成
        if (Input.GetKeyDown(KeyCode.M)) {
            
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 23; x++)
                {

                    x_p = x - 11;
                    y_p = 4 - y_p;
                    a = Map_saver[y, x];
                    
                    if (a == 3)
                    {
                        Instantiate(test,new Vector3(x_p,y_p,0),Quaternion.identity);
                    }
                }
            }
        }

    }

    //Map_save value print
    void CheackMapSave()
    {
        for (int y = 0;y < 10; y++){
            for (int x = 0; x < 23; x++)
            {
                if(Map_saver[y, x] != 0)
                {
                    Debug.Log(y.ToString() + ',' + x.ToString() + '＝' + Map_saver[y, x].ToString());
                    sd.yValue[y].xValue[x] = Map_saver[y, x];//save
                }
            }
        }

    }
    //
    public void InputTo(int y,int x,int value)
    {
        Map_saver[y, x] = value;
    }void Savedata_input()
    {
        sd = loadsaveData();

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 23; x++)
            {
                Map_saver[y, x] = sd.yValue[y].xValue[x];
            }
        }
    }

    //save activety
    public void saveData_save(SavingData saver)
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(saver);
        writer = new StreamWriter(Application.dataPath + "/save/savedataJO.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
    public SavingData loadsaveData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath+"/save/savedataJO.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        //return JsonUtility.FromJson<StageDate>(datastr);
        try
        {
            
            return JsonUtility.FromJson<SavingData>(datastr);

        }
        catch (ArgumentException e)
        {
            Debug.LogError("Failed to deserialize JSON: " + e.Message);
            return null;
        }
    }
}
//saveDataclass
[Serializable]
public class YValue
{
    public int[] xValue = new int[23];
}
[Serializable]
public class SavingData
{
    public YValue[] yValue = new YValue[10];
}

//other object save class
public class SaveMapping{
    public int saveCode;
    public float saveX;
    public float saveY;
    public SaveManager sM;

    public void Input()
    {
        sM.Map_saver[((int)saveY), (int)saveX] = saveCode;
    }
}
