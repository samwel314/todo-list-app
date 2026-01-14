using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface ITagService
    {
        // add user soon  
        IEnumerable<TagDto>? GetUserTags(string userId);
        TagDto? Get(int id);
        bool Delete(int id);
        int Create (CreateUpdateTagDto dto);
        bool Update(int id, CreateUpdateTagDto dto);
    }   
}
