using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSequenceMiniGame : MonoBehaviour
{
    [SerializeField] int nextButton;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject[] myObjects;

    // Start is called before the first frame update
    void Start()
    {
        nextButton = 0;
    }

    private void OnEnable () {
        nextButton = 0;
        for(int i = 0; i < myObjects.Length; i++) {
            myObjects[i].transform.SetSiblingIndex(Random.Range(0, 9));
        }
    }

    public void ButtonOrder (int button) {
        Debug.Log("Pressed");
        if (button == nextButton) {
            nextButton++;
        } else {
            nextButton = 0;
        }
        if (button == 9) {
            nextButton = 0;
            ButtonPanelClose();
        }
    }

    public void ButtonPanelClose() {
        GamePanel.SetActive(false);
    }

    public void ButtonPanelOpen() {
        GamePanel.SetActive(true);
    }
}
