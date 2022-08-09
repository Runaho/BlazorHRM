using System;
using Blazored.Modal;
using Blazored.Modal.Services;
using BlazorHRM.Client.Services;
using BlazorHRM.Shared;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;

namespace BlazorHRM.Client.Components.Modals
{
    public partial class JobCategoryModal : ComponentBase
    {

        [Inject]
        IJobCategoryService JobCategoryService { get; set; }

        [Inject]
        protected IToaster Toaster { get; set; }

        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int JobCategoryId { get; set; } = 0;

        JobCategory JobCategory { get; set; } = new JobCategory();
        List<JobCategory> jobCategories { get; set; } = new List<JobCategory>();

        private bool updating = false;

        [Parameter]
        public bool Updating
        {
            get => updating;
            set
            {
                updating = value;

                if (updating == false)
                    StateHasChanged();
            }
        }

        protected override async void OnInitialized()
        {
            Updating = true;
            if (JobCategoryId != 0)
                await updateSelectedJobCategoryAsync();

            await updatejobCategories();
            Updating = false;
        }



        async void SaveJobCategory()
        {
            Updating = true;
            if (JobCategoryId == 0)
            {
                var jobCategory = await JobCategoryService.AddJobCategory(JobCategory);

                JobCategoryId = jobCategory.JobCategoryId;

                Toaster.Success("Category Added");

                await ModalInstance.CloseAsync(ModalResult.Ok<int>(JobCategoryId));
            }
            else
            {
                await JobCategoryService.UpdateJobCategory(JobCategory);

                jobCategories.First(f => f.JobCategoryId == JobCategory.JobCategoryId).JobCategoryName = JobCategory.JobCategoryName;

                JobCategoryId = 0;
                await updateSelectedJobCategoryAsync();

                Toaster.Success("Category name updated");

            }
            Updating = false;
        }

        async void RemoveJobCategory(int categorieId)
        {
            Updating = true;
            var result = await JobCategoryService.DeleteJobCategory(categorieId);
            if (result.IsSuccessStatusCode)
            {
                jobCategories.Remove(jobCategories.First(f => f.JobCategoryId == categorieId));

                JobCategoryId = 0;

                Toaster.Success("Category deleted.");
            }

            Updating = false;
        }

        public async Task SelectCategorieAsync(int categorieId)
        {
            JobCategoryId = categorieId;
            await updateSelectedJobCategoryAsync();
        }

        private async Task updatejobCategories()
        {
            jobCategories = (await JobCategoryService.GetJobCategories()).ToList();
        }

        private async Task updateSelectedJobCategoryAsync()
        {
            if (JobCategoryId == 0)
            {
                JobCategory = new JobCategory();
                return;
            }

            JobCategory = (await JobCategoryService.GetJobCategory(JobCategoryId));
        }
    }
}

