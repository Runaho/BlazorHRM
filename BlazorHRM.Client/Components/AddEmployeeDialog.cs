using System;
using BlazorHRM.Client.Services;
using BlazorHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorHRM.Client.Components
{
    public partial class AddEmployeeDialog : ComponentBase
    {
        public Employee Employee { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> SavedEventCallBack { get; set; }

        public void Show()
        {
            ResetDialogModel();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialogModel()
        {
            Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
        }

        protected async Task ValidSubmit()
        {
            await EmployeeService.AddEmployee(Employee);
            await SavedEventCallBack.InvokeAsync(true);
            ShowDialog = false;

            StateHasChanged();
        }

    }
}

