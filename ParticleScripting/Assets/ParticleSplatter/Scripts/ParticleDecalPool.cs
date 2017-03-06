using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour {

    public int maxDecals = 100;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem particleDecalSystem;
    private int particleDecalIndex;
    private ParticleDecalsData[] particleData;
    private ParticleSystem.Particle[] particles;

	// Use this for initialization
	void Start () 
    {
        particles = new ParticleSystem.Particle[maxDecals];
        particleDecalSystem = GetComponent<ParticleSystem>();
        particleData = new ParticleDecalsData[maxDecals];
        for (int i = 0; i < particleData.Length; i++)
        {
            particleData[i] = new ParticleDecalsData();
        }
	}

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisPlayParticle();
    }

    void SetParticleData(ParticleCollisionEvent particlecollisionEvent,Gradient colorgradient1)
    {
        if (particleDecalIndex >= maxDecals)
        {
            particleDecalIndex = 0;
        }
        //record collision position,rotation,size and color; 

        particleData[particleDecalIndex].position = particlecollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particlecollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0,360);
        particleData[particleDecalIndex].rotation = particleRotationEuler;
        particleData[particleDecalIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalIndex].color1 = colorgradient1.Evaluate(Random.Range(0f, 1f));

        particleDecalIndex++;
    }

    void DisPlayParticle()
    {
        for (int i = 0; i <particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color1;

        }
        //显示粒子
        particleDecalSystem.SetParticles(particles, particles.Length);
    }

}
