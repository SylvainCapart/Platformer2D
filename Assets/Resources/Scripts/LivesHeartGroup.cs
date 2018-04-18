using UnityEngine;
using UnityEngine.UI;


public class LivesHeartGroup : MonoBehaviour
{

    [SerializeField]
    private Sprite emptyHeart;

    [SerializeField]
    private Sprite fullHeart;

    private int pastRemainingLives;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameMaster.gm.MaxLives; i++)
        {
            GameObject go = new GameObject("HeartImage" + i.ToString());
            go.transform.SetParent(this.transform);

            go.AddComponent<Image>().sprite = fullHeart;
            go.GetComponent<Image>().transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        pastRemainingLives = GameMaster.RemainingLives;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.RemainingLives != pastRemainingLives)
        {
            //for (int i = GameMaster.gm.MaxLives - 1; i >= GameMaster.RemainingLives - 1; i--)

            GameObject go = GameObject.Find("HeartImage" + (GameMaster.RemainingLives).ToString());
            if (go != null)
            {
                go.GetComponent<Image>().sprite = emptyHeart;
            }




        }
        pastRemainingLives = GameMaster.RemainingLives;

    }
}
