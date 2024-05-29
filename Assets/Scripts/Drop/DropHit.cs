using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHit : MonoBehaviour
{
    public Action<DropHit> OnStopped;
    private ParticleSystem particleSystemComp;

    public ParticleSystem ParticleSystem
    {
        get
        {
            if (particleSystemComp == null)
            {
                particleSystemComp = GetComponent<ParticleSystem>();
            }

            return particleSystemComp;
        }
        private set
        {
            particleSystemComp = value;
        }
    }

    private void Awake()
    {
        particleSystemComp = GetComponent<ParticleSystem>();
    }

    private void OnParticleSystemStopped()
    {
        OnStopped?.Invoke(this);
    }
}
