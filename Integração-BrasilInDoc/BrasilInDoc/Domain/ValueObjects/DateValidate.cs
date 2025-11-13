using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integração_BrasilInDoc.BrasilInDoc.Domain.ValueObjects
{
    public class DateValidate
    {
        public static bool ValidateBithDate(string? birthDate, int minAge = 18, int maxAge = 80)
        {
            if (string.IsNullOrWhiteSpace(birthDate)) return false;

            if (birthDate.Contains(' '))
                birthDate = birthDate.Split(' ')[0];

            if (!DateOnly.TryParse(birthDate, out var date)) return false;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            int age = today.Year - date.Year;

            if (date > today.AddYears(-age)) 
                age--;

            if (age < minAge || age > maxAge) return false;

            return true;
        }

       public static bool ValidateDocumentExpeditionDate(string? expeditionDate, int maxYearsOld = 15)
        {
            if (string.IsNullOrWhiteSpace(expeditionDate))
                return false;

            if (expeditionDate.Contains(' '))
                expeditionDate = expeditionDate.Split(' ')[0];

            if (!DateOnly.TryParse(expeditionDate, out var date))
                return false;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (date > today) return false;

            if ((today.Year - date.Year) > maxYearsOld) return false;

            return true;
        }
    }
}
