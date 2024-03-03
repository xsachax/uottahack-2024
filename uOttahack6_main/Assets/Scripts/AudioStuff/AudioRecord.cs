using UnityEngine;
using System.Collections;
using System.IO;
using System;
using OpenAI;
public class AudioRecord : MonoBehaviour
{
    AudioClip myAudioClip;
    private string output;
    public ChatBotManager chatBotManager;
    private OpenAIApi openai = new OpenAIApi();

    public void StartRecording()
    {
        myAudioClip = Microphone.Start(null, false, 10, 44100);
    }
    
    public async void StopRecording()
    {
        byte[] data = SavWav.Save("last_question_recorded", myAudioClip);
        Microphone.End(null); 
        
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() {Data = data, Name = "audio.wav"},
            Model = "whisper-1",
            Language = "en"
        };
        var res = await openai.CreateAudioTranscription(req);
        output = res.Text;
        chatBotManager.ManageRecordingData(output);
    }
}