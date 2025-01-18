using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropPanel : MonoBehaviour
{
    // References to UI components
    public Prop prop;
    public Image propImageContainer;        // Image container to hold the prop icon
    public TextMeshProUGUI propNameContainer; // Text container to display the prop name

    void Awake()
    {
        // Get references to the UI components
        propNameContainer = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (prop != null)  // Make sure prop is assigned
        {
            propImageContainer.sprite = prop.propIcon;
            propNameContainer.text = prop.propName;
        }
    }
}

