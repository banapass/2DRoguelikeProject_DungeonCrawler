using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public static ToolTip instance;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI toolTip;

    private void Awake()
    {
        instance = this;
    }
}
