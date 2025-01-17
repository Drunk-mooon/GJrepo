using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BasePanel<MenuUI>
{
    public Button btnStart;
    public Slider sliMusic;

    public override void Init()
    {
        btnStart.onClick.AddListener(() => { UIManager.ChangeScene(""); });
        sliMusic.onValueChanged.AddListener((float value) => { SettingData.VolumeData.MusicVol = UIManager.PassValue(value); });
    }






}
