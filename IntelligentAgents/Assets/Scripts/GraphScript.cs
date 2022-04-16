using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphScript : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        List<int> valueList = new List<int>() { 5, 98, 56, 35, 30, 22, 15, 23, 16, 22, 27,37, 38, 46};

        ShowGraph(valueList);
        // CreateCircle(new Vector2(200, 200));
    }

    private void CreateCircle(Vector2 anchoredPosition){
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    public void ShowGraph(List<int> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        
        Debug.Log(graphHeight);
        float yMaximum = 100f;
        float xSize = 50;
        for(int i=0; i<valueList.Count; i++){
            float xPos = i * xSize;
            float yPos = (valueList[i] /yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPos, yPos));
        }
    }
}
