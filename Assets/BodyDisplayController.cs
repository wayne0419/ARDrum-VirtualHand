using System.Collections.Generic;
using UnityEngine;

public class BodyDisplayController : MonoBehaviour
{
    [System.Serializable]
    public class BodyGroup
    {
        public string groupName;
        public List<Renderer> renderers;
        [Range(0, 1)] public float transparency = 1.0f;
    }

    public List<BodyGroup> bodyGroups = new List<BodyGroup>();

    public void SetTransparency(string groupName, float alpha)
    {
        BodyGroup bodyGroup = bodyGroups.Find(group => group.groupName == groupName);
        if (bodyGroup != null)
        {
            foreach (Renderer renderer in bodyGroup.renderers)
            {
                SetRendererTransparency(renderer, alpha);
            }
        }
        else
        {
            Debug.LogWarning("Body group not found: " + groupName);
        }
    }

    private void SetRendererTransparency(Renderer renderer, float alpha)
    {
        foreach (Material mat in renderer.materials)
        {
            Color color = mat.color;
            color.a = alpha;
            mat.color = color;
        }
    }

    public void UpdateTransparency(BodyGroup bodyGroup)
    {
        foreach (Renderer renderer in bodyGroup.renderers)
        {
            SetRendererTransparency(renderer, bodyGroup.transparency);
        }
    }
}
