using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorable : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.material.color;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetDefaultColor()
    {

    }
}
