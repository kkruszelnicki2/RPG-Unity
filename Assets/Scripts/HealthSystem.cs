public class HealthSystem
{
    private int maxHealth;
    private int health;

    public HealthSystem(int health)
    {
        this.maxHealth = health;
        this.health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        this.health = newHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int newHealth)
    {
        this.maxHealth = newHealth;
    }

    public void Damage(int health)
    {
        if (this.health - health <= 0)
        {
            this.health = 0;
        }
        else
        {
            this.health = this.health - health;
        }
    }

    public void Heal(int health)
    {
        if(this.health + health >= maxHealth)
        {
            this.health = maxHealth;
        }
        else
        {
            this.health = this.health + health;
        }
    }

    public void setHealth(int health)
    {
        this.maxHealth = this.maxHealth + health;
        this.health = this.health + health;
    }

    public void resetHealth(int newHealth)
    {
        this.maxHealth = newHealth;
        this.health = newHealth;
    }
}
