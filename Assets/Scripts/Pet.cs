using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Pet : MonoBehaviour
    {
        public int ID;                          //寵物代號(唯一)
        public string Kind;                        //寵物種類
        public string Name;
        public int Level = 1;
        public float Mood = 90;
        public float Hunger = 90;

        public const float HappyMood = 80;

        public string PetSpriteName;            //寵物圖片
        public string PetAnimatorName;          //寵物動畫名稱
        public string PetTalkingFilename;       //寵物說話檔案位置
        public string[] PetTalkingContents;      //寵物說話內容
        public string[] PetTalkingContents_normal;
        public string[] PetTalkingContents_hungry;
        public string[] PetTalkingContents_happy;

        public float Speed = 1;
        public float MaxHP = 100;
        public float HPRecover = 0.005f;
        public float MaxMP = 100;
        public float MPRecover = 0.005f;
        public float PetfireAttack = 100;
        public float PetwaterAttack = 100;
        public float PetwindAttack = 100;
        public float PetAttack = 100;
        public float PetDefence = 0;
        public float ExpPerLevel = 1000;
        public float maxExp;
        public float Exp;

        
        public const float UpgradeBase = 1;
        public const float UpgradeMin = 0;
        public const float UpgradeMax = 2;
        public const float UpgradeMaxHPPerPoint = 10;
        public const float UpgradeHPRecoverPerPoint = 0.005f;
        public const float UpgradeMaxMPPerPoint = 10;
        public const float UpgradeMPRecoverPerPoint = 0.005f;
        public const float UpgradeAttackPerPoint = 1;
        public const float UpgradeDefencePerPoint = 1;
        public float UpgradeMaxHP = 1;
        public float UpgradeHPRecover = 1;
        public float UpgradeMaxMP = 1;
        public float UpgradeMPRecover = 1;
        public float UpgradeAttack = 1;
        public float UpgradeDefence = 1;

        public int Points = 0;


        /*  */
        public Pet()
        {

        }



        /* Load a pet */
        public void LoadPet(int _id)
        {
            ID = _id;

            LoadData();
        }



        public void NewPet(int _id, string _kind, string _name)
        {
            ID = _id;
            Kind = _kind;
            Name = _name;
            PetSpriteName = Kind.ToString() + "/Idle/idle_1";
            PetAnimatorName = Kind.ToString() + "/" + Kind.ToString();
            PetTalkingFilename = Kind.ToString() + "/talks";

            Debug.Log(ID);
            PlayerData.instance.AddPet(ID);
            SaveData();
        }


        public void EatFood(Food _food)
        {
            _food.AddStatus(ref UpgradeMaxHP, _food.EffectMaxHP, "Max HP");
            _food.AddStatus(ref UpgradeHPRecover, _food.EffectHPRecover, "HP Recover");
            _food.AddStatus(ref UpgradeMaxMP, _food.EffectMaxMP, "Max MP");
            _food.AddStatus(ref UpgradeMPRecover, _food.EffectMPRecover, "MP Recover");
            _food.AddStatus(ref UpgradeAttack, _food.EffectAttack, "Attack");
            _food.AddStatus(ref UpgradeDefence, _food.EffectDefence, "Defence");
            
            /*
            MaxHP += _food.EffectMaxHP;
            HPRecover += _food.EffectHPRecover;
            MaxMP += _food.EffectMaxMP;
            MPRecover +=_food. EffectMPRecover;
            PetAttack += _food.EffectAttack;
            PetDefence += _food.EffectDefence;
            */
        }
        public void LevelUp()
        {
            if(Exp>maxExp)
            {
                Level += 1;
                MaxHP += UpgradeMaxHP * Pet.UpgradeMaxHPPerPoint;
                UpgradeMaxHP = UpgradeBase;
                HPRecover += UpgradeHPRecover * Pet.UpgradeHPRecoverPerPoint;
                UpgradeHPRecover = UpgradeBase;
                MaxMP += UpgradeMaxMP * Pet.UpgradeMaxMPPerPoint;
                UpgradeMaxMP = UpgradeBase;
                MPRecover += UpgradeMPRecover * Pet.UpgradeMPRecoverPerPoint;
                UpgradeMPRecover = UpgradeBase;
                PetAttack += UpgradeAttack * Pet.UpgradeAttackPerPoint;
                UpgradeAttack = UpgradeBase;
                PetDefence += UpgradeDefence * Pet.UpgradeDefencePerPoint;
                UpgradeDefence = UpgradeBase;
                Points += 3;
                Exp -= maxExp;
                maxExp += ExpPerLevel;
            }
        }
        public void Buff(float _multiplier)
        {
            MaxHP *= _multiplier;
            HPRecover  *= _multiplier;
            MaxMP  *= _multiplier;
            MPRecover  *= _multiplier;
            PetAttack  *= _multiplier;
            PetDefence  *= _multiplier;
        }


        protected void LoadData()
        {
            string petid = ID.ToString();
            PetSpriteName = PlayerData.instance.LoadData<string>(petid, "pet_image_name");
            PetAnimatorName = PlayerData.instance.LoadData<string>(petid, "pet_animator_name");
            PetTalkingFilename = PlayerData.instance.LoadData<string>(petid, "pet_taking_filename");
            Name = PlayerData.instance.LoadData<string>(petid, "pet_name");
            Kind = PlayerData.instance.LoadData<string>(petid, "pet_kind");
            Level = PlayerData.instance.LoadData<int>(petid, "pet_level");
            Mood = PlayerData.instance.LoadData<int>(petid, "pet_mood");
            Hunger = PlayerData.instance.LoadData<int>(petid, "pet_hunger");

            Speed = PlayerData.instance.LoadData<float>(petid, "pet_speed");
            MaxHP = PlayerData.instance.LoadData<float>(petid, "pet_maxHP");
            HPRecover = PlayerData.instance.LoadData<float>(petid, "pet_HPRecover");
            MaxMP = PlayerData.instance.LoadData<float>(petid, "pet_maxMP");
            MPRecover = PlayerData.instance.LoadData<float>(petid, "pet_MPRecover");
            PetfireAttack = PlayerData.instance.LoadData<float>(petid, "pet_petfireAttack");
            PetwaterAttack = PlayerData.instance.LoadData<float>(petid, "pet_petwaterAttack");
            PetwindAttack = PlayerData.instance.LoadData<float>(petid, "pet_petwindAttack");
            PetAttack = PlayerData.instance.LoadData<float>(petid, "pet_attack");
            PetDefence = PlayerData.instance.LoadData<float>(petid, "pet_defence");
            maxExp = PlayerData.instance.LoadData<float>(petid, "pet_maxExp");
            Exp = PlayerData.instance.LoadData<float>(petid, "pet_Exp");
            
            Points = PlayerData.instance.LoadData<int>(petid, "pet_points");
            UpgradeMaxHP = PlayerData.instance.LoadData<float>(petid, "pet_upgradeMaxHP");
            UpgradeHPRecover = PlayerData.instance.LoadData<float>(petid, "pet_upgradeHPRecover");
            UpgradeMaxMP = PlayerData.instance.LoadData<float>(petid, "pet_upgradeMaxMP");
            UpgradeMPRecover = PlayerData.instance.LoadData<float>(petid, "pet_upgradeMPRecover");
            UpgradeAttack = PlayerData.instance.LoadData<float>(petid, "pet_upgradeAttack");
            UpgradeDefence = PlayerData.instance.LoadData<float>(petid, "pet_upgradeDefence");
        }



        protected void SaveData()
        {
            string petid = ID.ToString();
            Debug.Log(ID);
            PlayerData.instance.SaveData<string>(petid, "pet_image_name", PetSpriteName);
            PlayerData.instance.SaveData<string>(petid, "pet_animator_name", PetAnimatorName);
            PlayerData.instance.SaveData<string>(petid, "pet_taking_filename", PetTalkingFilename);
            PlayerData.instance.SaveData<string>(petid, "pet_name", Name);
            PlayerData.instance.SaveData<string>(petid, "pet_kind", Kind);
            PlayerData.instance.SaveData<int>(petid, "pet_level", Level);
            PlayerData.instance.SaveData<float>(petid, "pet_mood", Mood);
            PlayerData.instance.SaveData<float>(petid, "pet_hunger", Hunger);

            PlayerData.instance.SaveData<float>(petid, "pet_speed", Speed);
            PlayerData.instance.SaveData<float>(petid, "pet_maxHP", MaxHP);
            PlayerData.instance.SaveData<float>(petid, "pet_HPRecover", HPRecover);
            PlayerData.instance.SaveData<float>(petid, "pet_maxMP", MaxMP);
            PlayerData.instance.SaveData<float>(petid, "pet_MPRecover", MPRecover);
            PlayerData.instance.SaveData<float>(petid, "pet_petfireAttack", PetfireAttack);
            PlayerData.instance.SaveData<float>(petid, "pet_petwaterAttack", PetwaterAttack);
            PlayerData.instance.SaveData<float>(petid, "pet_petwindAttack", PetwindAttack);
            PlayerData.instance.SaveData<float>(petid, "pet_attack", PetAttack);
            PlayerData.instance.SaveData<float>(petid, "pet_defence", PetDefence);
            PlayerData.instance.SaveData<float>(petid, "pet_maxExp", maxExp);
            PlayerData.instance.SaveData<float>(petid, "pet_Exp", Exp);

            PlayerData.instance.SaveData<int>(petid, "pet_points", Points);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeMaxHP", UpgradeMaxHP);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeHPRecover", UpgradeHPRecover);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeMaxMP", UpgradeMaxMP);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeMPRecover", UpgradeMPRecover);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeAttack", UpgradeAttack);
            PlayerData.instance.SaveData<float>(petid, "pet_upgradeDefence", UpgradeDefence);
        
        }

        protected void SavaExp()
        {
            PlayerData.instance.SaveData<float>(ID.ToString(), "pet_Exp", Exp);
        }
        public void AddExp(float _add)
        {
            Exp += _add;
            PlayerData.instance.SaveData<float>(ID.ToString(), "pet_Exp", Exp);
        }
        public void AddMood(float _add)
        {
            Mood += _add;
            PlayerData.instance.SaveData<float>(ID.ToString(), "pet_mood", Mood);
        }

    }
}


