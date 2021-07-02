using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AddressBookW
{
    [Serializable]
    public class Person : IDataErrorInfo
    {
        [DisplayName("Ключ")]
        public double ID { get; set; } = -1;
        [DisplayName("Фамилия")]
        public string FirstName { get; set; }
        [DisplayName("Имя")]
        public string LastName { get; set; }
        [DisplayName("Отчество")]
        public string MidleName { get; set; }
        [DisplayName("Телефон")]
        public string Phone { get; set; }
        [DisplayName("Почта")]
        public string EMail { get; set; }
        [DisplayName("Адрес")]
        public string Address { get; set; }
        [Browsable(false)]
        [XmlIgnore]
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "ID":
                        if (ID < 0) { error = Helper.ErrorsMsg.NoSelectItem.ErrName; }
                        break;
                    case "FirstName":
                        if (!Helper.ValidValue.IsNamePerson(FirstName)) { error = Helper.ErrorsMsg.NotAdmissibleSymbol.ErrName; }
                        break;
                    case "LastName":
                        if (!Helper.ValidValue.IsNamePerson(LastName)) { error = Helper.ErrorsMsg.NotAdmissibleSymbol.ErrName; }
                        break;
                    case "MidleName":
                        if (!Helper.ValidValue.IsNamePerson(MidleName)) { error = Helper.ErrorsMsg.NotAdmissibleSymbol.ErrName; }
                        break;
                    case "Phone":
                        if (!Helper.ValidValue.IsTelephone(Phone)) { error = Helper.ErrorsMsg.NotTelephoneFormat.ErrName; }
                        break;
                }
                return error;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public Person Clone()
        {
            return new Person()
            {
                Address = this.Address,
                EMail = this.EMail,
                FirstName = this.FirstName,
                ID = this.ID,
                LastName = this.LastName,
                MidleName = this.MidleName,
                Phone = this.Phone
            };
        }

        public void ImportShort(Person migratePerson)
        {
            this.Address = migratePerson.Address;
            this.EMail = migratePerson.EMail;
            this.FirstName = migratePerson.FirstName;
            this.LastName = migratePerson.LastName;
            this.MidleName = migratePerson.MidleName;
            this.Phone = migratePerson.Phone;
        }
    }
}
