using UnityEngine;

public class EndTrigger : MonoBehaviour {

    public GameObject gameFinishedUI;

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameFinishedUI != null)
            {
                GameMaster.gm.EndIsReached = true;
                gameFinishedUI.SetActive(true);
                GameMaster.gm.DeactivatePlayerEnemy();

            }
            else
            {
                Debug.LogError("No UI found to finish the game in EndTrigger.cs");
            }
        }
    }
}
