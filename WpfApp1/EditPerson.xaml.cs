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

}
