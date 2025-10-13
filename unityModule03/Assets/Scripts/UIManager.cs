using UnityEngine;

[System.Serializable]
public class UIManager
{
    public HPBar hpBar;

    public void SetHP(float hp, float maxHP)
    {
        hpBar.SetHP(hp, maxHP);
    }
}
