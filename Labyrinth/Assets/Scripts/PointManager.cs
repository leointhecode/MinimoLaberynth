using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    private int point = 0;

    public void IncreasePoint()
    {
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        ++point;
        textMeshPro.text = "" + point;
    }
}
