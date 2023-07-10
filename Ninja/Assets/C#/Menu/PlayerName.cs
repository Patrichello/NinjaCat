using UnityEngine;
using TMPro;


public class PlayerName : MonoBehaviour
{
    public string playerName;
    private TMP_Text playerText;
    private TMP_InputField inputField;
    public bool nameInput;

    private MainMenu mainMenu;

    void Start()
    {
        playerText = GetComponent<TextMeshPro>();
        inputField = GetComponentInParent<TMP_InputField>();
        mainMenu = FindObjectOfType<MainMenu>();
    }

    void Update()
    {
        playerName = inputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            nameInput = true;
        }
    }
}
