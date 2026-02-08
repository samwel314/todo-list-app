using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Data.Repository;
using ToDoListApp.Data.Repository.Implementation;
using ToDoListApp.Models;
using ToDoListApp.Services;
using ToDoListApp.Services.Dto;
using ToDoListApp.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// work with swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "To-Do-List App",
        Version = "1.0",
        Description = "An API to manage your To-Do List"
    });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });

});
// add jwt authentication 

// get jwt settings 
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthorization();

// to make all responses standard
builder.Services.AddProblemDetails();
// Work With DB 

string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("No Connection Found");
builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(ConnectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Business Logic
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ITagService, TagService>();


builder.Services.AddIdentity<User, IdentityRole>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 10;
    o.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<IUserService, UserService>();
// لازم بعد بعد identity services عشان موضع authentication schema 
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(otp =>
{
    otp.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = jwtSettings["ValidAudiences"],
        ValidIssuer = jwtSettings["validIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
    //otp.Events = new JwtBearerEvents
    //{
    //    OnAuthenticationFailed = context =>
    //    {
    //        Console.WriteLine(context.Exception.Message);
    //        return Task.CompletedTask;
    //    }
    //};
});

var app = builder.Build();
// map endpoints 
RouteGroupBuilder taskAppApi = app.MapGroup("/api");
taskAppApi.MapPost("/Register",  async (UserRegisterDto model, IUserService Service) =>
{
    var result = await Service.RegisterNewUser(model); 
    if (result.Succeeded)
        return TypedResults.Created();
    var errors = string.Join(", ", result.Errors.Select(r => r.Description));

    return Results.Problem( detail : errors , statusCode : 400);
}).WithParameterValidation();
taskAppApi.MapPost("/Login", async (UserLoginDto model, IUserService service) =>
{
    var result = await service.ValidateUser(model);
    if (result.IsAuthenticated)
        return TypedResults.Ok(result); 
    return Results.Problem(detail: result.Message, statusCode: 401);
}).WithParameterValidation();
// task endPoints 
RouteGroupBuilder tasks = taskAppApi.MapGroup("/tasks").RequireAuthorization();
tasks.WithTags("Tasks");
tasks.MapGet("/", (ITaskService Service) =>
{
    return TypedResults.Ok(Service.GetAll());
});
tasks.MapGet("/{Id}", (int Id, ITaskService Service) =>
{
    var task = Service.Get(Id);
    if (task == null)
        return Results.Problem(statusCode: 404, detail: "Task not found");

    return TypedResults.Ok(task);
}).WithName("GetTaskById").Produces<TaskDetailsDto>(200).Produces(404);
tasks.MapPost("/", (CreateUpdateTaskDTo model, ITaskService Service, LinkGenerator link) =>
{
    if (!IsValidExpectedEndDate(model.ExpectedEndDate))
        return Results.Problem(statusCode: 400, detail: "This Date Must be after 30 Minutes");

    var taskId = Service.Create(model);
    if (taskId == 0)
        return Results.Problem(statusCode: 404, detail: "This Tag Not Found");
    var url = link.GetPathByName("GetTaskById", new { id = taskId });
    return TypedResults.Created(url);
}).WithParameterValidation().Produces(201).ProducesValidationProblem();
tasks.MapPut("/{id}", (int id, CreateUpdateTaskDTo model, ITaskService Service) =>
{
    // check model dto using validation library 
    return Service.Update(id, model) ? TypedResults.NoContent()
    : Results.Problem(statusCode: 404, detail: "Task Not Found");
}).WithParameterValidation().Produces(204).ProducesValidationProblem(400);
tasks.MapPatch("/{id}/ExtendTime", (int id, ChangeStatusDto model, ITaskService Service) =>
{
    if (!IsValidExpectedEndDate(model.ExpectedEndDate))
        return Results.Problem(statusCode: 400, detail: "This Date Must be after 30 Minutes");

    return Service.ExtendTime(id, model) ? TypedResults.NoContent()
: Results.Problem(statusCode: 404, detail: "Task Not Found Or This Action Not Allowed");
}).WithTags("Tasks").Produces(204).Produces(404);
tasks.MapPatch("/{id}", (int id, ChangeStatusDto model, ITaskService Service) =>
{
    if (model.NewStatus == null || !Enum.IsDefined(typeof(ToDoListApp.Models.TaskStatus), model.NewStatus.Value))
        return Results.Problem(statusCode: 400, detail: "This Value Is Not Defined ");

    return Service.ChangeStatus(id, model) ? TypedResults.NoContent()
: Results.Problem(statusCode: 404, detail: "Task Not Found Or This Action Not Allowed");
}).Produces(204).Produces(404);
tasks.MapDelete("/{id}", (int id, ITaskService Service) =>
{
    return Service.Delete(id) ? TypedResults.NoContent()
: Results.Problem(statusCode: 404, detail: "Task Not Found Or This Action Not Allowed");
}).Produces(204).Produces(404);

RouteGroupBuilder notes = taskAppApi.MapGroup("/notes").RequireAuthorization();
notes.WithTags("Notes");
notes.MapGet("/task/{taskId}", (int taskId, INoteService Service) =>
{
    var notes = Service.GetTaskNotes(taskId);
    return notes != null ? TypedResults.Ok(notes) :
    Results.Problem(statusCode: 404, detail: "This Task Not Found");
});
notes.MapGet("/{id}", (int id, INoteService Service) =>
{
    var note = Service.Get(id);
    return note != null ? TypedResults.Ok(note) :
    Results.Problem(statusCode: 404, detail: "This Note Not Found");
}).WithName("GetNoteById");
notes.MapPost("/task/{taskId}", (int taskId, CreateUpdateNoteDto model, INoteService Service, LinkGenerator link) =>
{
    // check model dto  Progress_Note
    var noteId = Service.Create(taskId, model);
    var url = link.GetPathByName("GetNoteById", new { id = noteId });
    return noteId != 0 ? TypedResults.Created(url) :
    Results.Problem(statusCode: 404, detail: "This Task Not Found");
}).WithParameterValidation();
notes.MapPut("/{id}", (int id, CreateUpdateNoteDto model, INoteService Service) =>
{
    // check model dto  Progress_Note
    return Service.Update(id, model) ? TypedResults.NoContent()
    : Results.Problem(statusCode: 404, detail: "This Note Not Found");
}).WithParameterValidation();
notes.MapDelete("/{id}", (int id, INoteService Service) =>
{
    return Service.Delete(id) ? TypedResults.NoContent()
: Results.Problem(statusCode: 404, detail: "This Note Not Found");
});

RouteGroupBuilder tags = taskAppApi.MapGroup("/tags").RequireAuthorization();
tags.WithTags("Tags");
tags.MapGet("/", (ITagService Service , ClaimsPrincipal user) =>
{
    var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var tags = Service.GetUserTags("");
    return TypedResults.Ok(tags);
});
tags.MapGet("/{id}", (int id, ITagService Service , ClaimsPrincipal user) =>
{
    // get user id from token and pass it to service soon
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value; 
    var tag = Service.Get(id , userId);
    return tag != null ? TypedResults.Ok(tag)
    : Results.Problem(statusCode: 404, detail: "This Tag Not Found");
}).WithName("GetTagById");
tags.MapPost("/", (CreateUpdateTagDto model,ClaimsPrincipal user ,  ITagService Service, LinkGenerator link) =>
{
    model.UserId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    var tagId = Service.Create(model);
    var url = link.GetPathByName("GetTagById", new { id = tagId });
    if (tagId == -1)
        return Results.Problem(statusCode: 400, detail: $"You Already Have {model.TagName} Tag "); 
    return tagId != 0 ? TypedResults.Created(url) :
    Results.Problem(statusCode: 400, detail: "Some Error , please try later");
});
tags.MapPut("/{id}", (int id,ClaimsPrincipal user , CreateUpdateTagDto model, ITagService Service) =>
{
    model.UserId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    return Service.Update(id, model) ? TypedResults.NoContent() :
    Results.Problem(statusCode: 404, detail: "This tag Not Found");
});
tags.MapDelete("/{id}", (int id, ClaimsPrincipal user,  ITagService Service) =>
{
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    return Service.Delete(id , userId) ? TypedResults.NoContent() :
    Results.Problem(statusCode: 404, detail: "This tag Not Found");
});

//app.MapHealthChecks("/healthz");
///
app.UseRouting();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();

bool IsValidExpectedEndDate(DateTime? time)
{
    if (time == null)
        return false;
    return time >= DateTime.Now.AddMinutes(30);
}


//add this package to the app 
// MinimalApis.Extensions 
