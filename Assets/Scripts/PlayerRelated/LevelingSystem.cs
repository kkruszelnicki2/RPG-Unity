using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelingSystem
{
    public int currentExp = 0;

    public int[] maxExp = { 50, 100, 200, 350, 550, 800, 1100, 1450, 1850, 2300 , 2800};

    public int level  = 0;
    public bool GrantExp(int Exp)
    {
        this.currentExp = this.currentExp + Exp;
        if(this.currentExp >= maxExp[level])
        {
            LevelUp();
            return true;
        }
        return false;
    }

    public int GetMaxExp()
    {
        return this.maxExp[level];
    }

    public void LoadLevel(int level)
    {
        this.level = level;
    }

    private void LevelUp()
    {
        this.currentExp = this.currentExp - maxExp[level];
        this.level++;
    }

    public void ResetLevel()
    {
        level = 0;
        currentExp = 0;
    }
}
