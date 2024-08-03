using System.Collections.Generic;
using UnityEngine;

public class BodyDisplayController : MonoBehaviour
{
    [System.Serializable]
    public class BodyGroup
    {
        public string groupName;
        public List<Renderer> renderers;
        public Color color = Color.white;
    }

    public List<BodyGroup> bodyGroups = new List<BodyGroup>();

    public void SetColor(string groupName, Color color)
    {
        BodyGroup bodyGroup = bodyGroups.Find(group => group.groupName == groupName);
        if (bodyGroup != null)
        {
            foreach (Renderer renderer in bodyGroup.renderers)
            {
                SetRendererColor(renderer, color);
            }
        }
        else
        {
            Debug.LogWarning("Body group not found: " + groupName);
        }
    }

    private void SetRendererColor(Renderer renderer, Color color)
    {
        foreach (Material mat in renderer.sharedMaterials)
        {
            if (color.a < 1.0f)
            {
                mat.SetFloat("_Surface", 1); // Transparent mode
                mat.SetOverrideTag("RenderType", "Transparent");
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
            else
            {
                mat.SetFloat("_Surface", 0); // Opaque mode
                mat.SetOverrideTag("RenderType", "Opaque");
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
            }

            mat.color = color;
        }
    }

    public void UpdateColor(BodyGroup bodyGroup)
    {
        foreach (Renderer renderer in bodyGroup.renderers)
        {
            SetRendererColor(renderer, bodyGroup.color);
        }
    }
}
