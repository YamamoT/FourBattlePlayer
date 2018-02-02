using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public Transform target;
    [SerializeField]
    LayerMask layerMask;

    public ParticleSystem effect;
    public EffectMove move;
    int coolTime = 180;
    enum Turn
    {
        none,
        fifing,
        cool
    }
    [SerializeField]
    Turn flag = Turn.none;
    // Use this for initialization
    void Start () {
        move.target = target.position;
	}
	
	// Update is called once per frame
	void Update () {
        float range = 0;
        if(transform.position.x > target.position.x) { range = transform.position.x - target.position.x; }
        else { range = target.position.x - transform.position.x; }
        Ray ray = new Ray(transform.position, new Vector3(range, 0, 0));
        Debug.DrawLine(transform.position, target.position, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            if (flag == Turn.none)
            {
                Debug.Log("hit");
                flag = Turn.cool;
                ParticleSystem _effect = Instantiate(effect, transform.position, Quaternion.identity);
                _effect.transform.localScale = new Vector3(2, 2, 2);
                _effect.Play();
            }
        }

        if (flag == Turn.cool)
        {
            coolTime--;
            if (coolTime < 0)
            {
                flag = Turn.none;
                coolTime = 180;
            }
        }
    }
}
