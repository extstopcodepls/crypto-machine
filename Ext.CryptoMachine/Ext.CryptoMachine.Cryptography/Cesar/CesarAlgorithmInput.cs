using System;
using System.Collections.Generic;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.Cesar
{
    public class CesarAlgorithmInput : IAlgorithmInput
    {
        public Operation Operation { get; set; }
        public String Input { get; set; }
        public int Move { get; set; }

        public static CesarAlgorithmInput GetInput(IEnumerable<String> fileContent)
        {
            var data = fileContent as string[] ?? fileContent.ToArray();

            var operation = OperationProcess(data[0]);

            var content = data[1];

            var moveNumber = data[2];

            return new CesarAlgorithmInput
            {
                Operation = operation,
                Input = content,
                Move = Int32.Parse(moveNumber)
            };
        }

        private static Operation OperationProcess(String data)
        {
            switch (data)
            {
                case "encrypt":
                    return Operation.Encrypt;
                case "decrypt":
                    return Operation.Decrypt;
                default:
                    return Operation.Encrypt;
            }
        }
    }
}