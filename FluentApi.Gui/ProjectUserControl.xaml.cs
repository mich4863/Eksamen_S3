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
    /// Interaction logic for ProjectUserControl.xaml
    /// </summary>
    public partial class ProjectUserControl :UserControl
    {
        protected Model model;
        private Project selectedProject;
        private Team selectedTeam;
        private List<Team> projectTeams;

        public ProjectUserControl()
        {
            InitializeComponent();

            model = new Model();
            dataGridProjects.ItemsSource = model.Projects.ToList();

            this.DataContext = selectedProject;
           

        }

        private void Button_SaveNewProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project project = new Project();
                project.Name = textBoxProjectName.Text;
                project.StartDate = datePickerProjectStartDate.SelectedDate;
                project.Description = textBoxProjectDescription.Text;
                project.EndDate = datePickerProjectEndDate.SelectedDate;

                //decimal.TryParse(textBoxBudgetLimit.Text, out decimal number);
                //project.BudgetLimet = number;
                bool couldParse = decimal.TryParse(textBoxBudgetLimit.Text, out decimal number);
                if(couldParse)
                {
                    project.BudgetLimet = number;
                }
                else
                {
                    project.BudgetLimet = null;
                }
                model.Projects.Add(project);
                model.SaveChanges();
                ReloadAllTeams();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void ReloadAllTeams()
        {
            dataGridAddOrDeleteFromProject.ItemsSource = null;
            dataGridAddOrDeleteFromProject.ItemsSource = projectTeams;

            dataGridProjects.ItemsSource = model.Projects.ToList();
        }

        public void LoadTeamsIntoComboBox()
        {
            List<Team> teamCombo = model.Teams.ToList();
            List<Team> erre = teamCombo.Where(team => team.ProjectId != selectedProject.Id).ToList();
            comboBoxAllTeams.ItemsSource = erre;
        }

        private void DataGrid_Projects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = dataGridProjects.SelectedItem as Project;
            if(selectedProject != null)
            {
                /* Load only Teams that are not members of the selected Project into ComboBox  */
                LoadTeamsIntoComboBox();

                /* Load the selected project into the textboxes and datepicker's */
                textBoxProjectName.Text = selectedProject.Name;
                datePickerProjectStartDate.SelectedDate = selectedProject.StartDate;
                textBoxProjectDescription.Text = selectedProject.Description;
                datePickerProjectEndDate.SelectedDate = selectedProject.EndDate;
                textBoxBudgetLimit.Text = selectedProject.BudgetLimet.ToString();

                /* Load the Project members into datagrid */
                List<Team> teams = model.Teams.ToList();
                projectTeams = teams.Where(team => team.ProjectId == selectedProject.Id).ToList();
                dataGridAddOrDeleteFromProject.ItemsSource = projectTeams;

                /* Load expenses for project into label. */
                CalculateExpensesForProject();

                /* Count the members of selected project and set the property MembersOfProject to the new member number. */
                int count = projectTeams.Count();
                selectedProject.MembersOfProject = count;
            }
        }

        private void DataGrid_Projects_KeyDown(object sender, KeyEventArgs e)
        {
            if(dataGridProjects.SelectedItem != null)
            {
                if(e.Key == Key.Escape)
                {
                    dataGridProjects.SelectedItem = selectedProject = null;

                    textBoxProjectName.Text = String.Empty;
                    datePickerProjectStartDate.Text = null;
                    textBoxProjectDescription.Text = String.Empty;
                    datePickerProjectEndDate.Text = null;
                    comboBoxAllTeams.Text = "Vælg et team";
                    dataGridAddOrDeleteFromProject.ItemsSource = null;
                    comboBoxAllTeams.ItemsSource = null;
                    textBoxBudgetLimit.Text = String.Empty;
                    labelTotalExpenses.Content = "";

                    textBoxProjectName.Focus();
                }
            }
        }

        private void Button_UpdateProject_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProject != null)
            {
                try
                {
                    if(textBoxProjectName.Text != selectedProject.Name || textBoxProjectDescription.Text != selectedProject.Description || datePickerProjectStartDate.SelectedDate != selectedProject.StartDate || datePickerProjectEndDate.SelectedDate != selectedProject.EndDate || textBoxBudgetLimit.Text != selectedProject.BudgetLimet.ToString())
                    {
                        selectedProject.Name = textBoxProjectName.Text;
                        selectedProject.Description = textBoxProjectDescription.Text;
                        selectedProject.StartDate = datePickerProjectStartDate.SelectedDate.Value;
                        selectedProject.EndDate = datePickerProjectEndDate.SelectedDate.Value;

                        decimal.TryParse(textBoxBudgetLimit.Text, out decimal number);
                        selectedProject.BudgetLimet = number;
                    }
                    model.SaveChanges();
                    ReloadAllTeams();
                }
                catch(Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }

        private void Button_AddTeamToProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedTeam = comboBoxAllTeams.SelectedItem as Team;
                selectedTeam.ProjectId = selectedProject.Id;
                selectedProject.MembersOfProject++;

                projectTeams.Add(selectedTeam);
                CalculateExpensesForProject();
                model.SaveChanges();
                ReloadAllTeams();
                LoadTeamsIntoComboBox();

                
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
            comboBoxAllTeams.Text = "Vælg et team";
        }

        private void Button_DeleteTeamFromProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedTeam = dataGridAddOrDeleteFromProject.SelectedItem as Team;
                projectTeams.Remove(selectedTeam);
                selectedTeam.ProjectId = null;
                selectedProject.MembersOfProject--;
                LoadTeamsIntoComboBox();

                model.SaveChanges();
                CalculateExpensesForProject();
                ReloadAllTeams(); 
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public void CalculateExpensesForProject()
        {
            decimal result = 0;

            foreach(var e in projectTeams.Select(e => e.TeamExpenses))
            {
                result = result + e.Value;
                selectedProject.ProjectExpenses = result;
            }
            model.SaveChanges();
            labelTotalExpenses.Content = result.ToString("C");
        }
    }
}
