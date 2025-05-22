using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        text.text = "Score: " +  gameManager.PlayerSelect.Score;
    }
}
