using ChatAppApi.Hubs;
using ChatAppApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseInMemoryDatabase("ChatApp");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddSignalR();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("https://localhost:44398")
            .AllowCredentials();
    });
});

builder.Services.AddSingleton(typeof(MessagesService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//localhost:1311/api/account
app.MapControllers();

//localhost:1311/chathub
app.MapHub<ChatHub>("/chathub");

app.Run();
