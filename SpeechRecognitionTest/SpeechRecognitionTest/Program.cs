using System;
using Microsoft.Speech.Recognition;
using System.Globalization;

namespace ConsoleApplication7
{
	class Program
	{
        static void Main(string[] args)
        {
            var c = new CultureInfo("en-US");
            // Create a new Speech Recognizer
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(c);
             
            // Configure the input to the recognizer.
            recognizer.SetInputToWaveFile(@"C:\Users\rmarchena\Downloads\speech.wav");

            // Create Speech Recognition Grammar 
            Choices keyPhrases = new Choices();
            keyPhrases.Add(new string[] { "credit card number" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(keyPhrases);

            // Create the Grammar instance.
            Grammar g = new Grammar(gb);


            // Load the Grammar into the Speech Recognizer
            recognizer.LoadGrammar(g);

            // Register for Speech Recognition Event Notification
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);

            recognizer.Recognize();

        } 

        static void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //if (e.Result == null) return;
            Console.WriteLine("Speech recognized: " + e.Result.Text);
            Console.WriteLine(" Start time: " + e.Result.Audio.AudioPosition.Seconds);
            Console.WriteLine(" Duration: " + e.Result.Audio.Duration);
            Console.ReadLine();
        }


    }
}
