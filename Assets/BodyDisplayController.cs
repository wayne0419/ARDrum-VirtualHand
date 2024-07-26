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
            if (alpha < 1.0f)
            {
                mat.SetFloat("_Mode", 2); // Transparent mode
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
            else
            {
                mat.SetFloat("_Mode", 0); // Opaque mode
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
            }

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
