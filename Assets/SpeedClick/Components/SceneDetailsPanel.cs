using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneDetailsPanel : MonoBehaviour
{
	
	public Text Title;
	public Text Author;
	public Text Properties;
	public Text Instructions;
	public RankingPanel Ranking;

    public float alpha { get { return this._canvas.alpha; } set { this._canvas.alpha = value; } }

    private CanvasGroup _canvas;

    void Start()
    {
        if (this._canvas == null)
            this._canvas = this.GetComponent<CanvasGroup>();
        this.alpha = 0;
    }

    public void Expand()
    {
        //RectTransform rect = (this.gameObject.transform as RectTransform);
        //rect.TransformVector();
    }

}

