namespace WebApplication4.Database
{
    public interface INoticeRepositoryAsync : ICrudNoticeRepositoryAsync<Notice>
    {
        Task<Tuple<int, int>> GetStatus(int parentId);

        Task<bool> DeleteAllByParentId(int parentId);
    }
}
