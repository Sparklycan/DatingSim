using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtilities : MonoBehaviour
{

    private Dictionary<string, Action> events = new Dictionary<string, Action>();
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    public void PlayParticleSystem(int index)
    {
        if (index >= particleSystems.Count)
            return;
        if (particleSystems[index] == null)
            return;

        particleSystems[index].Play();
    }

    public void StopParticleSystem(int index)
    {
        if (index >= particleSystems.Count)
            return;
        if (particleSystems[index] == null)
            return;

        particleSystems[index].Stop();
    }

    public void StopAllParticleSystems()
    {
        foreach(ParticleSystem system in particleSystems)
        {
            if (system != null)
                system.Stop();
        }
    }

    public void EmitParticles(AnimationEvent animationEvent)
    {
        int system = animationEvent.intParameter;
        int count = (int)animationEvent.floatParameter;

        if (system >= particleSystems.Count)
            return;
        if (particleSystems[system] == null)
            return;

        particleSystems[system].Emit(count);
    }

    public void RegisterEvent(string eventName, Action action)
    {
        events.Add(eventName, action);
    }

    public void UnregisterEvent(string eventName)
    {
        events.Remove(eventName);
    }

    public void InvokeEvent(string eventName)
    {
        if (events.TryGetValue(eventName, out Action action))
            action.Invoke();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void InstantiatePrefab(GameObject prefab)
    {
        Instantiate(prefab, transform);
    }

}
