using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategory>> GetJobCategories();
        Task<JobCategory> GetJobCategory(int jobCategoryId);
        Task<JobCategory> AddJobCategory(JobCategory JobCategory);
        Task<JobCategory> UpdateJobCategory(JobCategory JobCategory);
        Task<HttpResponseMessage> DeleteJobCategory(int jobCategoryId);
    }
}

