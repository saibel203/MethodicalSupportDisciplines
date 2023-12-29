using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Shared.Enums;
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
            new() { SpecialityName = "Інформаційні системи та технології", SpecialityNumber = 126 },
            new() { SpecialityName = "Кібербезпека", SpecialityNumber = 125 },
            new() { SpecialityName = "Системний аналіз", SpecialityNumber = 124 },
            new() { SpecialityName = "Комп'ютерна інженерія", SpecialityNumber = 123 },
            new() { SpecialityName = "Комп'ютерні науки", SpecialityNumber = 122 },
            new() { SpecialityName = "Екологія", SpecialityNumber = 101 },
            new() { SpecialityName = "Фізика та астрономія", SpecialityNumber = 104 },
            new() { SpecialityName = "Прикладна фізика та наноматеріали", SpecialityNumber = 105 },
            new() { SpecialityName = "Математика", SpecialityNumber = 111 },
            new() { SpecialityName = "Прикладна математика", SpecialityNumber = 113 },
            new() { SpecialityName = "Журналістика", SpecialityNumber = 061 },
            new() { SpecialityName = "Соціологія", SpecialityNumber = 054 },
            new() { SpecialityName = "Економіка", SpecialityNumber = 051 },
            new() { SpecialityName = "Філологія", SpecialityNumber = 035 },
            new()
            {
                SpecialityName = "Образотворче мистецтво, декоративне мистецтво, реставрація", SpecialityNumber = 023
            },
            new() { SpecialityName = "Менеджмент", SpecialityNumber = 073 },
            new() { SpecialityName = "Маркетинг", SpecialityNumber = 075 },
            new() { SpecialityName = "Право", SpecialityNumber = 081 },
            new() { SpecialityName = "Прикладна механіка", SpecialityNumber = 131 },
            new() { SpecialityName = "Матеріалознавство", SpecialityNumber = 132 },
            new() { SpecialityName = "Галузеве машинобудування", SpecialityNumber = 133 },
            new() { SpecialityName = " Авіаційна та ракетно-космічна технік", SpecialityNumber = 134 }
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
            new()
            {
                UserName = "admin", Email = "admin@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960498533"
            },
            new()
            {
                UserName = "guest1", Email = "guest1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960897523"
            },
            new()
            {
                UserName = "guest2", Email = "guest2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0864729518"
            },
            new()
            {
                UserName = "guest3", Email = "guest3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0125876912"
            },
            new()
            {
                UserName = "guest4", Email = "guest4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0598461579"
            },
            new()
            {
                UserName = "guest5", Email = "guest5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574129638"
            },
            new()
            {
                UserName = "guest6", Email = "guest6@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0258741369"
            },
            new()
            {
                UserName = "guest7", Email = "guest7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0973149317"
            },
            new()
            {
                UserName = "guest8", Email = "guest8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0963142567"
            },
            new()
            {
                UserName = "guest9", Email = "guest9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0142587369"
            },
            new()
            {
                UserName = "guest10", Email = "guest10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0546821793"
            },
            new()
            {
                UserName = "student1", Email = "student1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0529416720"
            },
            new()
            {
                UserName = "student2", Email = "student2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0540230596"
            },
            new()
            {
                UserName = "student3", Email = "student3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574100029"
            },
            new()
            {
                UserName = "student4", Email = "student4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0114558963"
            },
            new()
            {
                UserName = "student5", Email = "student5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0962587777"
            },
            new()
            {
                UserName = "teacher1", Email = "teacher1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0541290054"
            },
            new()
            {
                UserName = "teacher2", Email = "teacher2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0781230059"
            },
            new()
            {
                UserName = "teacher3", Email = "teacher3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960420099"
            },
            new()
            {
                UserName = "teacher4", Email = "teacher4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0127453688"
            },
            new()
            {
                UserName = "teacher5", Email = "teacher5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0124758222"
            },
            new()
            {
                UserName = "guest11", Email = "guest11@gmail.com", EmailConfirmed = true, // 21
                PhoneNumber = "0267710000"
            },
            new()
            {
                UserName = "guest12", Email = "guest12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0731500000"
            },
            new()
            {
                UserName = "guest13", Email = "guest13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0500000001"
            },
            new()
            {
                UserName = "guest14", Email = "guest14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0462011256"
            },
            new()
            {
                UserName = "guest15", Email = "guest15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0624700000"
            },
            new()
            {
                UserName = "guest16", Email = "guest16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070405"
            },
            new()
            {
                UserName = "guest17", Email = "guest17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0102030405"
            },
            new()
            {
                UserName = "guest18", Email = "guest18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "6985478966"
            },
            new()
            {
                UserName = "guest19", Email = "guest19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7896541230"
            },
            new()
            {
                UserName = "guest20", Email = "guest20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "9632587410"
            },
            new()
            {
                UserName = "guest21", Email = "guest21@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070605"
            },
            new()
            {
                UserName = "guest22", Email = "guest22@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0102030904"
            },
            new()
            {
                UserName = "guest23", Email = "guest23@gmail.com", EmailConfirmed = true,
                PhoneNumber = "5040102026"
            },
            new()
            {
                UserName = "guest24", Email = "guest24@gmail.com", EmailConfirmed = true,
                PhoneNumber = "9080704050"
            },
            new()
            {
                UserName = "guest25", Email = "guest25@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7080604020"
            },
            new()
            {
                UserName = "guest26", Email = "guest26@gmail.com", EmailConfirmed = true, // 36
                PhoneNumber = "9060200104"
            },
            new()
            {
                UserName = "teacher6", Email = "teacher6@gmail.com", EmailConfirmed = true, // 37
                PhoneNumber = "0945038363"
            },
            new()
            {
                UserName = "teacher7", Email = "teacher7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0900800700"
            },
            new()
            {
                UserName = "teacher8", Email = "teacher8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0100000800"
            },
            new()
            {
                UserName = "teacher9", Email = "teacher9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0110022547"
            },
            new()
            {
                UserName = "teacher10", Email = "teacher10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0978470008"
            },
            new()
            {
                UserName = "teacher11", Email = "teacher11@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0700000800"
            },
            new()
            {
                UserName = "teacher12", Email = "teacher12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "010000010"
            },
            new()
            {
                UserName = "teacher13", Email = "teacher13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0200000020"
            },
            new()
            {
                UserName = "teacher14", Email = "teacher14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0200000020"
            },
            new()
            {
                UserName = "teacher15", Email = "teacher15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0300000300"
            },
            new()
            {
                UserName = "teacher16", Email = "teacher16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0400000040"
            },
            new()
            {
                UserName = "teacher17", Email = "teacher17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0500000050"
            },
            new()
            {
                UserName = "teacher18", Email = "teacher18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0600000060"
            },
            new()
            {
                UserName = "teacher19", Email = "teacher19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0700000070"
            },
            new()
            {
                UserName = "teacher20", Email = "teacher20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0800000080"
            },
            new()
            {
                UserName = "teacher21", Email = "teacher21@gmail.com", EmailConfirmed = true, // 52
                PhoneNumber = "0900000090"
            },
            new()
            {
                UserName = "student6", Email = "student6@gmail.com", EmailConfirmed = true, // 53
                PhoneNumber = "0403020100"
            },
            new()
            {
                UserName = "student7", Email = "student7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0504030201"
            },
            new()
            {
                UserName = "student8", Email = "student8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0605040630"
            },
            new()
            {
                UserName = "student9", Email = "student9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0706050403"
            },
            new()
            {
                UserName = "student10", Email = "student10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0807060504"
            },
            new()
            {
                UserName = "student11", Email = "student11@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070605"
            },
            new()
            {
                UserName = "student12", Email = "student12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0090807060"
            },
            new()
            {
                UserName = "student13", Email = "student13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "1090807060"
            },
            new()
            {
                UserName = "student14", Email = "student14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "2010908070"
            },
            new()
            {
                UserName = "student15", Email = "student15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "3020109080"
            },
            new()
            {
                UserName = "student16", Email = "student16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "4030201090"
            },
            new()
            {
                UserName = "student17", Email = "student17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "5040302010"
            },
            new()
            {
                UserName = "student18", Email = "student18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "6050403020"
            },
            new()
            {
                UserName = "student19", Email = "student19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7060504030"
            },
            new()
            {
                UserName = "student20", Email = "student20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "8070605040"
            },
            new()
            {
                UserName = "student21", Email = "student21@gmail.com", EmailConfirmed = true, // 68
                PhoneNumber = "9080706059"
            }
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
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest11"),
                FirstName = "Устина", LastName = "Яськевич", Patronymic = "Найденівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest12"),
                FirstName = "Йосипа", LastName = "Стефанівська", Patronymic = "Зорянівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest13"),
                FirstName = "Мар'я", LastName = "Коцюба", Patronymic = "Русланович"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest14"),
                FirstName = "Харитина", LastName = "Дзюба", Patronymic = "Левівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest15"),
                FirstName = "Степанова", LastName = "Дорофея", Patronymic = "Юхимівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest16"),
                FirstName = "Найдена", LastName = "Коровченко", Patronymic = "Артемівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest17"),
                FirstName = "Йосипа", LastName = "Удовенко", Patronymic = "Ростиславівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest18"),
                FirstName = "Люборада", LastName = "Ярич", Patronymic = "Глібівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest19"),
                FirstName = "Євгенія", LastName = "Сільченко", Patronymic = "Панасівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest20"),
                FirstName = "Єва", LastName = "Ярич", Patronymic = "Янівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest21"),
                FirstName = "Чеслава", LastName = "Чумак", Patronymic = "Устимівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest22"),
                FirstName = "Чеслава", LastName = "Савків", Patronymic = "Чеславівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest23"),
                FirstName = "Жозефіна", LastName = "Савойка", Patronymic = "Валентинівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest24"),
                FirstName = "Пульхера", LastName = "Іваничук", Patronymic = "Левівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest25"),
                FirstName = "Берта", LastName = "Зінкевич", Patronymic = "Давидівна"
            },
            new()
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest26"),
                FirstName = "Євдокія", LastName = "Комар", Patronymic = "Янівна"
            },
        };

        StudentUser[] studentUsers =
        {
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student1"),
                FirstName = "Фрол", LastName = "Гайдамаха", Patronymic = "Панасович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student2"),
                FirstName = "Судивой", LastName = "Демчина", Patronymic = "Леонідович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student3"),
                FirstName = "Августин", LastName = "Кабак", Patronymic = "Никодимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[1],
                Faculty = faculties[1], Specialty = specialties[2], Group = groups[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student4"),
                FirstName = "Жито", LastName = "Зарицький", Patronymic = "Сарматович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Specialty = specialties[0], Group = groups[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student5"),
                FirstName = "Едуард", LastName = "Вітовський", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Specialty = specialties[5], Group = groups[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student6"),
                FirstName = "Клара", LastName = "Богданюк", Patronymic = "Адріанівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[4], Specialty = specialties[3], Group = groups[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student7"),
                FirstName = "Єлизавета", LastName = "Караванська", Patronymic = "Полянівна",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[4], Specialty = specialties[3], Group = groups[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student8"),
                FirstName = "Борислава", LastName = "Коман", Patronymic = "Тимурівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[1],
                Faculty = faculties[3], Specialty = specialties[3], Group = groups[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student9"),
                FirstName = "Шушана", LastName = "Капуста", Patronymic = "Вікторівна",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Specialty = specialties[3], Group = groups[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student10"),
                FirstName = "Йовілла", LastName = "Йовілла", Patronymic = "Драганівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Specialty = specialties[2], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student11"),
                FirstName = "Карина", LastName = "Фурсенко", Patronymic = "Ярославівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Specialty = specialties[2], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student12"),
                FirstName = "Недан", LastName = "Таранюк", Patronymic = "Олександрович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Specialty = specialties[2], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student13"),
                FirstName = "Живорід", LastName = "Багінський", Patronymic = "Артурович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Specialty = specialties[2], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student14"),
                FirstName = "Флор", LastName = "Белмачовський", Patronymic = "Радимович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Specialty = specialties[1], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student15"),
                FirstName = "Едуард", LastName = "Вітовський", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Specialty = specialties[1], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student16"),
                FirstName = "Голубко", LastName = "Мосійчук", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Specialty = specialties[1], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student17"),
                FirstName = "Рудан", LastName = "Шматько", Patronymic = "Денисович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Specialty = specialties[1], Group = groups[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student18"),
                FirstName = "Уличан", LastName = "Машковський", Patronymic = "Захарович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Specialty = specialties[0], Group = groups[1]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student19"),
                FirstName = "Буйтур", LastName = "Боберський", Patronymic = "Іванович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Specialty = specialties[0], Group = groups[1]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student20"),
                FirstName = "Щастибог", LastName = "Ржепішевський", Patronymic = "Леонідович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[1]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student21"),
                FirstName = "Іларіон", LastName = "Авдуєвський", Patronymic = "Юліанович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Specialty = specialties[0], Group = groups[1]
            }
        };

        TeacherUser[] teacherUsers =
        {
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher1"),
                FirstName = "Лук'ян", LastName = "Юрковський", Patronymic = "Ігорович",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher2"),
                FirstName = "Щастибог", LastName = "Кулиняк", Patronymic = "Романович",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher3"),
                FirstName = "Андрющенко", LastName = "Фрол", Patronymic = "Іванович",
                Qualification = qualifications[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher4"),
                FirstName = "Лиско", LastName = "Любовир", Patronymic = "Ігорович",
                Qualification = qualifications[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher5"),
                FirstName = "Юліан", LastName = "Карпека", Patronymic = "Найденович",
                Qualification = qualifications[1]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher6"),
                FirstName = "Юлій", LastName = "Миколаєнко", Patronymic = "Яромирович",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher7"),
                FirstName = "Злат", LastName = "Врецьона", Patronymic = "Денисович",
                Qualification = qualifications[6]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher8"),
                FirstName = "Флор", LastName = "Громико", Patronymic = "Вітанович",
                Qualification = qualifications[5]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher9"),
                FirstName = "Житомир", LastName = "Єщенко", Patronymic = "Тихонович",
                Qualification = qualifications[4]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher10"),
                FirstName = "Віола", LastName = "Семеренко", Patronymic = "Полянівна",
                Qualification = qualifications[4]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher11"),
                FirstName = "Уляна", LastName = "Загороднюк", Patronymic = "Адамівна",
                Qualification = qualifications[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher12"),
                FirstName = "Осипа", LastName = "Борейко", Patronymic = "Федорівна",
                Qualification = qualifications[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher13"),
                FirstName = "Уляна", LastName = "Загороднюк", Patronymic = "Адамівна",
                Qualification = qualifications[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher14"),
                FirstName = "Осипа", LastName = "Борейко", Patronymic = "Федорівна",
                Qualification = qualifications[2]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher15"),
                FirstName = "Алла", LastName = "Колесник", Patronymic = "Златівна",
                Qualification = qualifications[0]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher16"),
                FirstName = "Шарлота", LastName = "Гісем", Patronymic = "Левівна",
                Qualification = qualifications[6]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher17"),
                FirstName = "Юхимина", LastName = "Засенко", Patronymic = "Соломонівна",
                Qualification = qualifications[1]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher18"),
                FirstName = "Цецілія", LastName = "Степула", Patronymic = "Адамівна",
                Qualification = qualifications[5]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher19"),
                FirstName = "Бажана", LastName = "Пероганич", Patronymic = "Тарасівна",
                Qualification = qualifications[4]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher20"),
                FirstName = "Любаня", LastName = "Юрчишин", Patronymic = "Леонідівна",
                Qualification = qualifications[3]
            },
            new()
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher21"),
                FirstName = "Хриса", LastName = "Бутенко", Patronymic = "Романівна",
                Qualification = qualifications[2]
            }
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
            await _userManager.AddToRoleAsync(defaultUsers[21], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[22], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[23], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[24], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[25], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[26], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[27], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[28], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[29], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[30], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[31], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[32], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[33], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[34], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[35], defaultRoles[3].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[36], defaultRoles[3].Name!);

            await _userManager.AddToRoleAsync(defaultUsers[11], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[12], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[13], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[14], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[15], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[53], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[54], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[55], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[56], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[57], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[58], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[59], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[60], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[61], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[62], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[63], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[64], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[65], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[66], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[67], defaultRoles[2].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[68], defaultRoles[2].Name!);

            await _userManager.AddToRoleAsync(defaultUsers[16], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[17], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[18], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[19], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[20], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[37], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[38], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[39], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[40], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[41], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[42], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[43], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[44], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[45], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[46], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[47], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[48], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[49], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[50], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[51], defaultRoles[1].Name!);
            await _userManager.AddToRoleAsync(defaultUsers[52], defaultRoles[1].Name!);

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
        
        /* ====================== SEED DISCIPLINES RELATIONSHIP ====================== */
        Discipline[] disciplines =
        {
            new() { DisciplineName = "Технології розробки програмного забезпечення (ТРПЗ)", Teacher = teacherUsers[0] },
            new() { DisciplineName = "Основи Front-end технологій", Teacher = teacherUsers[0] },
            new() { DisciplineName = "Технологія блокчейн", Teacher = teacherUsers[0] },
            new() { DisciplineName = "Сучасні технології розробки на .NET", Teacher = teacherUsers[0] },
            new() { DisciplineName = "Філософія", Teacher = teacherUsers[1] },
            new() { DisciplineName = "Соціальна психологія", Teacher = teacherUsers[1] },
            new() { DisciplineName = "Історія сучасної України", Teacher = teacherUsers[1] },
            new() { DisciplineName = "Безпека інформаційний систем (БІС)", Teacher = teacherUsers[1] },
            new() { DisciplineName = "Життєвий цикл розробки програмного забезпечення", Teacher = teacherUsers[2] },
            new() { DisciplineName = "Методи та технології штучного інтелекту", Teacher = teacherUsers[2] },
            new() { DisciplineName = "Комп'ютерна графіка та мультимедія", Teacher = teacherUsers[2] },
            new() { DisciplineName = "Основи Back-end технологій", Teacher = teacherUsers[2] },
            new() { DisciplineName = "Розробка програмного забезпечення на платформі Java", Teacher = teacherUsers[3] },
            new() { DisciplineName = "Технології розробки та просування сайтів", Teacher = teacherUsers[3] },
            new() { DisciplineName = "Системне програмування С і С++", Teacher = teacherUsers[3] },
            new() { DisciplineName = "Розробка програмного забезпечення на платформі Node.JS", Teacher = teacherUsers[3] },
            new() { DisciplineName = "Права і свободи людини", Teacher = teacherUsers[4] },
            new() { DisciplineName = "Інформаційно-сенсорні системи роботів", Teacher = teacherUsers[4] },
            new() { DisciplineName = "Бази даних", Teacher = teacherUsers[5] },
            new() { DisciplineName = "CAD-системи та мультимедіа", Teacher = teacherUsers[5] },
            new() { DisciplineName = "Комп'ютерні мережі", Teacher = teacherUsers[6] },
            new() { DisciplineName = "Економічна психологія", Teacher = teacherUsers[6] },
            new() { DisciplineName = "Практичний курс іноземної мови. Частина 1", Teacher = teacherUsers[7] },
            new() { DisciplineName = "Практичний курс іноземної мови. Частина 2", Teacher = teacherUsers[8] },
            new() { DisciplineName = "Захист персональних даних: стандарти ЄС та Ради Європи", Teacher = teacherUsers[9] }
        };

        if (!await _dataDbContext.Disciplines.AnyAsync())
        {
            await _dataDbContext.Disciplines.AddRangeAsync(disciplines);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED DISCIPLINE-GROUP RELATIONSHIP ====================== */
        DisciplineGroup[] disciplineGroups = // 25 disciplines, 4 groups
        {
            new() { DisciplineId = 1, GroupId = 3 },
            new() { DisciplineId = 2, GroupId = 4 },
            new() { DisciplineId = 3, GroupId = 2 },
            new() { DisciplineId = 4, GroupId = 1 },
            new() { DisciplineId = 5, GroupId = 1 },
            new() { DisciplineId = 5, GroupId = 3 },
            new() { DisciplineId = 5, GroupId = 4 },
            new() { DisciplineId = 6, GroupId = 1 },
            new() { DisciplineId = 7, GroupId = 4 },
            new() { DisciplineId = 8, GroupId = 1 },
            new() { DisciplineId = 8, GroupId = 4 },
            new() { DisciplineId = 8, GroupId = 3 },
            new() { DisciplineId = 8, GroupId = 2 },
            new() { DisciplineId = 9, GroupId = 3 },
            new() { DisciplineId = 9, GroupId = 4 },
            new() { DisciplineId = 10, GroupId = 1 },
            new() { DisciplineId = 10, GroupId = 3 },
            new() { DisciplineId = 10, GroupId = 2 },
            new() { DisciplineId = 11, GroupId = 2 },
            new() { DisciplineId = 12, GroupId = 4 },
            new() { DisciplineId = 12, GroupId = 3 },
            new() { DisciplineId = 13, GroupId = 1 },
            new() { DisciplineId = 13, GroupId = 4 },
            new() { DisciplineId = 14, GroupId = 3 },
            new() { DisciplineId = 15, GroupId = 4 },
            new() { DisciplineId = 15, GroupId = 2 },
            new() { DisciplineId = 16, GroupId = 1 },
            new() { DisciplineId = 17, GroupId = 2 },
            new() { DisciplineId = 18, GroupId = 3 },
            new() { DisciplineId = 19, GroupId = 4 },
            new() { DisciplineId = 20, GroupId = 1 },
            new() { DisciplineId = 20, GroupId = 2 },
            new() { DisciplineId = 20, GroupId = 3 },
            new() { DisciplineId = 21, GroupId = 4 },
            new() { DisciplineId = 21, GroupId = 2 },
            new() { DisciplineId = 22, GroupId = 3 },
            new() { DisciplineId = 22, GroupId = 4 },
            new() { DisciplineId = 23, GroupId = 1 },
            new() { DisciplineId = 23, GroupId = 2 },
            new() { DisciplineId = 23, GroupId = 3 },
            new() { DisciplineId = 24, GroupId = 4 },
            new() { DisciplineId = 24, GroupId = 1 },
            new() { DisciplineId = 24, GroupId = 2 },
            new() { DisciplineId = 25, GroupId = 3 },
            new() { DisciplineId = 25, GroupId = 4 }
        };

        if (!await _dataDbContext.DisciplineGroups.AnyAsync())
        {
            await _dataDbContext.DisciplineGroups.AddRangeAsync(disciplineGroups);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED DISCIPLINE MATERIALS RELATIONSHIP ====================== */
        DisciplineMaterial[] disciplineMaterials =
        {
            new()
            {
                DisciplineMaterialName = "Лекція #1",
                DisciplineMaterialDescription = "Введення в HTML та HTML5",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.LectureType
            },
            new()
            {
                DisciplineMaterialName = "Лекція #2",
                DisciplineMaterialDescription = "Основи CSS та CSS3",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.LectureType
            },
            new()
            {
                DisciplineMaterialName = "Лекція #3",
                DisciplineMaterialDescription = "Верстка та розмітка веб-сторінок",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.LectureType
            },
            new()
            {
                DisciplineMaterialName = "Лекція #4",
                DisciplineMaterialDescription = "Основи JavaScript",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.LectureType
            },
            new()
            {
                DisciplineMaterialName = "Лекція #5",
                DisciplineMaterialDescription = "Робота з DOM (Document Object Model)",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.LectureType
            },
            new()
            {
                DisciplineMaterialName = "Практичне завдання #1",
                DisciplineMaterialDescription = "Створити просту HTML/CSS верстку single-page, макет додано",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.PracticeType
            },
            new()
            {
                DisciplineMaterialName = "Практичне завдання #2",
                DisciplineMaterialDescription = "Добавити модальне вікно, яке буде відкриватися при натисканні",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.PracticeType
            },
            new()
            {
                DisciplineMaterialName = "Практичне завдання #3",
                DisciplineMaterialDescription = "Зробити slide-bar на головній сторінці",
                DisciplineId = 2,
                DisciplineMaterialType = (int)DisciplineMaterialType.PracticeType
            }
        };

        if (!await _dataDbContext.DisciplineMaterials.AnyAsync())
        {
            await _dataDbContext.DisciplineMaterials.AddRangeAsync(disciplineMaterials);
            await _dataDbContext.SaveChangesAsync();
        }
    }
}