using System.Collections.Generic;

namespace MyNeuralNetwork
{
    public interface INeuron
    {
        // Takes input from a synapse, proforms transformations, propagates forward to next destination
        double Propagate();

        void RecieveInput(double value);
    }
}
