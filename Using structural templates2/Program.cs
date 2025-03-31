using System;

namespace BridgePatternExample
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
        void RenderRectangle(float width, float height);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Отрисовка круга с радиусом {radius} в векторном формате");
        }

        public void RenderRectangle(float width, float height)
        {
            Console.WriteLine($"Отрисовка прямоугольника с шириной {width} и высотой {height} в векторном формате");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Отрисовка круга с радиусом {radius} в растровом формате");
        }

        public void RenderRectangle(float width, float height)
        {
            Console.WriteLine($"Отрисовка прямоугольника с шириной {width} и высотой {height} в растровом формате");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }
    }

    public class Rectangle : Shape
    {
        private float width;
        private float height;

        public Rectangle(IRenderer renderer, float width, float height) : base(renderer)
        {
            this.width = width;
            this.height = height;
        }

        public override void Draw()
        {
            renderer.RenderRectangle(width, height);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Выберите тип рендерера (1 - Векторный, 2 - Растровый):");
            int rendererChoice = GetValidInput(1, 2); 
            IRenderer renderer = rendererChoice == 1 ? new VectorRenderer() : new RasterRenderer();

            Console.WriteLine("Выберите фигуру (1 - Круг, 2 - Прямоугольник):");
            int shapeChoice = GetValidInput(1, 2); 
            Shape shape = null;

            if (shapeChoice == 1)
            {
                Console.WriteLine("Введите радиус круга:");
                float radius = GetValidFloatInput(); 
                shape = new Circle(renderer, radius);
            }
            else if (shapeChoice == 2)
            {
                Console.WriteLine("Введите ширину прямоугольника:");
                float width = GetValidFloatInput(); 
                Console.WriteLine("Введите высоту прямоугольника:");
                float height = GetValidFloatInput(); 
                shape = new Rectangle(renderer, width, height);
            }

            shape?.Draw();
        }

        static int GetValidInput(int min, int max)
        {
            int choice;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine($"Введите число от {min} до {max}:");
                }
            }
        }

        static float GetValidFloatInput()
        {
            float value;
            while (true)
            {
                string input = Console.ReadLine();
                if (float.TryParse(input, out value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Введите корректное положительное число:");
                }
            }
        }
    }
}
