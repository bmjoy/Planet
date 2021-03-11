using UnityEngine;
public class ComposeData
{
    private Vector3 position;
    private Quaternion rotation;
    private int type;
    public Vector3 Position {
        get {
            return position;
        }
        set {
            position = value;
        }
    }
    public Quaternion Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            rotation = value;
        }
    }
    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
}
