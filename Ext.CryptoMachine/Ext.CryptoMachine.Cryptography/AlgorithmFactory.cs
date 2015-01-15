using System;
using System.Collections.Generic;
using Ext.CryptoMachine.Cryptography.Cesar;
using Ext.CryptoMachine.Cryptography.IbanValidator;
using Ext.CryptoMachine.Cryptography.Matrix;
using Ext.CryptoMachine.Cryptography.Midnight;
using Ext.CryptoMachine.Cryptography.NipValidatior;

namespace Ext.CryptoMachine.Cryptography
{
    public static class AlgorithmFactory
    {
        static AlgorithmFactory()
        {
            Algorithms = new Dictionary<string, ICryptoAlgorithm>
            {
                {AlgoritmType.Cesar, new CesarCryptoAlgorithm()},
                {AlgoritmType.Midnight, new MidnightCryptoAlgorithm()},
                {AlgoritmType.Matrix, new MatrixCryptoAlgorithm()},
                {AlgoritmType.Nip, new NipCryptoAlgorithm()},
                {AlgoritmType.Iban, new IbanCryptoAlgorithm()}

            };
        }
        public static Dictionary<string, ICryptoAlgorithm> Algorithms { get; set; }
    }

    public static class AlgorithmInputFactory
    {
        static AlgorithmInputFactory()
        {
            InputTypes = new Dictionary<Type, Func<IEnumerable<String>, IAlgorithmInput>>
            {
                {typeof (CesarCryptoAlgorithm), CesarAlgorithmInput.GetInput},
                {typeof (MidnightCryptoAlgorithm), MidnightAlgorithmInput.GetInput},
                {typeof (NipCryptoAlgorithm), NipAlgorithmInput.GetInput},
                {typeof (IbanCryptoAlgorithm), IbanAlgorithmInput.GetInput},
                {typeof (MatrixCryptoAlgorithm), MatrixAlgorithmInput.GetInput}
            };
        }
        public static Dictionary<Type, Func<IEnumerable<String>, IAlgorithmInput>> InputTypes { get; set; }
    }

    public static class AlgoritmType
    {
        public static string Cesar = "Cesar";
        public static string Midnight = "Midnight";
        public static string Matrix = "Macierzowo";
        public static string Nip = "Walidacja Nr Nip";
        public static string Iban = "Walidacja Nr IBAN";
    }
}