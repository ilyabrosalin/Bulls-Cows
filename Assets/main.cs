using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    public Main()
    {
        hiddenNumber = GetRandomNumber();
    }

    public InputField inputField;
    public Button button;
    public Text output;

    public static string hiddenNumber;

    public string GetRandomNumber()
    {
        System.Random rnd = new System.Random();
        string result;
        do
        {
            result = (rnd.Next(1000, 9999)).ToString();
        }
        while (!OnlyUniqueDigits(result));
        return result;
    }

    public int SearchBulls(string hiddenNumber, string userNumber)
    {
        Debug.Log(hiddenNumber);
        int bulls = 0;
        for (int i = 0, n = 4; i < n; i++)
        {
            if (hiddenNumber[i] == userNumber[i])
            {
                bulls++;
            }
        }
        return bulls;
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
    public void OnClick()
    {
        string userNumber = inputField.text;
        var isNumeric = int.TryParse(userNumber, out _);
        var isUnique = OnlyUniqueDigits(userNumber);

        if (isNumeric && isUnique && userNumber.Length==4)
        {
            int bulls = SearchBulls(hiddenNumber, userNumber);
            output.text += inputField.text + ": " + bulls + " быков" + "\n";
        }
        else
        {
            if (!isUnique) Debug.Log("Цифры не должны повторяться");
            if (!isNumeric || userNumber.Length != 4) Debug.Log("Код - четырехзначное число");
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
