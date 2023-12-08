using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.Seeds;

public class SeedDataDbContext
{
    private readonly DataDbContext _dataDbContext;
    private readonly ILogger<SeedDataDbContext> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedDataDbContext(DataDbContext dataDbContext, ILogger<SeedDataDbContext> logger,
        RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _dataDbContext = dataDbContext;
        _logger = logger;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            if (_dataDbContext.Database.IsSqlServer())
                await _dataDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to initialize the database.");
            throw;
        }
    }

    public async Task SeedContextDataAsync()
    {
        try
        {
            await TrySeedDataAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to seed the database.");
            throw;
        }
    }

    private async Task TrySeedDataAsync()
    {
        /* ====================== SEED QUALIFICATIONS ====================== */
        Qualification[] qualifications =
        {
            new() { QualificationName = "Доцент " },
            new() { QualificationName = "Старший викладач " },
            new() { QualificationName = "Асистент " },
            new() { QualificationName = "Професор " },
            new() { QualificationName = "Старший лаборант" },
            new() { QualificationName = "Інженер 1 категорії" },
            new() { QualificationName = "Завідувач кафедри" }
        };

        if (!await _dataDbContext.Qualifications.AnyAsync())
        {
            await _dataDbContext.Qualifications.AddRangeAsync(qualifications);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED FACULTIES ====================== */
        Faculty[] faculties =
        {
            new() { FacultyName = "Факультет інформатики та обчислювальної техніки", FacultyShortName = "ФІОТ" },
            new() { FacultyName = "Факультет лінгвістики", FacultyShortName = "ФЛ" },
            new() { FacultyName = "Факультет менеджменту та маркетингу", FacultyShortName = "ФММ" },
            new() { FacultyName = "Факультет прикладної математики", FacultyShortName = "ФПМ" },
            new() { FacultyName = "Факультет соціології і права", FacultyShortName = "ФСП" },
            new() { FacultyName = "Хіміко-технологічний факультет", FacultyShortName = "ХТФ" },
            new() { FacultyName = "Радіотехнічний факультет", FacultyShortName = "РФ" },
            new() { FacultyName = "Фізико-математичний факультет", FacultyShortName = "ФМФ" },
            new() { FacultyName = "Факультет біомедичної інженерії", FacultyShortName = "ФБІ" },
            new() { FacultyName = "Факультет біотехнології і біотехніки", FacultyShortName = "ФББ" },
            new() { FacultyName = "Факультет електроенерготехніки та автоматики", FacultyShortName = "ФЕА" }
        };

        if (!await _dataDbContext.Faculties.AnyAsync())
        {
            await _dataDbContext.Faculties.AddRangeAsync(faculties);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED SPECIALITIES ====================== */
        Specialty[] specialties =
        {
            new() { SpecialtyName = "Інформаційні системи та технології", SpecialityNumber = 126 },
            new() { SpecialtyName = "Кібербезпека", SpecialityNumber = 125 },
            new() { SpecialtyName = "Системний аналіз", SpecialityNumber = 124 },
            new() { SpecialtyName = "Комп'ютерна інженерія", SpecialityNumber = 123 },
            new() { SpecialtyName = "Комп'ютерні науки", SpecialityNumber = 122 },
            new() { SpecialtyName = "Екологія", SpecialityNumber = 101 },
            new() { SpecialtyName = "Фізика та астрономія", SpecialityNumber = 104 },
            new() { SpecialtyName = "Прикладна фізика та наноматеріали", SpecialityNumber = 105 },
            new() { SpecialtyName = "Математика", SpecialityNumber = 111 },
            new() { SpecialtyName = "Прикладна математика", SpecialityNumber = 113 },
            new() { SpecialtyName = "Журналістика", SpecialityNumber = 061 },
            new() { SpecialtyName = "Соціологія", SpecialityNumber = 054 },
            new() { SpecialtyName = "Економіка", SpecialityNumber = 051 },
            new() { SpecialtyName = "Філологія", SpecialityNumber = 035 },
            new() { SpecialtyName = "Образотворче мистецтво, декоративне мистецтво, реставрація", SpecialityNumber = 023 },
            new() { SpecialtyName = "Менеджмент", SpecialityNumber = 073 },
            new() { SpecialtyName = "Маркетинг", SpecialityNumber = 075 },
            new() { SpecialtyName = "Право", SpecialityNumber = 081 },
            new() { SpecialtyName = "Прикладна механіка", SpecialityNumber = 131 },
            new() { SpecialtyName = "Матеріалознавство", SpecialityNumber = 132 },
            new() { SpecialtyName = "Галузеве машинобудування", SpecialityNumber = 133 },
            new() { SpecialtyName = " Авіаційна та ракетно-космічна технік", SpecialityNumber = 134 }
        };

        if (!await _dataDbContext.Specialties.AnyAsync())
        {
            await _dataDbContext.Specialties.AddRangeAsync(specialties);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED LEARNING STATUSES ====================== */
        LearningStatus[] learningStatuses =
        {
            new() { LearningStatusName = "Навчається" },
            new() { LearningStatusName = "Відраховано" },
            new() { LearningStatusName = "Проходить календарний контроль" },
            new() { LearningStatusName = "Проходить сесію" }
        };

        if (!await _dataDbContext.LearningStatuses.AnyAsync())
        {
            await _dataDbContext.LearningStatuses.AddRangeAsync(learningStatuses);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED FORMATS LEARNING ====================== */
        FormatLearning[] formatLearnings =
        {
            new() { FormatLearningName = "Очна (денна)" },
            new() { FormatLearningName = "Очна (вечірня)" },
            new() { FormatLearningName = "Заочна" }
        };

        if (!await _dataDbContext.FormatLearnings.AnyAsync())
        {
            await _dataDbContext.FormatLearnings.AddRangeAsync(formatLearnings);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED GROUPS ====================== */
        Group[] groups =
        {
            new() { GroupName = "ІК-12", GroupCourse = 1 },
            new() { GroupName = "ІК-13", GroupCourse = 1 },
            new() { GroupName = "ІК-14", GroupCourse = 2 },
            new() { GroupName = "ІК-15", GroupCourse = 3 }
        };

        if (!await _dataDbContext.Groups.AnyAsync())
        {
            await _dataDbContext.Groups.AddRangeAsync(groups);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED ROLES ====================== */
        ApplicationRole[] defaultRoles =
        {
            new("admin", "Адмін."),
            new("teacher", "Вчитель."),
            new("student", "Студент."),
            new("guest", "Гість. Роль отримується одразу при реєстрації. Після реєстрації, admin " +
                         "користувач може змінити роль гостя на student або teacher у своєму списку.")
        };

        foreach (ApplicationRole defaultRole in defaultRoles)
        {
            bool isRoleExist = await _roleManager.RoleExistsAsync(defaultRole.Name!);

            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(defaultRole);
            }
        }
        
        /* ====================== SEED DEFAULT USERS ====================== */
        ApplicationUser[] defaultUsers =
        {
            new() { UserName = "admin", Email = "admin@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960498533" },
            new() { UserName = "guest1", Email = "guest1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960897523" },
            new() { UserName = "guest2", Email = "guest2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0864729518" },
            new() { UserName = "guest3", Email = "guest3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0125876912" },
            new() { UserName = "guest4", Email = "guest4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0598461579" },
            new() { UserName = "guest5", Email = "guest5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574129638" },
            new() { UserName = "guest6", Email = "guest6@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0258741369" },
            new() { UserName = "guest7", Email = "guest7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0973149317" },
            new() { UserName = "guest8", Email = "guest8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0963142567" },
            new() { UserName = "guest9", Email = "guest9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0142587369" },
            new() { UserName = "guest10", Email = "guest10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0546821793" },
            new() { UserName = "student1", Email = "student1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0529416720" },
            new() { UserName = "student2", Email = "student2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0540230596" },
            new() { UserName = "student3", Email = "student3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574100029" },
            new() { UserName = "student4", Email = "student4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0114558963" },
            new() { UserName = "student5", Email = "student5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0962587777" },
            new() { UserName = "teacher1", Email = "teacher1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0541290054" },
            new() { UserName = "teacher2", Email = "teacher2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0781230059" },
            new() { UserName = "teacher3", Email = "teacher3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960420099" },
            new() { UserName = "teacher4", Email = "teacher4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0127453688" },
            new() { UserName = "teacher5", Email = "teacher5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0124758222" }
        };

        AdminUser adminUser = new AdminUser
        {
            ApplicationUser = defaultUsers.FirstOrDefault(adminUserData => adminUserData.UserName == "admin"),
            FirstName = "Станіслав", LastName = "Володимирович", Patronymic = "Петренко"
        };

        GuestUser[] guestUsers =
        {
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest1"),
                FirstName = "Анатолій", LastName = "Боднаренко", Patronymic = "Олександрович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest2"),
                FirstName = "Юлія", LastName = "Тарасівна", Patronymic = "Броварчук"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest3"),
                FirstName = "Інна", LastName = "Василенко", Patronymic = "Андріївна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest4"),
                FirstName = "Наталія", LastName = "Боднаренко", Patronymic = "Олександрівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest5"),
                FirstName = "Остромисл", LastName = "Мойбенко", Patronymic = "Вадимович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest6"),
                FirstName = "Чеслав", LastName = "Ващенко", Patronymic = "Панасович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest7"),
                FirstName = "Євгеній", LastName = "Павленко", Patronymic = "Орестович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest8"),
                FirstName = "Любодар", LastName = "П'ятаченко", Patronymic = "Яромирович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest9"),
                FirstName = "Нагнибіда", LastName = "Бурій", Patronymic = "Устимович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest10"),
                FirstName = "Щастислав", LastName = "Коцюба", Patronymic = "Русланович"
            }
        };

        StudentUser[] studentUsers =
        {
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student1"),
                FirstName = "Фрол", LastName = "Гайдамаха", Patronymic = "Панасович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0], 
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student2"),
                FirstName = "Судивой", LastName = "Демчина", Patronymic = "Леонідович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0], 
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student3"),
                FirstName = "Августин", LastName = "Кабак", Patronymic = "Никодимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[1], 
                Faculty = faculties[1], Specialty = specialties[2], Group = groups[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student4"),
                FirstName = "Жито", LastName = "Зарицький", Patronymic = "Сарматович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0], 
                Faculty = faculties[2], Specialty = specialties[1], Group = groups[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student5"),
                FirstName = "Едуард", LastName = "Вітовський", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0], 
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[1]
            }
        };

        TeacherUser[] teacherUsers =
        {
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher1"),
                FirstName = "Лук'ян", LastName = "Юрковський", Patronymic = "Ігорович",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher2"),
                FirstName = "Щастибог", LastName = "Кулиняк", Patronymic = "Романович",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher3"),
                FirstName = "Андрющенко", LastName = "Фрол", Patronymic = "Іванович",
                Qualification = qualifications[2]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher4"),
                FirstName = "Лиско", LastName = "Любовир", Patronymic = "Ігорович",
                Qualification = qualifications[3]
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher5"),
                FirstName = "Юліан", LastName = "Карпека", Patronymic = "Найденович",
                Qualification = qualifications[1]
            },
        };

        if (!await _dataDbContext.Users.AnyAsync())
        {
            const string userPassword = "Test123";

            foreach (ApplicationUser user in defaultUsers)
            {
                await _userManager.CreateAsync(user, userPassword);
            }

            await _userManager.AddToRoleAsync(defaultUsers[1], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[2], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[3], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[4], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[5], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[6], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[7], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[8], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[9], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[10], defaultRoles[3].Name!);
            
            await _userManager.AddToRoleAsync(defaultUsers[11], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[12], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[13], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[14], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[15], defaultRoles[2].Name!);
            
            await _userManager.AddToRoleAsync(defaultUsers[16], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[17], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[18], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[19], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[20], defaultRoles[1].Name!);

            await _userManager.AddToRoleAsync(defaultUsers[0], defaultRoles[0].Name!);

            await _dataDbContext.GuestUsers.AddRangeAsync(guestUsers);
            await _dataDbContext.StudentUsers.AddRangeAsync(studentUsers);
            await _dataDbContext.TeacherUsers.AddRangeAsync(teacherUsers);
            await _dataDbContext.AdminUsers.AddAsync(adminUser);

            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED GROUPS-TEACHERS RELATIONSHIP ====================== */
        GroupTeacher[] groupTeachers =
        {
            new() { TeacherUser = teacherUsers[0], Group = groups[0] },
            new() { TeacherUser = teacherUsers[0], Group = groups[2] },
            new() { TeacherUser = teacherUsers[0], Group = groups[3] },
            new() { TeacherUser = teacherUsers[1], Group = groups[1] },
            new() { TeacherUser = teacherUsers[1], Group = groups[2] },
            new() { TeacherUser = teacherUsers[1], Group = groups[3] },
            new() { TeacherUser = teacherUsers[2], Group = groups[0] },
            new() { TeacherUser = teacherUsers[2], Group = groups[2] },
            new() { TeacherUser = teacherUsers[2], Group = groups[1] },
            new() { TeacherUser = teacherUsers[2], Group = groups[3] },
            new() { TeacherUser = teacherUsers[3], Group = groups[1] },
            new() { TeacherUser = teacherUsers[3], Group = groups[2] },
            new() { TeacherUser = teacherUsers[3], Group = groups[3] },
            new() { TeacherUser = teacherUsers[4], Group = groups[2] },
            new() { TeacherUser = teacherUsers[4], Group = groups[1] },
            new() { TeacherUser = teacherUsers[4], Group = groups[0] }
        };

        if (!await _dataDbContext.GroupTeachers.AnyAsync())
        {
            await _dataDbContext.GroupTeachers.AddRangeAsync(groupTeachers);
            await _dataDbContext.SaveChangesAsync();
        }
    }
}