namespace MyNeuralNetwork
{
    public interface ISynapse
    {
        // Takes input from subsquent nuron and sends the value to the next nuron
        void Fire(double value);
    }
}