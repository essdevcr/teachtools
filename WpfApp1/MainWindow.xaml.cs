using AdressBookW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AddressBookW
{
    /// <summary>
    /// 
    ///    Необходимо написать приложение "Адресная книга", которое обладает следующими функциями:
    ///    - Список всех контактов(ID, ФИО, телефон)
    ///    - Добавление контакта
    ///    - Удаление контакта
    ///    - Редактирование контакта
    ///   - Данные хранятся в xml
    ///    - есть валидация данных(телефон по маске +7-xxx-xxx-xx-xx, фамилия/имя от 2 до 50 символов), border полей подсвечивается красным у невалидных полей
    ///    - приложение обладает кастомизированным стилем, который описан в отдельном файле xaml(изменить стиль по своему усмотрению)
    ///    * ID - номер сотрудника по порядку, закрепленный за ним в момент добавления контакта.
    ///       
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //dgAdressbook
 
            BookPersons = new BindingList<Person>();
            DeSerialization();
            dgAdressbook.ItemsSource = BookPersons;
        }

        private Binding BindDataPersons { get; set; }
        private BindingList<Person> BookPersons { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int c = 10 % 4;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnNewPerson_Click(object sender, RoutedEventArgs e)
        {
            var newPerson = new Person() { ID = GeneratePersonID(), FirstName = "Вашеимя" };

            var editPersonForm = new EditPerson(newPerson, AdressBookW.Helper.EditedMode.Newly);
            var dialog = editPersonForm.ShowDialog();
            if (dialog == true)
            {
                BookPersons.Add(newPerson);
                dgAdressbook.Items.Refresh();
            }

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //телефон по маске +7-xxx-xxx-xx-xx
            bool test1 =  Helper.ValidValue.IsTelephone("7845212");
            bool test2 = Helper.ValidValue.IsTelephone("+7-960-707-60-21");

            var selectedPerson = GetSelectedPerson();
            if (selectedPerson == null)
            {
                MessageBox.Show(Helper.ErrorsMsg.NoSelectItem.ErrName);
                return;


            }
            var editPersonForm = new EditPerson(selectedPerson, AdressBookW.Helper.EditedMode.Edit);
            var dialog = editPersonForm.ShowDialog();
            if (dialog == true)
            {
                dgAdressbook.Items.Refresh();
            }

        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {

            var removePerson = GetSelectedPerson();
            if (removePerson == null)
            {
                Console.WriteLine(Helper.ErrorsMsg.NoSelectItem.ErrName);
                MessageBox.Show(Helper.ErrorsMsg.NoSelectItem.ErrName);
                return;

            }

            if (MessageBox.Show($"{removePerson.FirstName}. Удалить?", "Удаление персоны", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
              BookPersons.Remove(removePerson); 
            }
            
        }
        private double GeneratePersonID()
        {
            if (BookPersons.Count > 0) return BookPersons == null ? -1 : BookPersons.Max(t => t.ID) + 1;
            else
                return 1;

        }
        private void DgAdressbook_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
            if (((PropertyDescriptor)e.PropertyDescriptor).IsBrowsable == false) e.Cancel = true;
        }
        private void OnSerialization()
        {

            using (FileStream fs = new FileStream("Addressbook.xml", FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(BindingList<Person>));
                formatter.Serialize(fs, BookPersons);
            }

        }
        private void DeSerialization()
        {
            try
            {
                using (FileStream fs = new FileStream("Addressbook.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(BindingList<Person>));
                    BindingList<Person> newAB = (BindingList<Person>)formatter.Deserialize(fs);
                    if (newAB.Count > 0)
                    {
                        BookPersons.Clear();
                        foreach (var item in newAB) BookPersons.Add(item);

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }
        private Person GetSelectedPerson()
        {
            try
            {
                var selectedPerson = dgAdressbook.SelectedItem;
                if (selectedPerson != null)
                {
                    return (Person)selectedPerson;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Выбранный элемент недоступен!");
            }
            return null;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            OnSerialization();
        }
    }
}
