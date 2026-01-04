using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class JaiBookUiManager : MonoBehaviour
{
    public GameObject bookPanel;
    public TextMeshProUGUI pageLeft;
    public TextMeshProUGUI pageRight;

    public string[] remainderPages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bookPanel.SetActive(false);
        }
    }

    public void LoadPages(TextAsset book)
    {
        bookPanel.SetActive(true);
        if (remainderPages.Length == 0)
        {
            string contents = book.text;
            string[] splitOutput = contents.Split('+');
            pageLeft.text = splitOutput[0];
            pageRight.text = splitOutput[1];

            //maybe removes val at [0]?
            splitOutput = ReduceArray(splitOutput);

            if (splitOutput.Length > 2)
            {
                remainderPages = splitOutput;
            }
        }
        else
        {
            string contents = book.text;
            string[] splitOutput = contents.Split('+');
            //combine arrays
            //remainderPages += splitOutput;
            
            pageLeft.text = remainderPages[0];
            pageRight.text = remainderPages[1];
            
            //reduce remainder pages
            remainderPages = ReduceArray(remainderPages);
        }
    }

    public string[] ReduceArray(string[] array)
    {
        string[] newArray = new string[array.Length-1];
        for (int i = 0; i < array.Length-1; i++)
        {
            newArray[i] = array[i + 1];
        }
        return newArray; 
    }

    public void IncrimentPages()
    {
        
    }
}
