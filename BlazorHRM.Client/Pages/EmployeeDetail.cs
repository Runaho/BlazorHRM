using System;
using BlazorHRM.Client.Services;
using BlazorHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorHRM.Client.Pages
{
    public partial class EmployeeDetail : ComponentBase
    {
        [Parameter]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = new Employee();
       
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = (await EmployeeService.GetEmployeeDetail(int.Parse(EmployeeId)));
        }
    }
}
