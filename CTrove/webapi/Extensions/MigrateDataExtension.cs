using CTrove.Infrastructure;
using CTrove.Core.Entity;
using Microsoft.EntityFrameworkCore;
using CTrove.Core.Common;

namespace CTrove.Api.Extensions
{
    public static class MigrateDataExtension
    {
        public static WebApplication MigrateDataSuperUser(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CtroveDatabaseContext>())
                {
                    try
                    {
                        var userList = appContext.User.ToList();
                        var adminUser = userList.Where(f => f.ObjectId == Guid.Parse("b3fd282f-169a-4722-b805-ed3a1c873e74")).FirstOrDefault();

                        if (adminUser == null)
                        {
                            User user = new User
                            {
                                Id = Guid.NewGuid(),
                                ObjectId = Guid.Parse("b3fd282f-169a-4722-b805-ed3a1c873e74"),
                                Firstname = "Administrator",
                                Lastname = "Administrator",
                                Email = "hrisadmin@gertnewx.onmicrosoft.com",
                                Mobile = "",
                                Prefix = "",
                                Suffix = "",
                                Middlename = "",
                                StartDate = DateTime.Now.ToUniversalTime(),
                                EndDate = DateTime.Now.ToUniversalTime(),
                                Organization = "Ctrove",
                                Status = true,
                                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f")
                            };

                            appContext.User.Add(user);
                            appContext.SaveChanges();
                        }

                        var settings = appContext.Settings.Where(f => f.UserId == adminUser!.Id).FirstOrDefault();
                        if (settings == null)
                        {
                            var newSettings = new CTrove.Core.Entity.Settings
                            {
                                Id = Guid.NewGuid(),
                                UserId = adminUser!.Id,
                                TimeZone = "Asia/Singapore"
                            };

                            appContext.Settings.Add(newSettings);
                            appContext.SaveChanges();
                        }


                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApplication;
        }

        public static WebApplication MigrateTestUser(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CtroveDatabaseContext>())
                {
                    try
                    {
                        foreach (var user in UserList(appContext.Roles.ToList()))
                        {
                            var userExist = appContext.User
                                .FirstOrDefault(f => f.Email.ToUpper().Trim() == user.Email.ToUpper().Trim());

                            if (userExist == null)
                            {
                                appContext.User.Add(user);
                                appContext.SaveChanges();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApplication;
        }

        private static List<User> UserList(List<Roles> roles)
        {
            List<User> users = new List<User>();

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Alyssa",
                Lastname = "",
                Email = "sponsor1@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.SPPM_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Charo",
                Lastname = "",
                Email = "sponsor2@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.SPCRA_BLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Banjo",
                Lastname = "",
                Email = "sponsor3@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.SPCRA_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Carmel",
                Lastname = "",
                Email = "cro1@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.CLPM_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Angeli",
                Lastname = "",
                Email = "cro2@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.CLCRA_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Diann",
                Lastname = "",
                Email = "Site1@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.STPI_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Liz",
                Lastname = "",
                Email = "Site2@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.STSTF_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Jay",
                Lastname = "",
                Email = "Site3@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.STCOR_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Pat",
                Lastname = "",
                Email = "cis21@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.STSPLD_UBLD)!.Id
            });

            users.Add(new User
            {
                Id = Guid.NewGuid(),
                ObjectId = null,
                Firstname = "Jess",
                Lastname = "",
                Email = "cis22@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = roles.FirstOrDefault(f => f.Code == CtroveRoles.STSPAS_UBLD)!.Id
            });

            return users;

        }


        public static WebApplication MigrateRolesPagesSuperUser(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CtroveDatabaseContext>())
                {
                    try
                    {
                        var administratorRolesPages = appContext.RolesPages
                            .Where(f => f.RolesId == Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"))
                            .ToList();

                        if (!administratorRolesPages.Any())
                        {
                            appContext.RolesPages.AddRange(RolesPagesListSuperUser());
                            appContext.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApplication;
        }

        private static List<RolesPages> RolesPagesListSuperUser()
        {
            List<RolesPages> returnList = new List<RolesPages>();

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.Subject,
                Status = true
            });

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.Sites,
                Status = true
            });

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.Config,
                Status = true
            });

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.Access,
                Status = true
            });

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.Roles,
                Status = true
            });

            returnList.Add(new RolesPages
            {
                Id = Guid.NewGuid(),
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"),
                Pages = Core.Enum.Pages.User,
                Status = true
            });

            return returnList;
        }
    }
}
