using System;
using System.Globalization;
using System.Linq;
using Ext.CryptoMachine.Cryptography.Cesar;

namespace Ext.CryptoMachine.Cryptography.Matrix
{
    public class MatrixCryptoAlgorithm : BaseCryptoAlgorithm<MatrixAlgorithmInput, MatrixAlgorithmOutput>, ICryptoAlgorithm
    {
        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as MatrixAlgorithmInput);
        }

        public override MatrixAlgorithmOutput ProcessOut(MatrixAlgorithmInput input)
        {
            switch (input.Operation)
            {
                case Operation.Encrypt:
                    return Encrypt(input);
                case Operation.Decrypt:
                    return Decrypt(input);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static MatrixAlgorithmOutput Decrypt(MatrixAlgorithmInput input)
        {
            var data = input.Input;
            var result = String.Empty;
            var strLength = data.Length;
            var sqrt = Convert.ToInt32(Math.Ceiling((Math.Sqrt(strLength))));

            var matrix = new char[sqrt, sqrt];

            var ij = 0;

            InitArray(matrix);

            for (var i = 0; i < sqrt; i++)
            {
                for (var j = 0; j < sqrt; j++)
                {
                    if (ij < data.Length)
                    {
                        matrix[i, j] = data[ij++];
                    }
                }
            }

            for (var i = 0; i < sqrt; i++)
            {
                for (var j = 0; j < sqrt; j++)
                {
                    var text = matrix[j, i];
                    result += Char.IsWhiteSpace(text) ? String.Empty : text.ToString(CultureInfo.InvariantCulture);
                }
            }

            return new MatrixAlgorithmOutput
            {
                Result =
                    result
            };
        }

        private static void InitArray(char[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = ' ';
                }
            }
        }

        private static MatrixAlgorithmOutput Encrypt(MatrixAlgorithmInput input)
        {
            var data = input.Input;
            var result = String.Empty;
            var strLength = data.Length;
            var sqrt = Convert.ToInt32(Math.Ceiling((Math.Sqrt(strLength))));

            var matrix = new char[sqrt, sqrt];

            var ij = 0;

            InitArray(matrix);

            for (var i = 0; i < sqrt; i++)
            {
                for (var j = 0; j < sqrt; j++)
                {
                    if (ij < data.Length)
                    {
                        matrix[i, j] = data[ij++];
                    }
                }
            }

            for (var i = 0; i < sqrt; i++)
            {
                for (var j = 0; j < sqrt; j++)
                {
                    var text = matrix[j, i];
                    result += Char.IsWhiteSpace(text) ? String.Empty : text.ToString(CultureInfo.InvariantCulture);
                }
            }

            return new MatrixAlgorithmOutput
            {
                Result =
                    result
            };
        }
    }
}