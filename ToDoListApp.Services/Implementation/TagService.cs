using System.Xml;
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
          var haveTagWithThisName =  _db.Tags.GetAll().Any(t => t.TagName.ToLower() == dto.TagName.ToLower() && t.UserId == dto.UserId);
            if (haveTagWithThisName)
                return -1;
            var tag = new Tag
            {
                TagName = dto.TagName,
                UserId = dto.UserId!
            };
            _db.Tags.Add(tag); 
            _db.Save();

            return tag.TagId;   
        }
        public bool Delete(int id , string userId)
        {
            var tagFromDB =
                _db.Tags.Get(t => t.TagId == id && t.UserId == userId);
            if (tagFromDB == null)
                return false;
            _db.Tags.Delete(tagFromDB); 
            _db.Save(); 
            return true;    
        }
        public TagDto? Get(int id , string userId )
        {
            // we can replace this by authorization by resource 
            var tagFromDB =
               _db.Tags.Get(t => t.TagId == id && t.UserId == userId);
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
                _db.Tags.Get(t => t.TagId == id && t.UserId == dto.UserId ,Tracking : true );
            if (tagFromDB == null)
                return false;
            tagFromDB.TagName = dto.TagName; 
            _db.Save(); 
            return true;
        }
    }
}
