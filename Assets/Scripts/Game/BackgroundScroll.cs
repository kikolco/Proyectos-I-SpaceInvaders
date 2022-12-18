using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{  
    [Range(-1f, 1f)]
    [SerializeField] private float scrollSpeed = -0.5f;
    private float offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.mainTextureOffset = (new Vector2(0, offset));
    }
}
