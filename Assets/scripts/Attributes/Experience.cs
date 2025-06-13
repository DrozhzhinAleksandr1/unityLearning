using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
  public class Experience : MonoBehaviour, ISaveable
  {
    [SerializeField] float experiencePoints = 0;
    // public delegate void ExperienceGainedDelegate();
    // public event ExperienceGainedDelegate onExperienceGained;

    // using Action instead => public delegate void ExperienceGainedDelegate()
    public event Action onExperienceGained;

    public void GainExperience(float experience)
    {
      experiencePoints += experience;
      onExperienceGained();
    }
    public object CaptureState()
    {
      return experiencePoints;
    }

    public void RestoreState(object state)
    {
      experiencePoints = (float)state;
    }
    public float GetPoints()
    {
      return experiencePoints;
    }
  }
}