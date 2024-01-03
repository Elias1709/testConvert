// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using DlibDotNet;
using OpenCvSharp;



class Program
{
    static void Main()
    {
        // Ruta del archivo de video
        string rutaVideo = "D:/ProyectosNet/testConvert/videos/CR7.mp4";

        // Ruta de la carpeta para guardar los frames
        string carpetaFrames = "D:/ProyectosNet/testConvert/videos/conversion";

        // Abre el archivo de video
        using (VideoCapture capture = new VideoCapture(rutaVideo))
        {
            if (!capture.IsOpened())
            {
                Console.WriteLine("No se pudo abrir el archivo de video.");
                return;
            }

            // Asegura que la carpeta de frames exista
            System.IO.Directory.CreateDirectory(carpetaFrames);

            // Obtiene la frecuencia de cuadros por segundo del video
            double fps = capture.Fps;

            // Calcula el número total de frames
            int totalFrames = (int)(fps * 10); // 10 segundos

            Console.WriteLine($"Extrayendo {totalFrames} frames...");

            // Lee y guarda cada frame
            for (int i = 0; i < totalFrames; i++)
            {
                using (Mat frame = new Mat())
                {
                    capture.Read(frame);

                    if (frame.Empty())
                        break;

                    // Guarda el frame como una imagen
                    string nombreArchivo = $"frame_{i:D5}.png"; // Por ejemplo, frame_00000.png
                    string rutaFrame = System.IO.Path.Combine(carpetaFrames, nombreArchivo);

                    Cv2.ImWrite(rutaFrame, frame);

                    Console.WriteLine($"Guardado: {rutaFrame}");
                }
            }

            Console.WriteLine("Extracción de frames completada.");
        }
    }
}


