using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    public void ScoreUpdate()
    {
        text.text = "Score: " +  gameManager.PlayerSelect.Score;
    }
}
