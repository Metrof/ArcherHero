
using UnityEngine;

public static class DamageHandler
{
    
    private static readonly float[,] vulnerabilityMultipliers =
    {
        // Уязвимость         Physical   Fire   Ice    Electric
        /* Default */        {  1.0f,    1.3f,  1.3f,   1.3f },
        /* FireArmor */      {  1.0f,    0.5f,  3.0f,   1.0f },
        /* IceArmor */       {  1.0f,    3.0f,  0.5f,   1.0f },
        /* RoboArmor */      {  1.0f,    1.0f,  1.0f,   3.0f }
    };

    public static int CalculateDamage(int damageAmount, DamageType damageType, ArmorTypeExp armorType)
    {
        int damageTypeIndex = (int)damageType;
        int armorTypeIndex = (int)armorType;

        
        float vulnerabilityMultiplier = vulnerabilityMultipliers[armorTypeIndex, damageTypeIndex];
        
        damageAmount = Mathf.RoundToInt(damageAmount * vulnerabilityMultiplier);

        return damageAmount;
    }
}
