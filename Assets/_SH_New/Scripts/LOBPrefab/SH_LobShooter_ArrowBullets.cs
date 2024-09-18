using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class SH_LobShooter_ArrowBullets : MonoBehaviour
    {
        public GameObject ArrowBulletPrefab;
        public Transform ArrowBulletStart;
        public int NoItemsToPool;
        public float Rate;

        private List<GameObject> ArrowBulletPool;
        private float elapsedTime = 0f;
        private bool isPaused = false;

        void Start()
        {
            SH_EventManager.Instance.PauseLOBAnimation += SH_EventManager_SetPause;
            ArrowBulletPool = new List<GameObject>();
            for (int i = 0; i < NoItemsToPool; i++)
            {
                var bullet = Instantiate(ArrowBulletPrefab, ArrowBulletStart.transform.position, ArrowBulletStart.transform.rotation, this.transform) as GameObject;
                bullet.SetActive(false);
                ArrowBulletPool.Add(bullet);
            }

        }

        private void SH_EventManager_SetPause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > Rate && !isPaused)
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
}

