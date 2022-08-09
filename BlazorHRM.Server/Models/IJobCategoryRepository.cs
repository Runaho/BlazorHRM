using BlazorHRM.Shared;

namespace BlazorHRM.Server.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
        JobCategory AddJobCategory(JobCategory jobCategory);
        JobCategory UpdateJobCategory(JobCategory JobCategory);
        bool DeleteJobCategory(int jobCategoryId);
    }
}
