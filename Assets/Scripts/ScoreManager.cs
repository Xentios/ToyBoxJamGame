using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    FloatVariable score;
    [SerializeField]
    List<Sprite> numberSprites;

    [SerializeField]
    List<Image> canvasScoreCanvas;

    private void Awake()
    {
        score.SetValue(0);
    }

    public void RefreshScore()
    {
        string format = "";
        foreach (var item in canvasScoreCanvas)
        {
            format += "0";
        }

        var result=score.Value.ToString(format);
        var charArray = result.ToCharArray();
        for (int i = 0; i < canvasScoreCanvas.Count; i++)
        {            
            var index=(int)char.GetNumericValue(result[i]);
            canvasScoreCanvas[i].sprite = numberSprites[index];
        }
       
    }
}
