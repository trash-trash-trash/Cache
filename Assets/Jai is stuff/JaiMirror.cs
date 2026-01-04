using UnityEngine;
using UnityEngine.UI;

public class JaiMirror : MonoBehaviour
{
    public Camera camera;
    public RenderTexture renderTexture;
    public Texture normalTexture;
    public GameObject surface;

    void Start()
    {
        renderTexture = new RenderTexture(2048, 2048, 16);
        renderTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
        renderTexture.Create();
        camera.targetTexture = renderTexture;
        surface.GetComponent<MeshRenderer>().material = new Material(surface.GetComponent<MeshRenderer>().material);
        surface.GetComponent<MeshRenderer>().material.mainTexture = renderTexture;
        surface.GetComponent<MeshRenderer>().material.SetTexture("_BumpMap", normalTexture);
        surface.GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", 0);
        surface.GetComponent<MeshRenderer>().material.SetFloat("_BumpScale", 1);
        surface.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", new Color(1,1,1,1));
    }
}