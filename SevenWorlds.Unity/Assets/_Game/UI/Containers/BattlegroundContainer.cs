using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitsScale
{
    Small,
    Medium,
    Big
}

[ExecuteInEditMode]
public class BattlegroundContainer : MonoBehaviour
{
    public UnitsScale scale;
    public bool enable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        Vector2 unitSize = GetUnitSize();

        for (int i = 0; i < transform.childCount; i++) {
            GetUnitSize();
            RectTransform child = (RectTransform)transform.GetChild(i);
            child.transform.localPosition = new Vector3(-800, 0);

            child.sizeDelta = unitSize;
            //childTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, unitSize.x);
            //childTransform.Set(RectTransform.Axis.Horizontal, unitSize.y);
            //childTransform.transform.wir
        }
    }

    private Vector2 GetUnitSize()
    {
        switch (scale) {
            case UnitsScale.Small:
                return new Vector2(50, 100);
            case UnitsScale.Medium:
                return new Vector2(75, 150);
            case UnitsScale.Big:
                return new Vector2(100, 200);
        }

        return Vector2.zero;
    }
}
