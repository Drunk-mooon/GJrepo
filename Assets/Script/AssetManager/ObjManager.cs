using UnityEngine;

public static class ObjManager 
{
    public static void AddObj(string fileName, GameObject father, Transform pos) //有父物体 但是不和父物体重合
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, GameObject father) //有父物体 和父物体重合
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), father.transform);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, Transform pos) //无父物体直接存在
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
    }

    public static void Wake()
    {
    }
}

//将做好的预设体置于resource文件夹 直接文件名读取 