using FrontTestDataSystem.Enums;
using FrontTestDataSystem.Model;
using FrontTestDataSystem.Service;
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

namespace FrontTestDataSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApiService api = new ApiService();
        private PaginationParams paginationParams = new PaginationParams();
        private Jobs job = new Jobs();

        private int maximumPageNumber = 100;
        private int minimumPageNumber = 1;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            paginationParams.PageNumber = 1;
            paginationParams.PageSize = 10;
        }

        #region Events
        private void blClean_Click(object sender, RoutedEventArgs e)
        {
            CleanFields();
        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            string resultValidate = ValidateFields();
            if (string.IsNullOrEmpty(resultValidate)) 
            {
                bool result = false;

                PopulateTransferData();

                try
                {
                    if (job.Id == 0)
                    {
                        result = await api.CreateAsync(job);
                        if (result)
                            FormHandler.ActionSuccess("A tarefa foi cadastrada com sucesso!");
                    }
                    else
                    {
                        result = await api.UpdateAsync(job);
                        if (result)
                            FormHandler.ActionSuccess("A tarefa foi alterada com sucesso!");
                    }
                    CleanFields();
                }
                catch (Exception ex)
                {
                    FormHandler.ActionError(ex.Message);
                }
            }
            else
            {
                FormHandler.ActionError(resultValidate);
            }
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            CleanFields();

            if (dgJobs.SelectedItem != null)
            {
                Jobs _job = (Jobs)dgJobs.SelectedItem;

                PopulateTcRegister(_job);

                tcJobs.SelectedItem = tpRegister;
            }
        }

        private async void btSearc_Click(object sender, RoutedEventArgs e)
        {
            await LoadJobs();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CleanFields();

            if (dgJobs.SelectedItem != null)
            {
                Jobs _job = (Jobs)dgJobs.SelectedItem;

                PopulateTcRegister(_job);

                tcJobs.SelectedItem = tpRegister;
            }
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgJobs.SelectedItem != null)
            {
                try
                {
                    MessageBoxResult resultConfirmation = FormHandler.ActionDelete("Tem certeza de que deseja excluir a tarefa selecionada?");

                    if (resultConfirmation == MessageBoxResult.Yes)
                    {
                        Jobs _job = (Jobs)dgJobs.SelectedItem;

                        bool resultDelete = await api.DeleteAsync(_job.Id);

                        if (resultDelete)
                            FormHandler.ActionSuccess("A tarefa foi deletada com sucesso!");

                        await LoadJobs();
                    }
                }catch(Exception ex)
                {
                    FormHandler.ActionError(ex.Message);
                }
            }
        }

        private async void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            paginationParams.PageNumber--;
            await LoadJobs();
        }

        private async void BtnProxima_Click(object sender, RoutedEventArgs e)
        {
            paginationParams.PageNumber++;
            await LoadJobs();
        }

        private async void tcJobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl && e.AddedItems.Contains(tpSearch))
            {
                CleanFields();
                await LoadJobs();
            }
        }
        #endregion

        #region Functions
        private void InitializeComboBoxes()
        {
            cbStatus.ItemsSource = Enum.GetValues(typeof(StatusEnum));
            cbStatus.SelectedIndex = 0;
            cbFilter.ItemsSource = Enum.GetValues(typeof(StatusEnum));
        }

        private void CleanFields()
        {
            tbTittle.Text = "";
            tbDescription.Text = "";
            dpCreateDate.SelectedDate = null;
            dpConclusionDate.SelectedDate = null;
            cbStatus.SelectedItem = StatusEnum.Pendente;
            cbFilter.SelectedItem = null;
        }

        private async Task LoadJobs()
        {
            List<Jobs> jobs = new List<Jobs>();
            List<Jobs> jobsNextPage = new List<Jobs>();

            if (cbFilter.SelectedItem == null)
            {
                jobs = await api.GetAllAsync(paginationParams.PageNumber, paginationParams.PageSize);
                jobsNextPage = await api.GetAllAsync(paginationParams.PageNumber + 1, paginationParams.PageSize);
            }
            else
            {
                int filter = Convert.ToInt32((StatusEnum)cbFilter.SelectedItem);
                jobs = await api.GetAllAsyncByState(filter, paginationParams.PageNumber, paginationParams.PageSize);
                jobsNextPage = await api.GetAllAsyncByState(filter, paginationParams.PageNumber + 1, paginationParams.PageSize);
            }
            CalculateLimitPagination(jobs);
            PaginationButtonStatus(jobsNextPage);
            dgJobs.ItemsSource = jobs;
            dgJobs.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void PopulateTcRegister(Jobs _job)
        {
            job.Id = _job.Id;
            tbTittle.Text = _job.Titulo;
            tbDescription.Text = _job.Descricao;
            dpCreateDate.SelectedDate = _job.DataCriacao;
            dpConclusionDate.SelectedDate = _job.DataConclusao;
            cbStatus.SelectedItem = _job.Status;
        }

        private void PopulateTransferData()
        {
            job.Titulo = tbTittle.Text;
            job.Descricao = tbDescription.Text;
            job.DataCriacao = dpCreateDate.SelectedDate.Value.Date;
            job.DataConclusao = dpConclusionDate.SelectedDate.Value.Date; 
            job.Status = (StatusEnum)cbStatus.SelectedItem;
        }

        private string ValidateFields()
        {
            string result = "Erro no cadastro da tarefa:\n";

            if (string.IsNullOrEmpty(tbTittle.Text))
                result += $"- O campo {lbTittle.Content} deve ser preenchido. \n";

            if(dpConclusionDate.SelectedDate == null)
                result += $"- O campo {lbCreateDate.Content} deve ser preenchido. \n";

            if(dpConclusionDate.SelectedDate != null && dpConclusionDate.SelectedDate.Value < dpCreateDate.SelectedDate.Value)
                result += $"- O campo {lbConclusionDate.Content} não pode ser uma data inferior a {lbCreateDate.Content}. \n";

            if (result == "Erro no cadastro da tarefa:\n")
                result = "";

            return result;
        }

        private void CalculateLimitPagination(List<Jobs> jobs)
        {
            int quantityJob = jobs.Count;
            maximumPageNumber = (int) Math.Ceiling(quantityJob / (double)paginationParams.PageSize);
        }

        private void PaginationButtonStatus(List<Jobs> jobsNextPage)
        {
            if (jobsNextPage.Count > 0)
                EnablePagiinationButton(btnNext, true);
            else
                EnablePagiinationButton(btnNext, false);

            if (paginationParams.PageNumber <= minimumPageNumber)
                EnablePagiinationButton(btnPrevious, false);
            else
                EnablePagiinationButton(btnPrevious, true);
        }

        private void EnablePagiinationButton(Button button, bool status)
        {
            button.IsEnabled = status;
        }
        #endregion
    }
}
