using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface ITagService
    {
        // add user soon  
        IEnumerable<TagDto>? GetUserTags(string userId);
        TagDto? Get(int id, string userId);
        bool Delete(int id , string userId);
        int Create (CreateUpdateTagDto dto);
        bool Update(int id, CreateUpdateTagDto dto);
    }   
}
