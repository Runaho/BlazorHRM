using System;
using Blazored.Modal.Services;
using BlazorHRM.Client.Components.Modals;
using BlazorHRM.Client.Services;
using BlazorHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorHRM.Client.Pages
{
    public partial class EmployeeEdit : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public ICountryService CountryService { get; set; }

        [Inject]
        public IJobCategoryService JobCategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        protected string SelectedCountry { get; set; }
        protected string SelectedJobCategory { get; set; }


        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool? Saved;

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected override async Task OnInitializedAsync()
        {

            Countries = (await CountryService.GetCountries()).ToList();
            JobCategories = (await JobCategoryService.GetJobCategories()).ToList();

            int.TryParse(EmployeeId, out int employeeId);

            if (employeeId == 0)
                return;

            Employee = await EmployeeService.GetEmployeeDetail(int.Parse(EmployeeId));
            SelectedCountry = Employee.CountryId.ToString();
            SelectedJobCategory = Employee.JobCategoryId.ToString();
        }

        protected async Task ValidSubmit()
        {
            Employee.JobCategoryId = int.Parse(SelectedJobCategory);
            Employee.CountryId = int.Parse(SelectedCountry);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = await EmployeeService.AddEmployee(Employee);

                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong, please try agein later.";
                    Saved = false;
                }

            }
            else
            {
                await EmployeeService.UpdateEmployee(Employee);

                StatusClass = "alert-success";
                Message = "New employee added successfully.";
                Saved = true;
            }
        }

        public async Task DeleteEmployee()
        {
            var responseMessage = await EmployeeService.DeleteEmployee(Employee.EmployeeId);

            if (responseMessage.IsSuccessStatusCode)
            {

                StatusClass = "alert-success";
                Message = "Employeed Deleted.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong, please try agein later.";
                Saved = false;
            }
        }

        public void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }

        async Task ShowAddJobCategoryModal()
        {
            var jobCategoryModal = Modal.Show<JobCategoryModal>("New Job Category");
            var result = await jobCategoryModal.Result;
            if (!result.Cancelled)
            {
                if ((int)result.Data != 0)
                {
                    JobCategories = (await JobCategoryService.GetJobCategories()).ToList();
                    SelectedJobCategory = result.Data.ToString();

                    StateHasChanged();
                }
            }
        }
    }
}

