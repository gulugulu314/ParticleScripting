using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {


    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticle;
    public Gradient particleColorGradient;
    public ParticleDecalPool splatDecalPool; 

    //定义一个碰撞事件列表
    List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () 
    {
        collisionEvents = new List<ParticleCollisionEvent>();
	}


    void OnParticleCollision(GameObject other)
    {
        //返回碰撞事件列表
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particlecollisionEvent)
    {
        //将splatterparticle的位置和方向转换到碰撞点的位置和方向
        splatterParticle.transform.position = particlecollisionEvent.intersection;
        splatterParticle.transform.rotation = Quaternion.LookRotation(particlecollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticle.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));

        splatterParticle.Emit(1);
    }


	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            //改变颜色
            ParticleSystem.MainModule psMain = particleLauncher.main;
            psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
            particleLauncher.Emit(1);
        }

	}
}
