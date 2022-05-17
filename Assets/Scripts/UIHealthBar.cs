using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;
    public TextMeshProUGUI livesText;

    private int maxHealth = 10;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        livesText.text = maxHealth + " / " + maxHealth;
    }

    public void SetValue(int currentHealth, int maxHealth)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (currentHealth / (float)maxHealth));
        livesText.text = currentHealth +  " / " + maxHealth;
    }
}
