using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{

    public List<StatBonus> BaseAdditivies { get; set; }

    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        this.BaseAdditivies = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditivies.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditivies.Remove(BaseAdditivies.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetCalculatedStatValue()
    {
        this.FinalValue = 0;
        this.BaseAdditivies.ForEach(x => this.FinalValue += x.BonusValue);
        this.FinalValue += BaseValue;
        return FinalValue;
    }

}
