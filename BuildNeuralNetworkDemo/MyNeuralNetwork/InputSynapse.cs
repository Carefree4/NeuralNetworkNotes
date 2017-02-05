namespace MyNeuralNetwork
{
    /// <summary>
    /// This synapse takes input from the Neural Netwok and are the 
    /// ones responsable for delivering the values to the Input nurons
    /// 
    /// Has no input nuron, but one output neuron
    /// </summary>
    public class InputSynapse : ISynapse
    {
        public InputNeuron InputNeuron { get; set; }

        public void Fire(double value)
        {
            InputNeuron.RecieveInput(value);
        }
    }
}
