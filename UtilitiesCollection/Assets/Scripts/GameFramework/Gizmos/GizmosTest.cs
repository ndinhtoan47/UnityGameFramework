using GameFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosTest : MonoBehaviour
{
    public bool IsClosedLine = true;
    public List<Vector3> linePoints = new List<Vector3>();
    
    void Start()
    {
        var lineDrawer = new GmLineDrawer();
        lineDrawer.IsClosed = IsClosedLine;
        lineDrawer.Points.AddRange(linePoints);
        
        GizmosUtils.Instance.AddDrawer(lineDrawer);
    }

    
}
