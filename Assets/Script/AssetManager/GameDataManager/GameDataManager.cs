using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public static class GameDataManager
{
    static void SaveGameData()
    {
        SettingData.Write();
    }

    static void LoadGameData()
    {
        SettingData.Read();
    }


    static GameDataManager()
    {
        SettingData.Wake();
    }


    static public void Wake()
    {
    }
}

//使用方法 将相关数据的读写方法加入 SaveGameData()，LoadGameData()，并在GameDataManager() 中唤醒
