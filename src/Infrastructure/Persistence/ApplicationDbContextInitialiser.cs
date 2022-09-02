using Blog.Domain.Entities;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var publicRole = new IdentityRole("Public");

        if (_roleManager.Roles.All(r => r.Name != publicRole.Name))
        {
            await _roleManager.CreateAsync(publicRole);
        }

        var writerRole = new IdentityRole("Writer");

        if (_roleManager.Roles.All(r => r.Name != writerRole.Name))
        {
            await _roleManager.CreateAsync(writerRole);
        }

        var editorRole = new IdentityRole("Editor");

        if (_roleManager.Roles.All(r => r.Name != editorRole.Name))
        {
            await _roleManager.CreateAsync(editorRole);
        }

        //Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }

        //Default users
        var publicUser = new ApplicationUser { UserName = "publicuser@localhost", Email = "publicuser@localhost" };

        if (_userManager.Users.All(u => u.UserName != publicUser.UserName))
        {
            await _userManager.CreateAsync(publicUser, "Pa$$word1!");
            await _userManager.AddToRolesAsync(publicUser, new[] { publicRole.Name });
        }

        var writerUser = new ApplicationUser { UserName = "writeruser@localhost", Email = "writeruser@localhost" };

        if (_userManager.Users.All(u => u.UserName != writerUser.UserName))
        {
            await _userManager.CreateAsync(writerUser, "Pa$$word1!");
            await _userManager.AddToRolesAsync(writerUser, new[] { writerRole.Name });
        }

        var writerUser2 = new ApplicationUser { UserName = "writeruser2@localhost", Email = "writeruser2@localhost" };

        if (_userManager.Users.All(u => u.UserName != writerUser2.UserName))
        {
            await _userManager.CreateAsync(writerUser2, "Pa$$word1!");
            await _userManager.AddToRolesAsync(writerUser2, new[] { writerRole.Name });
        }

        var editorUser = new ApplicationUser { UserName = "editoruser@localhost", Email = "editoruser@localhost" };

        if (_userManager.Users.All(u => u.UserName != editorUser.UserName))
        {
            await _userManager.CreateAsync(editorUser, "Pa$$word1!");
            await _userManager.AddToRolesAsync(editorUser, new[] { editorRole.Name });
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
