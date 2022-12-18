using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarController : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    [SerializeField] private Vector3 Offset;
    
    public void SetHealthBar(float health, float maxHealth)
    {
        healthBar.value = health;
        healthBar.maxValue = maxHealth;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }
}
