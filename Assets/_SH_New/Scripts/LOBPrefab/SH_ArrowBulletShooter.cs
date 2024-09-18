using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_ArrowBulletShooter : MonoBehaviour {

    public GameObject ArrowBulletPrefab;
    public Transform ArrowBulletStart;
    public int NoItemsToPool;
    public float Rate;

    private List<GameObject> ArrowBulletPool;
    private float elapsedTime = 0f;

	void Start () {

        ArrowBulletPool = new List<GameObject>();
        for (int i = 0; i < NoItemsToPool; i++)
        {
            var bullet = Instantiate(ArrowBulletPrefab, ArrowBulletStart.transform.position, ArrowBulletStart.transform.rotation, this.transform) as GameObject;
            bullet.SetActive(false);
            ArrowBulletPool.Add(bullet);
        }

	}
	
	void Update () {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > Rate)
        {
            GameObject arrowbullet = GetPooledArrowBullet();
            if (arrowbullet != null)
            {
                arrowbullet.transform.position = ArrowBulletStart.transform.position;
                arrowbullet.transform.rotation = ArrowBulletStart.transform.rotation;
                arrowbullet.SetActive(true);
            }
            elapsedTime = 0f;
        }
	}

    private GameObject GetPooledArrowBullet()
    {
        for (int i = 0; i < ArrowBulletPool.Count; i++)
        {
            if (!ArrowBulletPool[i].activeInHierarchy)
            {
                return ArrowBulletPool[i];
            }
        }
        return null;
    }
}
