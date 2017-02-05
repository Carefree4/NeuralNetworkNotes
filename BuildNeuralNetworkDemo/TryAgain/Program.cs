using System;
using System.Linq;
using System.Text;

namespace TryAgain
{
    internal class Program
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

    internal class NeuralNetwork
    {
        // [input bias][output bias]
        private double[][] biasWeights;

        // [hidden nuron][output neuron]
        private double[][] hiddenOutputWeights;

        // [input neuron][hidden nuron]
        private double[][] inputHiddenWeights;

        private readonly double[][] inputs;
        private readonly int numHidden = 3;
        private readonly int numInputs = 3;
        private readonly int numOutputs = 3;
        private readonly Random randy = new Random(0);
        private readonly double[][] targetOutputs;

        public NeuralNetwork(double[][] data, double[][] target)
        {
            inputs = new double[data.Length][];
            for (var i = 0; i < data.Length; i++)
                inputs[i] = new double[numInputs];

            Array.Copy(data, inputs, data.Length);

            targetOutputs = new double[target.Length][];
            for (var i = 0; i < target.Length; i++)
                targetOutputs[i] = new double[numOutputs];
            Array.Copy(target, targetOutputs, target.Length);

            InitWeights();
            CalculateOutput();
            CalculateOutput();
        }

        private void BackPropagate(double[][] outputs)
        {
            var outputError = new double[outputs.Length][];
            for (var i = 0; i < outputs.Length; i++)
            {
                outputError[i] = new double[outputs[i].Length];
                for (var j = 0; j < outputs[i].Length; j++)
                    outputError[i][j] = targetOutputs[i][j] - outputs[i][j];
            }
        }

        private double[][] CalculateOutput()
        {
            // Init output array
            var outputs = new double[targetOutputs.Length][];
            // var softmaxOutput = new double[targetOutputs.Length][];
            for (var i = 0; i < outputs.Length; i++)
                outputs[i] = new double[numOutputs];

            // Input x Hidden Neuron
            var hidden = new double[inputs.Length][];
            for (var i = 0; i < hidden.Length; i++)
                hidden[i] = new double[numHidden];

            for (var i = 0; i < inputs.Length; i++)
            for (var j = 0; j < hidden[i].Length; j++)
            {
                double calc = 0;
                for (var k = 0; k < inputs[i].Length; k++)
                {
                    calc += inputs[i][k];

                    // multi by weight
                    calc *= inputHiddenWeights[j][k];
                    calc += biasWeights[0][k];
                }
                // activation function
                hidden[i][j] = Sigmoid(calc);
            }

            var preActivation = new double[outputs.Length][];
            for (var i = 0; i < hidden.Length; i++)
            for (var j = 0; j < outputs[i].Length; j++)
            {
                preActivation[i] = new double[outputs[i].Length];
                var calc = 0.0;
                for (var k = 0; k < hidden[i].Length; k++)
                {
                    calc += hidden[i][k];

                    // multi by weight
                    calc *= hiddenOutputWeights[j][k];
                    calc += biasWeights[1][k];
                }
                preActivation[i][j] = calc;
                outputs[i][j] = Sigmoid(calc);
            }

            Console.Out.WriteLine("\nInput to Hidden Weights: ");
            foreach (var b in inputHiddenWeights)
            {
                foreach (var d in b)
                    Console.Out.Write($"{d,-10:0.######} ");
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("\nHidden to Output Weights: ");
            foreach (var b in hiddenOutputWeights)
            {
                foreach (var d in b)
                    Console.Out.Write($"{d,-10:0.######} ");
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("\nBias Weights: ");
            foreach (var b in biasWeights)
            {
                foreach (var d in b)
                    Console.Out.Write($"{d,-10:0.######} ");
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("\nOutputs: ");
            foreach (var output in outputs)
            {
                foreach (var d in output)
                    Console.Out.Write($"{d,-10:0.######} ");
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine("\nTarget: ");
            foreach (var output in targetOutputs)
            {
                foreach (var d in output)
                    Console.Out.Write($"{d,-10:0.######} ");
                Console.Out.WriteLine();
            }

            Console.OutputEncoding = Encoding.Unicode;
            var error = new double[targetOutputs.Length][];
            var deltaOutputLayer = new double[targetOutputs.Length][];
            Console.Out.WriteLine("\nError: ");
            for (var i = 0; i < targetOutputs.Length; i++)
            {
                error[i] = new double[targetOutputs[i].Length];
                deltaOutputLayer[i] = new double[targetOutputs[i].Length];
                for (var j = 0; j < targetOutputs[i].Length; j++)
                {
                    error[i][j] = targetOutputs[i][j] - outputs[i][j];
                    deltaOutputLayer[i][j] = GetDelta(preActivation[i][j], error[i][j]);

                    Console.ForegroundColor = Math.Abs(error[i][j]) < 0.5 ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Out.Write($"{error[i][j],-10:0.######} ");
                    Console.Out.Write($"(Δsum = {deltaOutputLayer[i][j],-10:0.######}) ");
                }
                Console.Out.WriteLine();
            }
            Console.ResetColor();
            // https://stevenmiller888.github.io/mind-how-to-build-a-neural-network-part-2/


            var learnRate = 0.7;

            
            // Outputs or preActivation?
            var delta = MatrixMultiply(error, Transpose(preActivation));
            var changes = ScalarMultiply(MatrixMultiply(delta, Transpose(hidden)), learnRate);

            for (int i = 0; i < hiddenOutputWeights.Length; i++)
            {
                for (int j = 0; j < hiddenOutputWeights[i].Length; j++)
                {
                    hiddenOutputWeights[i][j] += changes[i][j];
                }
            }

            return outputs;
        }

        // Sum is the pre-activation function sum
        // Error = y - y*
        private static double GetDelta(double sum, double error) => SigmoidPrime(sum) * error;

        private static double SigmoidPrime(double sum) => Sigmoid(sum) * (1 - Sigmoid(sum));

        // Multiply x to every row
        private static double[][] MatrixMultiply(double[][] x, double[][] y)
        {
            var newArray = new double[y.Length][];
            for (int i = 0; i < y.Length; i++)
            {
                newArray[i] = new double[y[i].Length];
                for (int j = 0; j < y[i].Length; j++)
                {
                    newArray[i][j] = x[i][j] * y[i][j];
                }
            }
            return newArray;
        }

        private static double[][] ScalarMultiply(double[][] x, double scalar)
        {
            var newArr = new double[x.Length][];

            for (var i = 0; i < x.Length; i++)
            {
                newArr[i] = new double[x[i].Length];
                for (var j = 0; j < x[i].Length; j++)
                {
                    newArr[i][j] = x[i][j] * scalar;
                }
            }

            return newArr;
        }

        // May not be jagged
        // May not be null
        private static double[][] Transpose(double[][] x)
        {
            var newArr = new double[x[0].Length][];

            for (var i = 0; i < newArr.Length; i++)
            {
                newArr[i] = new double[x.Length];
                for (var j = 0; j < newArr[i].Length; j++)
                    newArr[i][j] = x[j][i];
            }


            return newArr;
        }

        // switch to sigmoid because range is 0 < y < 1
        private static double Sigmoid(double d) => 1 / (1 + Math.Exp(d));

        /*private static double[] Softmax(double[] d)
        {
            var max = d[0];
            max = d.Concat(new[] {max}).Max();

            var scale = d.Sum(t => Math.Exp(t - max));

            var result = new double[d.Length];
            for (int i = 0; i < d.Length; i++)
            {
                result[i] = Math.Exp(d[i] - max) / scale;
            }
            return result;
        }*/

        private void InitWeights()
        {
            var high = 1.0;
            var low = 0.0;

            inputHiddenWeights = new double[numInputs][];
            for (var i = 0; i < inputHiddenWeights.Length; i++)
            {
                inputHiddenWeights[i] = new double[numHidden];
                for (var j = 0; j < inputHiddenWeights[i].Length; j++)
                    inputHiddenWeights[i][j] = (high - low) * randy.NextDouble() + low;
            }

            hiddenOutputWeights = new double[numHidden][];
            for (var i = 0; i < hiddenOutputWeights.Length; i++)
            {
                hiddenOutputWeights[i] = new double[numOutputs];
                for (var j = 0; j < hiddenOutputWeights[0].Length; j++)
                    hiddenOutputWeights[i][j] = (high - low) * randy.NextDouble() + low;
            }

            // 0 = hidden, 1 = output
            biasWeights = new double[2][];
            biasWeights[0] = new double[numHidden];
            biasWeights[1] = new double[numOutputs];
            foreach (var t in biasWeights)
                for (var j = 0; j < t.Length; j++)
                    t[j] = (high - low) * randy.NextDouble() + low;
        }

        public override string ToString()
        {
            var s = string.Empty;

            s += "Input to hidden weights (input x hiddens):\n";

            foreach (var inputHiddenWeight in inputHiddenWeights)
            {
                s = inputHiddenWeight.Aggregate(s, (current, d) => current + $"{d} ");
                s += $"\n";
            }

            s += "\n\nHidden to output (hidden x output):\n";
            foreach (var doubles in hiddenOutputWeights)
            {
                s = doubles.Aggregate(s, (current, d) => current + $"{d} ");
                s += $"\n";
            }

            return s;
        }
    }
}