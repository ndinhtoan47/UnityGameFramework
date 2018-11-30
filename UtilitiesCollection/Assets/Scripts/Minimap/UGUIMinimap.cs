using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class UGUIMinimap : MonoBehaviour
{
    private Graphic graphic;
    private GameObject mask;

    public int MaskId;
    public Color MaskColor;

    private void OnEnable()
    {
        mask = transform.GetChild(MaskId).gameObject;
        graphic = mask.GetComponentInChildren<Graphic>();
        graphic.color = MaskColor;
        mask.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        mask.gameObject.SetActive(false);
    }
}
