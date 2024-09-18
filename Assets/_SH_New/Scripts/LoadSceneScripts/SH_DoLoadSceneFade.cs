using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_DoLoadSceneFade : MonoBehaviour {

    public Color FadeColor = Color.black;
    public float LogoFadeInTime = 5f;
    public float LogoShowTime = 5f;
    public float LogoFadeOutTime = 5f;
    public string MainSceneName;

    private float elapsedTime = 0;
    private bool startTimer = false;

	void Start ()
    {
        SH_FadeManager.Instance.FadeOut(LogoFadeInTime, FadeColor, StartTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (startTimer)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > LogoShowTime)
        {
            SH_FadeManager.Instance.FadeIn(LogoFadeOutTime, FadeColor, Launch);
            elapsedTime = 0.0f;
            startTimer = false;
        }
	}

    public void StartTimer()
    {
        elapsedTime = 0;
        startTimer = true;
    }

    public void Launch()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainSceneName);
        Destroy(this.gameObject, 2);
    }
}
