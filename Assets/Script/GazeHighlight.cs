using UnityEngine;
using UnityEngine.UI;

public class GazeHighlight : MonoBehaviour
{
    public Image panelImage;

    public Color normalColor = Color.gray;
    public Color highlightColor = Color.white;

    private void Start()
    {
        if (panelImage == null)
            panelImage = GetComponent<Image>();

        panelImage.color = normalColor;
    }

    public void SetHighlight(bool on)
    {
        if (panelImage == null) return;
        panelImage.color = on ? highlightColor : normalColor;
    }
}

