using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct VolumeData //某一组数据（结构体） 
{
    public float MusicVol;
    public float SoundVol;

    public VolumeData(float musicVol, float soundVol)
    {
        MusicVol = musicVol;
        SoundVol = soundVol;
    }
}


#region 另一个例子
public struct ScreenData
{
    public float Brightness;
    public float Contrast;
    public float Saturation;
    public float Gamma;
    public ScreenData(float brightness, float contrast, float saturation, float gamma)
    {
        this.Brightness = brightness;
        this.Contrast = contrast;
        this.Saturation = saturation;
        this.Gamma = gamma;
    }
}



#endregion


public static class SettingData //某一类数据
{
    public static VolumeData VolumeData;
    public static  ScreenData ScreenData;
    static SettingData()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "VolumeData.json")))//因为数据组是一起的 所以VolumeData 不在 ScreenData 也肯定不在
        {
            VolumeData = new VolumeData(1, 1);
        //    ScreenData = new ScreenData(1, 1, 1, 1);//只是个例子
            AssetManager.WriteData(VolumeData, "VolumeData");
      //      AssetManager.WriteData(ScreenData, "ScreenData");
        }
        else Read();
    }

    static public void Read()
    {
        VolumeData = AssetManager.ReadData<VolumeData>("VolumeData");
      //  ScreenData   = AssetManager.ReadData<ScreenData>("ScreenData");//只是个例子
    }

    static public void Write()
    {
        AssetManager.WriteData(VolumeData, "VolumeData");
     //   AssetManager.WriteData(ScreenData, "ScreenData");//只是个例子
    }


    public static void Wake()
    {
    }
}

//用法 申明数据组 加入数据类 并加入读写函数 