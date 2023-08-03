using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Woodcutting,
    Rockcutting,
    Fighting
}

[Serializable]
public class Skill
{
    public int level = 1;
    public int experience = 0;
    public SkillType skillType;

    public int NextLevel
    {
        get
        {
            return level * 1000;
        }
    }

    public Skill(SkillType skillType)
    {
        this.skillType = skillType;
        level = 1;
        experience = 0;
    }

    public void AddExperience(int experience)
    {
        this.experience += experience;

        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (experience >= NextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        experience -= NextLevel;
        level += 1;
    }
}

public class CharacterLevel : MonoBehaviour
{
    [SerializeField] Skill woodCutting;
    [SerializeField] Skill rockCutting;
    [SerializeField] Skill fighting;

    private void Start()
    {
        woodCutting = new Skill(SkillType.Woodcutting);
        rockCutting = new Skill(SkillType.Rockcutting);
        fighting = new Skill(SkillType.Fighting);
    }

    public int GetLevel(SkillType skillType)
    {
        Skill skill = GetSkill(skillType);

        if (skill != null)
        {
            return skill.level;
        }

        return -1;
    }

    public void AddExperience(SkillType skillType, int experience)
    {
        Skill skill = GetSkill(skillType);
        skill.AddExperience(experience);
    }

    public Skill GetSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Woodcutting:
                return woodCutting;
            case SkillType.Rockcutting:
                return rockCutting;
            case SkillType.Fighting:
                return fighting;
        }

        return null;
    }
}
