using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] Image healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void updateHealthBar(float newHealth, float maxHealth)
    {
        healthBar.fillAmount = newHealth / maxHealth;
    }
}
