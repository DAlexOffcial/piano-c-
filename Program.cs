// libreies for sistem control
using System;
using static System.Console;

// libraries for sound 
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Runtime.CompilerServices;

namespace sinusoidal_wave
{
   class Program
   {
      // notes with their frecuncies 
      public static float[] notes =
         [
             261.63f, // Do (C)
             277.18f, // Do♯ (C♯)
             293.66f, // Re (D)
             311.13f, // Re♯ (D♯)
             329.63f, // Mi (E)
             349.23f, // Fa (F)
             369.99f, // Fa♯ (F♯)
             392.00f, // Sol (G)
             415.30f, // Sol♯ (G♯)
             440.00f, // La (A)
             466.16f, // La♯ (A♯)
             493.88f, // Si (B)
             523.25f  // Do (C) (octava superior)
      ];
      // tone of the sound Hz 
      public static float frequency = 0;
      public static void Main(string[] args)
      {
         //// params of the sinusoidal wave 
         // volume of the sound  V (voltage)
         float amplitude = 0.5f;

         // duration of the sound 
         float duration_time = 1;

         while (true)
         {
            WriteLine("que nota quieres reproducir ? ");
            WriteLine("incerta un numero del 1 al 12 que representan las 12 notas de la escala cromatica");
            int resp_note = Convert.ToInt32(ReadLine());
            updateFrecuencie(resp_note);

            //// create 3 octaves for every note  

            // generate sinusoidal wave 
            var wave_sinusoidal_MAYOR = new SignalGenerator()
            {
               Gain = amplitude,
               Frequency = frequency,
               Type = SignalGeneratorType.Sin
            };

            var wave_sinusoidal_MEDIO = new SignalGenerator()
            {
               Gain = amplitude,
               Frequency = frequency * 2,
               Type = SignalGeneratorType.Sin
            };

            var wave_sinusoidal_agudo = new SignalGenerator()
            {
               Gain = amplitude,
               Frequency = frequency * 4,
               Type = SignalGeneratorType.Sin
            };


            // create a mixer where the audio will play 
            using (var waveOut = new WaveOutEvent())
            {
               //asig de wave to mixer then read wave
               waveOut.Init(wave_sinusoidal_MAYOR.Take(TimeSpan.FromSeconds(duration_time)));

               // play the audio
               waveOut.Play();

               // wait until the sound reproduction end
               while (waveOut.PlaybackState == PlaybackState.Playing)
               {
                  System.Threading.Thread.Sleep(100); // wait in small intervals 
               }
            }
         }
      }
      public static void updateFrecuencie(int note)
      {
         frequency = notes[note];
      }
   }
}
