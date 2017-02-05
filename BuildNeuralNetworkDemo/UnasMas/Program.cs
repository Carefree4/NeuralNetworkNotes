using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnasMas
{
    class Program
    {
        private static void Main(string[] args)
        {
            var data = new[]
            {
                new double[] {0, 0, 0},
                new double[] {0, 0, 1},
                new double[] {0, 1, 0},
                new double[] {1, 0, 0},
                new double[] {1, 0, 1},
                new double[] {1, 1, 0},
                new double[] {1, 1, 1}
            };
            var answers = new[]
            {
                new double[] {0, 0, 1},
                new double[] {0, 1, 0},
                new double[] {1, 0, 0},
                new double[] {1, 0, 1},
                new double[] {1, 1, 0},
                new double[] {1, 1, 1},
                new double[] {0, 0, 0}
            };

            var n = new NeuralNetwork(data, answers);
            // Console.WriteLine(n);
            Console.ReadKey();
        }
    }
}
