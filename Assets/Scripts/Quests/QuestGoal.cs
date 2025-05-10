using UnityEngine;
using System;

public abstract class QuestGoal : ScriptableObject
{
    public string goalName;
    public bool IsCompleted { get; protected set; }

    public abstract void Initialize();
    public abstract void OnEventRaised(params object[] args);

    public abstract string GetProgression();

    public abstract void ChangeProgression();
    public event Action OnProgressChanged;

    protected void NotifyProgressChanged()
    {
        OnProgressChanged?.Invoke();
    }
}
