using System;
using System.Collections.Generic;
using System.Reflection;
using Ext.CryptoMachine.Cryptography.Cesar;
using Ext.CryptoMachine.Cryptography.Matrix;

namespace Ext.CryptoMachine.Cryptography
{
    public static class AlgorithmFactory
    {
        static AlgorithmFactory()
        {
            Algorithms = new Dictionary<string, ICryptoAlgorithm>
            {
                { AlgoritmType.Cesar, new CesarCryptoAlgorithm() },
                { AlgoritmType.Matrix, new MatrixCryptoAlgorithm() }
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
                { typeof(CesarCryptoAlgorithm), CesarAlgorithmInput.GetInput },
                { typeof(MatrixCryptoAlgorithm), MatrixAlgorithmInput.GetInput }
            };
        }
        public static Dictionary<Type, Func<IEnumerable<String>, IAlgorithmInput>> InputTypes { get; set; }
    }

    public static class AlgoritmType
    {
        public static string Cesar = "Cesar";
        public static string Matrix = "Macierzowo";
    }
}