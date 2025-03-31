using System;
using System.IO;

namespace AdapterPatternExample
{
    public interface IImageProcessor
    {
        void ProcessImage(string filePath);
    }

    public class ImageProcessor : IImageProcessor
    {
        public void ProcessImage(string filePath)
        {
            Console.WriteLine($"Обработка файла с помощью ImageProcessor: {filePath}");
        }
    }

    public class GraphicFileProcessor
    {
        public void ProcessFile(string filePath)
        {
            Console.WriteLine($"Обработка файла с помощью GraphicFileProcessor: {filePath}");
        }
    }

    public class GraphicFileAdapter : IImageProcessor
    {
        private readonly GraphicFileProcessor graphicFileProcessor;

        public GraphicFileAdapter(GraphicFileProcessor graphicFileProcessor)
        {
            this.graphicFileProcessor = graphicFileProcessor;
        }

        public void ProcessImage(string filePath)
        {
            graphicFileProcessor.ProcessFile(filePath);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу для обработки:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден. Пожалуйста, проверьте путь и попробуйте снова.");
                return;
            }

            IImageProcessor imageProcessor = new ImageProcessor();
            imageProcessor.ProcessImage(filePath);

            GraphicFileProcessor graphicFileProcessor = new GraphicFileProcessor();
            IImageProcessor graphicAdapter = new GraphicFileAdapter(graphicFileProcessor);
            graphicAdapter.ProcessImage(filePath);
        }
    }
}
