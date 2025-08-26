
public class Character
{
    public string ID { get; private set; }
    public int Level {  get; private set; }
    public float Exp { get; private set; }

    public float maxExp => Level + 2;
    public string Description { get; private set; }
    public int Atk {  get; private set; }
    public int Def {  get; private set; }
    public int Hp { get; private set; }
    public int Cri {  get; private set; }
    public int Gold {  get; private set; }


    public Character(string id, int level, float exp, string description, int atk, int def, int hp, int cri, int gold)
    {
        ID = id;
        Level = level;
        Exp = exp;
        Description = description;
        Atk = atk;
        Def = def;
        Hp = hp;
        Cri = cri;
        Gold = gold;
    }

    public void addExp(int amount)
    {
        Exp += amount;
        if(Exp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Exp -= maxExp;
        Level++;
        AddAtk(Level / 5);
        AddDef(Level / 8);
        AddHP(Level / 10);
        AddCri(Level / 20);
    }

    public void AddAtk(int extraAtk)
    {
        Atk += extraAtk;
    }

    public void AddDef(int extraDef)
    {
        Def += extraDef;
    }
    public void AddHP(int extraHp)
    {
        Hp += extraHp;
    }
    public void AddCri(int extraCri)
    {
        Cri += extraCri;
    }
    public void AddGold(int gold)
    {
        Gold += gold;
    }

}
