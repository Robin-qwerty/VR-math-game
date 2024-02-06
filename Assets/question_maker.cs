using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question_maker : MonoBehaviour
{
    public Text questionText;
    public Text optionText1;
    public Text optionText2;
    public Text optionText3;
    public int mathQuestion;
    public GameObject[] optionTextObjects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in optionTextObjects)
        {
            Debug.Log("Object Name: " + obj.name);
            // You can add more information if needed, like obj.transform.position, etc.
        }

        // Generate two random numbers between 1 and 10
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);

        // Choose a random operation (addition, subtraction, multiplication)
        string operation = GetRandomOperation();

        // Calculate the correct answer
        int correctAnswer = CalculateAnswer(num1, num2, operation);

        // Generate two incorrect answers
        int incorrectAnswer1 = correctAnswer + Random.Range(1, 6);
        int incorrectAnswer2 = correctAnswer - Random.Range(1, 6);

        // Create a list with all three answers
        int[] answers = { correctAnswer, incorrectAnswer1, incorrectAnswer2 };

        // Shuffle the list to randomize the order of answers
        ShuffleArray(answers);

        // Display the question and answers
        questionText.text = $"What is \n {num1} {operation} {num2}?";

        // Display the answers on Text objects in random order
        optionText1.text = $"{answers[0]}";
        optionText2.text = $"{answers[1]}";
        optionText3.text = $"{answers[2]}";

        // Determine which optionText contains the correct answer
        if (answers[0] == correctAnswer)
        {
            Debug.Log("Option 1 has the correct answer.");
            SetCorrectAnswer(0);
        }
        else if (answers[1] == correctAnswer)
        {
            Debug.Log("Option 2 has the correct answer.");
            SetCorrectAnswer(1);
        }
        else if (answers[2] == correctAnswer)
        {
            Debug.Log("Option 3 has the correct answer.");
            SetCorrectAnswer(2);
        }
    }

    void SetCorrectAnswer(int optionIndex)
    {
        // Get the corresponding Explode script
        Explode explodeScript = optionTextObjects[optionIndex].GetComponent<Explode>();

        // Set the answer variable to true for the correct answer block
        explodeScript.answer = true;
    }

    string GetRandomOperation()
    {
        string[] operations = { "+", "-", "*" };
        return operations[Random.Range(0, operations.Length)];
    }

    int CalculateAnswer(int num1, int num2, string operation)
    {
        switch (operation)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            default:
                return 0;
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
