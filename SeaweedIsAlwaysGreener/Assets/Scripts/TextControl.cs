using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextControl : MonoBehaviour
{
    public TextMeshProUGUI InstructionText;
    public TextMeshProUGUI PointsText;
    /* TODO: Change to health bar
    public Image healthImage;
    public RectTransform healthRect;
    public float healthHeight = 30.0f;
    public float healthWidth = 20.0f;
    public float healthx = 385.0f;
    public float healthy = 220.0f;
    public float pointCount;
    */
    

    public bool GameOver;
    public bool GameWinLose;

    private readonly string InstructionString = "Use WASD or arrow keys to move as you shelter on the island, hunt for seaweed, and try not to get eaten by a shark. Good luck!";
    private readonly string GameOverString = "GAME OVER";
    private readonly string GameWonString = "WE WIN!\n";

    public PlayerTurtle playerControl;
    public float InstructionTime = 7.0f;
    public void GameOverCall(bool winlose)
    {
        if (winlose == true)
        {
            //show VictoryText
            InstructionText.text = GameWonString;
        }
        else
        {
            //show GameOverText
            InstructionText.text = GameOverString;
        }

    }
    private void Start()
    {
        InstructionText.text = InstructionString;
        PointsText.text = playerControl.GetPointCount() + "/" + playerControl.GetPointTotalToWin() + "Seaweeds";
        /*
        pointCount = playerControl.GetPointCount();
        healthRect.position.x = new Vector3(healthx, healthy, 0.0f);
        print("health rectange at " + healthRect.position);
        healthRect.sizeDelta = new Vector2(healthWidth, healthHeight);
        print("health rectange size "+healthRect.sizeDelta);
        */
    }

    void Update()
    {
        PointsText.text = playerControl.GetPointCount() + "/" + playerControl.GetPointTotalToWin() + " Seaweeds";
        /*
        pointCount = 20.0f*(1.0f+playerControl.GetPointCount());
        healthRect.position = new Vector3(375.0f + 0.5f * pointCount, 220.0f, 0.0f);
        healthImage.rectTransform.sizeDelta = new Vector2(pointCount, healthHeight);
        */
        if (Time.realtimeSinceStartup >= InstructionTime)
        {
                //Instructions go away
                InstructionText.text = "";
        }
        if (playerControl.GetGameOver() == true)
        {
            GameOverCall(playerControl.GetWinLose());
        }
    }


}
