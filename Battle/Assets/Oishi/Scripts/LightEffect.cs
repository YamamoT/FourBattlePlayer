using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour {

    new ParticleSystem light;
    public ParticleSystem green;
    public ParticleSystem red;
    bool flag;
    public FloorSwitch f_switch;

	// Use this for initialization
	void Start () {
        light = null;
        CreateRedLight();
        flag = f_switch.flag;
	}
	
	// Update is called once per frame
	void Update () {
		if(flag != f_switch.flag)
        {
            flag = f_switch.flag;
            if (flag) { CreateGreenLight(); }
            else { CreateRedLight(); }
        }
	}

    void CreateGreenLight()
    {
        if(light != null) { OffLight(); }
        //light = Instantiate(Resources.Load<ParticleSystem>("Effect/GreanLight"),
        //    gameObject.transform.position, Quaternion.identity);
        light = Instantiate(green, gameObject.transform.position, Quaternion.identity);
        light.Play();
    }

    void CreateRedLight()
    {
        if (light != null) { OffLight(); }
        //light = Instantiate(Resources.Load<ParticleSystem>("Effect/RedLight"),
        //    gameObject.transform.position, Quaternion.identity);
        light = Instantiate(red, gameObject.transform.position, Quaternion.identity);
        light.Play();
    }

    void OffLight()
    {
        if(light != null)
        {
            Destroy(light.gameObject);
        }
    }
}
