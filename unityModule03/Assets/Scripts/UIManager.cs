using UnityEngine;

[System.Serializable]
public class UIManager
{
    public HPBar hpBar;
    public EnergyUI energyElement;

    public void SetHP(float hp, float maxHP)
    {
        hpBar.SetHP(hp, maxHP);
    }

    public void SetEnergy(float energy)
    {
        energyElement.SetEnergy(energy);
    }
}
