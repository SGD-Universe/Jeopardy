using UnityEngine;

namespace TMPro.Examples
{
    public class TMP_TextEventCheck : MonoBehaviour
    {
        public TMP_TextEventHandler TextEventHandler;

        private TMP_Text m_TextComponent;

        void OnEnable()
        {
            if (TextEventHandler != null)
            {
                // Get a reference to the text component
                m_TextComponent = TextEventHandler.GetComponent<TMP_Text>();
                
                TextEventHandler.OnCharacterSelection.AddListener(OnCharacterSelection);
                TextEventHandler.OnSpriteSelection.AddListener(OnSpriteSelection);
                TextEventHandler.OnWordSelection.AddListener(OnWordSelection);
                TextEventHandler.OnLineSelection.AddListener(OnLineSelection);
                TextEventHandler.OnLinkSelection.AddListener(OnLinkSelection);
            }
        }

        void OnDisable()
        {
            if (TextEventHandler != null)
            {
                TextEventHandler.OnCharacterSelection.RemoveListener(OnCharacterSelection);
                TextEventHandler.OnSpriteSelection.RemoveListener(OnSpriteSelection);
                TextEventHandler.OnWordSelection.RemoveListener(OnWordSelection);
                TextEventHandler.OnLineSelection.RemoveListener(OnLineSelection);
                TextEventHandler.OnLinkSelection.RemoveListener(OnLinkSelection);
            }
        }

        void OnCharacterSelection(char c, int index)
        {
            Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnSpriteSelection(char c, int index)
        {
            Debug.Log("Sprite [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnWordSelection(string word, int firstCharacterIndex, int length)
        {
            Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLineSelection(string lineText, int firstCharacterIndex, int length)
        {
            Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (m_TextComponent != null)
            {
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];
            }
            
            Debug.Log($"Link Index: {linkIndex} with ID [{linkID}] and Text \"{linkText}\" has been selected.");
        }
    }
}