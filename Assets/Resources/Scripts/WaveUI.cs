
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountDownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

	// Use this for initialization
	void Start () {
        if (!spawner)
        {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }
        if (!waveAnimator)
        {
            Debug.LogError("No waveanimator referenced");
            this.enabled = false;
        }
        if (!waveCountText)
        {
            Debug.LogError("No wavecounttext referenced");
            this.enabled = false;
        }
        if (!waveCountDownText)
        {
            Debug.LogError("No wavecountdown referenced");
            this.enabled = false;
        }


    }
	
	// Update is called once per frame
	void Update () {
		switch(spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();

                break;

            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }
        previousState = spawner.State;
	}


    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountDown", true);

        }
        waveCountDownText.text = ((int)spawner.WaveCountDown).ToString();
    }

    private void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveIncoming", true);
            waveAnimator.SetBool("WaveCountDown", false);


            waveCountText.text = spawner.NextWave.ToString();
        }
    }

}
