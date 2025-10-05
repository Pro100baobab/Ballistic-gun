using UnityEngine;
using UnityEngine.Rendering;

public class ForceVisulizers : MonoBehaviour
{
    [SerializeField] private System.Collections.Generic.List<Force> _forceList = new();

    public void OnDrawGizmos()
    {
    
        float cubeSize = 0.1f;

        foreach (var force in _forceList)
        {
            Gizmos.color = force._color;

            Vector3 startPoint = transform.position;
            Vector3 EndPoint = startPoint + force._vector;
            Gizmos.DrawLine(startPoint, EndPoint);
            Gizmos.DrawCube(EndPoint, Vector3.one * cubeSize);

            #if UNITY_EDITOR
            UnityEditor.Handles.Label(EndPoint, force._name);
            #endif
        }
    
    }

    public void AddForce(Vector3 force, Color color, string name)
    {
        Force newForce = new Force(force, color, name);
        _forceList.Add(newForce);
    }

    public void ForceClear()
    {
        _forceList.Clear();
    }
}


[System.Serializable]
public class Force
{
    public Vector3 _vector;
    public Color _color;
    public string _name;

    public Force(Vector3 vector, Color color, string name)
    {
        _vector = vector;
        _color = color;
        _name = name;
    }

}