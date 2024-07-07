using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void OnResetButtonClick()
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(1); // Change to the index of your scene
        if (scene.IsValid())
        {
            GameObject[] buttons = scene.GetRootGameObjects();
            foreach (GameObject obj in buttons)
            {
                Highlighter Highlighter = obj.GetComponent<Highlighter>();
                if (Highlighter != null)
                {
                   Highlighter.ResetButton();
                }
            }
        }
    }
}
namespace ResetButtonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter some text (type 'reset' to clear, 'exit' to quit):");
                input = Console.ReadLine();

                if (input.ToLower() == "reset")
                {
                    input = string.Empty;
                    Console.WriteLine("Text has been reset.");
                }
                else if (input.ToLower() == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"You entered: {input}");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}