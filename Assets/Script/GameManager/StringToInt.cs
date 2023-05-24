using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StringToInt
{
    ConditionFunctions conditionFunctions = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ConditionFunctions>();
    
    public static int TypeStringToInt(string typeString, string type)
    {

        switch (type)
        {
            #region TarGetType
            case "TargetType":
                {
                    if (typeString == "target")
                        return (int)TargetType.target;
                    else if (typeString == "notarget")
                        return (int)TargetType.notarget;
                    else return 1;
                }
            #endregion

            #region DamageType
            case "MagicDamageType":
                {
                    if (typeString == "none")
                        return (int)MagicDamageType.none;
                    else if (typeString == "physics")
                        return (int)MagicDamageType.physics;
                    else if (typeString == "fire")
                        return (int)MagicDamageType.fire;
                    else return 0;
                }
            #endregion

            #region EcoleType
            case "Ecole":
                {
                    if (typeString == "conjuration")
                        return (int)Ecole.conjuration;
                    else return 0;
                }
            #endregion


            #region MagicType
            case "MagicType":
                {
                    if (typeString == "att")
                        return (int)MagicType.attack;
                    else if (typeString == "buff")
                        return (int)MagicType.buff;
                    else
                        return 0;
                }
            #endregion
            #region EquipType
            case "EquipType":
                {
                    if (typeString == "None")
                        return (int)EquipType.None;
                    else if (typeString == "Weapon")
                        return (int)EquipType.Weapon;
                    else if (typeString == "Armor")
                        return (int)EquipType.Armor;
                    else if (typeString == "Glove")
                        return (int)EquipType.Glove;
                    else if (typeString == "Shield")
                        return (int)EquipType.Shield;
                    else if (typeString == "Helm")
                        return (int)EquipType.Helm;
                    else if (typeString == "Shoes")
                        return (int)EquipType.Shoes;
                    else if (typeString == "Ring")
                        return (int)EquipType.Ring;
                    else if (typeString == "Amulet")
                        return (int)EquipType.Amulet;
                    else
                        return 0;
                }
            #endregion
            #region Rairty
            case "Rarity":
                {
                    if (typeString == "Normal")
                        return (int)Rarity.Normal;
                    else if (typeString == "Enchanted")
                        return (int)Rarity.Enchanted;
                    else if (typeString == "RandomArtifact")
                        return (int)Rarity.RandomArtifact;
                    else if (typeString == "Artifact")
                        return (int)Rarity.Artifact;
                    else return 0;
                }
            #endregion
            #region WeaponType
            case "WeaponType":
                {
                    if (typeString == "NotWeaPon"||typeString==null)
                        return (int)WeaponType.NotWeaPon;
                    else if (typeString == "ShortSword")
                        return (int)WeaponType.ShortSword;
                    else if (typeString == "LongSword")
                        return (int)WeaponType.LongSword;
                    else if (typeString == "Axe")
                        return (int)WeaponType.Axe;
                    else if (typeString == "Hammer")
                        return (int)WeaponType.Hammer;
                    else
                        return 0;
                }
            #endregion
            #region ArmorType
            case "ArmorType":
                {
                    if (typeString == "NotArmor"||typeString==null)
                        return (int)ArmorType.NotArmor;
                    else if (typeString == "Cloth")
                        return (int)ArmorType.Cloth;
                    else if (typeString == "LightArmor")
                        return (int)ArmorType.LightArmor;
                    else if (typeString == "HeavyArmor")
                        return (int)ArmorType.HeavyArmor;
                    else if(typeString== "Shield")
                        return (int)ArmorType.Shield;
                    else
                        return 0;
                }
            #endregion
            #region ItempType
            case "ItemType":
                {
                    if (typeString == "Consumable")
                        return (int)ItemType.Consumable;
                    else if (typeString == "Equipment")
                        return (int)ItemType.Equipment;
                    else if (typeString == "None")
                        return (int)ItemType.None;
                    else
                        return (int)ItemType.None;
                }
            #endregion
            #region
            case "Slot":
                {
                    if (typeString == "OneHand")
                    {
                        return 1;//2^0
                    }
                    else if(typeString == "TwoHand")
                    {
                        return 2;//2^1
                    }
                    else if(typeString == "Armor")
                    {
                        return 4;//2^2
                    }
                    else if(typeString == "Glove")
                    {
                        return 8;//2^3
                    }
                    else if(typeString == "Helm")
                    {
                        return 16;//2^4
                    }
                    else if(typeString == "Shoes")
                    {
                        return 32;//2^5
                    }
                    else if(typeString == "Ring")
                    {

                        return 64;//2^6
                    }
                    else if(typeString == "Amulet")
                    {
                        return 256;//2^7
                    }
                    return 0;
                }
            case "ConsumKind":
                {
                    if (typeString == "Potion")
                        return (int)ConsumKind.Potion;
                    else if (typeString == "Scroll")
                        return (int)ConsumKind.Scroll;
                    else if (typeString == "Book")
                        return (int)ConsumKind.Book;
                    else if (typeString == "Evoke")
                        return (int)ConsumKind.Evoke;
                    else
                        return 0;
                }
            case "ConditionType":
                {
                    if (typeString == "Update")
                        return (int)ConditionType.update;
                    else if (typeString == "Start")
                        return (int)ConditionType.start;
                    else if (typeString == "End")
                        return (int)ConditionType.end;
                    else
                        return(int)ConditionType.none;
                }
            case "DamageType":
                {
                    if (typeString == "None")
                        return (int)DamageType.None;
                    else if (typeString == "Poison")
                        return (int)DamageType.Poison;
                    else if (typeString == "Fire")
                        return (int)DamageType.Fire;
                    else if (typeString == "Ice")
                        return (int)DamageType.Ice;
                    else if (typeString == "Magic")
                        return (int)DamageType.Magic;
                    else if (typeString == "Fure")
                        return (int)DamageType.Fure;
                    else
                    {
                        return 0;
                    }
                }
            #endregion
            #region Error
            #endregion

            default:
                return 0;
        }
    }
}
