namespace Ext.CryptoMachine.Cryptography
{
    public abstract class BaseCryptoAlgorithm<TIn, TOut>
        where TIn : IAlgorithmInput
        where TOut : IAlgorithmOutput
    {
        public abstract TOut ProcessOut(TIn input);
    }
}