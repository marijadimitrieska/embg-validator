using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmbgValidatorApi
{
     public class EmbgValidatorService
    {
        public bool ValidateEmbg(string? embg)
        {
            if(string.IsNullOrWhiteSpace(embg))
            {
                return false;
            }

            embg = embg.Trim();

            if(embg.Length != 13)
            {
                return false;
            }

            if(!Regex.IsMatch(embg, @"^\d{13}$"))
            {
                return false;
            }

            int day = int.Parse(embg.Substring(0,2));
            int month = int.Parse(embg.Substring(2,2));
            int year = int.Parse(embg.Substring(4,3));
            int region = int.Parse(embg.Substring(7,2));
            int checksum = int.Parse(embg.Substring(12,1));

            if(!IsValidDate(day,month,year))
            {
                return false;
            }

            if(!IsValidRegion(region)){
                return false;
            }

            int calculatedChecksum = CalculateChecksum(embg);

            return checksum == calculatedChecksum;
        }

         private int CalculateChecksum(string embg)
         {
            int[] digits = embg.Take(12).Select(c => c - '0').ToArray();

            int sum =   7 * (digits[0] + digits[6]) +
                        6 * (digits[1] + digits[7]) + 
                        5 * (digits[2] + digits[8]) +
                        4 * (digits[3] + digits[9]) +
                        3 * (digits[4] + digits[10]) +
                        2 * (digits[5] + digits[11]);

            int m = 11 - (sum%11);

            if (m == 10) return -1; 
            if (m == 11) return 0;
            return m;
         }


        private bool IsValidDate(int day, int month, int year){
            if(day < 1 || day>31 || month < 1 || month > 12)
            {
                return false;
            }

            if(month == 2 && day > 29)
            {
                return false;
            }

            if(month == 2 && day == 29 && !DateTime.IsLeapYear(year))
            {
                return false;
            }

            int fullYear = (year < 100) ? 1900 + year : year;


            try{

                var date = new DateTime(fullYear, month, day);
                return date <= DateTime.Now;
            }
            catch{
                return false;
            }

        }

        private bool IsValidRegion(int region){
            return (region>=41 && region<=49) || region == 4;
            
        }
    }
}