using System;
using System.Linq;

namespace BuildNeuralNetworkDemo
{
    internal class NeuralNetworkProgram
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("\nBegin Build 2014 neural network demo");
            Console.WriteLine("\nData is the famous Iris flower set.");
            Console.WriteLine("Data is sepal length, sepal width, petal length, petal width -> iris species");
            Console.WriteLine("Iris setosa = 0 0 1, Iris versicolor = 0 1 0, Iris virginica = 1 0 0 ");
            Console.WriteLine("The goal is to predict species from sepal length, width, petal length, width\n");

            Console.WriteLine("Raw data resembles:\n");
            Console.WriteLine(" 5.1, 3.5, 1.4, 0.2, Iris setosa");
            Console.WriteLine(" 7.0, 3.2, 4.7, 1.4, Iris versicolor");
            Console.WriteLine(" 6.3, 3.3, 6.0, 2.5, Iris virginica");
            Console.WriteLine(" ......\n");

            #region Declare Data

            var allData = new double[150][];
            allData[0] = new[] {5.1, 3.5, 1.4, 0.2, 0, 0, 1}; // sepal length, width, petal length, width
            allData[1] = new[] {4.9, 3.0, 1.4, 0.2, 0, 0, 1}; // Iris setosa = 0 0 1
            allData[2] = new[] {4.7, 3.2, 1.3, 0.2, 0, 0, 1}; // Iris versicolor = 0 1 0
            allData[3] = new[] {4.6, 3.1, 1.5, 0.2, 0, 0, 1}; // Iris virginica = 1 0 0
            allData[4] = new[] {5.0, 3.6, 1.4, 0.2, 0, 0, 1};
            allData[5] = new[] {5.4, 3.9, 1.7, 0.4, 0, 0, 1};
            allData[6] = new[] {4.6, 3.4, 1.4, 0.3, 0, 0, 1};
            allData[7] = new[] {5.0, 3.4, 1.5, 0.2, 0, 0, 1};
            allData[8] = new[] {4.4, 2.9, 1.4, 0.2, 0, 0, 1};
            allData[9] = new[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};

            allData[10] = new[] {5.4, 3.7, 1.5, 0.2, 0, 0, 1};
            allData[11] = new[] {4.8, 3.4, 1.6, 0.2, 0, 0, 1};
            allData[12] = new[] {4.8, 3.0, 1.4, 0.1, 0, 0, 1};
            allData[13] = new[] {4.3, 3.0, 1.1, 0.1, 0, 0, 1};
            allData[14] = new[] {5.8, 4.0, 1.2, 0.2, 0, 0, 1};
            allData[15] = new[] {5.7, 4.4, 1.5, 0.4, 0, 0, 1};
            allData[16] = new[] {5.4, 3.9, 1.3, 0.4, 0, 0, 1};
            allData[17] = new[] {5.1, 3.5, 1.4, 0.3, 0, 0, 1};
            allData[18] = new[] {5.7, 3.8, 1.7, 0.3, 0, 0, 1};
            allData[19] = new[] {5.1, 3.8, 1.5, 0.3, 0, 0, 1};

            allData[20] = new[] {5.4, 3.4, 1.7, 0.2, 0, 0, 1};
            allData[21] = new[] {5.1, 3.7, 1.5, 0.4, 0, 0, 1};
            allData[22] = new[] {4.6, 3.6, 1.0, 0.2, 0, 0, 1};
            allData[23] = new[] {5.1, 3.3, 1.7, 0.5, 0, 0, 1};
            allData[24] = new[] {4.8, 3.4, 1.9, 0.2, 0, 0, 1};
            allData[25] = new[] {5.0, 3.0, 1.6, 0.2, 0, 0, 1};
            allData[26] = new[] {5.0, 3.4, 1.6, 0.4, 0, 0, 1};
            allData[27] = new[] {5.2, 3.5, 1.5, 0.2, 0, 0, 1};
            allData[28] = new[] {5.2, 3.4, 1.4, 0.2, 0, 0, 1};
            allData[29] = new[] {4.7, 3.2, 1.6, 0.2, 0, 0, 1};

            allData[30] = new[] {4.8, 3.1, 1.6, 0.2, 0, 0, 1};
            allData[31] = new[] {5.4, 3.4, 1.5, 0.4, 0, 0, 1};
            allData[32] = new[] {5.2, 4.1, 1.5, 0.1, 0, 0, 1};
            allData[33] = new[] {5.5, 4.2, 1.4, 0.2, 0, 0, 1};
            allData[34] = new[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};
            allData[35] = new[] {5.0, 3.2, 1.2, 0.2, 0, 0, 1};
            allData[36] = new[] {5.5, 3.5, 1.3, 0.2, 0, 0, 1};
            allData[37] = new[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};
            allData[38] = new[] {4.4, 3.0, 1.3, 0.2, 0, 0, 1};
            allData[39] = new[] {5.1, 3.4, 1.5, 0.2, 0, 0, 1};

            allData[40] = new[] {5.0, 3.5, 1.3, 0.3, 0, 0, 1};
            allData[41] = new[] {4.5, 2.3, 1.3, 0.3, 0, 0, 1};
            allData[42] = new[] {4.4, 3.2, 1.3, 0.2, 0, 0, 1};
            allData[43] = new[] {5.0, 3.5, 1.6, 0.6, 0, 0, 1};
            allData[44] = new[] {5.1, 3.8, 1.9, 0.4, 0, 0, 1};
            allData[45] = new[] {4.8, 3.0, 1.4, 0.3, 0, 0, 1};
            allData[46] = new[] {5.1, 3.8, 1.6, 0.2, 0, 0, 1};
            allData[47] = new[] {4.6, 3.2, 1.4, 0.2, 0, 0, 1};
            allData[48] = new[] {5.3, 3.7, 1.5, 0.2, 0, 0, 1};
            allData[49] = new[] {5.0, 3.3, 1.4, 0.2, 0, 0, 1};

            allData[50] = new[] {7.0, 3.2, 4.7, 1.4, 0, 1, 0};
            allData[51] = new[] {6.4, 3.2, 4.5, 1.5, 0, 1, 0};
            allData[52] = new[] {6.9, 3.1, 4.9, 1.5, 0, 1, 0};
            allData[53] = new[] {5.5, 2.3, 4.0, 1.3, 0, 1, 0};
            allData[54] = new[] {6.5, 2.8, 4.6, 1.5, 0, 1, 0};
            allData[55] = new[] {5.7, 2.8, 4.5, 1.3, 0, 1, 0};
            allData[56] = new[] {6.3, 3.3, 4.7, 1.6, 0, 1, 0};
            allData[57] = new[] {4.9, 2.4, 3.3, 1.0, 0, 1, 0};
            allData[58] = new[] {6.6, 2.9, 4.6, 1.3, 0, 1, 0};
            allData[59] = new[] {5.2, 2.7, 3.9, 1.4, 0, 1, 0};

            allData[60] = new[] {5.0, 2.0, 3.5, 1.0, 0, 1, 0};
            allData[61] = new[] {5.9, 3.0, 4.2, 1.5, 0, 1, 0};
            allData[62] = new[] {6.0, 2.2, 4.0, 1.0, 0, 1, 0};
            allData[63] = new[] {6.1, 2.9, 4.7, 1.4, 0, 1, 0};
            allData[64] = new[] {5.6, 2.9, 3.6, 1.3, 0, 1, 0};
            allData[65] = new[] {6.7, 3.1, 4.4, 1.4, 0, 1, 0};
            allData[66] = new[] {5.6, 3.0, 4.5, 1.5, 0, 1, 0};
            allData[67] = new[] {5.8, 2.7, 4.1, 1.0, 0, 1, 0};
            allData[68] = new[] {6.2, 2.2, 4.5, 1.5, 0, 1, 0};
            allData[69] = new[] {5.6, 2.5, 3.9, 1.1, 0, 1, 0};

            allData[70] = new[] {5.9, 3.2, 4.8, 1.8, 0, 1, 0};
            allData[71] = new[] {6.1, 2.8, 4.0, 1.3, 0, 1, 0};
            allData[72] = new[] {6.3, 2.5, 4.9, 1.5, 0, 1, 0};
            allData[73] = new[] {6.1, 2.8, 4.7, 1.2, 0, 1, 0};
            allData[74] = new[] {6.4, 2.9, 4.3, 1.3, 0, 1, 0};
            allData[75] = new[] {6.6, 3.0, 4.4, 1.4, 0, 1, 0};
            allData[76] = new[] {6.8, 2.8, 4.8, 1.4, 0, 1, 0};
            allData[77] = new[] {6.7, 3.0, 5.0, 1.7, 0, 1, 0};
            allData[78] = new[] {6.0, 2.9, 4.5, 1.5, 0, 1, 0};
            allData[79] = new[] {5.7, 2.6, 3.5, 1.0, 0, 1, 0};

            allData[80] = new[] {5.5, 2.4, 3.8, 1.1, 0, 1, 0};
            allData[81] = new[] {5.5, 2.4, 3.7, 1.0, 0, 1, 0};
            allData[82] = new[] {5.8, 2.7, 3.9, 1.2, 0, 1, 0};
            allData[83] = new[] {6.0, 2.7, 5.1, 1.6, 0, 1, 0};
            allData[84] = new[] {5.4, 3.0, 4.5, 1.5, 0, 1, 0};
            allData[85] = new[] {6.0, 3.4, 4.5, 1.6, 0, 1, 0};
            allData[86] = new[] {6.7, 3.1, 4.7, 1.5, 0, 1, 0};
            allData[87] = new[] {6.3, 2.3, 4.4, 1.3, 0, 1, 0};
            allData[88] = new[] {5.6, 3.0, 4.1, 1.3, 0, 1, 0};
            allData[89] = new[] {5.5, 2.5, 4.0, 1.3, 0, 1, 0};

            allData[90] = new[] {5.5, 2.6, 4.4, 1.2, 0, 1, 0};
            allData[91] = new[] {6.1, 3.0, 4.6, 1.4, 0, 1, 0};
            allData[92] = new[] {5.8, 2.6, 4.0, 1.2, 0, 1, 0};
            allData[93] = new[] {5.0, 2.3, 3.3, 1.0, 0, 1, 0};
            allData[94] = new[] {5.6, 2.7, 4.2, 1.3, 0, 1, 0};
            allData[95] = new[] {5.7, 3.0, 4.2, 1.2, 0, 1, 0};
            allData[96] = new[] {5.7, 2.9, 4.2, 1.3, 0, 1, 0};
            allData[97] = new[] {6.2, 2.9, 4.3, 1.3, 0, 1, 0};
            allData[98] = new[] {5.1, 2.5, 3.0, 1.1, 0, 1, 0};
            allData[99] = new[] {5.7, 2.8, 4.1, 1.3, 0, 1, 0};

            allData[100] = new[] {6.3, 3.3, 6.0, 2.5, 1, 0, 0};
            allData[101] = new[] {5.8, 2.7, 5.1, 1.9, 1, 0, 0};
            allData[102] = new[] {7.1, 3.0, 5.9, 2.1, 1, 0, 0};
            allData[103] = new[] {6.3, 2.9, 5.6, 1.8, 1, 0, 0};
            allData[104] = new[] {6.5, 3.0, 5.8, 2.2, 1, 0, 0};
            allData[105] = new[] {7.6, 3.0, 6.6, 2.1, 1, 0, 0};
            allData[106] = new[] {4.9, 2.5, 4.5, 1.7, 1, 0, 0};
            allData[107] = new[] {7.3, 2.9, 6.3, 1.8, 1, 0, 0};
            allData[108] = new[] {6.7, 2.5, 5.8, 1.8, 1, 0, 0};
            allData[109] = new[] {7.2, 3.6, 6.1, 2.5, 1, 0, 0};

            allData[110] = new[] {6.5, 3.2, 5.1, 2.0, 1, 0, 0};
            allData[111] = new[] {6.4, 2.7, 5.3, 1.9, 1, 0, 0};
            allData[112] = new[] {6.8, 3.0, 5.5, 2.1, 1, 0, 0};
            allData[113] = new[] {5.7, 2.5, 5.0, 2.0, 1, 0, 0};
            allData[114] = new[] {5.8, 2.8, 5.1, 2.4, 1, 0, 0};
            allData[115] = new[] {6.4, 3.2, 5.3, 2.3, 1, 0, 0};
            allData[116] = new[] {6.5, 3.0, 5.5, 1.8, 1, 0, 0};
            allData[117] = new[] {7.7, 3.8, 6.7, 2.2, 1, 0, 0};
            allData[118] = new[] {7.7, 2.6, 6.9, 2.3, 1, 0, 0};
            allData[119] = new[] {6.0, 2.2, 5.0, 1.5, 1, 0, 0};

            allData[120] = new[] {6.9, 3.2, 5.7, 2.3, 1, 0, 0};
            allData[121] = new[] {5.6, 2.8, 4.9, 2.0, 1, 0, 0};
            allData[122] = new[] {7.7, 2.8, 6.7, 2.0, 1, 0, 0};
            allData[123] = new[] {6.3, 2.7, 4.9, 1.8, 1, 0, 0};
            allData[124] = new[] {6.7, 3.3, 5.7, 2.1, 1, 0, 0};
            allData[125] = new[] {7.2, 3.2, 6.0, 1.8, 1, 0, 0};
            allData[126] = new[] {6.2, 2.8, 4.8, 1.8, 1, 0, 0};
            allData[127] = new[] {6.1, 3.0, 4.9, 1.8, 1, 0, 0};
            allData[128] = new[] {6.4, 2.8, 5.6, 2.1, 1, 0, 0};
            allData[129] = new[] {7.2, 3.0, 5.8, 1.6, 1, 0, 0};

            allData[130] = new[] {7.4, 2.8, 6.1, 1.9, 1, 0, 0};
            allData[131] = new[] {7.9, 3.8, 6.4, 2.0, 1, 0, 0};
            allData[132] = new[] {6.4, 2.8, 5.6, 2.2, 1, 0, 0};
            allData[133] = new[] {6.3, 2.8, 5.1, 1.5, 1, 0, 0};
            allData[134] = new[] {6.1, 2.6, 5.6, 1.4, 1, 0, 0};
            allData[135] = new[] {7.7, 3.0, 6.1, 2.3, 1, 0, 0};
            allData[136] = new[] {6.3, 3.4, 5.6, 2.4, 1, 0, 0};
            allData[137] = new[] {6.4, 3.1, 5.5, 1.8, 1, 0, 0};
            allData[138] = new[] {6.0, 3.0, 4.8, 1.8, 1, 0, 0};
            allData[139] = new[] {6.9, 3.1, 5.4, 2.1, 1, 0, 0};

            allData[140] = new[] {6.7, 3.1, 5.6, 2.4, 1, 0, 0};
            allData[141] = new[] {6.9, 3.1, 5.1, 2.3, 1, 0, 0};
            allData[142] = new[] {5.8, 2.7, 5.1, 1.9, 1, 0, 0};
            allData[143] = new[] {6.8, 3.2, 5.9, 2.3, 1, 0, 0};
            allData[144] = new[] {6.7, 3.3, 5.7, 2.5, 1, 0, 0};
            allData[145] = new[] {6.7, 3.0, 5.2, 2.3, 1, 0, 0};
            allData[146] = new[] {6.3, 2.5, 5.0, 1.9, 1, 0, 0};
            allData[147] = new[] {6.5, 3.0, 5.2, 2.0, 1, 0, 0};
            allData[148] = new[] {6.2, 3.4, 5.4, 2.3, 1, 0, 0};
            allData[149] = new[] {5.9, 3.0, 5.1, 1.8, 1, 0, 0};

            Console.WriteLine("\nFirst 6 rows of entire 150-item data set:");
            ShowMatrix(allData, 6, 1, true);

            #endregion

            #region Split into test and train data

            Console.WriteLine("Creating 80% training and 20% test data matrices");
            double[][] trainData = null;
            double[][] testData = null;
            MakeTrainTest(allData, out trainData, out testData);

            Console.WriteLine("\nFirst 5 rows of training data:");
            ShowMatrix(trainData, 5, 1, true);
            Console.WriteLine("First 3 rows of test data:");
            ShowMatrix(testData, 3, 1, true);

            #endregion

            #region Normalize data

            Normalize(trainData, new[] {0, 1, 2, 3});
            Normalize(testData, new[] {0, 1, 2, 3});

            Console.WriteLine("\nFirst 5 rows of normalized training data:");
            ShowMatrix(trainData, 5, 1, true);
            Console.WriteLine("First 3 rows of normalized test data:");
            ShowMatrix(testData, 3, 1, true);

            #endregion

            #region Init brain

            Console.WriteLine("\nCreating a 4-input, 7-hidden, 3-output neural network");
            Console.Write("Hard-coded tanh function for input-to-hidden and softmax for ");
            Console.WriteLine("hidden-to-output activations");
            const int numInput = 4;
            const int numHidden = 7;
            const int numOutput = 3;
            var nn = new NeuralNetwork(numInput, numHidden, numOutput);

            Console.WriteLine("\nInitializing weights and bias to small random values");
            nn.InitializeWeights();

            #endregion

            #region Train model

            var maxEpochs = 2000;
            var learnRate = 0.05;
            var momentum = 0.01;
            var weightDecay = 0.0001;
            Console.WriteLine("Setting maxEpochs = 2000, learnRate = 0.05, momentum = 0.01, weightDecay = 0.0001");
            Console.WriteLine("Training has hard-coded mean squared error < 0.020 stopping condition");

            Console.WriteLine("\nBeginning training using incremental back-propagation\n");
            nn.Train(trainData, maxEpochs, learnRate, momentum, weightDecay);
            Console.WriteLine("Training complete");

            #endregion

            #region Display model weights and stats

            var weights = nn.GetWeights();
            Console.WriteLine("Final neural network weights and bias values:");
            ShowVector(weights, 10, 3, true);

            var trainAcc = nn.Accuracy(trainData);
            Console.WriteLine("\nAccuracy on training data = " + trainAcc.ToString("F4"));

            #endregion

            #region Test Model

            var testAcc = nn.Accuracy(testData);
            Console.WriteLine("\nAccuracy on test data = " + testAcc.ToString("F4"));

            #endregion

            Console.WriteLine("\nEnd Build 2013 neural network demo\n");
            Console.ReadLine();
        } // Main

        // split the data
        private static void MakeTrainTest(double[][] allData, out double[][] trainData, out double[][] testData)
        {
            // split allData into 80% trainData and 20% testData
            var rnd = new Random(0);
            var totRows = allData.Length;
            var numCols = allData[0].Length;

            var trainRows = (int) (totRows * 0.80); // hard-coded 80-20 split
            var testRows = totRows - trainRows;

            trainData = new double[trainRows][];
            testData = new double[testRows][];

            var sequence = new int[totRows]; // create a random sequence of indexes
            for (var i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            for (var i = 0; i < sequence.Length; ++i)
            {
                var r = rnd.Next(i, sequence.Length);
                var tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }

            var si = 0; // index into sequence[]
            var j = 0; // index into trainData or testData

            for (; si < trainRows; ++si) // first rows to train data
            {
                trainData[j] = new double[numCols];
                var idx = sequence[si];
                Array.Copy(allData[idx], trainData[j], numCols);
                ++j;
            }

            j = 0; // reset to start of test data
            for (; si < totRows; ++si) // remainder to test data
            {
                testData[j] = new double[numCols];
                var idx = sequence[si];
                Array.Copy(allData[idx], testData[j], numCols);
                ++j;
            }
        } // MakeTrainTest

        /**
         * Need to normalize data so it doesnt make as much of an impact
         * 
         * Standard Deviation (sd) = (grab each value - mean)^2 
         * 
         * foreach column
         *  get the avrage value
         *  
         * 
         **/

        private static void Normalize(double[][] dataMatrix, int[] cols)
        {
            // normalize specified cols by computing (x - mean) / sd for each value
            foreach (var col in cols)
            {
                // Get mean
                // Gets sum of single column
                var sum = dataMatrix.Sum(t => t[col]);
                var mean = sum / dataMatrix.Length;

                // Calculate SD
                sum = dataMatrix.Sum(t => (t[col] - mean) * (t[col] - mean));
                var sd = Math.Sqrt(sum / (dataMatrix.Length - 1));

                // replace values with its normilized value
                foreach (var t in dataMatrix)
                    t[col] = (t[col] - mean) / sd;
                // Debug.WriteLine("");
                // Debug.WriteLine($"mean = {mean}, sd = {sd}");
            }
        }

        #region Display stuff

        private static void ShowVector(double[] vector, int valsPerRow, int decimals, bool newLine)
        {
            for (var i = 0; i < vector.Length; ++i)
            {
                if (i % valsPerRow == 0)
                    Console.WriteLine("");

                Console.Write(vector[i].ToString("F" + decimals).PadLeft(decimals + 4) + " ");
            }
            if (newLine)
                Console.WriteLine("");
        }

        private static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool newLine)
        {
            for (var i = 0; i < numRows; ++i)
            {
                Console.Write(i.ToString().PadLeft(3) + ": ");
                for (var j = 0; j < matrix[i].Length; ++j)
                {
                    if (matrix[i][j] >= 0.0)
                        Console.Write(" ");
                    else
                        Console.Write("-");

                    Console.Write(Math.Abs(matrix[i][j]).ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine)
                Console.WriteLine("");
        }

        #endregion
    } // class Program

    public class NeuralNetwork
    {
        private double[] ComputeOutputs(double[] inputValues)
        {
            /**
            * Gets the input values
            * Multiplies by weight
            * Adds input bias
            * Proforms input activation function
            * Multiplies by weight
            * Adds bias
            * Sends out
            */

            #region Check correct number of inputs

            if (inputValues.Length != numInput)
                throw new Exception("Bad inputValues array length");

            #endregion

            #region Init arrays

            var hiddenSums = new double[numHidden]; // hidden nodes sums scratch array
            var outputSums = new double[numOutput]; // output nodes sums
            for (var i = 0; i < inputValues.Length; ++i) // copy inputValues to inputs
                inputs[i] = inputValues[i];

            #endregion

            #region  takes all inputs, multiplies by weights, adds togeather

            for (var j = 0; j < numHidden; ++j) // compute i-h sum of weights * inputs
            for (var i = 0; i < numInput; ++i)
                hiddenSums[j] += inputs[i] * inputHiddenWeights[i][j]; // note +=

            #endregion

            #region add biases to input-to-hidden sums

            for (var i = 0; i < numHidden; ++i)
                hiddenSums[i] += hiddenBiases[i];

            #endregion

            #region apply input activation => tanh()

            for (var i = 0; i < numHidden; ++i)
                hiddenOutputs[i] = HyperTanFunction(hiddenSums[i]); // hard-coded

            #endregion

            #region Take all hiddens, multiply by hidden output weight, output

            for (var j = 0; j < numOutput; ++j) // compute hidden output sum of weights * hiddenOutputs
            for (var i = 0; i < numHidden; ++i)
                outputSums[j] += hiddenOutputs[i] * hiddenOutputWeights[i][j];

            #endregion

            #region Add biases

            for (var i = 0; i < numOutput; ++i) // add biases to input-to-hidden sums
                outputSums[i] += outputBiases[i];

            #endregion

            #region Apply output function => Softmax()

            // Softmax forces outputs to sum to 1
            var softOut = Softmax(outputSums); // softmax output activation does all outputs at once for efficiency
            Array.Copy(softOut, outputs, softOut.Length);

            #endregion

            #region Make return safe and return

            var retResult = new double[numOutput]; // could define a GetOutputs method instead
            Array.Copy(outputs, retResult, retResult.Length);
            return retResult;

            #endregion
        }

        #region Math Functions

        private static double HyperTanFunction(double x)
        {
            if (x < -20.0)
                return -1.0; // approximation is correct to 30 decimals
            if (x > 20.0)
                return 1.0;

            return Math.Tanh(x);
        }

        // Softmax => scales everything down to 1
        private static double[] Softmax(double[] outputSums)
        {
            // determine max output sum
            // does all output nodes at once so scale doesn't have to be re-computed each time
            var max = outputSums[0];
            foreach (var sum in outputSums)
                max = Math.Max(max, sum);

            // determine scaling factor -- sum of exp(each val - max)
            // Sum of e^(everything - max)
            var scale = outputSums.Sum(t => Math.Exp(t - max));

            var result = new double[outputSums.Length];
            for (var i = 0; i < outputSums.Length; ++i)
                result[i] = Math.Exp(outputSums[i] - max) / scale;

            return result; // now scaled so that xi sum to 1.0
        }

        #endregion

        // ----------------------------------------------------------------------------------------

        private void UpdateWeights(double[] targetValues, double learnRate, double momentum, double weightDecay)
        {
            // update the weights and biases using back-propagation, with target values, eta (learning rate),
            // alpha (momentum).
            // assumes that SetWeights and ComputeOutputs have been called and so all the internal arrays
            // and matrices have values (other than 0.0)

            // finding derivatives to find how "sensitive" it is to change?

            // Momentum (0 < m < 1) => how fast the gradient decent should be
            // Learn Rate => How much the weights will change per itteration?

            // Derivitive tells us if we should be higher or lower

            #region Input validation

            if (targetValues.Length != numOutput)
                throw new Exception("target values not same Length as output in UpdateWeights");

            #endregion

            #region 1. compute output gradient error
            // Why use derivative of softmax? => measures how sensitive it is to change
            for (var i = 0; i < outputGradientError.Length; ++i)
            {
                // derivative of softmax = (1 - y) * y (same as log-sigmoid)
                var derivative = (1 - outputs[i]) * outputs[i];
                // 'mean squared error version' includes (1-y)(y) derivative
                outputGradientError[i] = derivative * (targetValues[i] - outputs[i]);
            }
            #endregion

            #region 2. compute hidden gradients
            for (var i = 0; i < hiddenGrads.Length; ++i)
            {
                // derivative of tanh = (1 - y) * (1 + y)
                var derivative = (1 - hiddenOutputs[i]) * (1 + hiddenOutputs[i]);
                var sum = 0.0;
                for (var j = 0; j < numOutput; ++j) // each hidden new weight is the sum of numOutput terms
                {
                    var x = outputGradientError[j] * hiddenOutputWeights[i][j];
                    sum += x;
                }
                hiddenGrads[i] = derivative * sum;
            }
            #endregion

            #region 3a. update hidden weights

            // (gradients must be computed right-to-left but weights can be updated in any order)
            for (var i = 0; i < inputHiddenWeights.Length; ++i) // 0..2 (3)
            for (var j = 0; j < inputHiddenWeights[0].Length; ++j) // 0..3 (4)
            {
                var newWeight = learnRate * hiddenGrads[j] * inputs[i]; // compute the new delta
                inputHiddenWeights[i][j] += newWeight;
                    // update. note we use '+' instead of '-'. this can be very tricky.
                // now add momentum using previous delta. on first pass old value will be 0.0 but that's OK.
                inputHiddenWeights[i][j] += momentum * inputHiddenPrevWeightsDelta[i][j];
                inputHiddenWeights[i][j] -= weightDecay * inputHiddenWeights[i][j]; // weight decay
                inputHiddenPrevWeightsDelta[i][j] = newWeight; // don't forget to save the delta for momentum 
            }

            #endregion

            #region 3b. update hidden biases

            for (var i = 0; i < hiddenBiases.Length; ++i)
            {
                // Bias is like another nuron/input. The input value is always 1, adding a bias changes the weight
                var newWeight = learnRate * hiddenGrads[i] * 1.0; // t1.0 is constant input for bias; could leave out
                hiddenBiases[i] += newWeight;
                hiddenBiases[i] += momentum * hiddenPrevBiasesDelta[i]; // momentum
                hiddenBiases[i] -= weightDecay * hiddenBiases[i]; // weight decay
                hiddenPrevBiasesDelta[i] = newWeight; // don't forget to save the delta
            }

            #endregion

            // 4. update hidden-output weights
            for (var i = 0; i < hiddenOutputWeights.Length; ++i)
            for (var j = 0; j < hiddenOutputWeights[0].Length; ++j)
            {
                // see above: hiddenOutputs are inputs to the nn outputs
                var newWeight = learnRate * outputGradientError[j] * hiddenOutputs[i];
                hiddenOutputWeights[i][j] += newWeight;
                hiddenOutputWeights[i][j] += momentum * hiddenOutputPrevWeightsDelta[i][j]; // momentum
                hiddenOutputWeights[i][j] -= weightDecay * hiddenOutputWeights[i][j]; // weight decay
                hiddenOutputPrevWeightsDelta[i][j] = newWeight; // save
            }

            // 4b. update output biases
            for (var i = 0; i < outputBiases.Length; ++i)
            {
                var newWeight = learnRate * outputGradientError[i] * 1.0;
                outputBiases[i] += newWeight;
                outputBiases[i] += momentum * outputPrevBiasesDelta[i]; // momentum
                outputBiases[i] -= weightDecay * outputBiases[i]; // weight decay
                outputPrevBiasesDelta[i] = newWeight; // save
            }
        } // UpdateWeights

        // ----------------------------------------------------------------------------------------

        public void Train(double[][] trainData, int maxEprochs, double learnRate, double momentum,
            double weightDecay)
        {
            // train a back-prop style NN classifier using learning rate and momentum
            // weight decay reduces the magnitude of a weight value over time unless that value
            // is constantly increased

            var epoch = 0; // Epoch => One forward pass and one backwards pass of all training examples
            var inputValues = new double[numInput]; // inputs
            var outputValues = new double[numOutput]; // output values

            #region  sequence => order of traing data

            var sequence = new int[trainData.Length];
            for (var i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            #endregion

            while (epoch < maxEprochs)
            {
                #region If the MSE is less then 0.02, congrats, you have a trained set!

                var mse = MeanSquaredError(trainData);
                if (mse < 0.020)
                    break; // consider passing value in as parameter
                //if (mse < 0.001) break; // consider passing value in as parameter

                #endregion

                Shuffle(sequence); // visit each training data in random order
                for (var i = 0; i < trainData.Length; ++i)
                {
                    var idx = sequence[i];

                    #region Split data into inputs and outputs

                    // Copies row of data into input values
                    Array.Copy(trainData[idx], inputValues, numInput);
                    // Copies row of answers into output
                    Array.Copy(trainData[idx], numInput, outputValues, 0, numOutput);

                    #endregion

                    ComputeOutputs(inputValues); // copy inputValues in, compute outputs (store them internally)
                    UpdateWeights(outputValues, learnRate, momentum, weightDecay); // find better weights
                } // each training tuple
                ++epoch;
            }
        } // Train

        private static void Shuffle(int[] sequence)
        {
            for (var i = 0; i < sequence.Length; ++i)
            {
                var r = randy.Next(i, sequence.Length);
                var tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }

        // MSE => the sum of the (diffrence of y value and regression line)^2
        // Do this to enphisise errors
        private double MeanSquaredError(double[][] trainData) // used as a training stopping condition
        {
            // average squared error per training tuple
            var sumSquaredError = 0.0;
            var inputValues = new double[numInput]; // first numInput values in trainData
            var targetValues = new double[numOutput]; // last numOutput values

            // walk thru each training case. looks like (6.9 3.2 5.7 2.3) (0 0 1)
            foreach (var t in trainData)
            {
                Array.Copy(t, inputValues, numInput);
                Array.Copy(t, numInput, targetValues, 0, numOutput); // get target values
                var computeOutputs = ComputeOutputs(inputValues); // compute output using current weights
                for (var j = 0; j < numOutput; ++j)
                {
                    // what we are suppose to get - what we got
                    var err = targetValues[j] - computeOutputs[j];
                    sumSquaredError += err * err;
                }
            }
            return sumSquaredError / trainData.Length;
        }

        // ----------------------------------------------------------------------------------------

        public double Accuracy(double[][] testData)
        {
            // percentage correct using winner-takes all
            var numCorrect = 0;
            var numWrong = 0;
            var xValues = new double[numInput]; // inputs
            var tValues = new double[numOutput]; // targets
            double[] yValues; // computed Y

            for (var i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, numInput); // parse test data into x-values and t-values
                Array.Copy(testData[i], numInput, tValues, 0, numOutput);
                yValues = ComputeOutputs(xValues);
                var maxIndex = MaxIndex(yValues); // which cell in yValues has largest value?

                if (tValues[maxIndex] == 1.0) // ugly. consider AreEqual(double x, double y)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return numCorrect * 1.0 / (numCorrect + numWrong); // ugly 2 - check for divide by zero
        }

        private static int MaxIndex(double[] vector) // helper for Accuracy()
        {
            // index of largest value
            var bigIndex = 0;
            var biggestVal = vector[0];
            for (var i = 0; i < vector.Length; ++i)
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }

            return bigIndex;
        }

        #region Member variables

        private static Random randy;
        private readonly double[] hiddenBiases;
        private readonly double[] hiddenGrads; // hidden gradients for back-propagation
        private readonly double[][] hiddenOutputPrevWeightsDelta;
        private readonly double[] hiddenOutputs;

        private readonly double[][] hiddenOutputWeights; // hidden-output
        private readonly double[] hiddenPrevBiasesDelta;

        // back-prop momentum specific arrays (could be local to method Train)
        private readonly double[][] inputHiddenPrevWeightsDelta; // for momentum with back-propagation

        private readonly double[][] inputHiddenWeights; // input-hidden

        private readonly double[] inputs;
        private readonly int numHidden;

        private readonly int numInput;
        private readonly int numOutput;
        private readonly double[] outputBiases;

        // back-prop specific arrays (these could be local to method UpdateWeights)
        private readonly double[] outputGradientError; // output gradients for back-propagation
        private readonly double[] outputPrevBiasesDelta;

        private readonly double[] outputs;

        #endregion

        #region Brain distraction

        public NeuralNetwork(int numInput, int numHidden, int numOutput)
        {
            randy = new Random(0); // for InitializeWeights() and Shuffle()

            this.numInput = numInput;
            this.numHidden = numHidden;
            this.numOutput = numOutput;

            inputs = new double[numInput];

            inputHiddenWeights = MakeMatrix(numInput, numHidden);
            hiddenBiases = new double[numHidden];
            hiddenOutputs = new double[numHidden];

            hiddenOutputWeights = MakeMatrix(numHidden, numOutput);
            outputBiases = new double[numOutput];

            outputs = new double[numOutput];

            // back-prop related arrays below
            hiddenGrads = new double[numHidden];
            outputGradientError = new double[numOutput];

            inputHiddenPrevWeightsDelta = MakeMatrix(numInput, numHidden);
            hiddenPrevBiasesDelta = new double[numHidden];
            hiddenOutputPrevWeightsDelta = MakeMatrix(numHidden, numOutput);
            outputPrevBiasesDelta = new double[numOutput];
        } // ctor

        /**
         * Initilizes a multi-dementional aray, rows X colums
         */

        private static double[][] MakeMatrix(int rows, int cols) // helper for ctor
        {
            var result = new double[rows][];
            for (var r = 0; r < result.Length; ++r)
                result[r] = new double[cols];

            return result;
        }

        public override string ToString() // yikes
        {
            var s = "";
            s += "===============================\n";
            s += "numInput = " + numInput + " numHidden = " + numHidden + " numOutput = " + numOutput + "\n\n";

            s += "inputs: \n";
            for (var i = 0; i < inputs.Length; ++i)
                s += inputs[i].ToString("F2") + " ";

            s += "\n\n";

            s += "inputHiddenWeights: \n";
            for (var i = 0; i < inputHiddenWeights.Length; ++i)
            {
                for (var j = 0; j < inputHiddenWeights[i].Length; ++j)
                    s += inputHiddenWeights[i][j].ToString("F4") + " ";

                s += "\n";
            }
            s += "\n";

            s += "hiddenBiases: \n";
            for (var i = 0; i < hiddenBiases.Length; ++i)
                s += hiddenBiases[i].ToString("F4") + " ";

            s += "\n\n";

            s += "hiddenOutputs: \n";
            for (var i = 0; i < hiddenOutputs.Length; ++i)
                s += hiddenOutputs[i].ToString("F4") + " ";

            s += "\n\n";

            s += "hiddenOutputWeights: \n";
            for (var i = 0; i < hiddenOutputWeights.Length; ++i)
            {
                for (var j = 0; j < hiddenOutputWeights[i].Length; ++j)
                    s += hiddenOutputWeights[i][j].ToString("F4") + " ";

                s += "\n";
            }
            s += "\n";

            s += "outputBiases: \n";
            for (var i = 0; i < outputBiases.Length; ++i)
                s += outputBiases[i].ToString("F4") + " ";

            s += "\n\n";

            s += "hiddenGrads: \n";
            for (var i = 0; i < hiddenGrads.Length; ++i)
                s += hiddenGrads[i].ToString("F4") + " ";

            s += "\n\n";

            s += "outputGradientError: \n";
            for (var i = 0; i < outputGradientError.Length; ++i)
                s += outputGradientError[i].ToString("F4") + " ";

            s += "\n\n";

            s += "inputHiddenPrevWeightsDelta: \n";
            for (var i = 0; i < inputHiddenPrevWeightsDelta.Length; ++i)
            {
                for (var j = 0; j < inputHiddenPrevWeightsDelta[i].Length; ++j)
                    s += inputHiddenPrevWeightsDelta[i][j].ToString("F4") + " ";

                s += "\n";
            }
            s += "\n";

            s += "hiddenPrevBiasesDelta: \n";
            for (var i = 0; i < hiddenPrevBiasesDelta.Length; ++i)
                s += hiddenPrevBiasesDelta[i].ToString("F4") + " ";

            s += "\n\n";

            s += "hiddenOutputPrevWeightsDelta: \n";
            for (var i = 0; i < hiddenOutputPrevWeightsDelta.Length; ++i)
            {
                for (var j = 0; j < hiddenOutputPrevWeightsDelta[i].Length; ++j)
                    s += hiddenOutputPrevWeightsDelta[i][j].ToString("F4") + " ";

                s += "\n";
            }
            s += "\n";

            s += "outputPrevBiasesDelta: \n";
            for (var i = 0; i < outputPrevBiasesDelta.Length; ++i)
                s += outputPrevBiasesDelta[i].ToString("F4") + " ";

            s += "\n\n";

            s += "outputs: \n";
            for (var i = 0; i < outputs.Length; ++i)
                s += outputs[i].ToString("F2") + " ";

            s += "\n\n";

            s += "===============================\n";
            return s;
        }

        #endregion

        #region Initilize and get Weights

        // ----------------------------------------------------------------------------------------
        /**
         * Generates inital random weights between hi and lo, then sends it to SetWeights
         * hi = 0.01
         * lo = -0.01
         */

        public void InitializeWeights()
        {
            // initialize weights and biases to small random values
            var numWeights = numInput * numHidden + numHidden * numOutput + numHidden + numOutput;
            var initialWeights = new double[numWeights];
            var lo = -0.01;
            var hi = 0.01;
            for (var i = 0; i < initialWeights.Length; ++i)
                initialWeights[i] = (hi - lo) * randy.NextDouble() + lo;

            SetWeights(initialWeights);
        }

        public void SetWeights(double[] weights)
        {
            // copy weights and biases in weights[] array to i-h weights, i-h biases, h-o weights, h-o biases

            #region Make sure the number of weights is the same as network needs

            var numWeights = numInput * numHidden + numHidden * numOutput + numHidden + numOutput;
            if (weights.Length != numWeights)
                throw new Exception("Bad weights array length: ");

            #endregion

            var k = 0; // points into weights param

            /**
             * 
             */
            for (var i = 0; i < numInput; ++i)
            for (var j = 0; j < numHidden; ++j)
                inputHiddenWeights[i][j] = weights[k++];

            for (var i = 0; i < numHidden; ++i)
                hiddenBiases[i] = weights[k++];

            for (var i = 0; i < numHidden; ++i)
            for (var j = 0; j < numOutput; ++j)
                hiddenOutputWeights[i][j] = weights[k++];

            for (var i = 0; i < numOutput; ++i)
                outputBiases[i] = weights[k++];
        }

        // returns the current set of wweights, presumably after training
        public double[] GetWeights()
        {
            var numWeights = numInput * numHidden + numHidden * numOutput + numHidden + numOutput;
            var result = new double[numWeights];
            var k = 0;
            foreach (var t in inputHiddenWeights)
                for (var j = 0; j < inputHiddenWeights[0].Length; ++j)
                    result[k++] = t[j];

            foreach (var t in hiddenBiases)
                result[k++] = t;

            foreach (var t in hiddenOutputWeights)
                for (var j = 0; j < hiddenOutputWeights[0].Length; ++j)
                    result[k++] = t[j];

            foreach (var t in outputBiases)
                result[k++] = t;

            return result;
        }

        // ----------------------------------------------------------------------------------------

        #endregion
    } // NeuralNetwork
} // ns