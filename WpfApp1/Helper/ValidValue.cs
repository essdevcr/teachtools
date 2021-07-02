using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBookW.Helper
{
    public static class ValidValue
    {
        static public bool IsTelephone(string telephone)
        {
            // телефон по маске +7-xxx-xxx-xx-xx
            string mask = @"^\+7-\d{3}-\d{3}-\d{2}-\d{2}$";
            if (Regex.IsMatch(telephone, mask, RegexOptions.IgnoreCase)) return true;
            return false;
        }

        static public bool IsNamePerson(string namestring)
        {
            // фамилия/имя от 2 до 50 символов)
            string mask = "^([a-zA-Z]{2,50}|[А-Яа-я]{2,50})$";
            if (Regex.IsMatch(namestring, mask, RegexOptions.Compiled)) return true;
            return false;
        }

    }
    public static class ErrorsMsg
    {
        public static ErrorMessage NoSelectItem { get { return new ErrorMessage("Не выбран элемент в списке", 101); } }
        public static ErrorMessage IdOverRange { get { return new ErrorMessage("ID должен быть больше 0 и меньше 100", 102); } }
        public static ErrorMessage NotAdmissibleSymbol { get { return new ErrorMessage("Не доспустимый состав символов в названии", 103); } }
        public static ErrorMessage NotTelephoneFormat { get { return new ErrorMessage("Не допустимый формат телефона. Нужен +7-xxx-xxx-xx-xx", 104); } }

    }
    public class ErrorMessage
    {
        public string ErrName { get; set; }
        public int ErrCode { get; set; }
        public ErrorMessage() { }
        public ErrorMessage(string name, int code) { ErrName = name; ErrCode = code; }

    }
}
