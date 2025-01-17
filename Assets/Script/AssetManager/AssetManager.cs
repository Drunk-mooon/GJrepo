using LitJson;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public static class AssetManager
{
    public static void WriteData(object obj, string fileName) //Application.persistentDataPath 
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (false) //如果是别的储存类型，游戏截图，或者内部录音。。。
        {
           
        }
        else if (obj.GetType().IsValueType && !obj.GetType().IsPrimitive) // 如果是类或者结构体（数据）
        {
            path += ".json";
            string jsonStr = "";
            jsonStr = JsonMapper.ToJson(obj);
            System.IO.File.WriteAllText(path, jsonStr);//这个数据容器就被写入名为path的json中
        }
    }

    public static T ReadData<T>(string fileName) //Application.persistentDataPath 
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (false)
        {
            //如果是别的储存类型，游戏截图，或者内部录音。。。
        }
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum)) //如果是类或者结构体（数据）
        {
            if (System.IO.File.Exists(path + ".json"))
            {
                path = path + ".json";
                string json = System.IO.File.ReadAllText(path);
                T data = JsonMapper.ToObject<T>(json);
                return data;
            }
            else
            {
                Debug.LogError("File not found: " + path);
                return default;
            }
        }
        else
        {
            return default(T);
        }
    }

    public static T LoadData<T>(string fileName) // Application.streamingAssetsPath
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);


        if (typeof(T) == typeof(AudioClip))
        {
            if (System.IO.File.Exists(path + ".wav") || System.IO.File.Exists(path + ".mp3"))
            {
                path = System.IO.File.Exists(path + ".wav") ? path + ".wav" : path + ".mp3";

                using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.UNKNOWN))
                {
                    var operation = request.SendWebRequest();


                    while (!operation.isDone)
                    {
                    }

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed to load audio clip: {request.error}");
                        return default;
                    }


                    return (T)(object)DownloadHandlerAudioClip.GetContent(request);
                }
            }
            else
            {
                Debug.LogError("Audio file not found at: " + path);
                return default;
            }
        }
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum)) //???????
        {
            if (System.IO.File.Exists(path + ".json"))
            {
                path = path + ".json";
                string json = System.IO.File.ReadAllText(path);
                T data = JsonMapper.ToObject<T>(json);
                return data;
            }
            else
            {
                Debug.LogError("File not found: " + path);
                return default;
            }
        }

        return default;
    }

    public static T LoadRes<T>(string fileName) //  Resources
    {
        if (typeof(T) == typeof(AudioClip)) //
        {
            object audioClip = Resources.Load<AudioClip>(fileName);
            return (T)audioClip;
        }
        else if (typeof(T) == typeof(GameObject)) //
        {
            object obj = Resources.Load<GameObject>(fileName);
            return (T)obj;
        }

        //???????????
        return default;
    }

    static AssetManager()
    {
        SoundManager.Wake();
        ObjManager.Wake();
        MusicManager.Wake();
      //  GameDataManager.Wake();
    }

    public static void Wake()
    {
    }
    
    
}
//基本都封装好了 不会直接用的   WriteData ReadData对于持久化数据的读写  LoadData对于大文件的读取 LoadRes对于内部文件的读取

//Application.persistentDataPath            可读写但不被打包
//Application.streamingAssetsPath           可读 独立于打包
//Resources                                 可读 被打包进程序