using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integração_BrasilInDoc.BrasilInDoc.Domain.ValueObjects
{
    public class IdentityValidate
    {
        public static bool ValidateCpf(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            var digits = new string(cpf.Where(char.IsDigit).ToArray());
            if (digits.Length != 11) return false;

            if (digits.Distinct().Count() == 1) return false;

            var numbers = digits.Select(c => c - '0').ToArray();

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += numbers[i] * (10 - i);

            int remainder = sum % 11;
            int firstCheck = (remainder < 2) ? 0 : 11 - remainder;
            if (numbers[9] != firstCheck) return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += numbers[i] * (11 - i);

            remainder = sum % 11;
            int secondCheck = (remainder < 2) ? 0 : 11 - remainder;
            if (numbers[10] != secondCheck) return false;

            return true;
        }

        public static bool ValidateCnpj(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            var digits = new string(cnpj.Where(char.IsDigit).ToArray());
            if (digits.Length != 14) return false;

            if (digits.Distinct().Count() == 1) return false;

            var numbers = digits.Select(c => c - '0').ToArray();

            int[] firstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += numbers[i] * firstWeights[i];

            int remainder = sum % 11;
            int firstCheck = (remainder < 2) ? 0 : 11 - remainder;
            if (numbers[12] != firstCheck) return false;

            int[] secondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += numbers[i] * secondWeights[i];

            remainder = sum % 11;
            int secondCheck = (remainder < 2) ? 0 : 11 - remainder;
            if (numbers[13] != secondCheck) return false;

            return true;
        }
    }
}
