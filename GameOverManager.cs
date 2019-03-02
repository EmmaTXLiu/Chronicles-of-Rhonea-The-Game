using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerMoveGame playerMove;

    private RefManager instance;

    private BestCamera cam;

    private GameObject player;

    Animator anim;

    public Text score;

    bool unsaved = true;

    void Start()
    {
        instance = RefManager.refManager;
 
        cam = instance.cam.GetComponent<BestCamera>();

        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveGame>();
        unsaved = true;

        //score = instance.HUDCanvas.transform.Find("ScoreText").gameObject.GetComponent<Text>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            playerMove.enabled = false;
            cam.enabled = false;
            Cursor.visible = true;
            anim.SetTrigger("GameOver");

            float delay = 2f;
            delay -= Time.deltaTime;

            if (unsaved)
            {
                UpdateGameControl(score);
                unsaved = false;
            }
            

            if (delay <= 0f)
            {
                player.SetActive(false);
            }
        }
    }

    void UpdateGameControl(Text score)
    {

        //string scoreVal = score.text.Replace("Score: ", "");
        //int newScore = int.Parse(scoreVal);

        int newScore = ScoreManager.score;


        if (newScore > GameControl.highScore)
        {
            GameControl.highScore = newScore;
        }

        GameControl.Save();
    }
}
