using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorable : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetAlpha(float current, float max)
    {
        float valueAlpha = Mathf.Clamp01(current / max);

        Color currentColor = _renderer.material.color;

        currentColor.a = valueAlpha;

        _renderer.material.color = currentColor;
    }
}
