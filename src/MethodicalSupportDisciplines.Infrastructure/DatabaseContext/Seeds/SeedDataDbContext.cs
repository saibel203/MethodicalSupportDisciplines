using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DisciplineMaterialType = MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.DisciplineMaterialType;

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
        [
            new Qualification { QualificationName = "Доцент " },
            new Qualification { QualificationName = "Старший викладач " },
            new Qualification { QualificationName = "Асистент " },
            new Qualification { QualificationName = "Професор " },
            new Qualification { QualificationName = "Старший лаборант" },
            new Qualification { QualificationName = "Інженер 1 категорії" },
            new Qualification { QualificationName = "Завідувач кафедри" }
        ];

        if (!await _dataDbContext.Qualifications.AnyAsync())
        {
            await _dataDbContext.Qualifications.AddRangeAsync(qualifications);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED FACULTIES ====================== */
        Faculty[] faculties =
        [
            new Faculty { FacultyName = "Факультет інформатики та обчислювальної техніки", FacultyShortName = "ФІОТ" },
            new Faculty { FacultyName = "Факультет лінгвістики", FacultyShortName = "ФЛ" },
            new Faculty { FacultyName = "Факультет менеджменту та маркетингу", FacultyShortName = "ФММ" },
            new Faculty { FacultyName = "Факультет прикладної математики", FacultyShortName = "ФПМ" },
            new Faculty { FacultyName = "Факультет соціології і права", FacultyShortName = "ФСП" },
            new Faculty { FacultyName = "Хіміко-технологічний факультет", FacultyShortName = "ХТФ" },
            new Faculty { FacultyName = "Радіотехнічний факультет", FacultyShortName = "РФ" },
            new Faculty { FacultyName = "Фізико-математичний факультет", FacultyShortName = "ФМФ" },
            new Faculty { FacultyName = "Факультет біомедичної інженерії", FacultyShortName = "ФБІ" },
            new Faculty { FacultyName = "Факультет біотехнології і біотехніки", FacultyShortName = "ФББ" },
            new Faculty { FacultyName = "Факультет електроенерготехніки та автоматики", FacultyShortName = "ФЕА" }
        ];

        if (!await _dataDbContext.Faculties.AnyAsync())
        {
            await _dataDbContext.Faculties.AddRangeAsync(faculties);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED SPECIALITIES ====================== */
        Speciality[] specialties =
        [
            new Speciality { SpecialityName = "Інформаційні системи та технології", SpecialityNumber = 126 },
            new Speciality { SpecialityName = "Кібербезпека", SpecialityNumber = 125 },
            new Speciality { SpecialityName = "Системний аналіз", SpecialityNumber = 124 },
            new Speciality { SpecialityName = "Комп'ютерна інженерія", SpecialityNumber = 123 },
            new Speciality { SpecialityName = "Комп'ютерні науки", SpecialityNumber = 122 },
            new Speciality { SpecialityName = "Екологія", SpecialityNumber = 101 },
            new Speciality { SpecialityName = "Фізика та астрономія", SpecialityNumber = 104 },
            new Speciality { SpecialityName = "Прикладна фізика та наноматеріали", SpecialityNumber = 105 },
            new Speciality { SpecialityName = "Математика", SpecialityNumber = 111 },
            new Speciality { SpecialityName = "Прикладна математика", SpecialityNumber = 113 },
            new Speciality { SpecialityName = "Журналістика", SpecialityNumber = 061 },
            new Speciality { SpecialityName = "Соціологія", SpecialityNumber = 054 },
            new Speciality { SpecialityName = "Економіка", SpecialityNumber = 051 },
            new Speciality { SpecialityName = "Філологія", SpecialityNumber = 035 },
            new Speciality
            {
                SpecialityName = "Образотворче мистецтво, декоративне мистецтво, реставрація", SpecialityNumber = 023
            },
            new Speciality { SpecialityName = "Менеджмент", SpecialityNumber = 073 },
            new Speciality { SpecialityName = "Маркетинг", SpecialityNumber = 075 },
            new Speciality { SpecialityName = "Право", SpecialityNumber = 081 },
            new Speciality { SpecialityName = "Прикладна механіка", SpecialityNumber = 131 },
            new Speciality { SpecialityName = "Матеріалознавство", SpecialityNumber = 132 },
            new Speciality { SpecialityName = "Галузеве машинобудування", SpecialityNumber = 133 },
            new Speciality { SpecialityName = " Авіаційна та ракетно-космічна технік", SpecialityNumber = 134 }
        ];

        if (!await _dataDbContext.Specialties.AnyAsync())
        {
            await _dataDbContext.Specialties.AddRangeAsync(specialties);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED LEARNING STATUSES ====================== */
        LearningStatus[] learningStatuses =
        [
            new LearningStatus { LearningStatusName = "Навчається" },
            new LearningStatus { LearningStatusName = "Відраховано" },
            new LearningStatus { LearningStatusName = "Проходить календарний контроль" },
            new LearningStatus { LearningStatusName = "Проходить сесію" }
        ];

        if (!await _dataDbContext.LearningStatuses.AnyAsync())
        {
            await _dataDbContext.LearningStatuses.AddRangeAsync(learningStatuses);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED FORMATS LEARNING ====================== */
        FormatLearning[] formatLearnings =
        [
            new FormatLearning { FormatLearningName = "Очна (денна)" },
            new FormatLearning { FormatLearningName = "Очна (вечірня)" },
            new FormatLearning { FormatLearningName = "Заочна" }
        ];

        if (!await _dataDbContext.FormatLearnings.AnyAsync())
        {
            await _dataDbContext.FormatLearnings.AddRangeAsync(formatLearnings);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED GROUPS ====================== */
        Group[] groups =
        [
            new Group { GroupName = "ІК-12", GroupCourse = 1 },
            new Group { GroupName = "ІК-13", GroupCourse = 1 },
            new Group { GroupName = "ІК-14", GroupCourse = 2 },
            new Group { GroupName = "ІК-15", GroupCourse = 3 }
        ];

        if (!await _dataDbContext.Groups.AnyAsync())
        {
            await _dataDbContext.Groups.AddRangeAsync(groups);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED ROLES ====================== */
        ApplicationRole[] defaultRoles =
        [
            new ApplicationRole("admin", "Адмін."),
            new ApplicationRole("teacher", "Вчитель."),
            new ApplicationRole("student", "Студент."),
            new ApplicationRole("guest", "Гість. Роль отримується одразу при реєстрації. Після реєстрації, admin " +
                                         "користувач може змінити роль гостя на student або teacher у своєму списку.")
        ];

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
        [
            new ApplicationUser
            {
                UserName = "admin", Email = "admin@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960498533"
            },
            new ApplicationUser
            {
                UserName = "guest1", Email = "guest1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960897523"
            },
            new ApplicationUser
            {
                UserName = "guest2", Email = "guest2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0864729518"
            },
            new ApplicationUser
            {
                UserName = "guest3", Email = "guest3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0125876912"
            },
            new ApplicationUser
            {
                UserName = "guest4", Email = "guest4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0598461579"
            },
            new ApplicationUser
            {
                UserName = "guest5", Email = "guest5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574129638"
            },
            new ApplicationUser
            {
                UserName = "guest6", Email = "guest6@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0258741369"
            },
            new ApplicationUser
            {
                UserName = "guest7", Email = "guest7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0973149317"
            },
            new ApplicationUser
            {
                UserName = "guest8", Email = "guest8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0963142567"
            },
            new ApplicationUser
            {
                UserName = "guest9", Email = "guest9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0142587369"
            },
            new ApplicationUser
            {
                UserName = "guest10", Email = "guest10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0546821793"
            },
            new ApplicationUser
            {
                UserName = "student1", Email = "student1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0529416720"
            },
            new ApplicationUser
            {
                UserName = "student2", Email = "student2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0540230596"
            },
            new ApplicationUser
            {
                UserName = "student3", Email = "student3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0574100029"
            },
            new ApplicationUser
            {
                UserName = "student4", Email = "student4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0114558963"
            },
            new ApplicationUser
            {
                UserName = "student5", Email = "student5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0962587777"
            },
            new ApplicationUser
            {
                UserName = "teacher1", Email = "teacher1@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0541290054"
            },
            new ApplicationUser
            {
                UserName = "teacher2", Email = "teacher2@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0781230059"
            },
            new ApplicationUser
            {
                UserName = "teacher3", Email = "teacher3@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0960420099"
            },
            new ApplicationUser
            {
                UserName = "teacher4", Email = "teacher4@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0127453688"
            },
            new ApplicationUser
            {
                UserName = "teacher5", Email = "teacher5@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0124758222"
            },
            new ApplicationUser
            {
                UserName = "guest11", Email = "guest11@gmail.com", EmailConfirmed = true, // 21
                PhoneNumber = "0267710000"
            },
            new ApplicationUser
            {
                UserName = "guest12", Email = "guest12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0731500000"
            },
            new ApplicationUser
            {
                UserName = "guest13", Email = "guest13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0500000001"
            },
            new ApplicationUser
            {
                UserName = "guest14", Email = "guest14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0462011256"
            },
            new ApplicationUser
            {
                UserName = "guest15", Email = "guest15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0624700000"
            },
            new ApplicationUser
            {
                UserName = "guest16", Email = "guest16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070405"
            },
            new ApplicationUser
            {
                UserName = "guest17", Email = "guest17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0102030405"
            },
            new ApplicationUser
            {
                UserName = "guest18", Email = "guest18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "6985478966"
            },
            new ApplicationUser
            {
                UserName = "guest19", Email = "guest19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7896541230"
            },
            new ApplicationUser
            {
                UserName = "guest20", Email = "guest20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "9632587410"
            },
            new ApplicationUser
            {
                UserName = "guest21", Email = "guest21@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070605"
            },
            new ApplicationUser
            {
                UserName = "guest22", Email = "guest22@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0102030904"
            },
            new ApplicationUser
            {
                UserName = "guest23", Email = "guest23@gmail.com", EmailConfirmed = true,
                PhoneNumber = "5040102026"
            },
            new ApplicationUser
            {
                UserName = "guest24", Email = "guest24@gmail.com", EmailConfirmed = true,
                PhoneNumber = "9080704050"
            },
            new ApplicationUser
            {
                UserName = "guest25", Email = "guest25@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7080604020"
            },
            new ApplicationUser
            {
                UserName = "guest26", Email = "guest26@gmail.com", EmailConfirmed = true, // 36
                PhoneNumber = "9060200104"
            },
            new ApplicationUser
            {
                UserName = "teacher6", Email = "teacher6@gmail.com", EmailConfirmed = true, // 37
                PhoneNumber = "0945038363"
            },
            new ApplicationUser
            {
                UserName = "teacher7", Email = "teacher7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0900800700"
            },
            new ApplicationUser
            {
                UserName = "teacher8", Email = "teacher8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0100000800"
            },
            new ApplicationUser
            {
                UserName = "teacher9", Email = "teacher9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0110022547"
            },
            new ApplicationUser
            {
                UserName = "teacher10", Email = "teacher10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0978470008"
            },
            new ApplicationUser
            {
                UserName = "teacher11", Email = "teacher11@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0700000800"
            },
            new ApplicationUser
            {
                UserName = "teacher12", Email = "teacher12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "010000010"
            },
            new ApplicationUser
            {
                UserName = "teacher13", Email = "teacher13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0200000020"
            },
            new ApplicationUser
            {
                UserName = "teacher14", Email = "teacher14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0200000020"
            },
            new ApplicationUser
            {
                UserName = "teacher15", Email = "teacher15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0300000300"
            },
            new ApplicationUser
            {
                UserName = "teacher16", Email = "teacher16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0400000040"
            },
            new ApplicationUser
            {
                UserName = "teacher17", Email = "teacher17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0500000050"
            },
            new ApplicationUser
            {
                UserName = "teacher18", Email = "teacher18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0600000060"
            },
            new ApplicationUser
            {
                UserName = "teacher19", Email = "teacher19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0700000070"
            },
            new ApplicationUser
            {
                UserName = "teacher20", Email = "teacher20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0800000080"
            },
            new ApplicationUser
            {
                UserName = "teacher21", Email = "teacher21@gmail.com", EmailConfirmed = true, // 52
                PhoneNumber = "0900000090"
            },
            new ApplicationUser
            {
                UserName = "student6", Email = "student6@gmail.com", EmailConfirmed = true, // 53
                PhoneNumber = "0403020100"
            },
            new ApplicationUser
            {
                UserName = "student7", Email = "student7@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0504030201"
            },
            new ApplicationUser
            {
                UserName = "student8", Email = "student8@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0605040630"
            },
            new ApplicationUser
            {
                UserName = "student9", Email = "student9@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0706050403"
            },
            new ApplicationUser
            {
                UserName = "student10", Email = "student10@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0807060504"
            },
            new ApplicationUser
            {
                UserName = "student11", Email = "student11@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0908070605"
            },
            new ApplicationUser
            {
                UserName = "student12", Email = "student12@gmail.com", EmailConfirmed = true,
                PhoneNumber = "0090807060"
            },
            new ApplicationUser
            {
                UserName = "student13", Email = "student13@gmail.com", EmailConfirmed = true,
                PhoneNumber = "1090807060"
            },
            new ApplicationUser
            {
                UserName = "student14", Email = "student14@gmail.com", EmailConfirmed = true,
                PhoneNumber = "2010908070"
            },
            new ApplicationUser
            {
                UserName = "student15", Email = "student15@gmail.com", EmailConfirmed = true,
                PhoneNumber = "3020109080"
            },
            new ApplicationUser
            {
                UserName = "student16", Email = "student16@gmail.com", EmailConfirmed = true,
                PhoneNumber = "4030201090"
            },
            new ApplicationUser
            {
                UserName = "student17", Email = "student17@gmail.com", EmailConfirmed = true,
                PhoneNumber = "5040302010"
            },
            new ApplicationUser
            {
                UserName = "student18", Email = "student18@gmail.com", EmailConfirmed = true,
                PhoneNumber = "6050403020"
            },
            new ApplicationUser
            {
                UserName = "student19", Email = "student19@gmail.com", EmailConfirmed = true,
                PhoneNumber = "7060504030"
            },
            new ApplicationUser
            {
                UserName = "student20", Email = "student20@gmail.com", EmailConfirmed = true,
                PhoneNumber = "8070605040"
            },
            new ApplicationUser
            {
                UserName = "student21", Email = "student21@gmail.com", EmailConfirmed = true, // 68
                PhoneNumber = "9080706059"
            }
        ];

        AdminUser adminUser = new AdminUser
        {
            ApplicationUser = defaultUsers.FirstOrDefault(adminUserData => adminUserData.UserName == "admin"),
            FirstName = "Станіслав", LastName = "Володимирович", Patronymic = "Петренко"
        };

        GuestUser[] guestUsers =
        [
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest1"),
                FirstName = "Анатолій", LastName = "Боднаренко", Patronymic = "Олександрович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest2"),
                FirstName = "Юлія", LastName = "Тарасівна", Patronymic = "Броварчук"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest3"),
                FirstName = "Інна", LastName = "Василенко", Patronymic = "Андріївна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest4"),
                FirstName = "Наталія", LastName = "Боднаренко", Patronymic = "Олександрівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest5"),
                FirstName = "Остромисл", LastName = "Мойбенко", Patronymic = "Вадимович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest6"),
                FirstName = "Чеслав", LastName = "Ващенко", Patronymic = "Панасович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest7"),
                FirstName = "Євгеній", LastName = "Павленко", Patronymic = "Орестович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest8"),
                FirstName = "Любодар", LastName = "П'ятаченко", Patronymic = "Яромирович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest9"),
                FirstName = "Нагнибіда", LastName = "Бурій", Patronymic = "Устимович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest10"),
                FirstName = "Щастислав", LastName = "Коцюба", Patronymic = "Русланович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest11"),
                FirstName = "Устина", LastName = "Яськевич", Patronymic = "Найденівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest12"),
                FirstName = "Йосипа", LastName = "Стефанівська", Patronymic = "Зорянівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest13"),
                FirstName = "Мар'я", LastName = "Коцюба", Patronymic = "Русланович"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest14"),
                FirstName = "Харитина", LastName = "Дзюба", Patronymic = "Левівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest15"),
                FirstName = "Степанова", LastName = "Дорофея", Patronymic = "Юхимівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest16"),
                FirstName = "Найдена", LastName = "Коровченко", Patronymic = "Артемівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest17"),
                FirstName = "Йосипа", LastName = "Удовенко", Patronymic = "Ростиславівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest18"),
                FirstName = "Люборада", LastName = "Ярич", Patronymic = "Глібівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest19"),
                FirstName = "Євгенія", LastName = "Сільченко", Patronymic = "Панасівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest20"),
                FirstName = "Єва", LastName = "Ярич", Patronymic = "Янівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest21"),
                FirstName = "Чеслава", LastName = "Чумак", Patronymic = "Устимівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest22"),
                FirstName = "Чеслава", LastName = "Савків", Patronymic = "Чеславівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest23"),
                FirstName = "Жозефіна", LastName = "Савойка", Patronymic = "Валентинівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest24"),
                FirstName = "Пульхера", LastName = "Іваничук", Patronymic = "Левівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest25"),
                FirstName = "Берта", LastName = "Зінкевич", Patronymic = "Давидівна"
            },
            new GuestUser
            {
                ApplicationUser = defaultUsers.FirstOrDefault(guestUserData => guestUserData.UserName == "guest26"),
                FirstName = "Євдокія", LastName = "Комар", Patronymic = "Янівна"
            },
        ];

        StudentUser[] studentUsers =
        [
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student1"),
                FirstName = "Фрол", LastName = "Гайдамаха", Patronymic = "Панасович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Speciality = specialties[0], Group = groups[0]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student2"),
                FirstName = "Судивой", LastName = "Демчина", Patronymic = "Леонідович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Speciality = specialties[0], Group = groups[0]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student3"),
                FirstName = "Августин", LastName = "Кабак", Patronymic = "Никодимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[1],
                Faculty = faculties[1], Speciality = specialties[2], Group = groups[0]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student4"),
                FirstName = "Жито", LastName = "Зарицький", Patronymic = "Сарматович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Speciality = specialties[0], Group = groups[0]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student5"),
                FirstName = "Едуард", LastName = "Вітовський", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Speciality = specialties[5], Group = groups[0]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student6"),
                FirstName = "Клара", LastName = "Богданюк", Patronymic = "Адріанівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[4], Speciality = specialties[3], Group = groups[3]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student7"),
                FirstName = "Єлизавета", LastName = "Караванська", Patronymic = "Полянівна",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[4], Speciality = specialties[3], Group = groups[3]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student8"),
                FirstName = "Борислава", LastName = "Коман", Patronymic = "Тимурівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[1],
                Faculty = faculties[3], Speciality = specialties[3], Group = groups[3]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student9"),
                FirstName = "Шушана", LastName = "Капуста", Patronymic = "Вікторівна",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Speciality = specialties[3], Group = groups[3]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student10"),
                FirstName = "Йовілла", LastName = "Йовілла", Patronymic = "Драганівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Speciality = specialties[2], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student11"),
                FirstName = "Карина", LastName = "Фурсенко", Patronymic = "Ярославівна",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[3], Speciality = specialties[2], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student12"),
                FirstName = "Недан", LastName = "Таранюк", Patronymic = "Олександрович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Speciality = specialties[2], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student13"),
                FirstName = "Живорід", LastName = "Багінський", Patronymic = "Артурович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Speciality = specialties[2], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student14"),
                FirstName = "Флор", LastName = "Белмачовський", Patronymic = "Радимович",
                FormatLearning = formatLearnings[2], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Speciality = specialties[1], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student15"),
                FirstName = "Едуард", LastName = "Вітовський", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[2], Speciality = specialties[1], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student16"),
                FirstName = "Голубко", LastName = "Мосійчук", Patronymic = "Вадимович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Speciality = specialties[1], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student17"),
                FirstName = "Рудан", LastName = "Шматько", Patronymic = "Денисович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Speciality = specialties[1], Group = groups[2]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student18"),
                FirstName = "Уличан", LastName = "Машковський", Patronymic = "Захарович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Speciality = specialties[0], Group = groups[1]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student19"),
                FirstName = "Буйтур", LastName = "Боберський", Patronymic = "Іванович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[1], Speciality = specialties[0], Group = groups[1]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student20"),
                FirstName = "Щастибог", LastName = "Ржепішевський", Patronymic = "Леонідович",
                FormatLearning = formatLearnings[1], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Speciality = specialties[0], Group = groups[1]
            },
            new StudentUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(studentUserData => studentUserData.UserName == "student21"),
                FirstName = "Іларіон", LastName = "Авдуєвський", Patronymic = "Юліанович",
                FormatLearning = formatLearnings[0], LearningStatus = learningStatuses[0],
                Faculty = faculties[0], Speciality = specialties[0], Group = groups[1]
            }
        ];

        TeacherUser[] teacherUsers =
        [
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher1"),
                FirstName = "Лук'ян", LastName = "Юрковський", Patronymic = "Ігорович",
                Qualification = qualifications[0]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher2"),
                FirstName = "Щастибог", LastName = "Кулиняк", Patronymic = "Романович",
                Qualification = qualifications[0]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher3"),
                FirstName = "Андрющенко", LastName = "Фрол", Patronymic = "Іванович",
                Qualification = qualifications[2]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher4"),
                FirstName = "Лиско", LastName = "Любовир", Patronymic = "Ігорович",
                Qualification = qualifications[3]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher5"),
                FirstName = "Юліан", LastName = "Карпека", Patronymic = "Найденович",
                Qualification = qualifications[1]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher6"),
                FirstName = "Юлій", LastName = "Миколаєнко", Patronymic = "Яромирович",
                Qualification = qualifications[0]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher7"),
                FirstName = "Злат", LastName = "Врецьона", Patronymic = "Денисович",
                Qualification = qualifications[6]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher8"),
                FirstName = "Флор", LastName = "Громико", Patronymic = "Вітанович",
                Qualification = qualifications[5]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher9"),
                FirstName = "Житомир", LastName = "Єщенко", Patronymic = "Тихонович",
                Qualification = qualifications[4]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher10"),
                FirstName = "Віола", LastName = "Семеренко", Patronymic = "Полянівна",
                Qualification = qualifications[4]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher11"),
                FirstName = "Уляна", LastName = "Загороднюк", Patronymic = "Адамівна",
                Qualification = qualifications[3]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher12"),
                FirstName = "Осипа", LastName = "Борейко", Patronymic = "Федорівна",
                Qualification = qualifications[3]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher13"),
                FirstName = "Уляна", LastName = "Загороднюк", Patronymic = "Адамівна",
                Qualification = qualifications[2]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher14"),
                FirstName = "Осипа", LastName = "Борейко", Patronymic = "Федорівна",
                Qualification = qualifications[2]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher15"),
                FirstName = "Алла", LastName = "Колесник", Patronymic = "Златівна",
                Qualification = qualifications[0]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher16"),
                FirstName = "Шарлота", LastName = "Гісем", Patronymic = "Левівна",
                Qualification = qualifications[6]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher17"),
                FirstName = "Юхимина", LastName = "Засенко", Patronymic = "Соломонівна",
                Qualification = qualifications[1]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher18"),
                FirstName = "Цецілія", LastName = "Степула", Patronymic = "Адамівна",
                Qualification = qualifications[5]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher19"),
                FirstName = "Бажана", LastName = "Пероганич", Patronymic = "Тарасівна",
                Qualification = qualifications[4]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher20"),
                FirstName = "Любаня", LastName = "Юрчишин", Patronymic = "Леонідівна",
                Qualification = qualifications[3]
            },
            new TeacherUser
            {
                ApplicationUser =
                    defaultUsers.FirstOrDefault(teacherUserData => teacherUserData.UserName == "teacher21"),
                FirstName = "Хриса", LastName = "Бутенко", Patronymic = "Романівна",
                Qualification = qualifications[2]
            }
        ];

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
        [
            new GroupTeacher { TeacherUser = teacherUsers[0], Group = groups[0] },
            new GroupTeacher { TeacherUser = teacherUsers[0], Group = groups[2] },
            new GroupTeacher { TeacherUser = teacherUsers[0], Group = groups[3] },
            new GroupTeacher { TeacherUser = teacherUsers[1], Group = groups[1] },
            new GroupTeacher { TeacherUser = teacherUsers[1], Group = groups[2] },
            new GroupTeacher { TeacherUser = teacherUsers[1], Group = groups[3] },
            new GroupTeacher { TeacherUser = teacherUsers[2], Group = groups[0] },
            new GroupTeacher { TeacherUser = teacherUsers[2], Group = groups[2] },
            new GroupTeacher { TeacherUser = teacherUsers[2], Group = groups[1] },
            new GroupTeacher { TeacherUser = teacherUsers[2], Group = groups[3] },
            new GroupTeacher { TeacherUser = teacherUsers[3], Group = groups[1] },
            new GroupTeacher { TeacherUser = teacherUsers[3], Group = groups[2] },
            new GroupTeacher { TeacherUser = teacherUsers[3], Group = groups[3] },
            new GroupTeacher { TeacherUser = teacherUsers[4], Group = groups[2] },
            new GroupTeacher { TeacherUser = teacherUsers[4], Group = groups[1] },
            new GroupTeacher { TeacherUser = teacherUsers[4], Group = groups[0] }
        ];

        if (!await _dataDbContext.GroupTeachers.AnyAsync())
        {
            await _dataDbContext.GroupTeachers.AddRangeAsync(groupTeachers);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED DISCIPLINES RELATIONSHIP ====================== */
        Discipline[] disciplines =
        [
            new Discipline
            {
                DisciplineName = "Технології розробки програмного забезпечення (ТРПЗ)", Teacher = teacherUsers[0],
                DisciplineDescription =
                    "Предмет \"Технології Розробки програмного забезпечення\" (ТРПЗ) в університеті охоплює широкий спектр тем, спрямованих на вивчення сучасних інструментів та технологій розробки програмного забезпечення. Студенти отримують поглиблені знання у використанні T-SQL для роботи з базами даних, розробці веб-додатків з використанням Web Forms, а також в основах .NET Core MVC та .NET Core API. Додатковий акцент робиться на вивченні шарів бізнес-логіки (BLL) та доступу до даних (DAL), що дозволяє студентам зрозуміти принципи створення ефективних та масштабованих програмних рішень. Курс покликаний підготувати студентів до розробки високоякісного програмного забезпечення з використанням сучасних технологій та найкращих практик в галузі розробки програмного забезпечення."
            },
            new Discipline
            {
                DisciplineName = "Основи Front-end технологій", Teacher = teacherUsers[0],
                DisciplineDescription =
                    "\"Основи Front-end технологій\" - це курс, який надає студентам комплексне розуміння основних інструментів розробки веб-інтерфейсів. Студенти знайомляться з мовами розмітки та стилізації HTML та CSS, вивчають базові принципи програмування на стороні клієнта за допомогою JavaScript. Курс також охоплює бібліотеку jQuery для полегшення роботи з DOM та подіями. Особлива увага приділяється навичкам верстки, включаючи адаптивний та резиновий дизайн для забезпечення ефективності та доступності веб-сайтів на різних пристроях та платформах. Студенти отримують практичні навички у розробці динамічних та привабливих веб-інтерфейсів, що відповідають сучасним вимогам та тенденціям в галузі Front-end розробки."
            },
            new Discipline
            {
                DisciplineName = "Технологія блокчейн", Teacher = teacherUsers[0],
                DisciplineDescription =
                    "\"Технологія блокчейн\" - це курс, спрямований на глибоке вивчення фундаментальних концепцій та практичного застосування технології блокчейн. Студенти ознайомляться з основами блокчейн-систем, включаючи структуру блоків, криптографічні методи забезпечення безпеки, алгоритми консенсусу та розподіленими реєстрами даних.Курс також охоплює розгляд різних типів блокчейн-мереж, таких як публічні та приватні, і їх використання в різних галузях, включаючи фінанси, логістику, охорону здоров'я та інші. Студенти матимуть можливість ознайомитися з платформами блокчейн, такими як Ethereum, Hyperledger, а також з вивченням мов програмування для розробки смарт-контрактів."
            },
            new Discipline
            {
                DisciplineName = "Сучасні технології розробки на .NET", Teacher = teacherUsers[0],
                DisciplineDescription =
                    "\"Сучасні технології розробки на .NET\" - це курс, спрямований на систематичне вивчення передових технологій та інструментів розробки, які базуються на платформі .NET. Студенти отримають глибокі знання в області розробки програмного забезпечення, використовуючи сучасні технології та практики. Курс включає в себе детальне вивчення .NET Core та ASP.NET Core, які визначають нові стандарти для розробки хмарних, масштабованих та кросплатформених додатків. Студенти ознайомляться з архітектурою та основними концепціями, такими як Middleware, Dependency Injection, та робота з Entity Framework Core для доступу до баз даних. Додатково, курс включає в себе вивчення технологій веб-розробки, таких як ASP.NET MVC та веб-служби за допомогою .NET Core API. Студенти також отримають навички роботи з мікросервісною архітектурою та контейнеризацією за допомогою Docker."
            },
            new Discipline
            {
                DisciplineName = "Філософія", Teacher = teacherUsers[1],
                DisciplineDescription =
                    "Предмет \"Філософія\" спрямований на вивчення фундаментальних питань ідентичності, знань, моралі та існування. Студенти вивчають історію філософської думки, аналізують основні концепції від давнини до сучасності, такі як метафізика, етика, епістемологія. Курс дозволяє розвивати критичне мислення, абстрактне мислення та здатність аналізувати складні проблеми, що веде до розуміння глибинних аспектів людського існування та культурного контексту. Студенти також мають можливість обговорювати етичні питання, філософські підходи до релігії, політики та суспільства, що розширює їхні горизонти та допомагає виробити власну філософію життя."
            },
            new Discipline
            {
                DisciplineName = "Соціальна психологія", Teacher = teacherUsers[1],
                DisciplineDescription =
                    "Предмет \"Соціальна психологія\" спрямований на вивчення взаємодії між індивідами в соціальних групах та спільнотах. Студенти досліджують вплив соціальних факторів на поведінку, переконання та сприйняття особистості. Курс охоплює теми, такі як соціальна взаємодія, міжособистісні відносини, соціальна ідентичність, стереотипи та упередження. Студенти аналізують роль групової динаміки, конформізму та влади в формуванні соціальних норм і структур. Курс також дозволяє розуміти вплив соціально-психологічних теорій на розвиток сучасних соціальних явищ та проблем. Здобуті знання допомагають студентам аналізувати та розуміти складні соціальні ситуації, сприяючи розвитку навичок комунікації, лідерства та сприйняття різноманітності в сучасному суспільстві."
            },
            new Discipline
            {
                DisciplineName = "Історія сучасної України", Teacher = teacherUsers[1],
                DisciplineDescription =
                    "Предмет \"Історія сучасної України\" присвячений вивченню ключових подій, трансформацій та важливих аспектів розвитку країни в XX-XXI століттях. Студенти досліджують історію України в контексті світових подій, аналізуючи період становлення та незалежності. Курс охоплює теми, такі як українська революція 1917-1921 років, голодомор, Друга світова війна, період радянської домінації, а також новітні етапи історії - незалежність та сучасні трансформації. Студенти аналізують соціальні, політичні та економічні зміни, що відбулися під час формування та консолідації української нації. Курс сприяє розвитку критичного мислення, аналітичних навичок та розуміння взаємозв'язків між історичним минулим та сучасністю. Студенти також розглядають роль України в контексті міжнародних відносин та геополітичних зрушень, що допомагає їм зрозуміти місце країни в сучасному світі."
            },
            new Discipline
            {
                DisciplineName = "Безпека інформаційний систем (БІС)", Teacher = teacherUsers[1],
                DisciplineDescription =
                    "Предмет \"Безпека інформаційних систем (БІС)\" спрямований на вивчення та розуміння основних аспектів забезпечення безпеки інформаційних технологій. Студенти освоюють шифрування, методи захисту від несанкціонованого доступу, технології виявлення та реагування на інциденти, а також концепції кібербезпеки. Курс включає аналіз та вивчення загроз кібербезпеці, розробку стратегій захисту інформації, та впровадження сучасних стандартів та практик в галузі інформаційної безпеки. Студенти отримують практичні навички у виявленні та вирішенні проблем безпеки інформаційних систем, що підготовлює їх до викликів у сфері кібербезпеки та забезпечує компетентність у збереженні конфіденційності, цілісності та доступності інформації."
            },
            new Discipline
            {
                DisciplineName = "Життєвий цикл розробки програмного забезпечення", Teacher = teacherUsers[2],
                DisciplineDescription =
                    "Життєвий цикл розробки програмного забезпечення (ЖЦПЗ) представляє собою систематичний процес від концепції до виведення програмного продукту в експлуатацію та його подальшого обслуговування. Цей цикл включає в себе такі етапи, як аналіз вимог, проектування, розробка, тестування, впровадження та підтримка. На етапі аналізу вимог визначаються потреби користувачів, наступні етапи включають розробку архітектури, написання коду, валідацію та верифікацію. Тестування включає фазу виявлення та усунення помилок. Впровадження включає в себе введення продукту в експлуатацію, а підтримка охоплює рутинне обслуговування та виправлення помилок. Цей цикл може бути лінійним, ітеративним або відповідати конкретним моделям розробки, таким як Agile чи Waterfall, залежно від потреб проекту та вибору команди розробників."
            },
            new Discipline
            {
                DisciplineName = "Методи та технології штучного інтелекту", Teacher = teacherUsers[2],
                DisciplineDescription =
                    "Методи та технології штучного інтелекту (ШІ) представляють собою розмаїття підходів та інструментів, спрямованих на створення систем, здатних виконувати завдання, що традиційно вимагають інтелекту людини. Серед основних методів можна виділити машинне навчання, яке включає навчання з учителем, навчання без учителя та підготовку з підкріпленням. Технології обробки природних мов, виявлення образів, обробка сигналів, планування, аналіз даних та інші також важливі для ШІ. Машинне навчання використовує алгоритми для аналізу та інтерпретації даних, щоб вдатися до певних висновків чи автоматизувати прийняття рішень. Технології обробки природних мов дозволяють системам розуміти та генерувати людську мову. Техніки обробки зображень та відео дозволяють комп'ютерам розпізнавати обличчя, об'єкти та інші елементи в графіці. Технології планування використовуються для розв'язання задач та оптимізації ресурсів."
            },
            new Discipline
            {
                DisciplineName = "Комп'ютерна графіка та мультимедія", Teacher = teacherUsers[2],
                DisciplineDescription =
                    "Комп'ютерна графіка та мультимедіа — це галузь, що охоплює методи, техніки та інструменти для створення, обробки та відтворення графічних та мультимедійних візуальних елементів за допомогою комп'ютерних технологій. Студенти вивчають основи растрової та векторної графіки, тридивимірного моделювання, анімації, а також технології обробки та синтезу аудіо та відео. Курс включає в себе розгляд алгоритмів обробки та візуалізації графіки, вивчення пакетів програмного забезпечення для графічного дизайну та роботи з мультимедійними ресурсами. Студенти також отримують навички роботи з графічними редакторами, інструментами моделювання та анімації, а також з програмування комп'ютерних ігор."
            },
            new Discipline
            {
                DisciplineName = "Основи Back-end технологій", Teacher = teacherUsers[2],
                DisciplineDescription =
                    "Предмет \"Основи Back-end технологій\" університетського курсу фокусується на розумінні та вивченні ключових концепцій та інструментів, що використовуються для розробки серверної частини веб-додатків. Студенти здобувають глибокі знання з мов програмування для back-end розробки, таких як Java, Python, Ruby, або Node.js, та розуміння архітектурних принципів, необхідних для створення ефективних та масштабованих серверних додатків. У рамках курсу студенти вивчають обробку HTTP-запитів, створення та оптимізацію баз даних, використання веб-фреймворків, таких як Django, Flask, Spring або Express.js, для швидкого розгортання серверних додатків. Також розглядаються аспекти безпеки back-end систем та вивчається робота з сесіями, обробка винятків, аутентифікація та авторизація."
            },
            new Discipline
            {
                DisciplineName = "Розробка програмного забезпечення на платформі Java", Teacher = teacherUsers[3],
                DisciplineDescription =
                    "Предмет \"Розробка програмного забезпечення на платформі Java\" в університетському курсі спрямований на вивчення високопродуктивної та універсальної мови програмування Java для створення різноманітних програмних рішень. Студенти отримують відмінне розуміння об'єктно-орієнтованого програмування, принципів розробки на Java та інструментів, необхідних для створення ефективних додатків. У рамках курсу студенти вивчають основні концепції мови Java, включаючи роботу з класами, інтерфейсами, зборкою сміття, обробкою винятків та роботу з колекціями. Курс також охоплює використання різних фреймворків, таких як Spring, для створення бекенд-додатків та управління компонентами програмного забезпечення."
            },
            new Discipline
            {
                DisciplineName = "Технології розробки та просування сайтів", Teacher = teacherUsers[3],
                DisciplineDescription =
                    "Предмет \"Технології розробки та просування сайтів\" в університетському курсі спрямований на надання студентам комплексного розуміння сучасних технологій у веб-розробці та методів просування в Інтернеті. Студенти вивчають ключові аспекти розробки веб-додатків, включаючи HTML, CSS, JavaScript та інші фронтенд технології, а також серверні мови програмування та бази даних для бекенд розробки. У рамках курсу студенти освоюють сучасні веб-фреймворки та інструменти, такі як React, Angular або Vue.js, які дозволяють ефективно створювати динамічні та інтерактивні веб-додатки. Курс також охоплює теми з адаптивного та мобільного дизайну, забезпечуючи студентам знання щодо оптимізації веб-сайтів для різних пристроїв."
            },
            new Discipline
            {
                DisciplineName = "Системне програмування С і С++", Teacher = teacherUsers[3],
                DisciplineDescription =
                    "Предмет \"Системне програмування на мовах програмування C і C++\" університетського курсу надає студентам глибоке розуміння основ системного програмування, використання мов програмування C і C++ для розробки низькорівневих системних додатків та оптимізації продуктивності програм. У рамках курсу студенти вивчають принципи роботи операційних систем, пам'яті та вводу/виводу, а також основи мультипроцесорного та багатозадачного програмування. Студенти оволодівають техніками роботи з мовами C та C++, вивчають важливі концепції, такі як покажчики, робота з пам'яттю, структури даних та алгоритми, які є ключовими для системного програмування. Курс включає в себе вивчення системного інтерфейсу програмування (API) та використання бібліотек для оптимізації розробки програмного забезпечення."
            },
            new Discipline
            {
                DisciplineName = "Розробка програмного забезпечення на платформі Node.JS", Teacher = teacherUsers[3],
                DisciplineDescription =
                    "Предмет \"Розробка програмного забезпечення на платформі Node.js\" університетського курсу вивчає сучасні підходи та техніки розробки високоефективних та масштабованих програм на базі Node.js. Студенти отримують глибокі знання щодо асинхронного програмування, створення серверів, роботи з базами даних, розробки API та інших ключових аспектів веб-розробки на цій платформі. Курс надає студентам можливість поглибити свої навички в JavaScript, вивчити сучасні фреймворки, такі як Express.js, та отримати практичний досвід розробки ефективних програмних рішень, відповідних вимогам сучасного індустріального середовища."
            },
            new Discipline
            {
                DisciplineName = "Права і свободи людини", Teacher = teacherUsers[4],
                DisciplineDescription =
                    "Права і свободи людини є фундаментальними принципами, які визнаються та гарантуються різними міжнародними та національними правовими документами. Ці права є невід'ємними і непорушними для кожної особи, незалежно від її раси, кольору шкіри, релігії, статі, мови, політичних чи інших переконань, національного чи соціального походження, майнового стану, належності до етнічної групи, або інших обставин. Ці права включають в себе, але не обмежуються, такі основні принципи, як право на життя, свободу та особисту недоторканність, свободу думки, совісті та вираження, право на освіту, працю, належні умови проживання, а також права народів на самовизначення."
            },
            new Discipline
            {
                DisciplineName = "Інформаційно-сенсорні системи роботів", Teacher = teacherUsers[4],
                DisciplineDescription =
                    "Інформаційно-сенсорні системи роботівІнформаційно-сенсорні системи роботів є ключовим елементом їхньої функціональності та визначають їх здатність взаємодіяти з навколишнім середовищем. Ці системи об'єднують в собі датчики, модулі збору та обробки даних, а також механізми зворотного зв'язку. Студенти, що вивчають цю тему, досліджують принципи розробки та оптимізації інформаційно-сенсорних систем для робототехніки. Це включає в себе вивчення різних типів сенсорів (від камер та гіроскопів до датчиків тиску та теплових датчиків), методів обробки та інтеграції даних, а також розробку алгоритмів для прийняття роботами рішень на основі інформації з оточуючого середовища. Ці знання є важливими для подальшого розвитку робототехніки та автоматизованих систем, забезпечуючи їхню ефективність та адаптованість до різноманітних завдань та умов."
            },
            new Discipline
            {
                DisciplineName = "Бази даних", Teacher = teacherUsers[5],
                DisciplineDescription =
                    "Курс включає в себе вивчення реляційних та нереляційних моделей даних, мов запитів SQL (Structured Query Language), нормалізації даних та концепцій транзакцій. Студенти також ознайомлюються з основними системами управління базами даних (СУБД), такими як MySQL, Oracle, Microsoft SQL Server та іншими. У практичних завданнях студенти отримують досвід створення баз даних, проектування схем, виконання запитів та оптимізації даних. Курс також може включати вивчення тем з розподіленого зберігання даних, безпеки баз даних, роботи з великими обсягами даних та іншими актуальними темами у галузі баз даних."
            },
            new Discipline
            {
                DisciplineName = "CAD-системи та мультимедіа", Teacher = teacherUsers[5],
                DisciplineDescription =
                    "CAD-системи (Computer-Aided Design) використовуються для автоматизованого проектування та моделювання об'єктів в інженерії, архітектурі та інших галузях. Системи CAD дозволяють інженерам та дизайнерам створювати, редагувати та аналізувати 2D та 3D моделі. Вони включають інструменти для вимірювання, валідації та оптимізації дизайнів, що полегшує процес створення складних та точних проектів. З іншого боку, мультимедіа охоплює різноманітні технології для створення, обробки та відтворення зображень, аудіо та відео матеріалів. Це може включати в себе графічний дизайн, анімацію, аудіо та відео обробку."
            },
            new Discipline
            {
                DisciplineName = "Комп'ютерні мережі", Teacher = teacherUsers[6],
                DisciplineDescription =
                    "Комп'ютерні мережі - це система взаємопов'язаних комп'ютерів та інших пристроїв, які дозволяють обмінюватися даними та ресурсами. Студенти, які вивчають цей предмет, отримують розуміння основних понять та принципів, що стосуються проектування, розгортання та управління комп'ютерними мережами. Курс включає в себе вивчення архітектур мереж, таких як локальні (LAN) та глобальні (WAN), протоколів передачі даних, зокрема TCP/IP, і понять безпеки мереж. Студенти навчаються налаштовувати мережеве обладнання, включаючи комутатори, маршрутизатори та файрволи, а також розробляти та впроваджувати стратегії забезпечення доступності та безпеки мережі."
            },
            new Discipline
            {
                DisciplineName = "Економічна психологія", Teacher = teacherUsers[6],
                DisciplineDescription =
                    "Економічна психологія - це галузь наукових досліджень, що об'єднує психологічні принципи та теорії з економічними процесами для розуміння та пояснення економічної поведінки та прийняття рішень. Студенти вивчають вплив психологічних факторів на економічні рішення, взаємодію між емоціями та фінансовими виборами, а також виявлення психологічних аспектів споживання та інвестування. Курс охоплює теми, такі як теорія привабливості та психологія ціноутворення, вивчення впливу соціальних чинників на економічне поведінку, аналіз рішень у умовах нестабільності та ризику. Студенти також досліджують психологію фінансових ринків, механізми утворення економічних очікувань та вплив психологічних стереотипів на фінансові рішення."
            },
            new Discipline
            {
                DisciplineName = "Практичний курс іноземної мови. Частина 1", Teacher = teacherUsers[7],
                DisciplineDescription =
                    "Предмет \"Практичний курс іноземної мови. Частина 1\" університетського курсу спрямований на вивчення основ іноземної мови та розвиток практичних навичок в її використанні. Студенти ознайомляться з базовою лексикою та граматикою, здобудуть вміння вести прості розмови, читати та писати на іноземній мові. Курс сприяє формуванню комунікативних навичок та створенню основ для подальшого вивчення мови."
            },
            new Discipline
            {
                DisciplineName = "Практичний курс іноземної мови. Частина 2", Teacher = teacherUsers[8],
                DisciplineDescription =
                    "\"Практичний курс іноземної мови. Частина 2\" розширює іноземномовний досвід студентів, поглиблює їх знання граматики та розширює словниковий запас. Студенти будуть залучені до більш складних конструкцій мовлення, а також вивчатимуть теми, спрямовані на академічний та професійний розвиток. Курс спрямований на розвиток високорівневих комунікативних навичок та впровадження вивченої мови в реальні життєві та академічні ситуації."
            },
            new Discipline
            {
                DisciplineName = "Захист персональних даних: стандарти ЄС та Ради Європи", Teacher = teacherUsers[9],
                DisciplineDescription =
                    "Захист персональних даних у Європейському Союзі (ЄС) та Раді Європи регулюється рядом стандартів та законодавчих актів, спрямованих на забезпечення конфіденційності та безпеки особистої інформації громадян. Один з ключових інструментів в цьому контексті — Загальний регламент з захисту даних (General Data Protection Regulation, GDPR), який набув чинності в 2018 році та стосується всіх країн ЄС. GDPR встановлює обов'язки для організацій щодо обробки та захисту персональних даних громадян ЄС. Він визначає прозорі принципи збору, зберігання та обробки інформації, а також визначає права осіб, чиї дані обробляються, зокрема, право на доступ, виправлення, видалення та перенесення даних. До інших стандартів в сфері захисту персональних даних в ЄС включаються ініціативи Ради Європи, такі як Конвенція про захист осіб у зв'язку з автоматизованою обробкою персональних даних (Конвенція 108) та Європейська конвенція з інформаційної безпеки."
            }
        ];

        if (!await _dataDbContext.Disciplines.AnyAsync())
        {
            await _dataDbContext.Disciplines.AddRangeAsync(disciplines);
            await _dataDbContext.SaveChangesAsync();
        }

        /* ====================== SEED DISCIPLINE-GROUP RELATIONSHIP ====================== */
        DisciplineGroup[] disciplineGroups = // 25 disciplines, 4 groups
        [
            new DisciplineGroup { DisciplineId = 1, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 2, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 3, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 4, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 5, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 5, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 5, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 6, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 7, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 8, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 8, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 8, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 8, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 9, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 9, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 10, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 10, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 10, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 11, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 12, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 12, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 13, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 13, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 14, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 15, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 15, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 16, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 17, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 18, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 19, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 20, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 20, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 20, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 21, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 21, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 22, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 22, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 23, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 23, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 23, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 24, GroupId = 4 },
            new DisciplineGroup { DisciplineId = 24, GroupId = 1 },
            new DisciplineGroup { DisciplineId = 24, GroupId = 2 },
            new DisciplineGroup { DisciplineId = 25, GroupId = 3 },
            new DisciplineGroup { DisciplineId = 25, GroupId = 4 }
        ];

        if (!await _dataDbContext.DisciplineGroups.AnyAsync())
        {
            await _dataDbContext.DisciplineGroups.AddRangeAsync(disciplineGroups);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED DISCIPLINE MATERIAL TYLES RELATIONSHIP ====================== */
        MaterialType[] materialTypes = 
        [
            new MaterialType { MaterialTypeName = "Посилання на джерело" },    
            new MaterialType { MaterialTypeName = "Книга" },
            new MaterialType { MaterialTypeName = "Файл" }
        ];

        if (!await _dataDbContext.MaterialTypes.AnyAsync())
        {
            await _dataDbContext.MaterialTypes.AddRangeAsync(materialTypes);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED DISCIPLINE MATERIAL TYPES RELATIONSHIP ====================== */
        DisciplineMaterialType[] disciplineMaterialTypes = 
        [
            new DisciplineMaterialType { DisciplineMaterialTypeName = "Лекція" },    
            new DisciplineMaterialType { DisciplineMaterialTypeName = "Практика" },    
        ];

        if (!await _dataDbContext.DisciplineMaterialTypes.AnyAsync())
        {
            await _dataDbContext.DisciplineMaterialTypes.AddRangeAsync(disciplineMaterialTypes);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED DISCIPLINE MATERIALS RELATIONSHIP ====================== */
        DisciplineMaterial[] disciplineMaterials =
        [
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Лекція #1",
                DisciplineMaterialDescription = "Введення в HTML та HTML5",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.LectureType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Лекція #2",
                DisciplineMaterialDescription = "Основи CSS та CSS3",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.LectureType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Лекція #3",
                DisciplineMaterialDescription = "Верстка та розмітка веб-сторінок",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.LectureType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Лекція #4",
                DisciplineMaterialDescription = "Основи JavaScript",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.LectureType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Лекція #5",
                DisciplineMaterialDescription = "Робота з DOM (Document Object Model)",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.LectureType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Практичне завдання #1",
                DisciplineMaterialDescription = "Створити просту HTML/CSS верстку single-page, макет додано",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.PracticeType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Практичне завдання #2",
                DisciplineMaterialDescription = "Добавити модальне вікно, яке буде відкриватися при натисканні",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.PracticeType
            },
            new DisciplineMaterial
            {
                DisciplineMaterialName = "Практичне завдання #3",
                DisciplineMaterialDescription = "Зробити slide-bar на головній сторінці",
                DisciplineId = 2,
                DisciplineMaterialTypeId = (int)DisciplineMaterialTypes.PracticeType
            }
        ];

        if (!await _dataDbContext.DisciplineMaterials.AnyAsync())
        {
            await _dataDbContext.DisciplineMaterials.AddRangeAsync(disciplineMaterials);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED MATERIALS RELATIONSHIP ====================== */
        Material[] materials = 
        [
            new Material { MaterialTypeId = 1, MaterialPath = "http://physics.zfftt.kpi.ua/mod/book/view.php?id=272&chapterid=692" },
            new Material { MaterialTypeId = 1, MaterialPath = "http://physics.zfftt.kpi.ua/mod/book/view.php?id=272&chapterid=693" },
            new Material { MaterialTypeId = 1, MaterialPath = "http://physics.zfftt.kpi.ua/mod/book/view.php?id=272&chapterid=743" },
            new Material { MaterialTypeId = 1, MaterialPath = "http://physics.zfftt.kpi.ua/mod/book/view.php?id=272&chapterid=751" }
        ];

        if (!await _dataDbContext.Materials.AnyAsync())
        {
            await _dataDbContext.Materials.AddRangeAsync(materials);
            await _dataDbContext.SaveChangesAsync();
        }
        
        /* ====================== SEED MATERIAL DISCIPLINE-MATERIAL RELATIONSHIP ====================== */
        MaterialDisciplineMaterial[] materialDisciplineMaterials = 
        [
            new MaterialDisciplineMaterial { DisciplineMaterialId = 1, MaterialId = 1 },
            new MaterialDisciplineMaterial { DisciplineMaterialId = 2, MaterialId = 2 },
            new MaterialDisciplineMaterial { DisciplineMaterialId = 3, MaterialId = 3 },
            new MaterialDisciplineMaterial { DisciplineMaterialId = 4, MaterialId = 4 }
        ];

        if (!await _dataDbContext.MaterialDisciplineMaterials.AnyAsync())
        {
            await _dataDbContext.MaterialDisciplineMaterials.AddRangeAsync(materialDisciplineMaterials);
            await _dataDbContext.SaveChangesAsync();
        }
    }
}