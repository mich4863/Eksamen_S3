using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi.Ef
{
    public static class Validator
    {
        public static bool IsIdValid(int id)
        {
            if(id < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNameValid(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            else if(s.Length > 50)
            {
                return false;
            }
            else if(!s.All(c => char.IsLetter(c)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCprNumberValid(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            else if(s.Length != 10)
            {
                return false;
            }
            else if(!s.All(c => char.IsNumber(c)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsSalaryValid(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            else if(s.Length < 0)
            {
                return false;
            }
            else if(s.Any(c => char.IsLetter(c)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsBirthDayValid(DateTime d)
        {
            if(d == null)
            {
                return false;
            }
            else if(d > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsHireDayValid(DateTime d)
        {
            if(d == null)
            {
                return false;
            }
            else if(d < new DateTime(1950,01,01))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region Validate ContactInfo
        public static bool IsPhoneNumberValid(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            else if(s.Length != 8)
            {
                return false;
            }
            else if(s.Length < 0)
            {
                return false;
            }
            else if(!s.All(c => char.IsNumber(c)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsEmailValid(string s)
        {
            var addr = new System.Net.Mail.MailAddress(s);

            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            else if(addr.Address != s)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
