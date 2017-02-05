using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace BuildNeuralNetworkDemo.My
{
    class Program
    {
        public static void Main(string[] args)
        {
            var data = new double[][]
            {
                new double[]{0, 0, 0},
                new double[]{0, 1, 0},
                new double[]{1, 0, 0},
                new double[]{1, 1, 0}
            };
        }
    }

    #region Old Failures
    /*
        public class NeuralNetwork
        {

            private abstract class Nuron
            {
                public List<Synapse> InputSynapses { get; internal set; }

                private Synapse _outputSynapse;
                public Synapse OutputSynapse
                {
                    get { return _outputSynapse; }
                    internal set
                    {
                        if (Equals(_outputSynapse.GetOutputNuron()))
                        {
                            throw new Exception("Circular synapse reffrence exception.");
                        }
                        _outputSynapse = value;
                    }
                }

                internal static double ActivationFuncton(double x) => Math.Abs(x) >= 20 ? x / Math.Abs(x) : Math.Tanh(x);

                public abstract double PropagateOutput();

            }

            private class HiddenNuron : Nuron
            {
                public HiddenNuron(IEnumerable<Synapse> inputSynapses, Synapse outputSynapse) : base()
                {
                    InputSynapses = new List<Synapse>(inputSynapses);
                    OutputSynapse = new Synapse(outputSynapse.GetInputNuron(), outputSynapse.GetOutputNuron());
                }

                public double PropagateOutput()
                {
                    var sum = InputSynapses.Sum(synapse => synapse.Value);
                    sum = ActivationFuncton(sum);
                    OutputSynapse.Value = sum;
                    return sum;
                }
            }

            private class InputNuron : Nuron
            {
                public InputNuron(Synapse outputSynapse) : base()
                {
                    OutputSynapse = new Synapse(outputSynapse.GetInputNuron(), outputSynapse.GetOutputNuron());
                }

                public double PropagateOutput(double input)
                {
                    var sum = ActivationFuncton(input);
                    OutputSynapse.Value = sum;
                    return sum;
                }

                public override double PropagateOutput()
                {
                    throw new NotImplementedException();
                }
            }

            private class OutputNuron : Nuron
            {
                public OutputNuron(IEnumerable<Synapse> inputSynapses) : base()
                {
                    InputSynapses = new List<Synapse>(inputSynapses);
                }

                private double SoftMax(IEnumerable<> )
                {
                    var max = InputSynapses[0];
                    foreach (var synapse in InputSynapses)
                    {
                        max = Math.Max(max.Value)
                    }
                }

                public double PropagateOutput()
                {
                    var sum = InputSynapses.Sum(synapse => synapse.Value);
                    sum = ActivationFuncton(sum);



                    return sum;
                }
            }

            private class Synapse
            {
                private Nuron[] nurons;
                public double Weight { get; set; }

                public Nuron GetInputNuron() => nurons[0];
                public Nuron GetOutputNuron() => nurons[1];

                private double currentValue;
                public double Value
                {
                    get { return currentValue * Weight; }
                    set
                    {
                        currentValue = value;
                        GetOutputNuron().PropagateOutput();
                    }
                }

                public Synapse(Nuron input, Nuron output)
                {
                    nurons[0] = input;
                    nurons[1] = output;

                    if (Equals(nurons[1].OutputSynapse))
                    {
                        throw new Exception("Circular synapse reffrence exception.");
                    }

                    SetRandomWeight();
                }

                public Synapse(Nuron input, Nuron output, double weight) : this(input, output)
                {
                    this.Weight = weight;

                }

                private void SetRandomWeight()
                {
                    const double high = 0.01;
                    const double low = -0.01;
                    var randy = new Random(0);
                    Weight = (high - low) * randy.NextDouble() + low;
                }
            }

            private int numberOfInputs;
            private int numberOfHidden;
            private int numberOfOutputs;

            private Nuron[] nurons;

            public NeuralNetwork(int numberOfInputs, int numberOfHidden, int numberOfOutputs)
            {
                this.numberOfInputs = numberOfInputs;
                this.numberOfHidden = numberOfHidden;
                this.numberOfOutputs = numberOfOutputs;
            }






}*/
    #endregion
}
