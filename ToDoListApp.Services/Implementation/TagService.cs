using ToDoListApp.Data.Repository;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _db;
        public TagService(IUnitOfWork db)
        {
            _db = db;
        }
        public int Create(CreateUpdateTagDto dto)
        {
            // we check user her api
            // layer will provide user id from token claims

            var tag = new Tag
            {
                TagName = dto.TagName,
              //  UserId = dto.UserId
            };
            _db.Tags.Add(tag); 
            _db.Save();

            return tag.TagId;   
        }
        public bool Delete(int id)
        {
            var tagFromDB =
                _db.Tags.Get(t => t.TagId == id);
            if (tagFromDB == null)
                return false;
            _db.Tags.Delete(tagFromDB); 
            _db.Save(); 
            return true;    
        }
        public TagDto? Get(int id)
        {
            var tagFromDB =
               _db.Tags.Get(t => t.TagId == id);
            if (tagFromDB == null)
                return null;
            var tagDto = new TagDto
            {
                TagId = tagFromDB.TagId,    
                TagName = tagFromDB.TagName
            }; 
            return tagDto;  
        }
        public IEnumerable<TagDto>? GetUserTags(string userId)
        {
            var tagsFromDB =
                _db.Tags.GetAll(t => t.UserId == userId).Select(t=> new TagDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName
                }).ToList();
            return tagsFromDB;
        }
        public bool Update(int id, CreateUpdateTagDto dto)
        {
            var tagFromDB =
                _db.Tags.Get(t => t.TagId == id ,Tracking : true);
            if (tagFromDB == null /*|| tagFromDB.UserId * != dto.userId  */)
                return false;
            tagFromDB.TagName = dto.TagName; 
            _db.Save(); 
            return true;
        }
    }
}
