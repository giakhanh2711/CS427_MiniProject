using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
[Serializable]
public enum SkillType
{
    Woodcutting,
    Rockcutting,
    Fighting
}



//public class Skill
//{
//    public int level = 1;
//    public int experience = 0;
//    public SkillType skillType;

//    public int NextLevel
//    {
//        get
//        {
//            return level * 1000;
//        }
//    }

//    public Skill(SkillType skillType)
//    {
//        this.skillType = skillType;
//        level = 1;
//        experience = 0;
//    }

//    public void AddExperience(int experience)
//    {
//        this.experience += experience;

//        CheckLevelUp();
//    }

//    private void CheckLevelUp()
//    {
//        if (experience >= NextLevel)
//        {
//            LevelUp();
//        }
//    }

//    private void LevelUp()
//    {
//        experience -= NextLevel;
//        level += 1;
//    }
//}

//public class Level
//{
//    public int level = 1;
//    public int star = 0;

//    public int StartForNextLevel
//    {
//        get
//        {
//            return level * 500;
//        }
//    }

//    public Level()
//    {
//        level = 1;
//        star = 0;
//    }

//    public void AddStar(int star)
//    {
//        this.star += star;

//        //CheckLevelUp();
//    }
//}

//public class CharacterLevel : MonoBehaviour
//{
//    //[SerializeField] Skill woodCutting;
//    //[SerializeField] Skill rockCutting;
//    //[SerializeField] Skill fighting;

//    private Level playerLevel;

//    [SerializeField] TextMeshProUGUI textLevel;

//    private void Start()
//    {
//        //woodCutting = new Skill(SkillType.Woodcutting);
//        //rockCutting = new Skill(SkillType.Rockcutting);
//        //fighting = new Skill(SkillType.Fighting);

//        playerLevel = new Level();
//        UpdateTextLevel();
//    }

//    private void UpdateTextLevel()
//    {
//        textLevel.text = "Level " + playerLevel.level.ToString();
//    }

//    private void CheckLevelUp()
//    {
//        if (playerLevel.star >= playerLevel.StartForNextLevel)
//        {
//            LevelUp();
//        }
//    }

//    private void LevelUp()
//    {
//        playerLevel.star -= playerLevel.StartForNextLevel;
//        playerLevel.level += 1;

//        UpdateTextLevel();
//        GetComponent<Character>().UpdateStarBar();
//    }

//    public int GetLevel()
//    {
//        return playerLevel.level;
//    }

//    //public int GetLevel(SkillType skillType)
//    //{
//    //    Skill skill = GetSkill(skillType);

//    //    if (skill != null)
//    //    {
//    //        return skill.level;
//    //    }

//    //    return -1;
//    //}

//    public void AddStar(int star)
//    {
//        playerLevel.AddStar(star);

//        CheckLevelUp();
//    }

//    //public void AddExperience(SkillType skillType, int experience)
//    //{
//    //    Skill skill = GetSkill(skillType);
//    //    skill.AddExperience(experience);
//    //}

//    //public Skill GetSkill(SkillType skillType)
//    //{
//    //    switch (skillType)
//    //    {
//    //        case SkillType.Woodcutting:
//    //            return woodCutting;
//    //        case SkillType.Rockcutting:
//    //            return rockCutting;
//    //        case SkillType.Fighting:
//    //            return fighting;
//    //    }

//    //    return null;
//    //}
//}
