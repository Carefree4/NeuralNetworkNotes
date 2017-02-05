using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace SingleOutput
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var data = new[]
            {
                new double[]  {0, 0},
                new double[]  {0, 1},
                new double[]  {1, 0},
                new double[]  {1, 1}
            };
            double[] answers = {0, 1, 1, 0};

            var n = new NeuralNetwork(data, answers);
        }
    }

    // Can only do one output
    internal class NeuralNetwork
    {
        private readonly double[][] data;
        private double[] hiddenOutputWeights;

        // [numInputs][numHidden]
        private double[][] inputHiddenWeights;

        private readonly int numHidden = 3;
        private readonly double[] target;

        public NeuralNetwork(double[][] data, double[] target)
        {
            if (data.Length != target.Length)
            {
                throw new Exception("Data/target length mismatch");
            }

            this.data = new double[data.Length][];
            Array.Copy(data, this.data, data.Length);

            this.target = new double[target.Length];
            Array.Copy(target, this.target, target.Length);
            InitArrays();

            for (var i = 0; i < data.Length; i++)
            {
                Console.Out.WriteLine($"{data[i][0]}, {data[i][1]} = {Test(data[i])}");
            }
            var epoh = 20;
            for (int i = 0; i < epoh; i++)
            {
                Learn(data, target);
            }
            Console.Out.WriteLine();
            for (var i = 0; i < data.Length; i++)
            {
                Console.Out.WriteLine($"{data[i][0]}, {data[i][1]} = {Test(data[i])}");
            }

            foreach (var inputHiddenWeight in inputHiddenWeights)
            {
                foreach (var d in inputHiddenWeight)
                {
                    Console.Out.Write($"{d} | ");
                }
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine();
            foreach (var hiddenOutputWeight in hiddenOutputWeights)
            {
                Console.Out.Write($"{hiddenOutputWeight} | ");
            }

            Console.ReadLine();
        }

        private double Test(double[] input)
        {
            // Input > hidden
            var output = new double[numHidden];
            for (var i = 0; i < output.Length; i++)
            {
                // Multiply by weights
                for (var j = 0; j < input.Length; j++)
                {
                    output[i] += input[j] * inputHiddenWeights[j][i];
                }

                //Proform activation
                output[i] = Sigmoid(output[i]);
            }

            // Hidden > output
            var result = output.Select((t, i) => t * hiddenOutputWeights[i]).Sum();

            // Proform activation for output node
            result = Sigmoid(result);

            return result;
        }

        private void Learn(double[][] input, double[] target)
        {
            var hiddenActivation = new double[input.Length][];
            var preHiddenActivation = new double[input.Length][];
            var results = new double[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                hiddenActivation[i] = new double[numHidden];
                preHiddenActivation[i] = new double[numHidden];
            }

            for (var i = 0; i < input.Length; i++)
            {
                // Input > hidden
                var output = new double[numHidden];
                for (var j = 0; j < output.Length; j++)
                {
                    // Multiply by weights
                    for (var k = 0; k < input[i].Length; k++)
                    {
                        output[j] += input[i][k] * inputHiddenWeights[k][j];
                    }
                    //Proform activation
                    preHiddenActivation[i][j] = output[j];
                    output[j] = Sigmoid(output[j]);
                }
                Array.Copy(output, hiddenActivation[i], output.Length);

                // Hidden > output
                var result = output.Select((t, x) => t * hiddenOutputWeights[x]).Sum();

                // Proform activation for output node
                results[i] = Sigmoid(result);
            }
            
            var hiddenOutputDelta = new double[numHidden];
            for (int i = 0; i < hiddenOutputDelta.Length; i++)
            {
                var preOutputActivation = Transpose(hiddenActivation)[i].Sum();
                hiddenOutputDelta[i] = GetDelta(target[i], preOutputActivation, results[i]);
            }

            var hiddenOutputGradient = MatrixMultiply(Transpose(hiddenActivation), hiddenOutputDelta);

            // Delta2 = Delta3 * (weight^(2))^t * f'(z^(2))
            // ugh fine hard code it is
            // deltaTwo = hiddenOutputWeights.Zip(hiddenOutputDelta, (x, y) => x * y);
            var deltaTwo = new double[numHidden];
            for (int i = 0; i < numHidden; i++)
            {
                deltaTwo[i] = hiddenOutputWeights[i] * hiddenOutputDelta[i];
            }
            
            var tempSum = new double[numHidden];
            for (int i = 0; i < preHiddenActivation.Length; i++)
            {
                for (int j = 0; j < preHiddenActivation[i].Length; j++)
                {
                    tempSum[j] += preHiddenActivation[i][j];
                }
            }

            for (int i = 0; i < tempSum.Length; i++)
            {
                tempSum[i] = SigmoidPrime(tempSum[i]);
            }
            
            for (int i = 0; i < numHidden; i++)
            {
                deltaTwo[i] *= tempSum[i];
            }

            var inputHiddenGradient = new double[Transpose(input).Length][];
            for (int i = 0; i < inputHiddenGradient.Length; i++)
            {
                inputHiddenGradient[i] = new double[numHidden];
                for (int j = 0; j < inputHiddenGradient[i].Length; j++)
                {
                    inputHiddenGradient[i][j] = Transpose(input)[i][j] * deltaTwo[j];
                }
            }

            var learnRate = 1;

            for (int i = 0; i < inputHiddenWeights.Length; i++)
            {
                for (int j = 0; j < inputHiddenWeights[i].Length; j++)
                {
                    inputHiddenWeights[i][j] += inputHiddenGradient[i][j] * learnRate;
                }
            }

            for (int i = 0; i < hiddenOutputWeights.Length; i++)
            {
                hiddenOutputWeights[i] += hiddenOutputGradient[i] * learnRate;
            }

        }

        private void InitArrays()
        {
            var randy = new Random(0);

            var high = 1.0;
            var low = -1.0;

            inputHiddenWeights = new double[data[0].Length][];
            for (var i = 0; i < inputHiddenWeights.Length; i++)
            {
                inputHiddenWeights[i] = new double[numHidden];
                for (var j = 0; j < inputHiddenWeights[i].Length; j++)
                {
                    inputHiddenWeights[i][j] = (high - low) * randy.NextDouble() + low;
                }
            }

            hiddenOutputWeights = new double[numHidden];
            for (var i = 0; i < hiddenOutputWeights.Length; i++)
            {
                hiddenOutputWeights[i] = (high - low) * randy.NextDouble() + low;
            }
        }

        #region Math Functions

        private static double[] MatrixMultiply(double[][] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("Array size mismatch");
            }

            var results = new double[y.Length];

            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < x[i].Length; j++)
                {
                    results[i] += y[i] * x[i][j];
                }
            }

            return results;
        }

        private static double[] MatrixMultiply(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("Array size mismatch");
            }

            var results = new double[y.Length];

            for (int i = 0; i < y.Length; i++)
            {
                results[i] = y[i] * x[i];
            }

            return results;
        }

        private static double[][] ScalarMultiply(double[][] x, double scalar)
        {
            var result = new double[x.Length][];
            for (var i = 0; i < x.Length; i++)
            {
                result[i] = new double[x[i].Length];
                for (var j = 0; j < x[i].Length; j++)
                {
                    result[i][j] *= scalar;
                }
            }

            return result;
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
        
        // delta = -(y - y*)f'(z)
        private static double GetDelta(double target, double result, double output) => -(target - result) * SigmoidPrime(output);

        // Gradient of Sigmoid
        private static double SigmoidPrime(double x) => Math.Exp(-x) / Math.Pow(1 + Math.Exp(-x), 2);

        private static double Sigmoid(double d) => 1 / (1 + Math.Exp(d));

        #endregion
    }
}