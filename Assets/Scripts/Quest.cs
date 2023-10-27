using System.Collections.Generic;

public class Quest
{
    private List<ItemClass> rewards = new List<ItemClass>();

    private int exp;

    private string questName;

    private string objective;
    private string objectiveFull;
    private string target;

    private int maxAmount;
    private int amount = 0;

    private bool isFinished = false;

    private string npc;

    public Quest(string questName, string objective, string objectiveFull, string target, int maxAmount, string npc, int exp)
    {
        this.questName = questName;
        this.objective = objective;
        this.objectiveFull = objectiveFull;
        this.target = target;
        this.maxAmount = maxAmount;
        this.npc = npc;
        this.exp = exp;
    }

    public string GetName()
    {
        return this.questName;
    }

    public int GetExp()
    {
        return this.exp;
    }

    public string GetObjectiveFull()
    {
        return this.objectiveFull;
    }

    public string GetProgress()
    {
        return amount+"/"+maxAmount;
    }

    public void UpdateProgress(string progress)
    {
        if(progress == target && !isFinished)
        {
            amount++;
            CheckIfFinished();
        }
    }

    public void CheckIfFinished()
    {
        if(amount == maxAmount)
        {
            isFinished = true;
        }
    }

    public bool IsFinished()
    {
        return isFinished;
    }

    public void AddReward(ItemClass reward)
    {
        rewards.Add(reward);
    }

    public List<ItemClass> GetRewards()
    {
        return rewards;
    }

}
