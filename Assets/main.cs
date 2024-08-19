using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    public Main()
    {
        getRandomNumber();
        int hiddenNumber = getRandomNumber();
        Debug.Log(hiddenNumber);
    }

    public InputField inputField;
    public Button button;
    public Text output;

    public int getRandomNumber()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1000, 9999);
    }

    static bool OnlyUniqueDigits(string number)
    {
        for (int i = 0, n = number.Length; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (number[i] == number[j])
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void onClick()
    {
        string text = inputField.text;
        var isNumeric = int.TryParse(text, out _);
        var isUnique = OnlyUniqueDigits(text);

        if (isNumeric && isUnique && text.Length==4)
        {
            output.text += inputField.text + "\n";
        }
        else
        {
            if (!isUnique) Debug.Log("Цифры не должны повторяться");
            if (!isNumeric || text.Length != 4) Debug.Log("Код - четырехзначное число");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
