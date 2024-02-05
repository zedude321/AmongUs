using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            myObjects[i].GetComponent<Image>().color = Color.white;
            myObjects[i].transform.SetSiblingIndex(Random.Range(0, 9));
        }
    }

    public void ButtonOrder (int button) {
        if (button == nextButton) {
            myObjects[nextButton].GetComponent<Image>().color = Color.green;
            nextButton++;
            if (button == 9) {
                nextButton = 0;
                Debug.Log("finish");
                ButtonPanelClose();
            }
        } else if (button > nextButton) {
            Debug.Log("incorrect");
            StartCoroutine(Incorrect());
            nextButton = 0;
        }
    }

    IEnumerator Incorrect () {
        Debug.Log("Incorrect reached");
        for(int i = 0; i < 10; i++) {
            myObjects[i].GetComponent<Image>().color = Color.red;
        }
        yield return new WaitForSeconds(0.2F);
        for(int i = 0; i < 10; i++) {
            myObjects[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void ButtonPanelClose() {
        GamePanel.SetActive(false);
    }

    public void ButtonPanelOpen() {
        GamePanel.SetActive(true);
    }
}
