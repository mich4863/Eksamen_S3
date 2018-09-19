using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentApi.Ef;

namespace FluentApi.Gui
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl :UserControl
    {
        protected Model model;
        private Employee selectedEmployee;

        public EmployeeUserControl()
        {
            InitializeComponent();

            model = new Model();
            dataGridEmployees.ItemsSource = model.Employees.ToList();

            this.DataContext = selectedEmployee;
        }

        private void DataGrid_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridEmployees.SelectedItem as Employee;
            if(selectedEmployee != null)
            {
                buttonUpdateEmployee.IsEnabled = true;
                buttonSaveNewEmployee.IsEnabled = false;
                ///////
                //buttonUpdateContactInfo.IsEnabled = true;
                //buttonSaveNewContactInfo.IsEnabled = false;

                textBoxEmployeeFirstname.Text = selectedEmployee.Firstname;
                datePickerEmployeeEmployment.SelectedDate = selectedEmployee.EmploymentDate;
                datePickerEmployeeBirthday.SelectedDate = selectedEmployee.Birthday;
                textBoxEmployeeLastname.Text = selectedEmployee.Lastname;
                textBoxEmployeeCPRNumber.Text = selectedEmployee.CPRNumber;
                textBoxEmployeeSalary.Text = selectedEmployee.Salary.ToString();

                if(selectedEmployee.ContactInfo != null)
                {
                    textBoxEmail.Text = selectedEmployee.ContactInfo.Email;
                    textBoxPhone.Text = selectedEmployee.ContactInfo.Phone;
                }
                else
                {
                    textBoxEmail.Text = String.Empty;
                    textBoxPhone.Text = String.Empty;
                }
            }
        }

        private void ReloadAllEmployees()
            => dataGridEmployees.ItemsSource = model.Employees.ToList();

        private void Button_UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                try
                {
                    if(textBoxEmployeeFirstname.Text != selectedEmployee.Firstname || textBoxEmployeeLastname.Text != selectedEmployee.Lastname || datePickerEmployeeBirthday.SelectedDate != selectedEmployee.Birthday || textBoxEmployeeCPRNumber.Text != selectedEmployee.CPRNumber || textBoxEmployeeSalary.Text != selectedEmployee.Salary.ToString() || datePickerEmployeeEmployment.SelectedDate != selectedEmployee.EmploymentDate)
                    {
                        selectedEmployee.Firstname = textBoxEmployeeFirstname.Text;
                        selectedEmployee.Lastname = textBoxEmployeeLastname.Text;
                        selectedEmployee.Birthday = datePickerEmployeeBirthday.SelectedDate.Value;
                        selectedEmployee.CPRNumber = textBoxEmployeeCPRNumber.Text;

                        decimal.TryParse(textBoxEmployeeSalary.Text, out decimal number);
                        selectedEmployee.Salary = number;

                        selectedEmployee.EmploymentDate = datePickerEmployeeEmployment.SelectedDate.Value;  
                    }
                    model.SaveChanges();
                    ReloadAllEmployees();
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }

        private void Button_SaveNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = new Employee();
                employee.Firstname = textBoxEmployeeFirstname.Text;
                employee.EmploymentDate = datePickerEmployeeEmployment.SelectedDate;
                employee.Lastname = textBoxEmployeeLastname.Text;
                employee.CPRNumber = textBoxEmployeeCPRNumber.Text;
                employee.Birthday = datePickerEmployeeBirthday.SelectedDate;

                bool couldParse = decimal.TryParse(textBoxEmployeeSalary.Text, out decimal number);
                if(couldParse)
                {
                    employee.Salary = number;
                }
                else
                {
                    employee.Salary = null;
                }

                model.Employees.Add(employee);
                model.SaveChanges();
                ReloadAllEmployees();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void DataGrid_Employees_KeyDown(object sender, KeyEventArgs e)
        {
            if(dataGridEmployees.SelectedItem != null)
            {
                if(e.Key == Key.Escape)
                {
                    dataGridEmployees.SelectedItem = selectedEmployee = null;
                    buttonSaveNewEmployee.IsEnabled = true;
                    buttonUpdateEmployee.IsEnabled = false;
                    /////
                    //buttonSaveNewContactInfo.IsEnabled = true;
                    //buttonUpdateContactInfo.IsEnabled = false;

                    textBoxEmployeeFirstname.Text = String.Empty;
                    textBoxEmployeeLastname.Text = String.Empty;
                    datePickerEmployeeBirthday.Text = null;
                    textBoxEmployeeCPRNumber.Text = String.Empty;
                    textBoxEmployeeSalary.Text = String.Empty;
                    textBoxEmail.Text = String.Empty;
                    textBoxPhone.Text = String.Empty;
                    datePickerEmployeeEmployment.Text = null;
                    
                    textBoxEmployeeFirstname.Focus();
                }
            }
        }

        private void TextBox_EmployeeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBoxEmployeeFirstname.Text == "")
            {
                buttonSaveNewEmployee.IsEnabled = false;
                buttonUpdateEmployee.IsEnabled = false;
                //////
                //buttonSaveNewContactInfo.IsEnabled = false;
                //buttonUpdateContactInfo.IsEnabled = false;
            }
            else if(selectedEmployee == null)
            {
                buttonSaveNewEmployee.IsEnabled = true;
                buttonUpdateEmployee.IsEnabled = false;
                ///
                //buttonSaveNewContactInfo.IsEnabled = true;
                //buttonUpdateContactInfo.IsEnabled = false;
            }
            else
            {
                buttonSaveNewEmployee.IsEnabled = false;
                buttonUpdateEmployee.IsEnabled = true;
                //
                //buttonSaveNewContactInfo.IsEnabled = false;
                //buttonUpdateContactInfo.IsEnabled = true;
            }
        }

        private void Button_UpdateContactInfo_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                try
                {
                    if(textBoxEmail.Text != selectedEmployee.ContactInfo.Email || textBoxPhone.Text != selectedEmployee.ContactInfo.Phone)
                    {
                        selectedEmployee.ContactInfo.Email = textBoxEmail.Text;
                        selectedEmployee.ContactInfo.Phone = textBoxPhone.Text;
                    }
                    model.SaveChanges();
                    ReloadAllEmployees();
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }

        private void Button_SaveNewContactInfo_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                try
                {
                    ContactInfo contactInfo = new ContactInfo();
                    contactInfo.Email = textBoxEmail.Text;
                    contactInfo.Phone = textBoxPhone.Text;
                    contactInfo.Id = selectedEmployee.Id;
                    selectedEmployee.ContactInfo = contactInfo;

                    model.SaveChanges();
                    ReloadAllEmployees();
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }          
        }
    }
}
