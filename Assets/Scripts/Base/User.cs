using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string Name;
    public long ggyul;
    public long donate;
    public long ePc;
    public List<TaeyoungGrow> taeyounglist = new List<TaeyoungGrow>();
    public bool[] isChallenge;
    public bool istuto;
}
