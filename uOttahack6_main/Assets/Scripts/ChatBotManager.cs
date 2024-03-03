using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenAI;


public class ChatBotManager : MonoBehaviour
{
   
   [SerializeField] private TextMeshProUGUI _questionText;
   
   [SerializeField] private TextMeshProUGUI _responseText;
   
   private List<ChatMessage> messages = new List<ChatMessage>();
   
   private OpenAIApi openai = new OpenAIApi();
   
   public void ManageRecordingData(string data)
   {
      _questionText.text = data;
      FetchAIResponse(data);
   }

   public async void FetchAIResponse(string data)
   {
      var newMessage = new ChatMessage()
      {
         Role = "user",
         Content = data
      };
      
      messages.Add(newMessage);
      
      var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
      {
         Model = "gpt-3.5-turbo-0613",
         Messages = messages
      });
      
      if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
      {
         var message = completionResponse.Choices[0].Message;
         message.Content = message.Content.Trim();
                
         _responseText.text = message.Content;
      }
      else
      {
         Debug.LogWarning("No text was generated from this prompt.");
      }
   }
}
