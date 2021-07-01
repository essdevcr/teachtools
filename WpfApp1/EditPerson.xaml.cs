using AddressBookW;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdressBookW
{
    /// <summary>
    /// Логика взаимодействия для EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        public Person editperson { get; set; }
        private Person SavedParent { get; set; }
        bool IsEditPerson { get; set; } 

        public EditPerson(Person person, Helper.EditedMode viewmode)
        {
            IsEditPerson = Helper.DialogMode.GetMode(viewmode);
            SavedParent = person;
            editperson = person.Clone();
            InitializeComponent();
            this.DataContext = editperson;
            this.Title = IsEditPerson ? "Редактирование выбранной записи" : "Создание новой записи";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            PersonUpdata();
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close(); 
        }

        private void PersonUpdata()
        {
            SavedParent.ImportShort(editperson);
        }

    }
    /**
    public class FutureDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Test if date is valid
            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                // Date is not in the future, fail
                if (DateTime.Now > date)
                    return new ValidationResult(false, "Please enter a date in the future.");
            }
            else
            {
                // Date is not a valid date, fail
                return new ValidationResult(false, "Value is not a valid date.");
            }

            // Date is valid and in the future, pass
            return ValidationResult.ValidResult;
        }
    }
    */

}
