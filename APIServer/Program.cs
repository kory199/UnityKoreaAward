using APIServer;
using APIServer.Middleware;
using APIServer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));

builder.Services.AddSingleton<IMasterDataDb, MasterDataDb>();
builder.Services.AddTransient<IAccountDb, AccountDb>();
builder.Services.AddTransient<IAttendanceDb,AttendanceDb>();
builder.Services.AddTransient<IGameDb, GameDb>();
builder.Services.AddTransient<IStageDb, StageDb>();
builder.Services.AddSingleton<INextStage, NextStageDb>();
builder.Services.AddSingleton<IMemoryDb, RedisDb>();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        googleOptions.CallbackPath = "/Account/GoogleResponse"; // 리디렉션 URI 설정
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
LogManager.SetLoggerFactory(loggerFactory, "Global");

var masterdata = app.Services.GetService<IMasterDataDb>();
await masterdata.LoadAllMasterDataAsync();

var getStageInfo = app.Services.GetRequiredService<INextStage>();
await getStageInfo.LoadStageDataAsync();

app.UseRouting();

app.UseMiddleware<CheckUserAuth>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var redisDb = app.Services.GetRequiredService<IMemoryDb>();
var redisAddress = configuration.GetSection("DbConfig")["Redis"];
if (string.IsNullOrWhiteSpace(redisAddress))
{
    throw new Exception("Redis Address Is Not Defined In The Configuration");
}

redisDb.Init(redisAddress);

app.Run();