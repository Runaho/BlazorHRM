using System;
using BlazorHRM.Client.Components;
using BlazorHRM.Client.Services;
using BlazorHRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sotsera.Blazor.Toaster;

namespace BlazorHRM.Client.Pages
{
    public partial class EmployeeList : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        protected IToaster Toaster { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Toaster.Info("Please wait while we gathering employees");

            Employees = (await EmployeeService.GetEmployees()).ToList();
            Toaster.Clear();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void QuickAddEmployee_Saved()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            Toaster.Success("Employee added");
            StateHasChanged();
        }

    }
}
