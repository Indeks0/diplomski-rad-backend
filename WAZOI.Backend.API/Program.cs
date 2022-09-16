using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WAZOI.Backend.DAL;
using WAZOI.Backend.DAL.Entities;
using WAZOI.Backend.Infrastructure;
using WAZOI.Backend.Models;
using WAZOI.Backend.Models.Common;

using WAZOI.Backend.Models;

using WAZOI.Backend.Repository;
using WAZOI.Backend.Repository.Common;
using WAZOI.Backend.Services.Common.MailService;
using WAZOI.Backend.Services.MailService;
using WAZOI.Backend.API;
using System.Security.Claims;
using WAZOI.Backend.Services;
using WAZOI.Backend.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard auth header using the Bearer scheme",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ISubjectDto, SubjectDto>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<ISubjectService, SubjectService>();

builder.Services.AddTransient<IExamDto, ExamDto>();
builder.Services.AddTransient<IExamRepository, ExamRepository>();
builder.Services.AddTransient<IExamService, ExamService>();

builder.Services.AddTransient<ISolvedExamSubmitDto, SolvedExamSubmitDto>();
builder.Services.AddTransient<ISolvedExamAnswerDto, SolvedExamAnswerDto>();

builder.Services.AddTransient<IExamQuestionDto, ExamQuestionDto>();
builder.Services.AddTransient<IExamQuestionService, ExamQuestionService>();
builder.Services.AddTransient<IExamQuestionRepository, ExamQuestionRepository>();

builder.Services.AddTransient<ISolvedExamDto, SolvedExamDto>();
builder.Services.AddTransient<ISolvedExamService, SolvedExamService>();
builder.Services.AddTransient<ISolvedExamRepository, SolvedExamRepository>();

builder.Services.AddTransient<IExamStudentDto, ExamStudentDto>();
builder.Services.AddTransient<IExamStudentRepository, ExamStudentRepository>();
builder.Services.AddTransient<IExamStudentService, ExamStudentService>();

builder.Services.AddTransient<ISubjectGradingCriteriaDto, SubjectGradingCriteriaDto>();
builder.Services.AddTransient<ISubjectGradingCriteriaRepository, SubjectGradingCriteriaRepository>();
builder.Services.AddTransient<ISubjectGradingCriteriaService, SubjectGradingCriteriaService>();

builder.Services.AddTransient<IQuestionDto, QuestionDto>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IQuestionService, QuestionService>();

builder.Services.AddTransient<ISubjectNoticeDto, SubjectNoticeDto>();
builder.Services.AddTransient<ISubjectNoticeRepository, SubjectNoticeRepository>();
builder.Services.AddTransient<ISubjectNoticeService, SubjectNoticeService>();

builder.Services.AddTransient<ISubjectStudentDto, SubjectStudentDto>();
builder.Services.AddTransient<ISubjectStudentRepository, SubjectStudentRepository>();
builder.Services.AddTransient<ISubjectStudentService, SubjectStudentService>();

builder.Services.AddTransient<ISubjectTeacherDto, SubjectTeacherDto>();
builder.Services.AddTransient<ISubjectTeacherRepository, SubjectTeacherRepository>();
builder.Services.AddTransient<ISubjectTeacherService, SubjectTeacherService>();

builder.Services.AddTransient<IUserSubjectJoinRequestDto, UserSubjectJoinRequestDto>();
builder.Services.AddTransient<IUserSubjectJoinRequestRepository, UserSubjectJoinRequestRepository>();
builder.Services.AddTransient<IUserSubjectJoinRequestService, UserSubjectJoinRequestService>();

builder.Services.AddHostedService<ExamsLockingWorker>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
{
    opt.SignIn.RequireConfirmedAccount = true;
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    opt.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
})
    .AddEntityFrameworkStores<AppDataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.Configure<FrontendDomainOptions>(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();