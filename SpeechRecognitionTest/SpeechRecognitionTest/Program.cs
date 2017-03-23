using System;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using System.Threading;

using System.Globalization;

namespace ConsoleApplication7
{
	class Program
	{
        private static bool completed;
        static void Main(string[] args)
        {
            var c = new CultureInfo("en-US");
            // Create a new Speech Recognizer
            var recognizer = new SpeechRecognitionEngine(c);
            var recognizer2 = new SpeechRecognitionEngine(c);
            recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 30);
            Console.WriteLine(recognizer.BabbleTimeout);
            Console.WriteLine(recognizer.InitialSilenceTimeout);
            recognizer.InitialSilenceTimeout = TimeSpan.FromMilliseconds(0);
            //recognizer.EndSilenceTimeout = TimeSpan.FromMilliseconds(0);
            //recognizer.EndSilenceTimeoutAmbiguous = TimeSpan.FromMilliseconds(0);
            Console.WriteLine(recognizer.InitialSilenceTimeout);
            
            Console.WriteLine(recognizer.EndSilenceTimeout);
            Console.WriteLine(recognizer.EndSilenceTimeoutAmbiguous.TotalSeconds);
            // Configure the input to the recognizer.
            //recognizer.SetInputToWaveFile(@"C:\Users\march\Source\Repos\renzomarchena\SpeechRecognitionTest\SpeechRecognitionTest\2488415658_NYL_P410SM8R_149520478_101826_NYL_P410SM8R.wav");
            recognizer.SetInputToWaveFile(@"C:\Users\march\Downloads\10minutes.wav");
            recognizer2.SetInputToWaveFile(@"C:\Users\march\Downloads\10minutes.wav");

            var srgsDocument = new SrgsDocument(@"C:\Users\march\Source\Repos\renzomarchena\SpeechRecognitionTest\SpeechRecognitionTest\grammar.grxml");

            var srgsDocument2 = new SrgsDocument(@"C:\Users\march\Source\Repos\renzomarchena\SpeechRecognitionTest\SpeechRecognitionTest\KeyPhrases.grml");

            // Create the Grammar instance.

            var g = new Grammar(srgsDocument);
            var g2 = new Grammar(srgsDocument2);
            // Load the Grammar into the Speech Recognizer
       
            recognizer.LoadGrammar(g);
            recognizer2.LoadGrammar(g2);
            Console.WriteLine(recognizer.Grammars.Count);
            // Register for Speech Recognition Event Notification
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);
            recognizer.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(Recognizer_RecognizeCompleted);
            recognizer2 .SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);
            recognizer2.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(Recognizer_RecognizeCompleted);
            completed = false;
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            recognizer2.RecognizeAsync(RecognizeMode.Multiple);
            while (!completed) {

                Thread.Sleep(333);
            }

            Console.ReadLine();
        }

       
        private static void Recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            /*completed = true;
            Console.WriteLine(e.BabbleTimeout);
            Console.WriteLine(e.Cancelled);
            Console.WriteLine(e.Error);
            Console.WriteLine(e.InitialSilenceTimeout);
            Console.WriteLine(e.InputStreamEnded);
            Console.WriteLine(e.UserState);*/
            Console.WriteLine("Speech Recognition Complete!");
            //Console.WriteLine(e.Result.Text);
            
        }

        static void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result == null) return;
            if (e.Result.Confidence >= 0.5)
            {
                Console.WriteLine( + e.Result.Audio.AudioPosition.Hours + ":" +
                                                    e.Result.Audio.AudioPosition.Minutes + ":" +
                                                    e.Result.Audio.AudioPosition.Seconds + " " + e.Result.Text);
                /*Console.WriteLine(+ e.Result.Audio.Duration);
                Console.WriteLine(" Confidence: " + e.Result.Confidence);*/
            }

            /*foreach (var word in e.Result.Words)
            {
                
                    Console.WriteLine(word.Text);
                    Console.WriteLine(word.AudioPosition.Hours + ":" + word.AudioPosition.Minutes + ":" + word.AudioPosition.Seconds);

                    Console.WriteLine("Confidence: " + word.Confidence);

                
            }
            */
           
        }


    }
}
