namespace Ext.CryptoMachine.Cryptography
{
    public interface ICryptoAlgorithm
    {
        IAlgorithmOutput Process(IAlgorithmInput input);
    }
}