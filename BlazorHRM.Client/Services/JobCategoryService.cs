using System;
using BlazorHRM.Shared;

namespace BlazorHRM.Client.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        public HttpClient _httpClient { get; }

        public JobCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobCategory>> GetJobCategories()
        {
            return await new ServiceRequestHelper<IEnumerable<JobCategory>>(_httpClient).GetAsync("jobCategories");
        }

        public async Task<JobCategory> GetJobCategory(int jobCategoryId)
        {
            return await new ServiceRequestHelper<JobCategory>(_httpClient).GetAsync("jobCategory", $"?id={jobCategoryId}");
        }

        public async Task<JobCategory> AddJobCategory(JobCategory JobCategory)
        {
            return await new ServiceRequestHelper<JobCategory>(_httpClient).SendAsync("jobCategory", JobCategory, RequestTypes.Post);
        }

        public async Task<JobCategory> UpdateJobCategory(JobCategory JobCategory)
        {
            return await new ServiceRequestHelper<JobCategory>(_httpClient).SendAsync("jobCategory", JobCategory, RequestTypes.Put);
        }

        public async Task<HttpResponseMessage> DeleteJobCategory(int jobCategoryId)
        {
            return await new ServiceRequestHelper<JobCategory>(_httpClient).DeleteAsync("jobCategory", jobCategoryId.ToString());
        }
    }
}

